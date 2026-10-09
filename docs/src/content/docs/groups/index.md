---
title: What is a Smart Group?
description: Smart Groups let you and people you trust back up a wallet, share control of bitcoin, plan an inheritance or share passwords.
---

A **Smart Group** is you (the owner) plus contacts you invite, working together on one job that no single person
can do alone. The jobs are:

| Type of group | What it is for |
|---|---|
| [Consensus Backup](/groups/consensus-backup/) (in the app: *Wallet Backup*) | A backup of your wallet that a minimum number of members must agree to restore |
| [Time-Encrypted Vault](/groups/time-encrypted-vault/) | A backup that can only be opened after a date you keep renewing: inheritance and business continuity |
| [Delegated Multisignature](/groups/delegated-multisignature/) | A bitcoin wallet you can always spend from alone, and that K of the N people you choose can also spend from, built on Taproot |
| [Legacy Multisignature](/groups/legacy-multisignature/) | A classic multisignature wallet where K of N equal members must approve each payment, compatible with other software |
| [Shared passwords](/groups/shared-passwords/) (in the app: *Encrypted Passwords*) | A password vault where you decide which member sees which password |

## How every group works

All the types follow the same four steps.

1. **Create.** Open **Online → Smart Groups**, create a group, choose its type and give it a name.
2. **Invite.** Add members from your [contacts](/online/contacts/) and click **Send Invitation**. For the types
   that vote, you also set how many votes (shares) each member has and the **minimum** needed.
3. **Accept.** Each member opens *Smart Groups* in their own wallet and clicks **Accept Invitation**.
4. **Activate.** When everybody has accepted, the owner activates the group. This is the moment the secret is
   split, or the shared wallet becomes ready. Password groups and Legacy Multisignature groups need no
   activation.

:::note[Give it a minute]
Invitations and answers are encrypted messages. A member receives them when their wallet is open and logged in,
and delivery can take about a minute.
:::

## Before you start

- You need to be [logged in](/online/).
- Everyone you want in the group must already be an **accepted** [contact](/online/contacts/).

## What protects a group

- Everything the members exchange is **encrypted for the member who must read it**, on the sender's computer.
- The group is also stored on our servers so that members can read it and you can recover it on another
  computer, but it is stored **encrypted with a key only the members have**. See
  [What our servers can see](/security/privacy/).
- **Recover groups from server**, in *Smart Groups*, brings your groups back after you restore your wallet.

## Choosing between the two multisignature types

The difference is **who can pay alone**:

- In [Delegated Multisignature](/groups/delegated-multisignature/) the **owner can always pay alone**, and K of the
  N members can pay without the owner. It costs less in fees and shows less on the blockchain.
- In [Legacy Multisignature](/groups/legacy-multisignature/) **nobody pays alone**: every member, the owner
  included, is an equal signer and K of the N must approve each payment. It is the classic kind that other
  wallets and tools understand, and it is limited to 16 members.

Both can show their **descriptors**, so another wallet can watch the group without our software. The guide
[Bitcoin multisignature: a practical guide](/guides/multisig/) compares the costs with real numbers.
