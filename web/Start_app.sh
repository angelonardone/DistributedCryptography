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

# Ensure the executable has the correct permissions
chmod +x ./bin/GxWebStartup
sudo chown $(whoami) ./bin/GxWebStartup

# If on macOS, remove quarantine attribute recursively
if [[ "$OSTYPE" == "darwin"* ]]; then
    find ./bin -exec xattr -d com.apple.quarantine {} \;
fi

# Kill any process using port 5000
kill_port 5000

# Start the executable in the background
./bin/GxWebStartup &

# Wait for 3 seconds
sleep 3

# Open the URL in the default web browser
xdg-open http://localhost:5000/wallet.wallets.aspx 2>/dev/null || open http://localhost:5000/wallet.wallets.aspx

