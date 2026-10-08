---
title: What our servers can see
description: What stays on your computer, what goes through the Distributed Cryptography servers, and what we can and cannot read.
---

The application runs on your computer. Our servers are only used by the [online features](/online/), and they
are designed to hold as little as possible.

## Local (offline)

These never leave your computer:

- Your words, your password and your master key.
- Your bitcoin keys.
- Your encrypted notes, files, passwords and authenticator backups.
- Your chat history.

They are stored in your wallet folder, encrypted. See [How your data is protected](/security/).

The bitcoin wallet connects to the Bitcoin network to read balances and send payments. The offline tools need
no connection at all.

## Server (online)

When you [log in anonymously](/online/), this is what exists on our side.

| What | Can we read it? |
|---|---|
| Your **user name** and the public key behind it | Yes. It is a key of your wallet made only for logging in. It is not your name or your e-mail, and it is not one of your bitcoin addresses. |
| Your **contacts** | No. The list is encrypted on your computer before it is sent, with a new key every time it changes. |
| Your **groups**: who is in them, their shares, their settings | No. Each group is encrypted with a key that only its members have. |
| **Messages** between users (invitations, group messages, signatures) | No. Each one is encrypted for the person who must read it. Messages are deleted once they have been delivered. |
| **Chat** | No. It travels over Nostr relays, encrypted for your contact. |
| **Files** you encrypt for other users | We never receive them. You send the encrypted file yourself, any way you like. |
| Your **bitcoin addresses** | Balances are read from an Electrum server. As with any light wallet, the server that answers sees which addresses are asked about. They are not part of your user account. |

### What the server does know

To be clear about the limits:

- It knows that a user name exists, and when it connects.
- It knows that encrypted blobs belong to a user name, and their size.
- To deliver a message it has to know which user name the message is for.
- Like any internet service, it sees the network address (IP) a connection comes from. Use a VPN or Tor if that
  matters to you.

### No account, no personal data

There is no registration form. We do not ask for a name, an e-mail or a phone number, so we have none to lose,
sell or hand over.

## If our servers disappeared

- Your **bitcoin** is not affected. Your wallet is standard (BIP 39 and BIP 86) and can be restored with your
  words and your password, in this application or in other software that supports those standards.
- Your **offline tools** keep working.
- The online features (contacts, chat, Smart Groups) need the servers.
