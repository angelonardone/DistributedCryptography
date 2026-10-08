---
title: "Bitcoin multisignature: a practical guide"
description: What multisignature is, and how SegWit multisig, MuSig1 and MuSig2 compare in cost, privacy and complexity, with measured transaction sizes.
---

This guide is for readers who are **new to Bitcoin** and need a clear, practical understanding of **multisignature (multisig)** wallets and how three common approaches compare in **cost (virtual size → fees)**, **privacy**, and **complexity**:

- **SegWit multisig (P2WSH m-of-n)**
- **MuSig1 (embedded in Taproot scripts)**
- **MuSig2 (embedded in Taproot scripts)**

At the end, you’ll find an **appendix table** with **measured virtual sizes (vbytes)** for many `k-of-n` policies gathered from real transactions.


## 1. What is multisignature (multisig)?

**Multisig** is a way to protect Bitcoin by requiring **multiple approvals** to spend coins. Instead of one person with one key, you can require **k signatures out of n keys** (a **k-of-n** policy).  
Examples:

- **2-of-3**: any 2 of 3 people (or devices/HSMs) must sign
- **3-of-5**: any 3 of 5 signers must sign
- **n-of-n**: everyone must sign

**Why it’s useful**

- **Security**: No single lost/compromised key can spend alone.
- **Shared control**: Good for teams, companies, DAOs, or estates.
- **Redundancy**: Keep keys in different places/devices.

## 2. Key terms (fast glossary)

- **SegWit (Segregated Witness)**: A Bitcoin upgrade that reduced signature data cost and enabled better scaling. “**P2WSH**” is the SegWit “Pay to Witness Script Hash” version used for script-based multisig.
- **Taproot**: A newer upgrade that improves privacy and efficiency. Lets you:
    - Spend via a **key path** (looks like a single key) or
    - Reveal only the **script branch** you use (hiding the rest).
- **Schnorr signatures**: The signature scheme used with Taproot (instead of ECDSA). Enables **key aggregation** protocols like **MuSig2**.
- **MuSig**: Family of protocols that let multiple parties produce **one aggregated public key** and **one or many individual or aggregated signatures**, making a multi-party spend look like a single-signer spend (for Musig2).
    - **MuSig1**: Early variant—more interactive, trickier nonce handling.
    - **MuSig2**: Newer, **two-round** protocol—simpler and safer in practice, more complex to implement.
- **Virtual size (vsize)**: The size (in **vbytes**) the network uses to calculate fees. **Your fee = vsize × feerate** (sat/vB). Lower vsize → lower fees.

## 3. The three options (high level)

1. SegWit multisig (P2WSH m-of-n)
    - **How it works**: The spending rule (e.g., “k-of-n”) is a **script** that’s revealed when you spend.
    - **Cost**: Witness size **grows with n**; typically **more expensive** than MuSig for the same policy.
    - **Privacy**: Reveals your policy (m, n, and keys) on-chain when spending.
    - **Limits**: Script/witness size constraints make very large `n` impractical; the table below marks **X** once `n > 16`.
    - **Good for**: Legacy compatibility, simple committees already supported by existing tools.
2. MuSig1 (embedded in Taproot scripts)
    - **How it works**: All combinations of signatures are created and embedded in the **tap tree**, only the final tree is approved with the **minimum** amount of participants.
    - **Cost**: Often **smaller** than P2WSH multisig; good savings for typical committees.
    - **Privacy**: only the signers are revealed when signed (better than P2WSH).
    - **Complexity**: **More complex** than P2WSH multisig, adds complexity to the implementation.
3. MuSig2 (embedded in Taproot scripts)
    - **How it works**: Use **Taproot + Schnorr** with **MuSig2** for a **two-round** signing flow. You can keep complex policies as Taproot scripts (hidden unless used) and spend through a **key path** when possible.
    - **Cost**: **Best fee performance** in our measurements—often **lowest vsize** across `k-of-n`.
    - **Privacy**: **Best**: key-path spends look like single-sig. Only reveal a script branch if necessary; unused branches stay hidden.
    - **Complexity**: **More complex** than MuSig1; requires Taproot-aware wallets/signers and 2 rounds of signatures.

## 4. Fees in practice (vsize → sats)

Your fee is **vsize × feerate**. Example at **10 sat/vB**:

