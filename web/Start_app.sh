#!/bin/bash

# Start the executable in the background
./bin/GxWebStartup &

# Wait for 3 seconds
sleep 3

# Open the URL in the default web browser
xdg-open http://localhost:5000/wallet.wallets.aspx 2>/dev/null || open http://localhost:5000/wallet.wallets.aspx
