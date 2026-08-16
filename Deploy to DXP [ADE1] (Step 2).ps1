$ErrorActionPreference = "Stop"

# Configuration - Update these values as needed
$ClientKey = "GB3IZWEB2otcoYFuZiFHZMNYiZemVqgl1ugzMXegrKn15DfY"
$ClientSecret = "07x8voeYkBwLzT8m0Q9tRaGlD0Ckw/wepB/RaLl1ooNVM0aWDafkTT+HUICJcqxA"
$ProjectId = "b29ff4bd-ef79-4366-a34f-b219002cef85"
$EnvironmentName = "ADE1"

# Do not edit below this line
# --------------------------------------------------

# Find the latest package in the package directory
$packageDir = "."
$latestPackage = Get-ChildItem -Path $packageDir -Filter "*.nupkg" | Sort-Object LastWriteTime -Descending | Select-Object -First 1
if (-not $latestPackage) {
    Write-Error "No .nupkg package found in $packageDir."
    exit 1
}
$PackageName = $latestPackage.Name  # Use only the file name

# Extract version from package file name
if ($latestPackage.Name -match "Salam\.cms\.app\.(.+)\.nupkg") {
    $PackageVersion = $Matches[1]
} else {
    $PackageVersion = "unknown"
}

Write-Host "Uploading package to DXP (version: $PackageVersion)..."
Install-Module EpiCloud -Scope CurrentUser -Force

$sasUrl = Get-EpiDeploymentPackageLocation -ClientKey $ClientKey -ClientSecret $ClientSecret -ProjectId $ProjectId

Add-EpiDeploymentPackage -SasUrl $sasUrl -Path $PackageName

Write-Host "Package (version: $PackageVersion) uploaded successfully!"

# Deploy to ADE1
Write-Host "Deploying package (version: $PackageVersion) to ADE1 environment..."
Start-EpiDeployment -ClientKey $ClientKey -ClientSecret $ClientSecret -ProjectId $ProjectId -DeploymentPackage $PackageName -TargetEnvironment $EnvironmentName -DirectDeploy -Wait -Verbose

Write-Host "Deployment to DXP complete for package version: $PackageVersion!"
