# Setup Script for Environment Configuration
# This creates both local and production environment files

Write-Host "====================================" -ForegroundColor Cyan
Write-Host "  Environment Setup" -ForegroundColor Cyan
Write-Host "====================================" -ForegroundColor Cyan
Write-Host ""

# Check what needs to be created
$needsLocal = -not (Test-Path ".env.local")
$needsProduction = -not (Test-Path ".env.production")

if (-not $needsLocal -and -not $needsProduction) {
    Write-Host "Both .env.local and .env.production already exist." -ForegroundColor Yellow
    Write-Host ""
    $overwrite = Read-Host "Do you want to recreate them? (y/n)"
    if ($overwrite -ne "y") {
        Write-Host "Setup cancelled." -ForegroundColor Red
        exit
    }
    $needsLocal = $true
    $needsProduction = $true
}

# Create .env.local
if ($needsLocal) {
    Write-Host "Creating .env.local for local development..." -ForegroundColor Green
    
    Write-Host ""
    Write-Host "=== Local Development Configuration ===" -ForegroundColor Cyan
    $localHost = Read-Host "Database Host (e.g., 192.168.100.142 or localhost)"
    $localPort = Read-Host "Database Port (default: 5432)"
    if ([string]::IsNullOrWhiteSpace($localPort)) { $localPort = "5432" }
    $localDb = Read-Host "Database Name (e.g., BaseApiDb_Dev)"
    $localUser = Read-Host "Database Username"
    $localPass = Read-Host "Database Password" -AsSecureString
    $localPassPlain = [Runtime.InteropServices.Marshal]::PtrToStringAuto(
        [Runtime.InteropServices.Marshal]::SecureStringToBSTR($localPass))
    
    $localEnv = @"
# Local Development Environment
DB_HOST=$localHost
DB_PORT=$localPort
DB_NAME=$localDb
DB_USERNAME=$localUser
DB_PASSWORD=$localPassPlain
ASPNETCORE_ENVIRONMENT=Development
"@
    
    $localEnv | Out-File -FilePath ".env.local" -Encoding UTF8
    Write-Host "✅ Created .env.local" -ForegroundColor Green
}

# Create .env.production
if ($needsProduction) {
    Write-Host ""
    Write-Host "Creating .env.production for deployment..." -ForegroundColor Green
    
    Write-Host ""
    Write-Host "=== Production Deployment Configuration ===" -ForegroundColor Cyan
    
    Write-Host ""
    Write-Host "SSH Configuration:" -ForegroundColor Yellow
    $sshUser = Read-Host "SSH Username"
    $sshHost = Read-Host "SSH Host/IP"
    $sshPort = Read-Host "SSH Port (default: 22)"
    if ([string]::IsNullOrWhiteSpace($sshPort)) { $sshPort = "22" }
    
    Write-Host ""
    Write-Host "Server Paths:" -ForegroundColor Yellow
    $appPath = Read-Host "Application Path (e.g., /var/www/netcore-baseapi)"
    $serviceName = Read-Host "Systemd Service Name (e.g., netcore-baseapi)"
    
    Write-Host ""
    Write-Host "Production Database (runs on server, use 'localhost'):" -ForegroundColor Yellow
    $prodHost = Read-Host "Database Host (default: localhost)"
    if ([string]::IsNullOrWhiteSpace($prodHost)) { $prodHost = "localhost" }
    $prodPort = Read-Host "Database Port (default: 5432)"
    if ([string]::IsNullOrWhiteSpace($prodPort)) { $prodPort = "5432" }
    $prodDb = Read-Host "Database Name (e.g., BaseApiDb_Prod)"
    $prodUser = Read-Host "Database Username"
    $prodPass = Read-Host "Database Password" -AsSecureString
    $prodPassPlain = [Runtime.InteropServices.Marshal]::PtrToStringAuto(
        [Runtime.InteropServices.Marshal]::SecureStringToBSTR($prodPass))
    
    $prodEnv = @"
# Production Deployment Environment

# SSH Connection
SSH_USER=$sshUser
SSH_HOST=$sshHost
SSH_PORT=$sshPort

# Server Configuration
SERVER_TEMP_PATH=/tmp
SERVER_APP_PATH=$appPath
SERVICE_NAME=$serviceName
APP_ZIP_NAME=net-api.zip
LOCAL_PUBLISH_PATH=./publish

# Production Database
DB_HOST=$prodHost
DB_PORT=$prodPort
DB_NAME=$prodDb
DB_USERNAME=$prodUser
DB_PASSWORD=$prodPassPlain
ASPNETCORE_ENVIRONMENT=Production
"@
    
    $prodEnv | Out-File -FilePath ".env.production" -Encoding UTF8
    Write-Host "✅ Created .env.production" -ForegroundColor Green
}

Write-Host ""
Write-Host "====================================" -ForegroundColor Green
Write-Host "  Setup Complete!" -ForegroundColor Green
Write-Host "====================================" -ForegroundColor Green
Write-Host ""

if ($needsLocal) {
    Write-Host "Local Development:" -ForegroundColor Cyan
    Write-Host "  1. Use: .\setup-local-from-env.ps1" -ForegroundColor White
    Write-Host "     (Generates appsettings.Local.json from .env.local)" -ForegroundColor Gray
    Write-Host ""
}

if ($needsProduction) {
    Write-Host "Production Deployment:" -ForegroundColor Cyan
    Write-Host "  1. Use: .\deploy.ps1" -ForegroundColor White
    Write-Host "     (Uses .env.production automatically)" -ForegroundColor Gray
    Write-Host ""
}

Write-Host "⚠️  IMPORTANT: .env.local and .env.production are in .gitignore" -ForegroundColor Yellow
