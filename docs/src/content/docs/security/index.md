---
title: How your data is protected
description: The algorithms that protect your wallet, your files and your messages - Argon2id, AES-256-GCM, BIP 39 and public-key encryption.
---

We use well-known, widely trusted algorithms, and the security of the Bitcoin network itself. Nothing here is
home-made cryptography that you have to take on faith: the code is
[public](https://github.com/angelonardone/DistributedCryptography).

## Your password

Your password is hashed with **Argon2id**, with a 128-bit salt, 6 degrees of parallelism, 10 iterations and
**640 MiB of memory**.

As a reference,
[OWASP](https://github.com/OWASP/CheatSheetSeries/blob/master/cheatsheets/Password_Storage_Cheat_Sheet.md)
recommends a minimum of 19 MiB of memory, 2 iterations and 1 degree of parallelism. The much higher cost makes
guessing passwords slow and expensive for an attacker. It is also why opening a wallet takes a few seconds.

A strong algorithm does not rescue a weak password. Choose a long one.

## Your master key

- The master key of your wallet is created from your words with the **BIP 39** standard. Your password takes
  part in it as the BIP 39 passphrase, so the words alone are not enough to rebuild the wallet.
- On disk, the master key is encrypted with **AES-256-GCM**. The 256-bit key comes from the Argon2id hash of
  your password. A 128-bit authentication tag guarantees that the encrypted key has not been changed.
- The master key is decrypted only for the moment it is needed (opening the wallet, signing a payment) and is
  then discarded. Every payment asks for your password again.

## Separate keys for separate jobs

Your wallet derives a different key for each purpose: your bitcoin, logging in, chat, groups, your local files,
the HSM. They all come from the same master key, so one backup covers them all, but a key used in one place
tells nothing about the others.

## Your data on disk

Each piece of data (notes, passwords, contacts, groups, chat history) is encrypted with its **own random
256-bit key**, using **AES-GCM** with a 128-bit authentication tag. That key is in turn locked with a key of
your wallet. Each item is protected and tamper-evident on its own.

## Files

A **new random 256-bit key** is created for every file and locked with the public key of the person who may
open it (you, for your own files). The content is encrypted in pieces of 64 KiB with **AES-256-GCM**. The
original file name is encrypted with the content.

When a file is opened, everything is checked. A file is rejected, and nothing is written, if:

- any byte of it was changed,
- pieces were reordered, repeated or removed,
- it was cut short or extended,
- it was encrypted for another wallet.

Memory use does not depend on the size of the file, so there is no size limit in the format.

:::note
This is the file format (DCF2) of the next release. Earlier versions used AES in CBC mode with an HMAC for each
block.
:::

## Messages between users

Chat messages, invitations and everything the members of a group send each other are **encrypted on the
sender's computer with the public key of the person who must read them**. The servers and relays in between
carry data they cannot read. See [What our servers can see](/security/privacy/).

## Sharing a secret

[Consensus Backup](/groups/consensus-backup/), the [Time-Encrypted Vault](/groups/time-encrypted-vault/) and
[Split a secret](/offline/split-a-secret/) use **Shamir's secret sharing**: a secret is divided into shares, a
minimum number of shares rebuilds it, and any smaller number reveals nothing at all.

## What you have to do

The algorithms do their part. These are yours:

- **Keep your words and your password, in different places.** Without both, nobody can restore the wallet from
  the words: not you, and not us.
- **Back up your wallet folder.** It holds your encrypted notes, files and passwords.
- **Protect the computer.** Software running on your computer with your permissions can see what you see. Keep
  the system up to date and do not install the wallet on a machine you do not trust.
- **Do not expose the application to the internet** unless you know how to protect it. See
  [IIS (web app)](/install/iis/) and [HSM](/offline/hsm/).
