# Deployment script for Ubuntu Server
# This script transfers compiled files to the Ubuntu server

# IMPORTANT: Configure these variables in your environment or pass them as parameters
# DO NOT commit real values to the repository

param(
    [string]$ServerIP = $env:DEPLOY_SERVER_IP,
    [string]$ServerUser = $env:DEPLOY_SERVER_USER,
    [string]$RemotePath = $env:DEPLOY_REMOTE_PATH,
    [int]$ServerPort = $env:DEPLOY_SERVER_PORT
)

# Default values if not set in environment variables
if (-not $ServerIP) { $ServerIP = "YOUR_SERVER_IP" }
if (-not $ServerUser) { $ServerUser = "YOUR_USERNAME" }
if (-not $RemotePath) { $RemotePath = "/var/www/base-angular-app" }
if (-not $ServerPort) { $ServerPort = 4200 }

Write-Host "=====================================" -ForegroundColor Cyan
Write-Host "  Angular Deployment to Ubuntu       " -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host ""

# Check if dist/base-angular-app/browser folder exists
$browserPath = ".\dist\base-angular-app\browser"
if (-not (Test-Path $browserPath)) {
    Write-Host "Error: Folder $browserPath not found" -ForegroundColor Red
    Write-Host "Run 'ng build --configuration production' first." -ForegroundColor Red
    exit 1
}

# Validate configuration
if ($ServerIP -eq "YOUR_SERVER_IP" -or $ServerUser -eq "YOUR_USERNAME") {
    Write-Host "Error: You must configure environment variables or pass parameters:" -ForegroundColor Red
    Write-Host "  DEPLOY_SERVER_IP=your_ip" -ForegroundColor Yellow
    Write-Host "  DEPLOY_SERVER_USER=your_username" -ForegroundColor Yellow
    Write-Host "  DEPLOY_REMOTE_PATH=/var/www/base-angular-app" -ForegroundColor Yellow
    Write-Host "  DEPLOY_SERVER_PORT=4200" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Or use parameters:" -ForegroundColor Yellow
    Write-Host "  .\deploy.ps1 -ServerIP '192.168.1.100' -ServerUser 'user'" -ForegroundColor Yellow
    exit 1
}

Write-Host "Server: ${ServerIP}:${ServerPort}" -ForegroundColor Yellow
Write-Host "User: $ServerUser" -ForegroundColor Yellow
Write-Host "Remote path: $RemotePath" -ForegroundColor Yellow
Write-Host ""

# Check if SCP is available
try {
    $scpVersion = scp 2>&1
    Write-Host "OK SCP available" -ForegroundColor Green
} catch {
    Write-Host "ERROR SCP not available. Install OpenSSH Client from Windows Settings." -ForegroundColor Red
    Write-Host "  Settings > Apps > Optional Features > OpenSSH Client" -ForegroundColor Yellow
    exit 1
}

Write-Host ""
Write-Host "Starting file transfer..." -ForegroundColor Cyan
Write-Host ""

# Compress files from browser subdirectory
Write-Host "[1/3] Compressing files from browser/..." -ForegroundColor Yellow
$zipFile = ".\dist\app.zip"
Compress-Archive -Path "$browserPath\*" -DestinationPath $zipFile -Force

if (Test-Path $zipFile) {
    Write-Host "OK Files compressed successfully" -ForegroundColor Green
} else {
    Write-Host "ERROR Failed to compress files" -ForegroundColor Red
    exit 1
}

# Transfer compressed file to server
Write-Host ""
Write-Host "[2/3] Transferring files to server..." -ForegroundColor Yellow
Write-Host "Enter server password when prompted" -ForegroundColor Cyan

scp $zipFile "${ServerUser}@${ServerIP}:/tmp/app.zip"

if ($LASTEXITCODE -eq 0) {
    Write-Host "OK Files transferred successfully" -ForegroundColor Green
} else {
    Write-Host "ERROR Failed to transfer files" -ForegroundColor Red
    Remove-Item $zipFile -Force
    exit 1
}

# Decompress on server
Write-Host ""
Write-Host "[3/3] Decompressing and deploying files on server..." -ForegroundColor Yellow
Write-Host "You will be prompted for sudo password..." -ForegroundColor Cyan
Write-Host ""

# Extract to temp directory first (no sudo needed)
Write-Host "  - Extracting files to temp directory..." -ForegroundColor Gray
ssh "${ServerUser}@${ServerIP}" "mkdir -p ~/temp-deploy && rm -rf ~/temp-deploy/* && unzip -q /tmp/app.zip -d ~/temp-deploy"

if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR Failed to extract files" -ForegroundColor Red
    exit 1
}

# Deploy with sudo (will prompt for password)
Write-Host "  - Stopping Nginx..." -ForegroundColor Gray
Write-Host "  - Cleaning deployment directory..." -ForegroundColor Gray
Write-Host "  - Copying files to $RemotePath..." -ForegroundColor Gray
Write-Host "  - Setting permissions..." -ForegroundColor Gray
Write-Host "  - Starting Nginx..." -ForegroundColor Gray

ssh -t "${ServerUser}@${ServerIP}" @"
sudo systemctl stop nginx && \
sudo mkdir -p $RemotePath && \
sudo rm -rf $RemotePath/* && \
sudo cp -r ~/temp-deploy/* $RemotePath/ && \
sudo chown -R www-data:www-data $RemotePath && \
sudo chmod -R 755 $RemotePath && \
sudo systemctl start nginx && \
echo 'Deployment completed'
"@

if ($LASTEXITCODE -ne 0) {
    Write-Host ""
    Write-Host "ERROR Failed during deployment" -ForegroundColor Red
    exit 1
}

# Clean up temp files
Write-Host "  - Cleaning up..." -ForegroundColor Gray
ssh "${ServerUser}@${ServerIP}" "rm -rf ~/temp-deploy && rm -f /tmp/app.zip"

Write-Host ""
Write-Host "OK Deployment completed successfully" -ForegroundColor Green
Write-Host ""
Write-Host "Verifying deployment:" -ForegroundColor Cyan
ssh "${ServerUser}@${ServerIP}" "ls -lh $RemotePath"
Write-Host ""
Write-Host "Checking Nginx status:" -ForegroundColor Cyan
ssh "${ServerUser}@${ServerIP}" "sudo systemctl status nginx --no-pager -l | head -n 5"

# Clean local zip file
Remove-Item $zipFile -Force

Write-Host ""
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host "Your application should be available at:" -ForegroundColor Cyan
Write-Host "http://${ServerIP}:${ServerPort}" -ForegroundColor Yellow
Write-Host "=====================================" -ForegroundColor Cyan
