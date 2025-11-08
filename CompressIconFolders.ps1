# CompressIconFolders.ps1

[CmdletBinding()]
param (
    [Parameter(Mandatory)]
    [string]
    $BaseDirectory,

    [string]
    $IconSubDirectory = 'Resources\Icons',

    [string]
    $SevenZipSubPath = 'Resources\7z.exe'
)

# Terminate if we have any kind of errors
$ErrorActionPreference = 'Stop'

$divider = '-' * 128 # helper

# Ensure the BaseDirectory parameter is provided
# if (-not $BaseDirectory) {
#     Write-Error "BaseDirectory parameter is required."
#     exit 1
# }

# Clean up the BaseDirectory path.
# $BaseDirectory = $BaseDirectory.Trim('"').TrimEnd('\')
$BaseDirectory = $BaseDirectory -replace '\"' -replace "\'" -replace '\\$'

# Get all subdirectories in the specified base directory
$iconDirectory = Join-Path -Path $BaseDirectory -ChildPath $IconSubDirectory

# Although helpful in debugging, full path can make things less readable
$redDirectory = [regex]::Escape("$($PWD.Path)\$BaseDirectory\")

# Create 7z.exe alias. Because Compress-Archive is slow
New-Alias -Name 7z -Value "$BaseDirectory\$SevenZipSubPath"

Write-Output "`n$divider`n  CompressIconFolders`n$divider"

# Print the WorkingDirectory, BaseDirectory and IconDirectory for debugging
Write-Output "  WorkingDirectory: $($PWD.Path)"
Write-Output "  BaseDirectory   : $BaseDirectory"
Write-Output "  iconDirectory   : $iconDirectory"
Write-Output $divider

if (-not (Test-Path $iconDirectory)) {
    Write-Error "The directory $iconDirectory does not exist."
    exit 1
}

# Use Git to check for changes in the Resources/Icons directory
$gitChangedFiles = git status --porcelain | Select-String -Pattern "^.+/Output/Resources/Icons/"

$folders = Get-ChildItem -Path $iconDirectory -Directory

# Iterate through each folder
foreach ($folder in $folders) {
    # Define the full path to the folder and the destination zip file
    $folderPath = Join-Path -Path $folder.FullName -ChildPath "*.*"
    $destinationPath = "$($folder.FullName).zip"
    $svMessage = ''
    # Check if the zip file exists
    $zipExists = Test-Path $destinationPath

    # Determine if we need to update the zip file
    $updateRequired = $false
    if ($zipExists) {
        if ($gitChangedFiles.Count -gt 0) {
            foreach ($changedFile in $gitChangedFiles) {
                if ($changedFile -like "*$($folder.Name)/*") {
                    $updateRequired = $true
                    Write-Output "$destinationPath exists, but there are changes, updating it."
                    break
                }
            }
        }
    }
    else {
        $updateRequired = $true
        # Write-Output "$destinationPath doesn't exists, need to create it."
    }

    if ($updateRequired) {
        # Compress the folder into a zip file
        # Compress-Archive -Path $folderPath -DestinationPath $destinationPath -Force
        7z a -tzip $destinationPath $folderPath | Select-String -Pattern 'Creating archive' | foreach {
            # We should only have 1 message, but add just in case
            $svMessage += $_ -replace $redDirectory -replace 'Creating archive:','[created] [zip]'
        }
        if ($LASTEXITCODE) {
            throw "7z exit code: $LASTEXITCODE. Error creating $destinationPath. See output for more info"
        }
        else {
            Write-Output $svMessage
        }
    }
    else {
        # Write-Output "$destinationPath is up to date. No action required."
        Write-Output "[skipped] [zip] $($destinationPath -replace $redDirectory)"
    }


    # Check if the zip file was created successfully
    if (Test-Path $destinationPath) {
        # Delete the original folder if the zip file exists
        Remove-Item -Path $folder.FullName -Recurse -Force
    }
    else {
        Write-Error "Failed to create $destinationPath"
    }
}
