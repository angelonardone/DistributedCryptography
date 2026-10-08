---
title: Choose how to install
description: The ways to install Distributed Cryptography - Docker, ready-made downloads for Windows, Mac and Linux, or building from source.
---

There are three ways to get the application. Choose the one that fits what you need.

| Way | Best for | You need |
|---|---|---|
| [Docker](/install/docker/) | Trying it quickly, or running it on a server | Docker |
| Ready-made download: [Windows](/install/windows/), [Mac](/install/mac/), [Linux](/install/linux/) | Everyday use on your own computer | Nothing else |
| [Build from source](/install/build-from-source/) | Auditing the code, or checking the build yourself | The .NET 8 SDK |

On a Windows server you can also publish the app with Internet Information Services: see
[IIS (web app)](/install/iis/).

## After installing

The application is a local web server. Once it is running, open this address in your browser:

```
http://localhost:5000/wallet.wallets.aspx
```

Then [create your first wallet](/wallet/create/).

:::caution[Back up your wallet folder]
The wallets page shows the folder where your wallets are stored. Everything in it is encrypted. Your bitcoin can
always be recovered from your words and your password, but what exists only in that folder, such as your
encrypted notes and files, cannot. Copy the folder to a safe place from time to time.
:::
