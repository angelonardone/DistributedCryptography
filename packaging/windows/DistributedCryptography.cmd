@echo off
rem Starts the wallet and opens it in the browser. Used by the shortcuts that the installer creates.
rem The wallet runs in this window: close the window to stop it.

title Distributed Cryptography - close this window to stop the wallet
cd /d "%~dp0"

rem This computer only: the wallet is not offered to the rest of the network
set ASPNETCORE_URLS=http://localhost:5000
set APP_URL=http://localhost:5000/wallet.wallets.aspx

rem Already running (the shortcut was used twice): only show the page
curl -s -o nul -f %APP_URL% 2>nul
if not errorlevel 1 goto open

echo Starting Distributed Cryptography...
start "" /b "bin\GxWebStartup.exe"

rem Wait until the wallet answers (up to 60 seconds)
set /a tries=0
:wait
timeout /t 1 /nobreak >nul
curl -s -o nul -f %APP_URL% 2>nul
if not errorlevel 1 goto open
set /a tries+=1
if %tries% lss 60 goto wait

:open
start "" %APP_URL%
echo.
echo Distributed Cryptography is running at %APP_URL%
echo Close this window to stop it.
