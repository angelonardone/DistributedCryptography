# Copies the built documentation site (docs\dist) to the folder served by IIS.
# Run "npm run build" first. Files that are no longer part of the site are removed from the destination.
#
#   powershell -File deploy\update-site.ps1
#   powershell -File deploy\update-site.ps1 -Destination D:\sites\docs

param(
    [string]$Destination = 'C:\inetpub\docs.distributedcryptography.com'
)

$ErrorActionPreference = 'Stop'
$source = Join-Path (Split-Path $PSScriptRoot -Parent) 'dist'

if (-not (Test-Path (Join-Path $source 'index.html'))) {
    throw "No built site in $source. Run 'npm run build' in the docs folder first."
}

New-Item -ItemType Directory -Force -Path $Destination | Out-Null

# /MIR mirrors the folder (copies what changed, deletes what is gone). Exit codes below 8 mean success.
robocopy $source $Destination /MIR /R:2 /W:2 /NFL /NDL /NJH /NP
if ($LASTEXITCODE -ge 8) {
    throw "robocopy failed with exit code $LASTEXITCODE"
}

Write-Host "Site copied to $Destination"
exit 0
