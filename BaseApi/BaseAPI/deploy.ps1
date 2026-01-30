# ===================================
# 🚀 Automated Deployment Script
# ===================================
# Deploys .NET Core API to Ubuntu Server

param(
    [switch]$SkipBuild,
    [switch]$SkipTests,
    [switch]$Verbose
)

$ErrorActionPreference = "Stop"

# ===================================
# Helper Functions
# ===================================

function Write-Step {
    param([string]$Message)
    Write-Host ""
    Write-Host "===> $Message" -ForegroundColor Cyan
}

function Write-Success {
    param([string]$Message)
    Write-Host "✅ $Message" -ForegroundColor Green
}

function Write-Error-Message {
    param([string]$Message)
    Write-Host "❌ $Message" -ForegroundColor Red
}

function Write-Warning-Message {
    param([string]$Message)
    Write-Host "⚠️  $Message" -ForegroundColor Yellow
}

function Load-EnvFile {
    param([string]$FilePath)
    
    if (-not (Test-Path $FilePath)) {
        throw "Environment file not found: $FilePath"
    }

    Get-Content $FilePath | ForEach-Object {
        $line = $_.Trim()
        
        # Skip empty lines and comments
        if ($line -eq "" -or $line.StartsWith("#")) {
            return
        }

        # Parse key=value
        if ($line -match "^([^=]+)=(.*)$") {
            $key = $matches[1].Trim()
            $value = $matches[2].Trim()
            
            # Remove quotes if present
            $value = $value -replace '^"(.*)"$', '$1'
            $value = $value -replace "^'(.*)'$", '$1'
            
            # Set environment variable
            [Environment]::SetEnvironmentVariable($key, $value, "Process")
            
            if ($Verbose) {
                Write-Host "  Loaded: $key" -ForegroundColor Gray
            }
        }
    }
}

function Get-EnvVariable {
    param(
        [string]$Name,
        [bool]$Required = $true
    )
    
    $value = [Environment]::GetEnvironmentVariable($Name, "Process")
    
    if ($Required -and [string]::IsNullOrWhiteSpace($value)) {
        throw "Required environment variable not set: $Name"
    }
    
    return $value
}

# ===================================
# Main Deployment Script
# ===================================

Write-Host "====================================" -ForegroundColor Cyan
Write-Host "  BaseAPI Deployment Script" -ForegroundColor Cyan
Write-Host "====================================" -ForegroundColor Cyan

