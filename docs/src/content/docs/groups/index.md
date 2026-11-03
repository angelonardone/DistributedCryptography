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
| [Delegated Multi Signature Wallet](/groups/delegated-multisignature/) | A bitcoin wallet you can always spend from alone, and that K of the N people you choose can also spend from, built on Taproot |
| [Advanced Delegated Multi Signature Wallet](/groups/advanced-delegated-multisignature/) (in 0.925: *Delegated Multi Signature 2*) | The same, but the K members sign online at the same time (MuSig2): every payment carries one single signature |
| [Multi Signature Wallet](/groups/multi-signature/) | A Taproot bitcoin wallet where everybody, the creator included, is an equal signer and K of the N must sign each payment |
| [Advanced Multi Signature Wallet](/groups/advanced-multi-signature/) (in 0.925: *Multi Signature 2*) | The same, but the K signers sign online at the same time (MuSig2): every payment carries one single signature |
| [Legacy Multi Signature Wallet](/groups/legacy-multisignature/) | A classic multisignature wallet where K of N equal members must approve each payment, compatible with other software |
| [Shared passwords](/groups/shared-passwords/) (in the app: *Encrypted Passwords*) | A password vault where you decide which member sees which password |
| [Group Chat](/groups/group-chat/) | A chat of several people, everyone with everyone, encrypted and signed |

## How every group works

All the types follow the same four steps.

1. **Create.** Open **Online → Smart Groups**, create a group, choose its type and give it a name.
2. **Invite.** Add members from your [contacts](/online/contacts/) and click **Send Invitation**. For the types
   that vote, you also set how many votes (shares) each member has and the **minimum** needed.
3. **Accept.** Each member opens *Smart Groups* in their own wallet and clicks **Accept Invitation**, or
   **Decline Invitation**. Until a member accepts, the group does not open in their wallet. When a member
   declines, the owner gets a notification and sees that member as *(declined)*.
4. **Activate.** When everybody has accepted, the owner activates the group. This is the moment the secret is
   split, or the shared wallet becomes ready. Password groups and Legacy Multi Signature Wallet groups need no
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

## What the members see of each other

The name you give a contact is **yours alone**. It never travels in a group: not in the invitations, not in
the group messages, and not in the copy of the group stored on our servers. In a group, every wallet shows
each member as:

- **me**, for yourself;
- **your own name** for that person, when the member is one of your contacts;
- otherwise only the **beginning of the user name**, followed by `...`.

So a member who is not your contact learns nothing about who that person is, and nobody sees the names you
chose, not even in a group you created.

## Choosing a multisignature wallet

Two questions decide it: **can the owner pay alone?** and **can the signers be online at the same time?**

| | The signers sign one after the other, whenever each one can | The signers sign online at the same time (MuSig2) |
|---|---|---|
| **The owner can always pay alone** | [Delegated Multi Signature Wallet](/groups/delegated-multisignature/) | [Advanced Delegated Multi Signature Wallet](/groups/advanced-delegated-multisignature/) |
| **Nobody pays alone** (the creator is one more signer) | [Multi Signature Wallet](/groups/multi-signature/), or the [Legacy Multi Signature Wallet](/groups/legacy-multisignature/) | [Advanced Multi Signature Wallet](/groups/advanced-multi-signature/) |

- The **Advanced** wallets are the cheapest and the most private: a payment shows one key and one signature,
  whatever the number of signers. The price is that the K signers must be online during the same few minutes,
  each from their own wallet, wherever they are.
- The **Legacy Multi Signature Wallet** is the classic kind that other wallets and tools understand. It is not
  Taproot, it costs more in fees and it is limited to 16 members.

All of them can show their **descriptors**, so another wallet can watch the group without our software. The guide
[Bitcoin multisignature: a practical guide](/guides/multisig/) compares the costs with real numbers.
