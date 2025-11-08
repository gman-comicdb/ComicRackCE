# PostBuild.ps1

[CmdletBinding()]
param (
    [Parameter(Mandatory)]
    [string]
    $BaseDirectory,

    [string]
    $Configuration,

    [string[]]
    $RemoveDirectory = @('arm64', 'Lib', 'x64', 'x86', 'de')
)

# Terminate if we have any kind of errors
$ErrorActionPreference = 'Stop'

$divider = '-' * 64 # helper

# Clean up the BaseDirectory path. Declare resourceDirectory and libDirectory
$BaseDirectory     = $BaseDirectory -replace '\"' -replace "\'" -replace '\\$'
$resourceDirectory = "$BaseDirectory\Resources"
$libDirectory      = "$resourceDirectory\Lib"

# Although helpful in debugging, full path can make things less readable
$redDirectory = [regex]::Escape("$($PWD.Path)\$BaseDirectory\")

Write-Output "`n$divider`n  PostBuild`n$divider"

Write-Output "  WorkingDirectory  : $($PWD.Path)"
Write-Output "  BaseDirectory     : $BaseDirectory"
Write-Output "  resourceDirectory : $resourceDirectory"
Write-Output "  libDirectory      : $libDirectory"
Write-Output $divider

$dirList = @($resourceDirectory, $libDirectory, "$libDirectory\Microsoft", "$libDirectory\System")

$dirList | where {-not (Test-Path "$($PWD.Path)\$_")} | foreach {
    [void](New-Item -Path "$($PWD.Path)\$_" -ItemType Directory -Force)
    Write-Output "[created] [dir] $_"
}


if (Test-Path "$BaseDirectory\x64\pdfium.dll") {
    Move-Item -LiteralPath "$BaseDirectory\x64\pdfium.dll" -Destination "$resourceDirectory\pdfium_x64.dll" -Force
    Write-Output "[moved] [dll] x64\pdfium.dll -> Resources\pdfium_x64.dll"
}
else {
    throw "[ERROR] '$BaseDirectory\x64\pdfium.dll' does not exist"
}

if (Test-Path "$BaseDirectory\x86\pdfium.dll") {
    Move-Item -LiteralPath "$BaseDirectory\x86\pdfium.dll" -Destination "$resourceDirectory\pdfium_x86.dll" -Force
    Write-Output "[moved] [dll] x86\pdfium.dll -> Resources\pdfium_x86.dll"
}
else {
    throw "[ERROR] '$BaseDirectory\x86\pdfium.dll' does not exist"
}

Get-ChildItem -Path "$BaseDirectory\*.dll" | where {$_.Name -notmatch '^(cYo|ComicRack)\.'} | foreach {

    $destination = "$($PWD.Path)\$libDirectory\$($_.Name)"
    if ($_.BaseName -match '^Microsoft\.')
    {
        $destination = "$($PWD.Path)\$libDirectory\Microsoft\$($_.Name)"
    }
    elseif ($_.BaseName -match '^System\.') {
        $destination = "$($PWD.Path)\$libDirectory\System\$($_.Name)"
    }
    Move-Item $_.FullName -Destination $destination -Force
    Write-Output "[moved] [dll] $($_.Name) -> $($destination -replace $redDirectory)"
}

$RemoveDirectory | foreach {
    if (Test-Path "$BaseDirectory\$_") {
        Remove-Item "$BaseDirectory\$_" -Recurse -Force
    }
}

if ($Configuration -ne 'Debug') {
    Remove-Item "$BaseDirectory\*.pdb" -Force
}
