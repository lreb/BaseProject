# 🚀 Deployment Guide

This guide explains how to deploy the BaseAPI to an Ubuntu server using the automated deployment script.

## 📋 Prerequisites

### On Your Development Machine (Windows)
- PowerShell 5.1 or later
- .NET SDK installed
- SSH client (OpenSSH - included in Windows 10/11)
- SCP client (included with OpenSSH)

### On Your Ubuntu Server (24.04)
- .NET Runtime installed
- Nginx configured
- Systemd service created for the API
- SSH access with sudo privileges

## 🔧 Initial Setup

### 1. Create Deployment Configuration

Copy the example file and configure it with your server details:

```powershell
Copy-Item .env.deployment.example .env.deployment
```

Edit `.env.deployment` and fill in your values:

```bash
# SSH Connection Details
SSH_USER=chinodev
SSH_HOST=192.168.100.142
SSH_PORT=22

# Server Paths
SERVER_TEMP_PATH=/tmp
SERVER_APP_PATH=/var/www/netcore-baseapi
SERVICE_NAME=netcore-baseapi

# Application Settings
APP_ZIP_NAME=myapi.zip
LOCAL_PUBLISH_PATH=./publish
```

> ⚠️ **IMPORTANT**: `.env.deployment` is in `.gitignore` and will NOT be committed!

### 2. Configure SSH Access (Recommended)

For passwordless deployment, set up SSH key authentication:

```powershell
# Generate SSH key (if you don't have one)
ssh-keygen -t rsa -b 4096

# Copy public key to server
type $env:USERPROFILE\.ssh\id_rsa.pub | ssh chinodev@192.168.100.142 "cat >> ~/.ssh/authorized_keys"
```

Then add to `.env.deployment`:
```bash
SSH_KEY_PATH=~/.ssh/id_rsa
```

### 3. Verify Server Configuration

Ensure your Ubuntu server has the correct setup:

#### Systemd Service (`/etc/systemd/system/netcore-baseapi.service`)

