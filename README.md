# Distributed Cryptography


The documentation is at [distributedcryptography.com](https://distributedcryptography.com): how to install, build and use the product.

## Download

Ready-made installers for Windows (installer and zip), Mac (Apple Silicon and Intel) and Linux (x64 and ARM64) are on the
[releases page](https://github.com/angelonardone/DistributedCryptography/releases/latest), with the SHA-256 of every file.
They are built from this repository by the workflow [release.yml](.github/workflows/release.yml), which also starts
and tests each one on its own system before it is published.

## Run with Docker

The quickest way to try the wallet — no .NET SDK or GeneXus required, only [Docker](https://docs.docker.com/get-docker/):

```bash
git clone https://github.com/angelonardone/DistributedCryptography
cd DistributedCryptography
docker compose up -d
```

Then open http://localhost:5000 (it redirects to the wallets page).

* Wallet data is stored in the `distcrypt-wallets` and `distcrypt-data` Docker volumes, so it survives container rebuilds and updates. **It is only deleted if you run `docker compose down -v`.**
* To use a different external port, create a `.env` file with `APP_PORT=8080` (the app always listens on 5000 inside the container).
* The HSM REST APIs are exposed at `http://localhost:5000/HSM/rest/...` (see `web/HSM.rest.yaml` for the OpenAPI spec). **They require no authentication, so do not expose the port to untrusted networks.** To keep them local-only, change the port mapping to `127.0.0.1:5000:5000` in `docker-compose.yml`.
* To rebuild after pulling new code: `docker compose up -d --build`.


This is a Digital Vault/Wallet designed to operate seamlessly on any PC or server. It is a versatile, multi-platform application that is compatible with Windows, Linux, and Mac operating systems. With its robust architecture, this wallet ensures secure and efficient management of Bitcoin transactions across different environments, making it an ideal choice for users seeking flexibility and reliability in their Bitcoin management.

In addition, we support a novel interactive backup process that enhances security and user experience. Furthermore, our wallet incorporates a custom Taproot algorithm that significantly reduces transaction fees for any K-of-N multisignature setup, making it many times cheaper than traditional multisignature mechanisms. This combination of advanced features ensures that users can enjoy both cutting-edge security and cost-efficiency in their Bitcoin transactions.

The wallet sopport the following Bitcoin standards:

* Brain Wallets (not recommended).
* Wallet Import Format (WIF) for legacy individual keys.
* Hierarchical Deterministic Wallets [(BIP 32)](https://github.com/bitcoin/bips/blob/master/bip-0032.mediawiki)
* Mnemonic code for generating deterministic keys [(BIP 39)](https://github.com/bitcoin/bips/blob/master/bip-0039.mediawiki)
* Multi-Account Hierarchy for Deterministic Wallets - Legacy Format of HD [(BIP 44)](https://github.com/bitcoin/bips/blob/master/bip-0044.mediawiki)
* Derivation scheme for P2WPKH-nested-in-P2SH based accounts [(BIP 49)](https://github.com/bitcoin/bips/blob/master/bip-0049.mediawiki)
* Derivation scheme for P2WPKH based accounts [(BIP 84)](https://github.com/bitcoin/bips/blob/master/bip-0084.mediawiki)
* Segregated Witness [(BIP 141)](https://github.com/bitcoin/bips/blob/master/bip-0141.mediawiki), [(BIP 143)](https://github.com/bitcoin/bips/blob/master/bip-0143.mediawiki) and [(BIP 144)](https://github.com/bitcoin/bips/blob/master/bip-0144.mediawiki)
* Bech32 segwit address implementation with error detection [(BIP 163)](https://github.com/bitcoin/bips/blob/master/bip-0173.mediawiki)
* Key Derivation for Single Key P2TR Outputs [(BIP 86)](https://github.com/bitcoin/bips/blob/master/bip-0086.mediawiki) ~ <b>our default mode</b>
* Validation of Taproot Scripts ([BIP 341](https://github.com/bitcoin/bips/blob/master/bip-0341.mediawiki) and [BIP 342](https://github.com/bitcoin/bips/blob/master/bip-0342.mediawiki))
* Multy spending paths (address and script)
* K of N multisignature on Taproot: each combination of K signers is one [BIP 387](https://github.com/bitcoin/bips/blob/master/bip-0387.mediawiki) `sortedmulti_a` script, and the wallet is one [BIP 386](https://github.com/bitcoin/bips/blob/master/bip-0386.mediawiki) `tr()` descriptor that other wallets can import
* Classic SegWit multisignature (P2SH-P2WSH) with [BIP 48](https://github.com/bitcoin/bips/blob/master/bip-0048.mediawiki) keys sorted as in [BIP 67](https://github.com/bitcoin/bips/blob/master/bip-0067.mediawiki), signed with [PSBT (BIP 174)](https://github.com/bitcoin/bips/blob/master/bip-0174.mediawiki)


## Advanced Encryption Algorithms

Our security measures employ well-known and highly trusted algorithms to ensure data protection:

* Password Hashing: User passwords are securely hashed using Argon2id, configured with a 128-bit salt, 8 degrees of parallelism, 10 iterations, and 640 MiB of memory. As a reference, OWASP recommends a minimum of 19 MiB of memory, 2 iterations, and 1 degree of parallelism.
* Master Key Management: Each user’s Master Key is deterministically generated using the BIP-39 standard (mnemonic-based key derivation). This Master Key is encrypted using AES-256-GCM with a randomly generated 256-bit AES key, which is derived from the user’s password hash (via Argon2id). A 128-bit authentication tag is included to ensure the integrity and authenticity of the encrypted Master Key.
* Data Encryption: Each piece of user data is encrypted using a unique, randomly generated 256-bit AES key. Encryption is performed using AES-GCM with a 128-bit authentication tag, providing strong confidentiality and ensuring that each data item is individually tamper-resistant.
* File Encryption: A new random 256-bit key is created for every file and locked with the public key of the person who may open it. The content is encrypted in pieces of 64 KiB with AES-256-GCM, so files of any size are handled with constant memory. A file is rejected if any byte was changed, or if pieces were reordered, removed or cut off.

By integrating these advanced security features and leveraging the strength of the Bitcoin network, we provide our users with state-of-the-art tools to protect their privacy and secure their digital assets. Our commitment to security ensures that users can trust our solutions to keep their data safe and accessible only to them.


# Some external projects we realy on (thanks :-D)

* [NBitcoin](https://github.com/MetacoSA/NBitcoin)
* [SecretSharingDotNet](https://github.com/shinji-san/SecretSharingDotNet)
* [GoogleAuthenticator](https://github.com/BrandonPotter/GoogleAuthenticator)
* [QRCoder](https://github.com/codebude/QRCoder)
