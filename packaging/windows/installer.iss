; Installer for Windows (Inno Setup 6). Built by the release workflow:
;
;   iscc /DAppVersion=1.000 /DAppArch=x64 /DOutputName=DistributedCryptography-Windows-x64-Setup
;        /DSourceDir=<folder with the built application> /DOutputDir=<folder> installer.iss
;
; AppArch is x64 or arm64: the processor the application was built for. Both installers have the same AppId
; because they install the same application, so either one replaces the other.
;
; It installs for the current user only, without administrator rights: the wallet writes temporary files in
; its own folder, so it must not live under "Program Files".
; The wallets are NOT in the application folder (they are in <user folder>\DistributedCryptography), so
; installing a new version or uninstalling never touches them.

#ifndef AppVersion
  #define AppVersion "0.0.0"
#endif
#ifndef SourceDir
  #error SourceDir is not defined
#endif
#ifndef OutputDir
  #define OutputDir "."
#endif
#ifndef AppArch
  #define AppArch "x64"
#endif
#ifndef OutputName
  #define OutputName "DistributedCryptography-Windows-" + AppArch + "-Setup"
#endif
#if AppArch == "x64"
  ; x64compatible also accepts Windows 11 on ARM, which runs x64 programs
  #define ArchAllowed "x64compatible"
#elif AppArch == "arm64"
  #define ArchAllowed "arm64"
#else
  #error AppArch must be x64 or arm64
#endif

#define AppName "Distributed Cryptography"
#define AppUrl "https://github.com/angelonardone/DistributedCryptography"

[Setup]
; Never change AppId: it is how Windows knows that a new version replaces the installed one
AppId={{96A71195-51CC-489C-AB3F-EEFC0D2C5636}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher=Distributed Cryptography
AppPublisherURL={#AppUrl}
AppSupportURL={#AppUrl}
AppUpdatesURL={#AppUrl}/releases
VersionInfoVersion={#AppVersion}
PrivilegesRequired=lowest
DefaultDirName={autopf}\DistributedCryptography
DefaultGroupName={#AppName}
DisableProgramGroupPage=yes
DisableDirPage=auto
ArchitecturesAllowed={#ArchAllowed}
ArchitecturesInstallIn64BitMode={#ArchAllowed}
MinVersion=10.0
SetupIconFile=DistributedCryptography.ico
UninstallDisplayIcon={app}\DistributedCryptography.ico
UninstallDisplayName={#AppName}
WizardStyle=modern
Compression=lzma2/max
SolidCompression=yes
OutputDir={#OutputDir}
OutputBaseFilename={#OutputName}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[InstallDelete]
; Programs of the version that is being replaced
Type: filesandordirs; Name: "{app}\bin"

[Files]
Source: "{#SourceDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "DistributedCryptography.cmd"; DestDir: "{app}"; Flags: ignoreversion
Source: "DistributedCryptography.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#AppName}"; Filename: "{app}\DistributedCryptography.cmd"; WorkingDir: "{app}"; IconFilename: "{app}\DistributedCryptography.ico"; Comment: "Start the wallet and open it in the browser"
Name: "{autodesktop}\{#AppName}"; Filename: "{app}\DistributedCryptography.cmd"; WorkingDir: "{app}"; IconFilename: "{app}\DistributedCryptography.ico"; Tasks: desktopicon

[Run]
Filename: "{app}\DistributedCryptography.cmd"; WorkingDir: "{app}"; Description: "{cm:LaunchProgram,{#AppName}}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
; Temporary files written by the wallet while it runs
Type: filesandordirs; Name: "{app}\PublicTempStorage"
Type: filesandordirs; Name: "{app}\PrivateTempStorage"
Type: filesandordirs; Name: "{app}\logs"