- **2-of-3**: SegWit 212 vB → 2,120 sats; MuSig1 189 vB → 1,890 sats; **MuSig2 164 vB → 1,640 sats** (≈ 23% cheaper than SegWit).
- **3-of-3**: SegWit 230 vB; MuSig1 198 vB; **MuSig2 148 vB** (≈ 36% cheaper than SegWit).

The gap grows with larger policies.

## 5. Privacy & complexity at a glance

| Aspect | Best choice | Why |
| --- | --- | --- |
| **Privacy** | **MuSig2 (Taproot key path)** | On-chain looks like single-sig; scripts/branches stay hidden unless used. |
| **Fees** | **MuSig2** | Our measurements show consistently **lower vsize** across many `k-of-n`. |
| **Simplicity (small committees)** | **P2WSH** | Straightforward and widely supported; fine when n is small and privacy isn’t critical. |
| **Simplicity (modern setups)** | **MuSig2** | Two-round protocol; integrates well with Taproot PSBT/HSM workflows. |

**Rule of thumb:** If you can, **design for Taproot + MuSig2**. Fall back to **P2WSH** for legacy tooling or small fixed committees. Prefer **MuSig2** over **MuSig1** unless you’re locked into MuSig1.

## 6. Implementation notes (practical)

- **Key management**: Distribute keys/devices/roles to avoid single points of failure.
- **Backups**: Document recovery for lost keys or role changes (thresholds help).
- **Coordination**: Even with MuSig2’s simpler flow, plan message passing (nonces, partial sigs) in your wallet/HSM/PSBT stack.
- **Testing**: Start with low-value spends and test unusual paths (e.g., time-lock or recovery branches) before going live.

## Appendix — Virtual Sizes (vbytes)

Below is the measured table: **k-of-n**, **SegWit**, **MuSig1**, **MuSig2**, **Best**.  
“**X**” indicates SegWit multisig wasn’t possible (e.g., `n > 16`).  
We created the libraries for the three approaches and ran the same test with each one. These are the results:

