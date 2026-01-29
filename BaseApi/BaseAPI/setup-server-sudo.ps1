# Quick Server Setup Script
# Run this from your Windows machine to configure passwordless sudo on Ubuntu server

param(
    [string]$SSHUser = "chinodev",
    [string]$SSHHost = "192.168.100.142"
)

$ErrorActionPreference = "Stop"

Write-Host "====================================`n" -ForegroundColor Cyan
Write-Host "  Configuring Ubuntu Server for Deployment`n" -ForegroundColor Cyan
Write-Host "====================================`n" -ForegroundColor Cyan

Write-Host "Target: $SSHUser@$SSHHost`n" -ForegroundColor Yellow

# Create sudoers configuration with Unix line endings (LF only)
$sudoersLines = @(
    "# Allow passwordless sudo for deployment commands",
    "chinodev ALL=(ALL) NOPASSWD: /bin/systemctl start netcore-baseapi",
    "chinodev ALL=(ALL) NOPASSWD: /bin/systemctl stop netcore-baseapi",
    "chinodev ALL=(ALL) NOPASSWD: /bin/systemctl restart netcore-baseapi",
    "chinodev ALL=(ALL) NOPASSWD: /bin/systemctl status netcore-baseapi",
    "chinodev ALL=(ALL) NOPASSWD: /bin/rm -rf /var/www/netcore-baseapi/*",
    "chinodev ALL=(ALL) NOPASSWD: /usr/bin/unzip * -d /var/www/netcore-baseapi",
    "chinodev ALL=(ALL) NOPASSWD: /bin/chown -R chinodev\:chinodev /var/www/netcore-baseapi"
)

# Join with LF only (Unix line endings)
$sudoersContent = $sudoersLines -join "`n"

# Save to temporary file with Unix line endings
$tempFile = [System.IO.Path]::GetTempFileName()
$utf8NoBom = New-Object System.Text.UTF8Encoding $false
[System.IO.File]::WriteAllText($tempFile, $sudoersContent + "`n", $utf8NoBom)

Write-Host "Uploading sudoers configuration..." -ForegroundColor Yellow
scp $tempFile "${SSHUser}@${SSHHost}:/tmp/deployment-sudoers"

if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to upload configuration file`n" -ForegroundColor Red
    Remove-Item $tempFile
    exit 1
}

Write-Host "Installing sudoers configuration..." -ForegroundColor Yellow
Write-Host "(You will be prompted for your sudo password one time)`n" -ForegroundColor Gray

$setupCommands = @"
sudo mv /tmp/deployment-sudoers /etc/sudoers.d/deployment && \
sudo chown root:root /etc/sudoers.d/deployment && \
sudo chmod 440 /etc/sudoers.d/deployment && \
echo 'Configuration installed successfully!' && \
sudo visudo -c -f /etc/sudoers.d/deployment
"@

ssh -t "${SSHUser}@${SSHHost}" $setupCommands

if ($LASTEXITCODE -eq 0) {
    Write-Host "`n====================================`n" -ForegroundColor Green
    Write-Host "  Setup Complete!`n" -ForegroundColor Green
    Write-Host "====================================`n" -ForegroundColor Green
    Write-Host "You can now run: .\deploy.ps1`n" -ForegroundColor White
} else {
    Write-Host "`nSetup failed. Please check the error messages above.`n" -ForegroundColor Red
}

# Clean up
Remove-Item $tempFile
