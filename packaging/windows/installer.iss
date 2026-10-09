; Installer for Windows (Inno Setup 6). Built by the release workflow:
;
;   iscc /DAppVersion=0.923 /DSourceDir=<folder with the built application> /DOutputDir=<folder> installer.iss
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
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
MinVersion=10.0
SetupIconFile=DistributedCryptography.ico
UninstallDisplayIcon={app}\DistributedCryptography.ico
UninstallDisplayName={#AppName}
WizardStyle=modern
Compression=lzma2/max
SolidCompression=yes
OutputDir={#OutputDir}
OutputBaseFilename=DistributedCryptography-Windows-x64-Setup

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
