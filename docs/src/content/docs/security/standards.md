---
title: Bitcoin standards
description: The Bitcoin standards (BIPs) the wallet supports.
---

The wallet follows the published Bitcoin standards, so your coins are never tied to our software.

## Wallets and keys

| Standard | What it is |
|---|---|
| [BIP 32](https://github.com/bitcoin/bips/blob/master/bip-0032.mediawiki) | Hierarchical deterministic wallets |
| [BIP 39](https://github.com/bitcoin/bips/blob/master/bip-0039.mediawiki) | The list of words (mnemonic) that creates the keys |
| [BIP 86](https://github.com/bitcoin/bips/blob/master/bip-0086.mediawiki) | Taproot single-key addresses. **Our default.** |
| [BIP 84](https://github.com/bitcoin/bips/blob/master/bip-0084.mediawiki) | Native SegWit (P2WPKH) accounts |
| [BIP 49](https://github.com/bitcoin/bips/blob/master/bip-0049.mediawiki) | SegWit nested in P2SH accounts |
| [BIP 44](https://github.com/bitcoin/bips/blob/master/bip-0044.mediawiki) | The legacy format of deterministic wallets |
| Wallet Import Format (WIF) | Single legacy keys |
| Brain wallets | Supported, not recommended |

## Transactions and addresses

| Standard | What it is |
|---|---|
| [BIP 141](https://github.com/bitcoin/bips/blob/master/bip-0141.mediawiki), [BIP 143](https://github.com/bitcoin/bips/blob/master/bip-0143.mediawiki), [BIP 144](https://github.com/bitcoin/bips/blob/master/bip-0144.mediawiki) | Segregated Witness |
| [BIP 173](https://github.com/bitcoin/bips/blob/master/bip-0173.mediawiki) | Bech32 SegWit addresses, with error detection |
| [BIP 341](https://github.com/bitcoin/bips/blob/master/bip-0341.mediawiki), [BIP 342](https://github.com/bitcoin/bips/blob/master/bip-0342.mediawiki) | Taproot and the validation of Taproot scripts |

## Multisignature

- **K-of-N multisignature on Taproot**, with several spending paths (address and script) organised in Huffman
  TapTrees. This is what makes [Delegated Multisignature](/groups/delegated-multisignature/) cheaper.
- **Classic SegWit multisignature** (P2SH-P2WSH) with
  [BIP 48](https://github.com/bitcoin/bips/blob/master/bip-0048.mediawiki) keys and
  [PSBT (BIP 174)](https://github.com/bitcoin/bips/blob/master/bip-0174.mediawiki), in
  [Legacy Multisignature](/groups/legacy-multisignature/) (next release).

## Network

The wallet reads balances and sends payments with the
[Electrum protocol](https://electrumx-spesmilo.readthedocs.io/en/latest/protocol.html).

It works on **MainNet** (real bitcoin) and on the test networks.
