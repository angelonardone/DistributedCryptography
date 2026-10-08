---
title: What is Distributed Cryptography?
description: A digital vault and Bitcoin wallet that runs on your own PC or server.
---

Distributed Cryptography is a **digital vault and Bitcoin wallet** that runs on your own computer or server. It
works on Windows, Mac and Linux, and it also runs in Docker.

You use it from your browser, but the application is on your machine: it is a small local web server that you
open at `http://localhost:5000`. Your keys are created there and stay there, encrypted with your password.

## What makes it different

- **Backups that need agreement.** Besides the usual list of words, you can split your wallet secret among people
  you trust. Nobody can restore it alone. See [Consensus Backup](/groups/consensus-backup/).
- **A plan for when you cannot log in.** A [Time-Encrypted Vault](/groups/time-encrypted-vault/) opens for a group
  you chose only after a date that you keep moving forward.
- **Cheaper, more private multisignature.** [Delegated Multisignature](/groups/delegated-multisignature/) uses
  Taproot, so a K-of-N payment costs less in fees and shows only the people who signed.
- **A vault for everything else.** Notes, files, passwords and authenticator backups are encrypted with keys that
  come from the same wallet. See [Offline tools](/offline/).
- **Anonymous online services.** Chat, contacts and groups work through our servers, but you log in with a key of
  your wallet, not with your name or e-mail, and what you store there is encrypted on your computer first. See
  [What our servers can see](/security/privacy/).

## Two kinds of features

| | Needs internet | Examples |
|---|---|---|
| **Offline tools** | No | Encrypted notes, files and passwords, authenticator backups, QR codes, splitting a secret |
| **Online features** | Yes, after an [anonymous login](/online/) | Contacts, chat, sending encrypted files, Smart Groups |

The Bitcoin wallet itself needs a connection to read balances and to send payments.

## Where to start

1. [Install the app](/install/).
2. [Create a wallet](/wallet/create/) and write down your words **and** your password.
3. Try the [offline tools](/offline/).
4. [Log in anonymously](/online/), add a [contact](/online/contacts/) and create your first
   [Smart Group](/groups/).

## The source code is public

The code is on [GitHub](https://github.com/angelonardone/DistributedCryptography). You can read it, audit it and
[build it yourself](/install/build-from-source/). The terms are in the
[license](https://github.com/angelonardone/DistributedCryptography/blob/main/License.txt).

We rely on these projects, with thanks:

- [NBitcoin](https://github.com/MetacoSA/NBitcoin)
- [SecretSharingDotNet](https://github.com/shinji-san/SecretSharingDotNet)
- [GoogleAuthenticator](https://github.com/BrandonPotter/GoogleAuthenticator)
- [QRCoder](https://github.com/codebude/QRCoder)
