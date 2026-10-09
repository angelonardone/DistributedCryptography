// Where the installers are published: the releases of the public repository. The release workflow
// (.github/workflows/release.yml) builds them and always uses the same file names, so these links
// always give the current version.

const repo = 'angelonardone/DistributedCryptography';

/** The page of the current release */
export const releasePage = `https://github.com/${repo}/releases/latest`;

/** The current release as data (read by the Sha256 component) */
export const releaseApi = `https://api.github.com/repos/${repo}/releases/latest`;

/** Direct download of a file of the current release */
export const downloadUrl = (file: string) => `https://github.com/${repo}/releases/latest/download/${file}`;

export const files = {
	windowsSetup: 'DistributedCryptography-Windows-x64-Setup.exe',
	windowsZip: 'DistributedCryptography-Windows-x64.zip',
	macAppleSilicon: 'DistributedCryptography-Mac-AppleSilicon.dmg',
	macIntel: 'DistributedCryptography-Mac-Intel.dmg',
	linuxX64: 'DistributedCryptography-Linux-x64.tar.gz',
	linuxArm64: 'DistributedCryptography-Linux-arm64.tar.gz',
};
