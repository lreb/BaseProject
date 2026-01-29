# 🔧 Ubuntu Server Setup Guide

This guide covers the initial setup required on your Ubuntu Server 24.04 for automated deployments.

## 📋 Prerequisites

- Ubuntu Server 24.04
- User account with sudo access (e.g., `chinodev`)
- SSH access enabled

## 1️⃣ Install Required Packages

```bash
# Update package list
sudo apt update

# Install .NET Runtime (for running the API)
sudo apt install -y dotnet-runtime-8.0

# Install Nginx (reverse proxy)
sudo apt install -y nginx

# Install unzip (for extracting deployment packages)
sudo apt install -y unzip
```

## 2️⃣ Configure Passwordless Sudo for Deployment

To allow automated deployments without password prompts, configure passwordless sudo for specific commands:

```bash
# Edit sudoers file
sudo visudo -f /etc/sudoers.d/deployment
```

Add these lines (replace `chinodev` with your username):

```
# Allow passwordless sudo for deployment commands
chinodev ALL=(ALL) NOPASSWD: /bin/systemctl start netcore-baseapi
chinodev ALL=(ALL) NOPASSWD: /bin/systemctl stop netcore-baseapi
chinodev ALL=(ALL) NOPASSWD: /bin/systemctl restart netcore-baseapi
chinodev ALL=(ALL) NOPASSWD: /bin/systemctl status netcore-baseapi
chinodev ALL=(ALL) NOPASSWD: /bin/rm -rf /var/www/netcore-baseapi/*
chinodev ALL=(ALL) NOPASSWD: /usr/bin/unzip * -d /var/www/netcore-baseapi
chinodev ALL=(ALL) NOPASSWD: /bin/chown -R chinodev\:chinodev /var/www/netcore-baseapi
```

Save and exit (Ctrl+X, then Y, then Enter).

**Verify the configuration:**
```bash
# Test without password
sudo systemctl status netcore-baseapi
```

## 3️⃣ Create Application Directory

```bash
# Create application directory
sudo mkdir -p /var/www/netcore-baseapi

# Set ownership
sudo chown -R chinodev:chinodev /var/www/netcore-baseapi

# Set permissions
sudo chmod -R 755 /var/www/netcore-baseapi
```

## 4️⃣ Create Systemd Service

Create the service file:

```bash
sudo nano /etc/systemd/system/netcore-baseapi.service
```

Add this configuration:

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

# Optional: Set connection string as environment variable
# Environment=ConnectionStrings__DefaultConnection=Host=localhost;Database=BaseApiDb_Prod;...

[Install]
WantedBy=multi-user.target
```

**Enable and start the service:**

```bash
# Reload systemd daemon
sudo systemctl daemon-reload

# Enable service on boot
sudo systemctl enable netcore-baseapi

# Start service (will fail until first deployment)
sudo systemctl start netcore-baseapi

# Check status
sudo systemctl status netcore-baseapi
```

## 5️⃣ Configure Nginx

Edit the default site configuration:

```bash
sudo nano /etc/nginx/sites-available/default
```

Replace with:

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

**Test and reload Nginx:**

```bash
# Test configuration
sudo nginx -t

# Reload Nginx
sudo systemctl reload nginx

# Enable Nginx on boot
sudo systemctl enable nginx
```

## 6️⃣ Configure Firewall (Optional but Recommended)

```bash
# Allow SSH
sudo ufw allow 22/tcp

# Allow HTTP
sudo ufw allow 80/tcp

# Allow HTTPS (if using SSL later)
sudo ufw allow 443/tcp

# Enable firewall
sudo ufw enable

# Check status
sudo ufw status
```

## 7️⃣ Set Up SSH Key Authentication (Recommended)

On your **Windows machine**, generate an SSH key if you don't have one:

```powershell
# Generate SSH key
ssh-keygen -t rsa -b 4096 -f $env:USERPROFILE\.ssh\id_rsa
```

Copy your public key to the server:

```powershell
# Copy public key to server
type $env:USERPROFILE\.ssh\id_rsa.pub | ssh chinodev@192.168.100.142 "mkdir -p ~/.ssh && cat >> ~/.ssh/authorized_keys"
```

On the **Ubuntu server**, set correct permissions:

```bash
chmod 700 ~/.ssh
chmod 600 ~/.ssh/authorized_keys
```

**Test SSH key authentication:**

```powershell
ssh chinodev@192.168.100.142
```

You should connect without entering a password.

**Update `.env.deployment`:**

```bash
SSH_KEY_PATH=C:\Users\YourUsername\.ssh\id_rsa
```

## 8️⃣ Database Setup (If Using PostgreSQL)

```bash
# Install PostgreSQL
sudo apt install -y postgresql postgresql-contrib