| k-of-n | Comb. | SegWit | MuSig1 | MuSig2 | Best |
| --- | --- | --- | --- | --- | --- |
| 1-of-2 | 2 | 185 | 156 | 156 | MuSig1 |
| 2-of-2 | 1 | 203 | 173 | 148 | MuSig2 |
| 1-of-3 | 3 | 194 | 164 | 164 | MuSig1 |
| 2-of-3 | 3 | 212 | 189 | 164 | MuSig2 |
| 3-of-3 | 1 | 230 | 198 | 148 | MuSig2 |
| 1-of-4 | 4 | 202 | 164 | 164 | MuSig1 |
| 2-of-4 | 6 | 220 | 189 | 164 | MuSig2 |
| 3-of-4 | 4 | 238 | 214 | 164 | MuSig2 |
| 4-of-4 | 1 | 256 | 223 | 148 | MuSig2 |
| 1-of-5 | 5 | 211 | 164 | 164 | MuSig1 |
| 2-of-5 | 10 | 229 | 205 | 180 | MuSig2 |
| 3-of-5 | 10 | 247 | 230 | 180 | MuSig2 |
| 4-of-5 | 5 | 265 | 239 | 164 | MuSig2 |
| 5-of-5 | 1 | 283 | 248 | 148 | MuSig2 |
| 1-of-6 | 6 | 219 | 164 | 164 | MuSig1 |
| 2-of-6 | 15 | 237 | 205 | 180 | MuSig2 |
| 3-of-6 | 20 | 255 | 230 | 180 | MuSig2 |
| 4-of-6 | 15 | 273 | 255 | 180 | MuSig2 |
| 5-of-6 | 6 | 291 | 264 | 164 | MuSig2 |
| 6-of-6 | 1 | 309 | 273 | 148 | MuSig2 |
| 1-of-7 | 7 | 228 | 172 | 172 | MuSig1 |
| 2-of-7 | 21 | 246 | 205 | 180 | MuSig2 |
| 3-of-7 | 35 | 264 | 238 | 188 | MuSig2 |
| 4-of-7 | 35 | 282 | 263 | 188 | MuSig2 |
| 5-of-7 | 21 | 300 | 280 | 180 | MuSig2 |
| 6-of-7 | 7 | 318 | 297 | 172 | MuSig2 |
| 7-of-7 | 1 | 336 | 298 | 148 | MuSig2 |
| 1-of-8 | 8 | 237 | 172 | 172 | MuSig1 |
| 2-of-8 | 28 | 255 | 213 | 188 | MuSig2 |
| 3-of-8 | 56 | 273 | 246 | 196 | MuSig2 |
| 4-of-8 | 70 | 291 | 271 | 196 | MuSig2 |
| 5-of-8 | 56 | 309 | 296 | 196 | MuSig2 |
| 6-of-8 | 28 | 327 | 313 | 188 | MuSig2 |
| 7-of-8 | 8 | 345 | 322 | 172 | MuSig2 |
| 8-of-8 | 1 | 363 | 324 | 148 | MuSig2 |
| 1-of-9 | 9 | 245 | 172 | 172 | MuSig1 |
| 2-of-9 | 36 | 263 | 213 | 188 | MuSig2 |
| 3-of-9 | 84 | 281 | 255 | 204 | MuSig2 |
| 7-of-9 | 36 | 353 | 338 | 188 | MuSig2 |
| 8-of-9 | 9 | 371 | 348 | 172 | MuSig2 |
| 9-of-9 | 1 | 389 | 349 | 148 | MuSig2 |
| 1-of-10 | 10 | 254 | 180 | 180 | MuSig1 |
| 2-of-10 | 45 | 272 | 221 | 196 | MuSig2 |
| 3-of-10 | 120 | 290 | 255 | 204 | MuSig2 |
| 4-of-10 | 210 | 308 | 288 | 212 | MuSig2 |
| 7-of-10 | 120 | 362 | 355 | 204 | MuSig2 |
| 8-of-10 | 45 | 380 | 372 | 196 | MuSig2 |
| 9-of-10 | 10 | 398 | 381 | 180 | MuSig2 |
| 10-of-10 | 1 | 416 | 374 | 148 | MuSig2 |
| 1-of-11 | 11 | 262 | 172 | 172 | MuSig1 |
| 2-of-11 | 55 | 280 | 221 | 196 | MuSig2 |
| 3-of-11 | 165 | 298 | 263 | 212 | MuSig2 |
| 9-of-11 | 55 | 406 | 397 | 196 | MuSig2 |
| 10-of-11 | 11 | 424 | 398 | 172 | MuSig2 |
| 11-of-11 | 1 | 442 | 399 | 148 | MuSig2 |
| 1-of-12 | 12 | 271 | 172 | 172 | MuSig1 |
| 2-of-12 | 66 | 289 | 221 | 196 | MuSig2 |
| 3-of-12 | 220 | 307 | 263 | 212 | MuSig2 |
| 4-of-12 | 495 | 325 | 296 | 220 | MuSig2 |
| 9-of-12 | 220 | 415 | 413 | 212 | MuSig2 |
| 10-of-12 | 66 | 433 | 422 | 196 | MuSig2 |
| 11-of-12 | 12 | 451 | 423 | 172 | MuSig2 |
| 12-of-12 | 1 | 469 | 424 | 148 | MuSig2 |
| 1-of-13 | 13 | 279 | 180 | 180 | MuSig1 |
| 2-of-13 | 78 | 297 | 230 | 204 | MuSig2 |
| 3-of-13 | 286 | 315 | 263 | 212 | MuSig2 |
| 11-of-13 | 78 | 459 | 455 | 204 | MuSig2 |
| 12-of-13 | 13 | 477 | 456 | 180 | MuSig2 |
| 13-of-13 | 1 | 495 | 449 | 148 | MuSig2 |
| 1-of-14 | 14 | 288 | 180 | 180 | MuSig1 |
| 2-of-14 | 91 | 306 | 230 | 204 | MuSig2 |
| 3-of-14 | 364 | 324 | 271 | 220 | MuSig2 |
| 12-of-14 | 91 | 486 | 480 | 204 | MuSig2 |
| 13-of-14 | 14 | 504 | 481 | 180 | MuSig2 |
| 14-of-14 | 1 | 522 | 474 | 148 | MuSig2 |
| 1-of-15 | 15 | 296 | 180 | 180 | MuSig1 |
| 2-of-15 | 105 | 314 | 230 | 204 | MuSig2 |
| 3-of-15 | 455 | 332 | 271 | 220 | MuSig2 |
| 4-of-15 | 1365 | 350 | 312 | 236 | MuSig2 |
| 12-of-15 | 455 | 494 | 496 | 220 | MuSig2 |
| 13-of-15 | 105 | 512 | 505 | 204 | MuSig2 |
| 14-of-15 | 15 | 530 | 506 | 180 | MuSig2 |
| 15-of-15 | 1 | 548 | 499 | 148 | MuSig2 |
| 1-of-16 | 16 | 305 | 180 | 180 | MuSig1 |
| 2-of-16 | 120 | 323 | 230 | 204 | MuSig2 |
| 3-of-16 | 560 | 341 | 271 | 220 | MuSig2 |
| 4-of-16 | 1820 | 359 | 312 | 236 | MuSig2 |
| 5-of-16 | 4368 | 377 | 345 | 244 | MuSig2 |
| 12-of-16 | 1820 | 503 | 512 | 236 | MuSig2 |
| 13-of-16 | 560 | 521 | 521 | 220 | MuSig2 |
| 14-of-16 | 120 | 539 | 530 | 204 | MuSig2 |
| 15-of-16 | 16 | 557 | 531 | 180 | MuSig2 |
| 16-of-16 | 1 | 575 | 524 | 148 | MuSig2 |
| 1-of-17 | 17 | X | 180 | 180 | MuSig1 |
| 2-of-17 | 136 | X | 230 | 204 | MuSig2 |
| 3-of-17 | 680 | X | 279 | 228 | MuSig2 |
| 4-of-17 | 2380 | X | 312 | 236 | MuSig2 |
| 14-of-17 | 680 | X | 554 | 228 | MuSig2 |
| 15-of-17 | 136 | X | 555 | 204 | MuSig2 |
| 16-of-17 | 17 | X | 556 | 180 | MuSig2 |
| 17-of-17 | 1 | X | 549 | 148 | MuSig2 |
| 1-of-18 | 18 | X | 180 | 180 | MuSig1 |
| 2-of-18 | 153 | X | 230 | 204 | MuSig2 |
| 3-of-18 | 816 | X | 279 | 228 | MuSig2 |
| 4-of-18 | 3060 | X | 320 | 244 | MuSig2 |
| 15-of-18 | 816 | X | 579 | 228 | MuSig2 |
| 16-of-18 | 153 | X | 580 | 204 | MuSig2 |
| 17-of-18 | 18 | X | 581 | 180 | MuSig2 |
| 18-of-18 | 1 | X | 574 | 148 | MuSig2 |
| 1-of-19 | 19 | X | 180 | 180 | MuSig1 |
| 2-of-19 | 171 | X | 238 | 212 | MuSig2 |
| 3-of-19 | 969 | X | 279 | 228 | MuSig2 |
| 4-of-19 | 3876 | X | 320 | 244 | MuSig2 |
| 16-of-19 | 969 | X | 604 | 228 | MuSig2 |
| 17-of-19 | 171 | X | 614 | 212 | MuSig2 |
| 18-of-19 | 19 | X | 606 | 180 | MuSig2 |
| 19-of-19 | 1 | X | 599 | 148 | MuSig2 |
| 1-of-20 | 20 | X | 180 | 180 | MuSig1 |
| 2-of-20 | 190 | X | 238 | 212 | MuSig2 |
| 3-of-20 | 1140 | X | 279 | 228 | MuSig2 |
| 4-of-20 | 4845 | X | 320 | 244 | MuSig2 |
| 5-of-20 | 15504 | X | 361 | 260 | MuSig2 |
| 16-of-20 | 4845 | X | 620 | 244 | MuSig2 |
| 17-of-20 | 1140 | X | 630 | 228 | MuSig2 |
| 18-of-20 | 190 | X | 639 | 212 | MuSig2 |
| 19-of-20 | 20 | X | 631 | 180 | MuSig2 |
| 20-of-20 | 1 | X | 624 | 148 | MuSig2 |
| 1-of-21 | 21 | X | 180 | 180 | MuSig1 |
| 2-of-21 | 210 | X | 238 | 212 | MuSig2 |
| 3-of-21 | 1330 | X | 287 | 236 | MuSig2 |
| 4-of-21 | 5985 | X | 328 | 252 | MuSig2 |
| 18-of-21 | 1330 | X | 663 | 236 | MuSig2 |
| 19-of-21 | 210 | X | 664 | 212 | MuSig2 |
| 20-of-21 | 21 | X | 656 | 180 | MuSig2 |
| 21-of-21 | 1 | X | 649 | 148 | MuSig2 |
| 1-of-22 | 22 | X | 180 | 180 | MuSig1 |
| 2-of-22 | 231 | X | 238 | 212 | MuSig2 |
| 3-of-22 | 1540 | X | 287 | 236 | MuSig2 |
| 4-of-22 | 7315 | X | 328 | 252 | MuSig2 |
| 19-of-22 | 1540 | X | 688 | 236 | MuSig2 |
| 20-of-22 | 231 | X | 689 | 212 | MuSig2 |
| 21-of-22 | 22 | X | 681 | 180 | MuSig2 |
| 22-of-22 | 1 | X | 674 | 148 | MuSig2 |
| 1-of-23 | 23 | X | 180 | 180 | MuSig1 |
| 2-of-23 | 253 | X | 238 | 212 | MuSig2 |
| 3-of-23 | 1771 | X | 287 | 236 | MuSig2 |
| 4-of-23 | 8855 | X | 328 | 252 | MuSig2 |
| 20-of-23 | 1771 | X | 713 | 236 | MuSig2 |
| 21-of-23 | 253 | X | 714 | 212 | MuSig2 |
| 22-of-23 | 23 | X | 706 | 180 | MuSig2 |
| 23-of-23 | 1 | X | 699 | 148 | MuSig2 |
| 1-of-24 | 24 | X | 188 | 188 | MuSig1 |
| 2-of-24 | 276 | X | 238 | 212 | MuSig2 |
| 3-of-24 | 2024 | X | 287 | 236 | MuSig2 |
| 4-of-24 | 10626 | X | 336 | 260 | MuSig2 |
| 21-of-24 | 2024 | X | 738 | 236 | MuSig2 |
| 22-of-24 | 276 | X | 739 | 212 | MuSig2 |
| 23-of-24 | 24 | X | 739 | 188 | MuSig2 |
| 24-of-24 | 1 | X | 724 | 148 | MuSig2 |
| 1-of-25 | 25 | X | 188 | 188 | MuSig1 |
| 2-of-25 | 300 | X | 238 | 212 | MuSig2 |
| 3-of-25 | 2300 | X | 287 | 236 | MuSig2 |
| 4-of-25 | 12650 | X | 336 | 260 | MuSig2 |
| 21-of-25 | 12650 | X | 762 | 260 | MuSig2 |
| 22-of-25 | 2300 | X | 763 | 236 | MuSig2 |
| 23-of-25 | 300 | X | 764 | 212 | MuSig2 |
| 24-of-25 | 25 | X | 764 | 188 | MuSig2 |
| 25-of-25 | 1 | X | 749 | 148 | MuSig2 |
| 1-of-26 | 26 | X | 180 | 180 | MuSig1 |
| 2-of-26 | 325 | X | 238 | 212 | MuSig2 |
| 3-of-26 | 2600 | X | 295 | 244 | MuSig2 |
| 4-of-26 | 14950 | X | 336 | 260 | MuSig2 |
| 23-of-26 | 2600 | X | 796 | 244 | MuSig2 |
| 24-of-26 | 325 | X | 789 | 212 | MuSig2 |
| 25-of-26 | 26 | X | 781 | 180 | MuSig2 |
| 26-of-26 | 1 | X | 774 | 148 | MuSig2 |
| 1-of-27 | 27 | X | 188 | 188 | MuSig1 |
| 2-of-27 | 351 | X | 246 | 220 | MuSig2 |
| 3-of-27 | 2925 | X | 287 | 236 | MuSig2 |
| 4-of-27 | 17550 | X | 336 | 260 | MuSig2 |
| 24-of-27 | 2925 | X | 813 | 236 | MuSig2 |
| 25-of-27 | 351 | X | 822 | 220 | MuSig2 |
| 26-of-27 | 27 | X | 814 | 188 | MuSig2 |
| 27-of-27 | 1 | X | 799 | 148 | MuSig2 |
| 1-of-28 | 28 | X | 188 | 188 | MuSig1 |
| 2-of-28 | 378 | X | 246 | 220 | MuSig2 |
| 3-of-28 | 3276 | X | 295 | 244 | MuSig2 |
| 4-of-28 | 20475 | X | 344 | 268 | MuSig2 |
| 25-of-28 | 3276 | X | 846 | 244 | MuSig2 |
| 26-of-28 | 378 | X | 847 | 220 | MuSig2 |
| 27-of-28 | 28 | X | 839 | 188 | MuSig2 |
| 28-of-28 | 1 | X | 824 | 148 | MuSig2 |
| 1-of-29 | 29 | X | 188 | 188 | MuSig1 |
| 2-of-29 | 406 | X | 238 | 212 | MuSig2 |
| 3-of-29 | 3654 | X | 287 | 236 | MuSig2 |
| 4-of-29 | 23751 | X | 336 | 260 | MuSig2 |
| 26-of-29 | 3654 | X | 863 | 236 | MuSig2 |
| 27-of-29 | 406 | X | 864 | 212 | MuSig2 |
| 28-of-29 | 29 | X | 864 | 188 | MuSig2 |
| 29-of-29 | 1 | X | 849 | 148 | MuSig2 |
| 1-of-30 | 30 | X | 188 | 188 | MuSig1 |
| 2-of-30 | 435 | X | 246 | 220 | MuSig2 |
| 3-of-30 | 4060 | X | 295 | 244 | MuSig2 |
| 4-of-30 | 27405 | X | 336 | 260 | MuSig2 |
| 26-of-30 | 27405 | X | 887 | 260 | MuSig2 |
| 27-of-30 | 4060 | X | 896 | 244 | MuSig2 |
| 28-of-30 | 435 | X | 897 | 220 | MuSig2 |
| 29-of-30 | 30 | X | 889 | 188 | MuSig2 |
| 30-of-30 | 1 | X | 874 | 148 | MuSig2 |
| 1-of-32 | 32 | X | 188 | 188 | MuSig1 |
| 2-of-32 | 496 | X | 246 | 220 | MuSig2 |
| 3-of-32 | 4960 | X | 303 | 252 | MuSig2 |
| 4-of-32 | 35960 | X | 352 | 276 | MuSig2 |
| 29-of-32 | 4960 | X | 954 | 252 | MuSig2 |
| 30-of-32 | 496 | X | 947 | 220 | MuSig2 |
| 31-of-32 | 32 | X | 939 | 188 | MuSig2 |
| 32-of-32 | 1 | X | 924 | 148 | MuSig2 |
| 1-of-35 | 35 | X | 188 | 188 | MuSig1 |
| 2-of-35 | 595 | X | 254 | 228 | MuSig2 |
| 3-of-35 | 6545 | X | 303 | 252 | MuSig2 |
| 32-of-35 | 6545 | X | 1029 | 252 | MuSig2 |
| 33-of-35 | 595 | X | 1030 | 228 | MuSig2 |
| 34-of-35 | 35 | X | 1014 | 188 | MuSig2 |
| 35-of-35 | 1 | X | 999 | 148 | MuSig2 |
| 1-of-40 | 40 | X | 188 | 188 | MuSig1 |
| 2-of-40 | 780 | X | 254 | 228 | MuSig2 |
| 3-of-40 | 9880 | X | 311 | 260 | MuSig2 |
| 37-of-40 | 9880 | X | 1162 | 260 | MuSig2 |
| 38-of-40 | 780 | X | 1155 | 228 | MuSig2 |
| 39-of-40 | 40 | X | 1139 | 188 | MuSig2 |
| 40-of-40 | 1 | X | 1124 | 148 | MuSig2 |
| 1-of-50 | 50 | X | 188 | 188 | MuSig1 |
| 2-of-50 | 1225 | X | 254 | 228 | MuSig2 |
| 3-of-50 | 19600 | X | 319 | 268 | MuSig2 |
| 47-of-50 | 19600 | X | 1420 | 268 | MuSig2 |
| 48-of-50 | 1225 | X | 1405 | 228 | MuSig2 |
| 49-of-50 | 50 | X | 1389 | 188 | MuSig2 |
| 50-of-50 | 1 | X | 1374 | 148 | MuSig2 |
