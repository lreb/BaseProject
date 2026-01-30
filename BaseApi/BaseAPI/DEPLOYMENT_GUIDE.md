# 🚀 Deployment Guide

This guide explains how to deploy the BaseAPI to an Ubuntu server using the automated deployment script.

> 📋 **Quick Reference**: For a condensed deployment checklist, see [DEPLOYMENT_QUICK_REFERENCE.md](DEPLOYMENT_QUICK_REFERENCE.md)
> 🔒 **Security**: For credential management best practices, see [SECURITY_GUIDE.md](SECURITY_GUIDE.md)

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

### 1. Configure Ubuntu Server Environment Variables (RECOMMENDED)

For production deployments, database credentials and sensitive configuration should be stored securely on the server using environment variables, not in deployment files.

**On your Ubuntu server**, upload and run the setup script:

```bash
# Upload the setup script (from your dev machine)
scp setup-server-env.sh chinodev@192.168.100.142:/tmp/

# SSH into the server
ssh chinodev@192.168.100.142

# Make the script executable
chmod +x /tmp/setup-server-env.sh

# Run the setup script
sudo /tmp/setup-server-env.sh
```

The script will:
1. ✅ Prompt for database credentials securely
2. ✅ Create `/etc/baseapi/production.env` with secure permissions (600)
3. ✅ Update the systemd service to load environment variables
4. ✅ Build the connection string automatically
5. ✅ Offer to restart the service

**Example Setup Session:**
```
===================================
  BaseAPI Production Setup
===================================

===> Creating secure environment directory...
✅ Directory created

===> Database Configuration
  Database Host [localhost]: localhost
  Database Port [5432]: 5432
  Database Name [BaseApiDb_Prod]: BaseApiDb_Prod
  Database Username [baseapi_user]: baseapi_user
  Database Password: ********
  Confirm password: ********

===> Application Configuration
  Environment [Production]: Production
  Application URLs [http://localhost:5000]: http://localhost:5000

===> Creating environment file...
✅ Environment file created at /etc/baseapi/production.env

===> Updating systemd service...
✅ Added EnvironmentFile to service configuration

===> Reloading systemd daemon...
✅ Systemd daemon reloaded
```

> 🔒 **Security Benefits:**
> - Credentials stored in `/etc/baseapi/production.env` with 600 permissions (root only)
> - Never transmitted over network or stored in version control
> - Systemd service automatically loads variables at startup
> - Easy to rotate credentials without redeployment

### 2. Create Deployment Configuration (Windows Dev Machine)

Copy the example file and configure it with your server details:

```powershell
Copy-Item .env.deployment.example .env.deployment
```

Edit `.env.deployment` with your SSH connection details:

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
APP_ZIP_NAME=net-api.zip
LOCAL_PUBLISH_PATH=./publish
```

> ⚠️ **IMPORTANT**: `.env.deployment` is in `.gitignore` and will NOT be committed!
> ⚠️ **DO NOT** put database credentials in this file - they should be on the server only!

### 3. Configure SSH Access (Recommended)

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

### 4. Verify Server Configuration

Ensure your Ubuntu server has the correct setup:

#### Systemd Service (`/etc/systemd/system/netcore-baseapi.service`)

**Updated configuration with environment file:**

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
EnvironmentFile=/etc/baseapi/production.env

[Install]
WantedBy=multi-user.target
```

> 📝 **Note:** The `EnvironmentFile` directive loads all variables from `/etc/baseapi/production.env`
> automatically when the service starts. This includes the database connection string.

**Legacy Configuration (Not Recommended - Less Secure):**

If you prefer to set environment variables directly in the service file (not recommended for production):

```ini
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
Environment=ConnectionStrings__DefaultConnection=Host=localhost;Database=BaseApiDb_Prod;Username=...;Password=...
```

⚠️ **Warning:** This exposes credentials to anyone who can read the service file!

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

1. **Verify environment variables are loaded** (most common issue):
   ```bash
   # Check the environment file exists
   sudo cat /etc/baseapi/production.env
   
   # Verify systemd service references the environment file
   sudo systemctl cat netcore-baseapi | grep EnvironmentFile
   
   # Restart service after any changes
   sudo systemctl daemon-reload
   sudo systemctl restart netcore-baseapi
   ```

2. **Test database connection manually**:
   ```bash
   # Install PostgreSQL client if not already installed
   sudo apt-get install postgresql-client
   
   # Test connection using credentials from environment file
   psql -h localhost -U baseapi_user -d BaseApiDb_Prod
   ```

3. **Check logs for connection errors**:
   ```bash
   sudo journalctl -u netcore-baseapi -n 100 | grep -i "connection"
   ```

4. **Update credentials** (if needed):
   ```bash
   # Re-run the setup script to update credentials
   sudo /tmp/setup-server-env.sh
   
   # Or manually edit (requires root)
   sudo nano /etc/baseapi/production.env
   
   # After editing, reload and restart
   sudo systemctl daemon-reload
   sudo systemctl restart netcore-baseapi
   ```

5. **Alternative: Use appsettings.Production.json** (less secure):
   
   Create `/var/www/netcore-baseapi/appsettings.Production.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=BaseApiDb_Prod;Username=...;Password=..."
     }
   }
   ```
   
   ⚠️ **Warning:** This stores credentials in a file that may be readable by the service user.

## 🔒 Security Best Practices

### Environment Variables (RECOMMENDED)

1. **Use the server-side setup script**:
   ```bash
   sudo ./setup-server-env.sh
   ```
   
   This ensures:
   - ✅ Credentials stored in `/etc/baseapi/production.env` with 600 permissions
   - ✅ Only root can read the environment file
   - ✅ Service automatically loads variables at startup
   - ✅ No credentials in version control or deployment files

2. **Never commit** `.env.deployment` or any file with credentials to version control

3. **Verify file permissions**:
   ```bash
   # Environment file should be 600 (rw-------)
   ls -la /etc/baseapi/production.env
   # Output: -rw------- 1 root root ... /etc/baseapi/production.env
   ```

4. **Rotate credentials regularly**:
   ```bash
   # Re-run setup script with new credentials
   sudo ./setup-server-env.sh
   
   # Restart service
   sudo systemctl restart netcore-baseapi
   ```

### SSH Access

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

Restrict SSH key permissions:
```powershell
icacls $env:USERPROFILE\.ssh\id_rsa /inheritance:r /grant:r "$($env:USERNAME):(R)"
```

### Database Credentials

**✅ RECOMMENDED: Server-side environment variables**
- Stored in `/etc/baseapi/production.env` with 600 permissions
- Loaded by systemd service via `EnvironmentFile` directive
- Never transmitted over network

**❌ NOT RECOMMENDED: Embedded in deployment files**
- `.env.deployment` - Should NOT contain database credentials
- `appsettings.Production.json` - Should NOT be generated during deployment
- Connection strings - Should NOT be in version control