# Start PostgreSQL
sudo systemctl start postgresql
sudo systemctl enable postgresql

# Create database and user
sudo -u postgres psql

# In PostgreSQL prompt:
CREATE DATABASE "BaseApiDb_Prod";
CREATE USER produser WITH PASSWORD 'your_secure_password';
GRANT ALL PRIVILEGES ON DATABASE "BaseApiDb_Prod" TO produser;
\q
```

## 9️⃣ Verify Setup

Run these commands to verify everything is configured correctly:

```bash
# Check .NET is installed
dotnet --info

# Check Nginx is running
sudo systemctl status nginx

# Check service can be controlled without password
sudo systemctl status netcore-baseapi

# Check directory exists and has correct permissions
ls -la /var/www/netcore-baseapi

# Check unzip is installed
which unzip
```

## 🎯 Quick Setup Script

For a fresh Ubuntu 24.04 server, you can run this complete setup script:

```bash
#!/bin/bash

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

echo -e "${GREEN}=== BaseAPI Server Setup ===${NC}"

# Variables (modify these)
USERNAME="chinodev"
APP_DIR="/var/www/netcore-baseapi"
SERVICE_NAME="netcore-baseapi"

# Update system
echo -e "${YELLOW}Updating system...${NC}"
sudo apt update && sudo apt upgrade -y

# Install required packages
echo -e "${YELLOW}Installing required packages...${NC}"
sudo apt install -y dotnet-runtime-8.0 nginx unzip

# Create application directory
echo -e "${YELLOW}Creating application directory...${NC}"
sudo mkdir -p $APP_DIR
sudo chown -R $USERNAME:$USERNAME $APP_DIR
sudo chmod -R 755 $APP_DIR

# Configure passwordless sudo
echo -e "${YELLOW}Configuring passwordless sudo...${NC}"
sudo tee /etc/sudoers.d/deployment > /dev/null <<EOF
# Allow passwordless sudo for deployment commands
$USERNAME ALL=(ALL) NOPASSWD: /bin/systemctl start $SERVICE_NAME
$USERNAME ALL=(ALL) NOPASSWD: /bin/systemctl stop $SERVICE_NAME
$USERNAME ALL=(ALL) NOPASSWD: /bin/systemctl restart $SERVICE_NAME
$USERNAME ALL=(ALL) NOPASSWD: /bin/systemctl status $SERVICE_NAME
$USERNAME ALL=(ALL) NOPASSWD: /bin/rm -rf $APP_DIR/*
$USERNAME ALL=(ALL) NOPASSWD: /usr/bin/unzip * -d $APP_DIR
$USERNAME ALL=(ALL) NOPASSWD: /bin/chown -R $USERNAME\\:$USERNAME $APP_DIR
EOF

# Create systemd service
echo -e "${YELLOW}Creating systemd service...${NC}"
sudo tee /etc/systemd/system/$SERVICE_NAME.service > /dev/null <<EOF
[Unit]
Description=BaseAPI .NET Core Application
After=network.target

[Service]
WorkingDirectory=$APP_DIR
ExecStart=/usr/bin/dotnet $APP_DIR/BaseAPI.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=$SERVICE_NAME
User=$USERNAME
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
EOF

sudo systemctl daemon-reload
sudo systemctl enable $SERVICE_NAME

# Configure Nginx
echo -e "${YELLOW}Configuring Nginx...${NC}"
sudo tee /etc/nginx/sites-available/default > /dev/null <<'EOF'
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
EOF

sudo nginx -t && sudo systemctl reload nginx

echo -e "${GREEN}=== Setup Complete! ===${NC}"
echo -e "${YELLOW}Next steps:${NC}"
echo -e "1. Set up SSH key authentication from your development machine"
echo -e "2. Configure .env.deployment on your development machine"
echo -e "3. Run ./deploy.ps1 to deploy your application"
echo ""
echo -e "${GREEN}Verification commands:${NC}"
echo -e "  dotnet --info"
echo -e "  sudo systemctl status nginx"
echo -e "  sudo systemctl status $SERVICE_NAME"
echo -e "  ls -la $APP_DIR"
```

Save as `setup-server.sh` and run:

```bash
chmod +x setup-server.sh
./setup-server.sh
```

## ✅ Verification Checklist

- [ ] .NET Runtime installed
- [ ] Nginx installed and configured
- [ ] Unzip installed
- [ ] Application directory created with correct permissions
- [ ] Systemd service created and enabled
- [ ] Passwordless sudo configured for deployment commands
- [ ] SSH key authentication working
- [ ] Firewall configured (if applicable)
- [ ] Database setup (if using PostgreSQL)

---

**Now you're ready to deploy!** Go back to your Windows machine and run:

```powershell
.\deploy.ps1
```
