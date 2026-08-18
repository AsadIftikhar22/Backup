$ErrorActionPreference = "Stop"

# Configuration - Update these values as needed

$packageVersion = "08-17-2026-081720261601-081720261601"
$publishPath = "publish"
$zipPackageName = "Salam.cms.app.$packageVersion.zip"
$packageName = "Salam.cms.app.$packageVersion.nupkg"
$zipPackageFullPath = Join-Path $publishPath $zipPackageName
$packageFullPath = Join-Path $publishPath $packageName
$defaultPublishPath = "src/Salam.Cms.Web/bin/Release/net8.0/publish"

Write-Host "Restoring dependencies..."
dotnet restore src/ --configfile ./src/nuget.config

Write-Host "Building solution..."
dotnet build ./src/Salam.sln -c 'Release' --framework 'net8.0'

# Ensure the publish directory exists
if (-not (Test-Path $publishPath)) {
    Write-Host "Creating publish directory at $publishPath..."
    New-Item -ItemType Directory -Path $publishPath | Out-Null
}

Write-Host "Publishing project using publish profile..."
dotnet publish ./src/Salam.Cms.Web/Salam.Cms.Web.csproj -c 'Release' /p:PublishProfile=FolderProfile /p:PublishDir=../../publish/

# Check if publish directory is empty, if so, try copying from default location
if (-not (Get-ChildItem -Path $publishPath | Where-Object { -not $_.PSIsContainer })) {
    Write-Warning "Publish directory '$publishPath' is empty. Checking default publish output..."
    if (Test-Path $defaultPublishPath) {
        Write-Host "Copying files from $defaultPublishPath to $publishPath..."
        Copy-Item -Path "$defaultPublishPath\*" -Destination $publishPath -Recurse -Force
    }
}

# Check again if publish directory is empty
if (-not (Get-ChildItem -Path $publishPath | Where-Object { -not $_.PSIsContainer })) {
    Write-Error "Publish directory '$publishPath' is empty. Nothing to package."
    exit 1
}

Set-Location $publishPath

# Create the package using Compress-Archive (as .zip first)
Write-Host "Creating package: $zipPackageName"
Compress-Archive -Path * -DestinationPath $zipPackageName -Force

# Check if the zip file was created
if (-not (Test-Path $zipPackageName)) {
    Write-Error "Failed to create zip package at $zipPackageName."
    exit 1
}

# Rename .zip to .nupkg
Rename-Item -Path $zipPackageName -NewName $packageName

# Move package to root directory
Set-Location ".."
Move-Item "$publishPath/$packageName" .

Write-Host "Build and packaging complete!"
