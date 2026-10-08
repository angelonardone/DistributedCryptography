---
title: Run with Docker
description: Start the wallet with one command using Docker. No .NET SDK needed.
---

This is the quickest way to try the wallet. You only need [Docker](https://docs.docker.com/get-docker/).

```bash
git clone https://github.com/angelonardone/DistributedCryptography
cd DistributedCryptography
docker compose up -d
```

Then open `http://localhost:5000` in your browser. It takes you to the wallets page.

The first run builds the application from the source code, so it takes a few minutes.

## Where your wallets are kept

Wallet data is stored in the Docker volume **`distcrypt-wallets`**. It survives rebuilding and updating the
container.

:::danger
`docker compose down -v` deletes the volume, and your wallets with it. Use `docker compose down` (without `-v`)
to stop the application.
:::

## Use another port

Create a file called `.env` next to `docker-compose.yml` with the port you want:

```ini
APP_PORT=8080
```

Inside the container the app always listens on port 5000.

## Update

```bash
git pull
docker compose up -d --build
```

## The HSM service

The [HSM](/offline/hsm/) REST service is published at `http://localhost:5000/HSM/rest/...`.

:::caution
The HSM service has **no authentication**. Do not open the port to networks you do not trust. To keep it on your
own machine only, change the port line in `docker-compose.yml` to `127.0.0.1:5000:5000`.
:::