```ini
[Unit]
Description=BaseAPI .NET Core Application
After=network.target

[Service]
WorkingDirectory=/var/www/netcore-baseapi
ExecStart=/usr/bin/dotnet /var/www/netcore-baseapi/BaseAPI.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=netcore-baseapi
User=chinodev
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

#### Nginx Configuration (`/etc/nginx/sites-available/default`)

Your current configuration:
```nginx
server {
    listen 80 default_server;
    listen [::]:80 default_server;

    server_name _;

    location / {
        proxy_pass http://127.0.0.1:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

## 🚀 Deployment

### Basic Deployment

Run the deployment script:

```powershell
.\deploy.ps1
```

This will:
1. ✅ Load configuration from `.env.deployment`
2. 🧪 Run tests
3. 🔨 Build the application in Release mode
4. 📦 Create a zip file
5. 📤 Upload to server
6. 🛑 Stop the service
7. 🗑️ Remove old files
8. 📥 Extract new files
9. 🔐 Set permissions
10. 🚀 Start the service

### Advanced Options

#### Skip Tests
```powershell
.\deploy.ps1 -SkipTests
```

#### Skip Build (use existing publish folder)
```powershell
.\deploy.ps1 -SkipBuild
```

#### Verbose Output
```powershell
.\deploy.ps1 -Verbose
```

#### Combine Options
```powershell
.\deploy.ps1 -SkipTests -Verbose
```

## 🔍 Post-Deployment Verification

### Check Service Status

```powershell
ssh chinodev@192.168.100.142 'sudo systemctl status netcore-baseapi'
```

### View Logs in Real-time

```powershell
ssh chinodev@192.168.100.142 'sudo journalctl -u netcore-baseapi -f'
```

### View Last 50 Log Lines

```powershell
ssh chinodev@192.168.100.142 'sudo journalctl -u netcore-baseapi -n 50'
```

### Test the API

```powershell
# From your local machine
curl http://192.168.100.142/health

# Or using PowerShell
Invoke-RestMethod http://192.168.100.142/health
```

## 🛠️ Troubleshooting

### Deployment Script Fails to Connect

**Problem**: SSH connection timeout or refused

**Solution**:
1. Verify server IP and SSH port
2. Check firewall settings
3. Ensure SSH service is running: `sudo systemctl status ssh`

### Service Fails to Start

**Problem**: Service stops immediately after starting

**Solution**:
1. Check logs: `sudo journalctl -u netcore-baseapi -n 100`
2. Verify .NET runtime is installed: `dotnet --info`
3. Check file permissions: `ls -la /var/www/netcore-baseapi`
4. Verify configuration files are present

### Connection String Issues

**Problem**: Database connection fails

**Solution**:
1. Ensure `appsettings.json` or environment variables have correct connection string
2. For production, consider using environment variables in systemd service:

```ini
[Service]
Environment=ConnectionStrings__DefaultConnection=Host=localhost;Database=BaseApiDb_Prod;Username=...;Password=...
```

### Permission Denied Errors

**Problem**: Cannot write to `/var/www/netcore-baseapi`

**Solution**:
```bash
sudo chown -R chinodev:chinodev /var/www/netcore-baseapi
sudo chmod -R 755 /var/www/netcore-baseapi
```

### Nginx 502 Bad Gateway

**Problem**: Nginx shows 502 error

**Solution**:
1. Verify service is running: `sudo systemctl status netcore-baseapi`
2. Check if app is listening on correct port (5000): `sudo netstat -tlnp | grep 5000`
3. Review nginx error log: `sudo tail -f /var/log/nginx/error.log`

## 📝 Manual Deployment Commands

If you need to deploy manually without the script:

```powershell
# 1. Build and publish
dotnet publish -c Release -o ./publish

# 2. Create zip
Compress-Archive -Path ./publish/* -DestinationPath myapi.zip -Force

# 3. Upload to server
scp myapi.zip chinodev@192.168.100.142:/tmp/

# 4. Deploy on server (run via SSH)
ssh chinodev@192.168.100.142

sudo systemctl stop netcore-baseapi
sudo rm -rf /var/www/netcore-baseapi/*
sudo unzip /tmp/myapi.zip -d /var/www/netcore-baseapi
sudo chown -R chinodev:chinodev /var/www/netcore-baseapi
sudo systemctl start netcore-baseapi
rm /tmp/myapi.zip
```

## 🔒 Security Best Practices

### Environment Variables

1. **Never commit** `.env.deployment` to version control
2. Use SSH key authentication instead of passwords
3. Restrict SSH key permissions:
   ```powershell
   icacls $env:USERPROFILE\.ssh\id_rsa /inheritance:r /grant:r "$($env:USERNAME):(R)"
   ```

### Database Credentials

For production, use environment variables in the systemd service instead of appsettings.json:

```ini
[Service]
Environment=ConnectionStrings__DefaultConnection=Host=...
Environment=ASPNETCORE_ENVIRONMENT=Production
```

### File Permissions

Ensure proper ownership:
```bash
sudo chown -R chinodev:chinodev /var/www/netcore-baseapi
sudo chmod -R 755 /var/www/netcore-baseapi
```

## 🔄 Rollback Procedure

If deployment fails and you need to rollback:

1. Keep a backup of the previous deployment:
   ```bash
   sudo cp -r /var/www/netcore-baseapi /var/www/netcore-baseapi.backup
   ```

2. To restore:
   ```bash
   sudo systemctl stop netcore-baseapi
   sudo rm -rf /var/www/netcore-baseapi/*
   sudo cp -r /var/www/netcore-baseapi.backup/* /var/www/netcore-baseapi/
   sudo systemctl start netcore-baseapi
   ```

## 📊 Useful Server Commands

### Service Management

```bash
# Status
sudo systemctl status netcore-baseapi

# Start
sudo systemctl start netcore-baseapi

# Stop
sudo systemctl stop netcore-baseapi

# Restart
sudo systemctl restart netcore-baseapi

# Reload (without stopping)
sudo systemctl reload netcore-baseapi

# Enable on boot
sudo systemctl enable netcore-baseapi

# Disable on boot
sudo systemctl disable netcore-baseapi
```

### Log Management

```bash
# View logs in real-time
sudo journalctl -u netcore-baseapi -f

# View last 50 lines
sudo journalctl -u netcore-baseapi -n 50

# View logs since today
sudo journalctl -u netcore-baseapi --since today

# View logs with priority error or higher
sudo journalctl -u netcore-baseapi -p err

# Clear old logs (older than 2 weeks)
sudo journalctl --vacuum-time=2weeks
```

### Nginx Commands

```bash
# Test configuration
sudo nginx -t

# Reload configuration
sudo systemctl reload nginx

# View error logs
sudo tail -f /var/log/nginx/error.log

# View access logs
sudo tail -f /var/log/nginx/access.log
```

## 🎯 Quick Reference

### Deploy to Server
```powershell
.\deploy.ps1
```

### Check Service Status
```powershell
ssh chinodev@192.168.100.142 'sudo systemctl status netcore-baseapi'
```

### View Logs
```powershell
ssh chinodev@192.168.100.142 'sudo journalctl -u netcore-baseapi -f'
```

### Restart Service
```powershell
ssh chinodev@192.168.100.142 'sudo systemctl restart netcore-baseapi'
```

## 📞 Support

If you encounter issues:
1. Check the logs: `sudo journalctl -u netcore-baseapi -n 100`
2. Verify service status: `sudo systemctl status netcore-baseapi`
3. Check Nginx logs: `sudo tail -f /var/log/nginx/error.log`
4. Review this troubleshooting guide

---

**Last Updated**: January 29, 2026
