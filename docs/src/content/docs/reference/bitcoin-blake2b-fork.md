---
title: The Bitcoin BLAKE2b fork (BTCB2)
description: What the BLAKE2b fork of August 2026 is, why it does not change Distributed Cryptography, and what to know if you hold coins on both chains.
---

In August 2026 a minority of the Bitcoin network split off and started a separate chain, known as **Bitcoin
BLAKE2b** (also BTCB2 or BIP-110). This page explains what happened and what it means for your wallet.

**In short:** Distributed Cryptography is a wallet for **Bitcoin**, the chain that most miners kept running. Your wallet
keeps working as before. BTCB2 is a different coin, and the wallet does not support it.

## What happened

| | |
|---|---|
| Last common block | 961,631 (8 August 2026) |
| Split | Block 961,632, the start of the mandatory signaling period of BIP-110. Only about 2.5 % of the miners signaled, so Bitcoin went on without it |
| New mining algorithm | **BLAKE2b** instead of SHA-256d, from block 961,640 (30 August 2026). Sia ASICs can mine it |
| Software | Bitcoin Knots (29.4.1.knots20260508) |
| Rules | The "reduced data" rules of [BIP-110](https://bips.dev/110/), for one year (until September 2027) |
| Block header | 164 bytes instead of 80 |
| Addresses and keys | **The same as Bitcoin.** A BTCB2 address looks exactly like a Bitcoin address |
| Replay protection | **Opt-in only**, with a new signature hash type (`SIGHASH_UNIFIED`). An ordinary signature is valid on both chains |
| Support | Bitcoin Knots and a few community projects; a few small exchanges list it |

## What it means for Distributed Cryptography

- **Your bitcoin is not affected.** The wallet talks to Bitcoin Electrum servers. They follow the SHA-256d chain and
  never accept a BLAKE2b block, so the wallet cannot mix the two chains up.
- **The wallet does not support BTCB2.** Its default servers are Bitcoin servers, and it does not sign with the new
  replay-protected signature type. See [Electrum servers for BTCB2](#electrum-servers-for-btcb2) below.
- **Coins you held before 8 August 2026 exist on both chains**, because addresses and keys did not change. Only
  those coins: whatever you received after the split exists only on the chain it was sent on.
- **Replay.** A transaction the wallet signs uses ordinary signatures. If it spends coins from before the split,
  anybody can copy it to the other chain, where it moves the same coins to the same destination. If you only use
  Bitcoin, this costs you nothing on Bitcoin. At most you lose your BTCB2 copy of those coins. If you want to
  claim your BTCB2 coins, do it with BTCB2's own software, which supports the replay protection.

## Electrum servers for BTCB2

Stock Fulcrum and Electrs only understand Bitcoin's 80-byte block headers. There are **unofficial forks** that
read the 164-byte BLAKE2b headers and index the BTCB2 chain, for example
[privkeyio/Fulcrum](https://github.com/privkeyio/Fulcrum) (also packaged for StartOS). Their authors say they are
not audited and not reviewed by the original Fulcrum project.

The wallet asks its Electrum server only for the history of its addresses, transactions, fee estimates and the
broadcast of a payment. It never reads block headers. A wallet pointed at such a server
([Choose your servers](/wallet/servers/)) would therefore probably see and spend its BTCB2 coins. **We have not
tested it**, and the replay warning above applies to every payment made this way.

## Multisignature groups and BIP-110

BTCB2 enforces the BIP-110 rules, and one of them limits the size of a Taproot script tree. That limits the
multisignature groups that would work on that chain. See
[BIP-110 and multisignature groups](/reference/bip110-multisignature/).

## Sources

- [BIP 110: Reduced Data Temporary Softfork](https://bips.dev/110/)
- [BIP110 code review for humans (Bitcoin Knots)](https://bitcoinknots.org/learn/bip110-code-walkthrough.html)
- [bip110.org](https://www.bip110.org/)
- [The BIP-110 proposal (bitcoin/bips pull request 2017)](https://github.com/bitcoin/bips/pull/2017)
- [Bitcoin BLAKE2b: The Fork That Fired the Miners (Bitcoin Takeover)](https://bitcoin-takeover.com/bitcoin-blake2b-the-fork-that-fired-the-miners/)
- [Bitcoin BLAKE2b fork chain to begin testing on September 1 (KuCoin)](https://www.kucoin.com/news/flash/bitcoin-blake2b-fork-chain-to-begin-test-on-september-1)
- [Bitcoin BLAKE2b fork faces Sept. 1 launch test (crypto.news)](https://crypto.news/bitcoin-blake2b-fork-faces-sept-1-launch-test/)
- [Bitcoin Knots is trying to fork Bitcoin again (CryptoSlate)](https://cryptoslate.com/bitcoin-knots-is-trying-to-fork-bitcoin-again-after-its-last-chain-died-in-two-blocks/)
- [privkeyio/Fulcrum: unofficial Fulcrum fork with BLAKE2b proof-of-work support](https://github.com/privkeyio/Fulcrum)
- [privkeyio/fulcrum-startos: the same fork packaged for StartOS](https://github.com/privkeyio/fulcrum-startos)
- [awesome-bitcoin-blake: nodes, wallets, servers and explorers for the BLAKE2b chain](https://github.com/bitcoin-blake/awesome-bitcoin-blake)
- [Navigating the BIP-110 activation (Start9)](https://start9.com/bip110/)

The details of the fork come from these articles (September 2026); a few of them disagree on the exact dates.
