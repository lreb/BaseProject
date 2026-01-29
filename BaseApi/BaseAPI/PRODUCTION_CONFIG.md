# Production Configuration Guide

## Overview

The deployment script now automatically generates `appsettings.Production.json` from environment variables stored in `.env.deployment`. This ensures database credentials are never committed to source control.

## How It Works

### 1. Configure Database Credentials

Edit [.env.deployment](.env.deployment) and set your production database credentials:

```bash
# Production Database Configuration
DB_HOST=192.168.100.142
DB_PORT=5432
DB_NAME=BaseApiDb_Prod
DB_USERNAME=produser
DB_PASSWORD=your_secure_password
```

### 2. Deployment Process

When you run `.\deploy.ps1`, it will:

1. **Load** credentials from `.env.deployment`
2. **Generate** `appsettings.Production.json` with the connection string
3. **Build** the project in Release mode (includes the generated appsettings)
4. **Deploy** to the server
5. **Clean up** the temporary `appsettings.Production.json` file

### 3. Server Environment

The systemd service on the server should set the environment to Production:

```ini
[Service]
Environment=ASPNETCORE_ENVIRONMENT=Production
```

This ensures the app uses `appsettings.Production.json`.

## Security Best Practices

### ✅ What IS Committed
- `appsettings.json` (base configuration)
- `appsettings.Development.json.template` (template only)
- `appsettings.Production.json.template` (template only)
- `.env.deployment.example` (example file)
- All deployment scripts

### ❌ What is NOT Committed
- `appsettings.Production.json` (generated during deployment)
- `appsettings.Development.json` (contains local DB credentials)
- `appsettings.Local.json` (local overrides)
- `.env.deployment` (contains production credentials)

## Configuration Hierarchy

The app loads configuration in this order:

1. `appsettings.json` - Base settings
2. `appsettings.{Environment}.json` - Environment-specific settings
3. Environment variables - Override from systemd service
4. Command-line arguments - Highest priority

## Example: Complete Deployment Flow

```powershell
# 1. Ensure .env.deployment has correct database credentials
notepad .env.deployment

# 2. Run deployment
.\deploy.ps1

# Output:
# ===> Generating production configuration...
# ✅ Created appsettings.Production.json
# ===> Cleaning previous build artifacts...
# ===> Building and publishing application...
# ... (builds with appsettings.Production.json included)
# ===> Deploying application on server...
# ✅ Deployment Successful!
# (appsettings.Production.json is automatically cleaned up)
```

## Verify Production Configuration

After deployment, check the deployed configuration on the server:

```powershell
# SSH to server
ssh chinodev@192.168.100.142

# Check what configuration files are deployed
ls -la /var/www/netcore-baseapi/*.json

# You should see:
# appsettings.json
# appsettings.Production.json
# (appsettings.Development.json should NOT be here)
```

## Testing Database Connection

Test the database connection on the server:

```powershell
# Check app logs for connection issues
ssh chinodev@192.168.100.142 'sudo journalctl -u netcore-baseapi -n 50'

# Look for:
# ✅ "Application started successfully"
# ❌ "Failed to connect to database"
```

## Updating Database Credentials

If you need to change database credentials:

```powershell
# 1. Update .env.deployment
notepad .env.deployment

# 2. Redeploy (generates new appsettings.Production.json)
.\deploy.ps1 -SkipTests

# 3. Restart service on server
ssh chinodev@192.168.100.142 'sudo systemctl restart netcore-baseapi'
```

## Alternative: Using Environment Variables Only

If you prefer not to include database credentials in the deployed files, you can configure them directly in the systemd service:

```ini
[Service]
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ConnectionStrings__DefaultConnection="Host=localhost;Database=BaseApiDb_Prod;Username=produser;Password=secure_pass;Port=5432;Pooling=true;"
```

Then reload the service:
```bash
sudo systemctl daemon-reload
sudo systemctl restart netcore-baseapi
```

---

**Security Reminder**: Never commit `.env.deployment` or any file containing production credentials!
