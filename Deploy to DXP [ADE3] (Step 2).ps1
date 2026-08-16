$ErrorActionPreference = "Stop"

Import-Module .\EpiCloud.psm1

# Configuration - Update these values as needed
$ClientKey = "zWqQWaCg5uQoGCfj3VN2NSHRMfymLaXcolMAbhc4bYGYAnX4"
$ClientSecret = "ZaYtK5bs/njeNXjs4sygGnHgVtJ07QvY/t+sWBS4qV2ceisUTaxSDZOK6dmnq4AO"
$ProjectId = "b29ff4bd-ef79-4366-a34f-b219002cef85"
$EnvironmentName = "ADE3"

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
