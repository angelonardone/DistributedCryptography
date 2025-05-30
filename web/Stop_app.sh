#!/bin/bash

# Function to kill process using a specific port
kill_port() {
    local PORT=$1
    local PID=$(lsof -t -i:$PORT)
    if [ -n "$PID" ]; then
        kill -9 $PID
        echo "Killed process $PID using port $PORT"
    else
        echo "No process found using port $PORT"
    fi
}

# Function to check if an application is running
is_app_running() {
    local APP_NAME=$1
    if pgrep -x "$APP_NAME" > /dev/null; then
        return 0
    else
        return 1
    fi
}

# Function to close the specific tab with localhost:5000 in Safari
close_safari_tab() {
    osascript <<EOF
tell application "Safari"
    set windowList to every window
    repeat with aWindow in windowList
        set tabList to every tab of aWindow
        repeat with aTab in tabList
            if (URL of aTab contains "localhost:5000") then
                close aTab
            end if
        end repeat
    end repeat
end tell
EOF
    echo "Closed localhost:5000 tab in Safari"
}

# Function to close the specific tab with localhost:5000 in Google Chrome
close_chrome_tab() {
    osascript <<EOF
tell application "Google Chrome"
    set windowList to every window
    repeat with aWindow in windowList
        set tabList to every tab of aWindow
        repeat with aTab in tabList
            if (URL of aTab contains "localhost:5000") then
                close aTab
            end if
        end repeat
    end repeat
end tell
EOF
    echo "Closed localhost:5000 tab in Google Chrome"
}

# Kill any process using port 5000
kill_port 5000

# Close the specific tab with localhost:5000 in Safari if it's running
if is_app_running "Safari"; then
    close_safari_tab
else
    echo "Safari is not running"
fi

# Close the specific tab with localhost:5000 in Google Chrome if it's running
if is_app_running "Google Chrome"; then
    close_chrome_tab
else
    echo "Google Chrome is not running"
fi

