// @ts-check
import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import starlightLinksValidator from 'starlight-links-validator';
import starlightImageZoom from 'starlight-image-zoom';

// Pages that describe features which are not in the public download yet.
// Remove the badge from a page when the release that contains the feature is published.

// https://astro.build/config
export default defineConfig({
	site: 'https://distributedcryptography.com',
	integrations: [
		starlight({
			title: 'Distributed Cryptography',
			description:
				'User documentation of the Distributed Cryptography digital vault and Bitcoin wallet for desktops and servers.',
			logo: { src: './src/assets/logo.svg', alt: '' },
			favicon: '/favicon.svg',
			customCss: ['./src/styles/custom.css'],
			// The build fails if a page links to a page or heading that does not exist.
			// Screenshots open full size when clicked.
			plugins: [starlightLinksValidator(), starlightImageZoom()],
			lastUpdated: true,
			// English only for now. To add Spanish: add `es: { label: 'Español' }` here and
			// put the translated pages in src/content/docs/es/ (same file names).
			defaultLocale: 'root',
			locales: { root: { label: 'English', lang: 'en' } },
			social: [
				{
					icon: 'github',
					label: 'GitHub',
					href: 'https://github.com/angelonardone/DistributedCryptography',
				},
			],
			editLink: {
				baseUrl: 'https://github.com/angelonardone/DistributedCryptography/edit/main/docs/',
			},
			sidebar: [
				{
					label: 'Start here',
					items: [
						{ label: 'What is Distributed Cryptography?', slug: 'introduction' },
						{ label: 'FAQ', slug: 'faq' },
						{ label: 'Videos', slug: 'videos' },
					],
				},
				{
					label: 'Install',
					items: [
						{ label: 'Choose how to install', slug: 'install' },
						{ label: 'Docker', slug: 'install/docker' },
						{ label: 'Windows', slug: 'install/windows' },
						{ label: 'Mac', slug: 'install/mac' },
						{ label: 'Linux', slug: 'install/linux' },
						{ label: 'IIS (web app)', slug: 'install/iis' },
						{ label: 'Linux server (web app)', slug: 'install/linux-server' },
						{ label: 'Build from source', slug: 'install/build-from-source' },
					],
				},
				{
					label: 'Your wallet',
					items: [
						{ label: 'Create a wallet', slug: 'wallet/create' },
						{ label: 'Restore a wallet', slug: 'wallet/restore' },
						{ label: 'Advance Brain Wallet', slug: 'wallet/advance-brain-wallet' },
					],
				},
				{
					label: 'Online features',
					items: [
						{ label: 'Anonymous login', slug: 'online' },
						{ label: 'Contacts', slug: 'online/contacts' },
						{ label: 'Chat', slug: 'online/chat' },
						{ label: 'Send encrypted files', slug: 'online/send-files' },
					],
				},
				{
					label: 'Smart Groups',
					items: [
						{ label: 'What is a Smart Group?', slug: 'groups' },
						{ label: 'Consensus Backup', slug: 'groups/consensus-backup' },
						{ label: 'Delegated Multisignature', slug: 'groups/delegated-multisignature' },
						{ label: 'Legacy Multisignature', slug: 'groups/legacy-multisignature' },
						{ label: 'Time-Encrypted Vault', slug: 'groups/time-encrypted-vault' },
						{ label: 'Shared passwords', slug: 'groups/shared-passwords' },
					],
				},
				{
					label: 'Offline tools',
					items: [
						{ label: 'Overview', slug: 'offline' },
						{ label: 'Encrypted notes', slug: 'offline/notes' },
						{ label: 'Encrypted files', slug: 'offline/encrypted-files' },
						{ label: 'Encrypted passwords', slug: 'offline/passwords' },
						{ label: 'Authenticators', slug: 'offline/authenticators' },
						{ label: 'Create QR codes', slug: 'offline/qr-codes' },
						{ label: 'Split a secret', slug: 'offline/split-a-secret' },
						{ label: 'HSM', slug: 'offline/hsm' },
					],
				},
				{
					label: 'Security and privacy',
					items: [
						{ label: 'How your data is protected', slug: 'security' },
						{ label: 'What our servers can see', slug: 'security/privacy' },
						{ label: 'Bitcoin standards', slug: 'security/standards' },
					],
				},
				{
					label: 'Guides',
					items: [{ label: 'Bitcoin multisignature: a practical guide', slug: 'guides/multisig' }],
				},
			],
		}),
	],
});