try {
    # Load environment variables
    Write-Step "Loading deployment configuration..."
    
    # Try .env.production first, then fall back to .env.deployment
    $envFile = if (Test-Path ".env.production") {
        ".env.production"
    } elseif (Test-Path ".env.deployment") {
        ".env.deployment"
    } else {
        $null
    }
    
    if ($envFile) {
        Load-EnvFile $envFile
        Write-Success "Configuration loaded from $envFile"
    } else {
        Write-Error-Message "No deployment configuration found!"
        Write-Host ""
        Write-Host "Please create deployment configuration:" -ForegroundColor Yellow
        Write-Host "  Option 1 (Recommended): Run .\setup-env.ps1" -ForegroundColor White
        Write-Host "  Option 2: Copy .env.deployment.example to .env.production" -ForegroundColor White
        Write-Host "  Option 3: Copy .env.deployment.example to .env.deployment" -ForegroundColor White
        exit 1
    }

    # Get configuration variables
    $SSH_USER = Get-EnvVariable "SSH_USER"
    $SSH_HOST = Get-EnvVariable "SSH_HOST"
    $SSH_PORT = Get-EnvVariable "SSH_PORT" $false
    $SERVER_TEMP_PATH = Get-EnvVariable "SERVER_TEMP_PATH"
    $SERVER_APP_PATH = Get-EnvVariable "SERVER_APP_PATH"
    $SERVICE_NAME = Get-EnvVariable "SERVICE_NAME"
    $APP_ZIP_NAME = Get-EnvVariable "APP_ZIP_NAME"
    $LOCAL_PUBLISH_PATH = Get-EnvVariable "LOCAL_PUBLISH_PATH"
    $SSH_KEY_PATH = Get-EnvVariable "SSH_KEY_PATH" $false

    if ([string]::IsNullOrWhiteSpace($SSH_PORT)) {
        $SSH_PORT = "22"
    }

    # Display deployment info
    Write-Host ""
    Write-Host "Deployment Target:" -ForegroundColor Cyan
    Write-Host "  Server: ${SSH_USER}@${SSH_HOST}:${SSH_PORT}" -ForegroundColor White
    Write-Host "  Service: $SERVICE_NAME" -ForegroundColor White
    Write-Host "  Path: $SERVER_APP_PATH" -ForegroundColor White
    Write-Host ""

    # Confirmation
    $confirm = Read-Host "Proceed with deployment? (y/n)"
    if ($confirm -ne "y") {
        Write-Warning-Message "Deployment cancelled by user"
        exit 0
    }

    # Note: Production configuration should be managed via environment variables on the server
    # Run setup-server-env.sh on the Ubuntu server to configure production settings securely
    Write-Host ""
    Write-Host "Note: Production database configuration should be set on the server using:" -ForegroundColor Yellow
    Write-Host "  sudo ./setup-server-env.sh" -ForegroundColor White
    Write-Host ""

    # Clean previous publish
    Write-Step "Cleaning previous build artifacts..."
    if (Test-Path $LOCAL_PUBLISH_PATH) {
        Remove-Item -Path $LOCAL_PUBLISH_PATH -Recurse -Force
        Write-Success "Cleaned $LOCAL_PUBLISH_PATH"
    }
    if (Test-Path $APP_ZIP_NAME) {
        Remove-Item -Path $APP_ZIP_NAME -Force
        Write-Success "Removed old $APP_ZIP_NAME"
    }

    # Build and publish
    if (-not $SkipBuild) {
        Write-Step "Building and publishing application..."
        
        if (-not $SkipTests) {
            Write-Host "  Running tests..." -ForegroundColor Gray
            dotnet test
            if ($LASTEXITCODE -ne 0) {
                throw "Tests failed. Use -SkipTests to bypass."
            }
            Write-Success "Tests passed"
        }

        Write-Host "  Publishing Release build..." -ForegroundColor Gray
        dotnet publish -c Release -o $LOCAL_PUBLISH_PATH
        
        if ($LASTEXITCODE -ne 0) {
            throw "Build failed"
        }
        
        Write-Success "Application published to $LOCAL_PUBLISH_PATH"
    } else {
        Write-Warning-Message "Skipping build (using existing publish folder)"
        if (-not (Test-Path $LOCAL_PUBLISH_PATH)) {
            throw "Publish folder not found: $LOCAL_PUBLISH_PATH"
        }
    }

    # Create zip archive
    Write-Step "Creating deployment package..."
    Compress-Archive -Path "$LOCAL_PUBLISH_PATH/*" -DestinationPath $APP_ZIP_NAME -Force
    
    $zipSize = (Get-Item $APP_ZIP_NAME).Length / 1MB
    Write-Success "Created $APP_ZIP_NAME ($([math]::Round($zipSize, 2)) MB)"

    # Upload to server
    Write-Step "Uploading to server..."
    
    $scpArgs = @()
    if (-not [string]::IsNullOrWhiteSpace($SSH_KEY_PATH)) {
        $scpArgs += @("-i", $SSH_KEY_PATH)
    }
    $scpArgs += @("-P", $SSH_PORT)
    $scpArgs += @($APP_ZIP_NAME, "${SSH_USER}@${SSH_HOST}:${SERVER_TEMP_PATH}/")
    
    Write-Host "  Transferring file..." -ForegroundColor Gray
    & scp @scpArgs
    
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to upload file to server"
    }
    
    Write-Success "File uploaded to $SERVER_TEMP_PATH/$APP_ZIP_NAME"

    # Deploy on server
    Write-Step "Deploying application on server..."
    Write-Host "You will be prompted for sudo password..." -ForegroundColor Gray
    Write-Host ""
    
    Write-Host "  [1/6] Stopping service..." -ForegroundColor Gray
    Write-Host "  [2/6] Removing old files..." -ForegroundColor Gray
    Write-Host "  [3/6] Extracting new files..." -ForegroundColor Gray
    Write-Host "  [4/6] Setting permissions..." -ForegroundColor Gray
    Write-Host "  [5/6] Starting service..." -ForegroundColor Gray
    Write-Host "  [6/6] Cleaning up..." -ForegroundColor Gray
    Write-Host ""
    
    # Create deployment commands (similar to Angular approach with ssh -t)
    $deployCommands = @"
sudo systemctl stop $SERVICE_NAME && \
sudo rm -rf $SERVER_APP_PATH/* && \
sudo unzip -q $SERVER_TEMP_PATH/$APP_ZIP_NAME -d $SERVER_APP_PATH && \
sudo chown -R ${SSH_USER}:${SSH_USER} $SERVER_APP_PATH && \
sudo systemctl start $SERVICE_NAME && \
rm $SERVER_TEMP_PATH/$APP_ZIP_NAME && \
echo 'Deployment complete!' && \
sudo systemctl status $SERVICE_NAME --no-pager -l
"@

    $sshArgs = @("-t")
    if (-not [string]::IsNullOrWhiteSpace($SSH_KEY_PATH)) {
        $sshArgs += @("-i", $SSH_KEY_PATH)
    }
    $sshArgs += @("-p", $SSH_PORT)
    $sshArgs += @("${SSH_USER}@${SSH_HOST}", $deployCommands)
    
    & ssh @sshArgs
    
    if ($LASTEXITCODE -ne 0) {
        Write-Error-Message "Deployment commands failed on server"
        Write-Host ""
        Write-Host "To check logs, run:" -ForegroundColor Yellow
        Write-Host "  ssh ${SSH_USER}@${SSH_HOST} 'sudo journalctl -u $SERVICE_NAME -n 50'" -ForegroundColor White
        exit 1
    }

    # Success
    Write-Host ""
    Write-Host "====================================" -ForegroundColor Green
    Write-Host "  ✅ Deployment Successful!" -ForegroundColor Green
    Write-Host "====================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "Service Status Commands:" -ForegroundColor Cyan
    Write-Host "  Status:  ssh ${SSH_USER}@${SSH_HOST} 'sudo systemctl status $SERVICE_NAME'" -ForegroundColor White
    Write-Host "  Logs:    ssh ${SSH_USER}@${SSH_HOST} 'sudo journalctl -u $SERVICE_NAME -f'" -ForegroundColor White
    Write-Host "  Restart: ssh ${SSH_USER}@${SSH_HOST} 'sudo systemctl restart $SERVICE_NAME'" -ForegroundColor White
    Write-Host ""

} catch {
    Write-Host ""
    Write-Error-Message "Deployment failed: $_"
    Write-Host ""
    Write-Host $_.ScriptStackTrace -ForegroundColor Red
    
    exit 1
}
