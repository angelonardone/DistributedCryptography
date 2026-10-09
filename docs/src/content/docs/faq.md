---
title: Questions and answers
description: The questions people ask first about Distributed Cryptography - custody, backups, passwords, privacy and cost.
---

## The basics

### What is Distributed Cryptography?

A digital vault and Bitcoin wallet that runs on your own computer or server. Besides holding bitcoin, it
encrypts your notes, files and passwords, and it lets people you trust help you back up, share or inherit what
you protect. See [What is Distributed Cryptography?](/introduction/)

### Is it a web site? I use it from my browser.

No. The application runs **on your machine** and your browser is only its screen. The address
`http://localhost:5000` means "this computer". Nothing you type there goes to a web site of ours.

### Who holds my keys?

You do, and only you. They are created on your computer and stored there, encrypted with your password. We never
receive them, so we cannot move your money, and we cannot give your keys to anybody.

### Which systems does it run on?

Windows, Mac and Linux, and anywhere Docker runs. See [Install](/install/).

### Can I see the source code?

Yes. The code is public on [GitHub](https://github.com/angelonardone/DistributedCryptography). The
[license](https://github.com/angelonardone/DistributedCryptography/blob/main/License.txt) lets you read it,
audit it, [build it yourself](/install/build-from-source/), use it and share it. It does not allow distributing
modified versions.

## Passwords and backups

### I forgot my password. Can you reset it?

No. Nobody can. Your password is not stored anywhere, and it is part of how your keys are made.

What you can do depends on what you prepared:

- If you have a [Consensus Backup](/groups/consensus-backup/), the members of the group can restore your wallet,
  and you choose a new password.
- If you have a [Time-Encrypted Vault](/groups/time-encrypted-vault/), its members can restore the wallet after
  the date.
- Otherwise the wallet cannot be opened. This is why we insist on setting up a backup while everything is fine.

### I have my words. Why can I not restore my wallet?

Because the wallet is made from **your words and your password together**. In the restore page, the field
*passphrase* must contain the password the wallet had when it was created. With another password you get a
valid, but different and empty, wallet. See [Restore a wallet](/wallet/restore/).

### What exactly do I need to keep safe?

1. **Your words.**
2. **Your password.** Keep it in a different place from the words.
3. **A copy of your wallet folder**, for your encrypted notes, files and passwords.

The first two bring back your bitcoin and your identity. The third brings back the things you stored.

### Can I restore my wallet in other software?

Yes. It is a standard wallet: BIP 39 words, with your password as the BIP 39 passphrase, and BIP 86 (Taproot)
addresses. Software that supports those standards can restore it. See [Bitcoin standards](/security/standards/).

### My computer broke. What did I lose?

Your bitcoin, your user name, your contacts and your groups come back when you restore the wallet and log in
(*Recover contacts from server*, *Recover groups from server*). Your encrypted notes, files, passwords and chat
history were only on that computer, so they come back only if you kept a copy of the wallet folder.

## Backups with other people

### Can the members of my Consensus Backup take my money?

Not one of them alone, and not fewer than the minimum you chose. But **enough members together can restore your
wallet**: that is what the backup is. Choose people you trust and a minimum that takes a real agreement. You are
warned when someone starts a restore and you can stop it. See
[Consensus Backup](/groups/consensus-backup/#what-you-are-trusting).

### What is the difference between Consensus Backup and a Time-Encrypted Vault?

In a Consensus Backup the members can restore **at any time**, as soon as enough of them agree. In a
Time-Encrypted Vault they can restore **only after a date**, which you keep pushing forward. The first is for
"I lost my wallet". The second is for "I am no longer here".

### What if I forget to renew my Time-Encrypted Vault?

Then the date arrives and the vault can be opened by the people you chose. That is its purpose. If it happens by
mistake, move your funds to a new wallet. Put a reminder in your calendar.

### What happens if a member of my group loses their wallet?

A member who restores their own wallet (from their words or from their own backup) gets their place in the group
back. If a member is gone for good, the group still works as long as the remaining members reach the minimum.
This is a good reason not to set the minimum equal to the number of members.

## Multisignature

### Which multisignature should I use, Delegated or Legacy?

It depends on who should be able to pay alone.

Use [Delegated Multisignature](/groups/delegated-multisignature/) when the wallet is **yours** and you want
people you trust to be able to spend from it without you: you can always pay alone, and K of the N members can
pay together. It costs less in fees and shows less on the blockchain.

Use [Legacy Multisignature](/groups/legacy-multisignature/) when **nobody** should pay alone, you included, or
when you need the classic format that other wallets and tools understand.

### Can I see my multisignature group in another wallet?

Yes. The **Descriptors** button of the group's *Wallet Balance* tab shows two standard texts that
describe all the addresses of the group. A wallet that understands descriptors, Bitcoin Core for example, shows
the same addresses and coins from them. They hold only public keys: they can watch, never spend. See
[Delegated Multisignature](/groups/delegated-multisignature/#8-see-the-group-in-another-wallet-descriptors) and
[Legacy Multisignature](/groups/legacy-multisignature/#7-see-the-group-in-another-wallet-descriptors).

### Do all the signers have to be connected at the same time?

No. Each one signs when they open their wallet. The payment is sent when the last needed signature arrives.

## Privacy

### Do I need an account?

No. There is no registration. For the online features you [log in anonymously](/online/) with a key of your
wallet.

### What can you see about me?

A user name that is a key, not a name. Your contacts, groups and messages are encrypted on your computer before
they reach us. The full list is in [What our servers can see](/security/privacy/).

### Why does my user name look like a bitcoin address?

Because it is made the same way, from a key of your wallet that is used only for logging in. It is not an
address for receiving bitcoin.

## Files

### How large can an encrypted file be?

Up to 1 GB through the browser. Any size with the
[exchange folder](/online/send-files/#large-files-no-upload), because the file does not go through the browser
at all.

### How do I send the encrypted file to the other person?

Any way you like: e-mail, a USB drive, a cloud folder. It can only be opened by the wallet it was encrypted for,
so the channel does not need to be safe.

## Problems

### On Linux the wallet shows an error where a QR code should be

QR codes need the library `libgdiplus` and, in version 0.923, one setting. See
[QR codes on Linux](/install/linux/#qr-codes).

### On Mac the app does not open

macOS blocks apps that are not signed with an Apple Developer ID. See [Install on Mac](/install/mac/) for the
command that allows it.

### I invited a contact and nothing happens

Messages are delivered when the other person has their wallet open and logged in, and it can take about a
minute. See [Contacts](/online/contacts/).

### Something else

Open an issue on [GitHub](https://github.com/angelonardone/DistributedCryptography/issues).
