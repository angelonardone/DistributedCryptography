---
title: Bitcoin standards
description: The Bitcoin standards (BIPs) and the other published standards the wallet uses, and where it uses them.
---

The wallet follows the published Bitcoin standards, so your coins are never tied to our software: your words,
your backups and your group wallets can be opened by other wallets that follow the same standards.

## Wallets and keys

| Standard | What it is | Where the wallet uses it |
|---|---|---|
| [BIP 32](https://github.com/bitcoin/bips/blob/master/bip-0032.mediawiki) | Hierarchical deterministic wallets | Every key of a wallet comes from one seed. The wallet file keeps only that seed, encrypted |
| [BIP 39](https://github.com/bitcoin/bips/blob/master/bip-0039.mediawiki) | The list of words (mnemonic) that creates the keys | Create and restore a wallet, in English or Spanish; the optional password is the BIP39 passphrase |
| [BIP 86](https://github.com/bitcoin/bips/blob/master/bip-0086.mediawiki) | Taproot single-key accounts | **Our default** wallet type |
| [BIP 84](https://github.com/bitcoin/bips/blob/master/bip-0084.mediawiki) | Native SegWit (P2WPKH) accounts | Wallet type |
| [BIP 49](https://github.com/bitcoin/bips/blob/master/bip-0049.mediawiki) | SegWit nested in P2SH accounts | Wallet type |
| [BIP 44](https://github.com/bitcoin/bips/blob/master/bip-0044.mediawiki) | The legacy format of deterministic wallets | Wallet type |
| [BIP 48](https://github.com/bitcoin/bips/blob/master/bip-0048.mediawiki) | Keys for multisignature wallets | The keys of each member of a [Legacy Multi Signature Wallet](/groups/legacy-multisignature/) |
| [BIP 85](https://github.com/bitcoin/bips/blob/master/bip-0085.mediawiki) | Child seeds made from a master seed | [Hot wallets](/wallet/ruby-hot-wallets/) made by Ruby from its words |
| [SLIP-132](https://github.com/satoshilabs/slips/blob/master/slip-0132.md) | `ypub` / `zpub` key formats | The extended keys of BIP49 and BIP84 wallets |
| Wallet Import Format (WIF) | Single legacy keys | Import a single key |
| Brain wallets | A key made from a phrase | Classic brain wallets can be imported; not recommended. The [Advance Brain Wallet](/wallet/advance-brain-wallet/) makes its key with Argon2id |

## Backups

| Standard | What it is | Where the wallet uses it |
|---|---|---|
| [BIP 93 (codex32)](https://github.com/bitcoin/bips/blob/master/bip-0093.mediawiki) | Splitting a seed into shares, each with a strong checksum | [Consensus Backup](/groups/consensus-backup/) and [Time-Encrypted Vault](/groups/time-encrypted-vault/) split the wallet seed; a [Ruby](/wallet/ruby/) splits its own seed |
| COLDCARD backup files | Encrypted backups of a COLDCARD | Restore a wallet from a COLDCARD backup |

## Transactions and addresses

| Standard | What it is |
|---|---|
| [BIP 141](https://github.com/bitcoin/bips/blob/master/bip-0141.mediawiki), [BIP 143](https://github.com/bitcoin/bips/blob/master/bip-0143.mediawiki), [BIP 144](https://github.com/bitcoin/bips/blob/master/bip-0144.mediawiki) | Segregated Witness |
| [BIP 173](https://github.com/bitcoin/bips/blob/master/bip-0173.mediawiki), [BIP 350](https://github.com/bitcoin/bips/blob/master/bip-0350.mediawiki) | Bech32 and bech32m addresses, with error detection |
| [BIP 340](https://github.com/bitcoin/bips/blob/master/bip-0340.mediawiki) | Schnorr signatures |
| [BIP 341](https://github.com/bitcoin/bips/blob/master/bip-0341.mediawiki), [BIP 342](https://github.com/bitcoin/bips/blob/master/bip-0342.mediawiki) | Taproot: spends by the key or by one script of a tree, and the validation of Taproot scripts |
| [BIP 65](https://github.com/bitcoin/bips/blob/master/bip-0065.mediawiki) | Time locks (`OP_CHECKLOCKTIMEVERIFY`): the bounty of a [Time-Encrypted Vault](/groups/time-encrypted-vault/) cannot be collected before its date |
| [BIP 125](https://github.com/bitcoin/bips/blob/master/bip-0125.mediawiki) | Replace-by-fee: in group payments a higher fee level, signed in advance, can replace a lower one |
| [BIP 21](https://github.com/bitcoin/bips/blob/master/bip-0021.mediawiki) | `bitcoin:` payment links and QR codes, with amount and label |

## Multisignature

| Standard | What it is |
|---|---|
| [BIP 327 (MuSig2)](https://github.com/bitcoin/bips/blob/master/bip-0327.mediawiki) | Several keys sign together and look like one key and one signature on the blockchain |
| [BIP 67](https://github.com/bitcoin/bips/blob/master/bip-0067.mediawiki) | Keys sorted in a multisignature script, so every member builds the same address |
| [BIP 174 (PSBT)](https://github.com/bitcoin/bips/blob/master/bip-0174.mediawiki) | Partially signed transactions, passed from one signer to the next |
| [BIP 371](https://github.com/bitcoin/bips/blob/master/bip-0371.mediawiki) | The Taproot fields of a PSBT, used to sign on Ruby |
| [BIP 373](https://github.com/bitcoin/bips/blob/master/bip-0373.mediawiki) | The MuSig2 fields of a PSBT, used by Ruby in the Advanced groups |

How each group wallet uses them:

- **[Delegated Multi Signature Wallet](/groups/delegated-multisignature/)**: the owner's key is the Taproot key
  path, so the owner pays alone; each combination of K members is one script of the tree, a `sortedmulti_a`.
- **[Multi Signature Wallet](/groups/multi-signature/)**: nobody pays alone. The key path is the MuSig2
  aggregate of all the signers, and each combination of K signers is one `sortedmulti_a` script.
- **[Advanced Delegated](/groups/advanced-delegated-multisignature/)** and
  **[Advanced Multi Signature](/groups/advanced-multi-signature/) Wallets**: like the two above, but each
  combination of K signers is a single MuSig2 key, so a payment carries one signature, as cheap and private as a
  single-key payment.
- **[Legacy Multi Signature Wallet](/groups/legacy-multisignature/)**: classic SegWit multisignature (P2SH-P2WSH)
  with BIP 48 keys sorted as in BIP 67, signed with PSBT.

## Descriptors

Every group wallet is described by a standard **output descriptor**, so other wallets that read descriptors can
import it and see the same addresses.

| Standard | What it is | Used for |
|---|---|---|
| [BIP 380](https://github.com/bitcoin/bips/blob/master/bip-0380.mediawiki) | Descriptors: key expressions and checksum | Every descriptor the wallet exports |
| [BIP 381](https://github.com/bitcoin/bips/blob/master/bip-0381.mediawiki), [BIP 382](https://github.com/bitcoin/bips/blob/master/bip-0382.mediawiki), [BIP 383](https://github.com/bitcoin/bips/blob/master/bip-0383.mediawiki) | `sh()`, `wsh()`, `sortedmulti()` | Legacy Multi Signature: `sh(wsh(sortedmulti(...)))` |
| [BIP 386](https://github.com/bitcoin/bips/blob/master/bip-0386.mediawiki) | `tr()`: Taproot with a script tree | Delegated, Multi Signature and the two Advanced wallets |
| [BIP 387](https://github.com/bitcoin/bips/blob/master/bip-0387.mediawiki) | `multi_a()` / `sortedmulti_a()`: multisignature scripts for Taproot | The scripts of the Delegated and Multi Signature wallets |
| [BIP 390](https://github.com/bitcoin/bips/blob/master/bip-0390.mediawiki) | `musig()`: a MuSig2 key inside a descriptor | The key path of the Multi Signature wallets, and the `pk(musig(...))` scripts of the Advanced wallets |

## Messages, login and network

- **Signed messages:** every message between wallets, and the [anonymous login](/online/), are signed with
  [BIP 322](https://github.com/bitcoin/bips/blob/master/bip-0322.mediawiki), the standard for messages signed by a
  bitcoin address.
- **Messages travel over Nostr** ([NIP-01](https://github.com/nostr-protocol/nips/blob/master/01.md)): signed
  events with BIP 340 Schnorr signatures, encrypted end to end by the wallet before they are sent.
- **Balances and payments** use the
  [Electrum protocol](https://electrumx-spesmilo.readthedocs.io/en/latest/protocol.html) (plain or over TLS), with
  the server [you choose](/wallet/servers/).
- **Second factor:** the authenticator code of a wallet is a standard TOTP code
  ([RFC 6238](https://www.rfc-editor.org/rfc/rfc6238)), so any authenticator app works.
- **Ruby** talks to the application with CBOR ([RFC 8949](https://www.rfc-editor.org/rfc/rfc8949)), in the
  message format of Blockstream's Jade, and keeps the blind PIN oracle of Jade.

It works on **MainNet** (real bitcoin) and on the test networks.

## Encryption

| Standard | Where the wallet uses it |
|---|---|
| Argon2id ([RFC 9106](https://www.rfc-editor.org/rfc/rfc9106)) | The key of the wallet file, from its password, and the key of the Advance Brain Wallet: 640 MiB of memory, 10 iterations, 8 lanes |
| AES-256-GCM | The wallet file, every piece of data, and files (in pieces of 64 KiB) |
| ECIES (secp256k1, the "BIE1" format of Electrum) | Locks each random AES key with the public key of the person who may open it: messages, chat, backup shares, files |
| HKDF ([RFC 5869](https://www.rfc-editor.org/rfc/rfc5869)) | The keys of encrypted files |
| HMAC-SHA256 ([RFC 2104](https://www.rfc-editor.org/rfc/rfc2104)) | Shared records of a group (the server sees only opaque tags) and the API key of the HSM |

See [Security](/security/) for how these pieces protect your wallet.
