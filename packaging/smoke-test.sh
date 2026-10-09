#!/bin/bash
# Checks that a built package really works: unpacks or installs it the way a user would, starts the wallet on
# a spare port with an empty data folder and asks for the wallets page.
#
#   packaging/smoke-test.sh <package>      package = .zip, -Setup.exe, .tar.gz or .dmg made by the release workflow
#
# Runs on Windows (Git Bash), Linux and macOS.

set -u

PACKAGE="${1:?usage: smoke-test.sh <package>}"
PORT="${SMOKE_PORT:-5055}"
URL="http://127.0.0.1:$PORT/wallet.wallets.aspx"

# The same AppId as packaging/windows/installer.iss
UNINSTALL_KEY='HKCU\Software\Microsoft\Windows\CurrentVersion\Uninstall\{96A71195-51CC-489C-AB3F-EEFC0D2C5636}_is1'

WORK="$(mktemp -d)"
LOG="$WORK/server.log"
SERVER_PID=""
MOUNT=""
UNINSTALLER=""

stop_server() {
    if [ -n "$SERVER_PID" ]; then
        # On Windows (Git Bash) stop the real process by its Windows id: only the one started here
        WINPID="$(cat "/proc/$SERVER_PID/winpid" 2> /dev/null)"
        kill "$SERVER_PID" > /dev/null 2>&1
        [ -n "$WINPID" ] && taskkill //F //PID "$WINPID" > /dev/null 2>&1
        SERVER_PID=""
        sleep 2
    fi
}

uninstall() {
    if [ -n "$UNINSTALLER" ] && [ -f "$UNINSTALLER" ]; then
        MSYS_NO_PATHCONV=1 MSYS2_ARG_CONV_EXCL='*' "$UNINSTALLER" /VERYSILENT /SUPPRESSMSGBOXES /NORESTART
        # The uninstaller finishes its work from a copy of itself: wait for it
        for _ in $(seq 1 30); do
            [ -f "$UNINSTALLER" ] || break
            sleep 1
        done
    fi
}

finish() {
    stop_server
    uninstall
    [ -n "$MOUNT" ] && hdiutil detach "$MOUNT" -quiet > /dev/null 2>&1
    rm -rf "$WORK" > /dev/null 2>&1
}
trap finish EXIT

fail() {
    echo "SMOKE TEST FAILED: $1"
    if [ -f "$LOG" ]; then echo "--- server output ---"; tail -n 60 "$LOG"; fi
    if [ -f "$WORK/install.log" ]; then echo "--- installer log ---"; tail -n 40 "$WORK/install.log"; fi
    exit 1
}

echo "Unpacking $PACKAGE"
case "$PACKAGE" in
    *.zip)    unzip -q "$PACKAGE" -d "$WORK/app" || fail "cannot unzip" ;;
    *.tar.gz) mkdir "$WORK/app" && tar -xzf "$PACKAGE" -C "$WORK/app" || fail "cannot untar" ;;
    *.dmg)    MOUNT="$WORK/mnt"; mkdir "$MOUNT" "$WORK/app"
              hdiutil attach "$PACKAGE" -nobrowse -readonly -mountpoint "$MOUNT" -quiet || fail "cannot open the dmg"
              [ -L "$MOUNT/Applications" ] || fail "the dmg has no link to Applications"
              cp -R "$MOUNT"/*.app "$WORK/app/" || fail "cannot copy the app out of the dmg" ;;
    *-Setup.exe)
              # A test installation would replace a real one: only where the application is not installed
              reg query "$UNINSTALL_KEY" > /dev/null 2>&1 && fail "the application is installed on this computer: the installer is not tested here"
              INSTALLED="$WORK/app/DistributedCryptography"
              MSYS_NO_PATHCONV=1 MSYS2_ARG_CONV_EXCL='*' "$PACKAGE" /VERYSILENT /SUPPRESSMSGBOXES /NORESTART /SP- \
                  "/DIR=$(cygpath -w "$INSTALLED")" "/LOG=$(cygpath -w "$WORK/install.log")" || fail "the installer failed"
              UNINSTALLER="$INSTALLED/unins000.exe"
              [ -f "$UNINSTALLER" ] || fail "the installer left no uninstaller"
              [ -f "$INSTALLED/DistributedCryptography.cmd" ] || fail "the launcher is not installed"
              reg query "$UNINSTALL_KEY" > /dev/null 2>&1 || fail "the installation is not registered in Windows"
              SHORTCUT="$APPDATA/Microsoft/Windows/Start Menu/Programs/Distributed Cryptography/Distributed Cryptography.lnk"
              [ -f "$SHORTCUT" ] || fail "no shortcut in the Start menu"
              ;;
    *)        fail "unknown package type" ;;
esac

# The folder that holds bin/GxWebStartup: the top folder of the archive, or kestrel_app inside the Mac app
SERVER="$(find "$WORK/app" \( -name GxWebStartup -o -name GxWebStartup.exe \) -type f | head -n 1)"
[ -n "$SERVER" ] || fail "GxWebStartup is not in the package"
APP_DIR="$(dirname "$(dirname "$SERVER")")"

case "$PACKAGE" in
    *.zip | *.exe) ;;  # Windows has no executable bit
    *)             [ -x "$SERVER" ] || fail "GxWebStartup is not executable in the package" ;;
esac
if [ -n "$MOUNT" ]; then
    LAUNCHER="$(find "$WORK/app" -path '*/Contents/MacOS/launch_app' | head -n 1)"
    [ -x "$LAUNCHER" ] || fail "launch_app is missing or not executable"
    bash -n "$LAUNCHER" || fail "launch_app has a syntax error"
    plutil -lint "$(dirname "$(dirname "$LAUNCHER")")/Info.plist" || fail "Info.plist is not valid"
fi

echo "Starting $SERVER"
cd "$APP_DIR" || fail "cannot enter $APP_DIR"
export DISTCRYPT_DATA_DIR="$WORK/data"
export ASPNETCORE_URLS="http://127.0.0.1:$PORT"
mkdir -p "$DISTCRYPT_DATA_DIR"
"$SERVER" > "$LOG" 2>&1 &
SERVER_PID=$!

CODE=""
for _ in $(seq 1 60); do
    kill -0 "$SERVER_PID" > /dev/null 2>&1 || fail "the server stopped by itself"
    CODE="$(curl -s -o "$WORK/page.html" -w '%{http_code}' "$URL" 2> /dev/null)"
    [ "$CODE" = "200" ] && break
    sleep 2
done
[ "$CODE" = "200" ] || fail "the wallets page answered '$CODE' instead of 200"
grep -q "<title>Wallets" "$WORK/page.html" || fail "the page is not the wallets page"

# The wallets page creates the data folder: proves the app can write where it keeps the wallets
[ -d "$DISTCRYPT_DATA_DIR/Wallets" ] || fail "the data folder was not created in $DISTCRYPT_DATA_DIR"

if [ -n "$UNINSTALLER" ]; then
    cd "$WORK" || exit 1
    stop_server
    uninstall
    [ -f "$SERVER" ] && fail "the uninstaller left the application behind"
    reg query "$UNINSTALL_KEY" > /dev/null 2>&1 && fail "the uninstaller left the registration in Windows"
    [ -f "$SHORTCUT" ] && fail "the uninstaller left the shortcut in the Start menu"
    UNINSTALLER=""
    echo "Installed, started and uninstalled cleanly"
fi

echo "SMOKE TEST OK: $(basename "$PACKAGE") serves the wallets page"
