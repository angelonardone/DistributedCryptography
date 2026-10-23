---
title: BIP-110 and multisignature groups
description: Why the BIP-110 rules of the BLAKE2b fork would limit the size of Taproot multisignature groups, which groups are affected, and what we are considering.
---

[BIP-110](https://bips.dev/110/) is a set of temporary "reduced data" rules. Bitcoin did not adopt them, but the
[Bitcoin BLAKE2b fork (BTCB2)](/reference/bitcoin-blake2b-fork/) enforces them until September 2027. One of these
rules limits how large a **Taproot multisignature group** can be. This page explains that limit.

**On Bitcoin nothing changes:** every group size described in this documentation works.

## The rule

A Taproot address can hold a tree of scripts. To spend with one of them, the transaction includes a *control
block*: the path from that script to the root of the tree, 32 bytes for every level, plus 33 bytes. BIP-110 makes
control blocks larger than **257 bytes** invalid. That allows **7 levels**, so a tree of at most **128 scripts**.

## Why it matters for our groups

[Multi Signature](/groups/multi-signature/) and [Delegated Multisignature](/groups/delegated-multisignature/)
put **one script for every combination of K signers** in the tree. Today a group can have up to 2000 combinations,
an 11-level tree. Under BIP-110 only groups with **128 combinations or fewer** could pay with their scripts.

| Group | Combinations | Under BIP-110 |
|---|---|---|
| 2 of 3, 3 of 5, 5 of 7 | 3, 10, 21 | Works |
| 3 of 10 | 120 | Works |
| 2 of 16 | 120 | Works |
| 4 of 9, 5 of 9 | 126 | Works |
| 2 of 17 | 136 | Does not work |
| 3 of 11 | 165 | Does not work |
| 4 of 10 | 210 | Does not work |
| 5 of 10 | 252 | Does not work |

In Multi Signature, N counts everybody, the creator included. In Delegated Multisignature, N counts the members
without the owner.

What it means in practice:

- **Multi Signature** pays only with its scripts. A group above 128 combinations could not spend coins it received
  on BTCB2 after the split.
- **Delegated Multisignature**: the owner still pays alone, without scripts. The members' K-of-N payments would have
  the same limit.
- **[Legacy Multisignature](/groups/legacy-multisignature/)** (addresses starting with `3`) does not use Taproot
  trees and is not affected.
- **Coins received before the split** (8 August 2026) are exempt from all the BIP-110 rules.
- None of our scripts uses the other things BIP-110 forbids (`OP_IF` in Taproot scripts, outputs longer than 34
  bytes, the Taproot annex).

## What we are considering

The wallet does not support BTCB2 today. **We are considering a version of the multisignature groups that is
compatible with BIP-110**, for example:

- keeping the group at 128 combinations or fewer when compatibility is needed;
- for larger groups, a single script with every key of the group (K of N in one script). It makes payments larger,
  but it fits the rule.

If this matters to you, tell us on [GitHub](https://github.com/angelonardone/DistributedCryptography).

## Sources

- [BIP 110: Reduced Data Temporary Softfork](https://bips.dev/110/) (rule 5: control blocks larger than 257 bytes)
- [bip110.org](https://www.bip110.org/)
- [BIP110 code review for humans (Bitcoin Knots)](https://bitcoinknots.org/learn/bip110-code-walkthrough.html)
- [The BIP-110 proposal and its discussion (bitcoin/bips pull request 2017)](https://github.com/bitcoin/bips/pull/2017)
- [BIP341: Taproot](https://github.com/bitcoin/bips/blob/master/bip-0341.mediawiki) (the control block)
