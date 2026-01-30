# ?? Security & Configuration Guide

This guide explains how to securely manage production credentials and configuration for BaseAPI.

## ?? Quick Security Checklist

- [ ] Database credentials stored in `/etc/baseapi/production.env` (not in files committed to Git)
- [ ] Environment file has 600 permissions (owner read/write only)
- [ ] `.env.deployment` is in `.gitignore` and does NOT contain database passwords
- [ ] SSH key-based authentication configured for deployments
- [ ] Connection strings use environment variables, not hardcoded values
- [ ] Production `appsettings.json` does NOT contain connection strings
- [ ] Service user has minimal required permissions
- [ ] Database user has minimal required privileges

## ?? Production Configuration Strategy

### The Problem with Hardcoded Credentials

**? BAD - Common Security Mistakes:**

1. **Storing passwords in version control**:
   ```json
   // appsettings.Production.json (committed to Git)
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Password=MySecretPassword123"
     }
   }
   ```

2. **Embedding passwords in deployment scripts**:
   ```powershell
   # deploy.ps1
   $connectionString = "Host=server;Password=hardcoded"
   ```

3. **Storing passwords in plaintext files on dev machine**:
   ```bash
   # .env.deployment (on Windows machine)
   DB_PASSWORD=ProductionPassword123
   ```

### ? RECOMMENDED: Server-Side Environment Variables

**The Secure Approach:**

1. **Credentials live ONLY on the production server**
2. **Stored in a secure file** with restrictive permissions
3. **Loaded by systemd** at service startup
4. **Never transmitted** over network or stored in Git

## ??? Setup Guide

### Step 1: Upload Setup Script to Server

From your Windows development machine:

```powershell
# Upload the setup script
scp setup-server-env.sh chinodev@192.168.100.142:/tmp/

# Optionally, verify the file was uploaded
ssh chinodev@192.168.100.142 'ls -la /tmp/setup-server-env.sh'
```

### Step 2: Run Setup Script on Ubuntu Server

```bash
# SSH into your server
ssh chinodev@192.168.100.142

# Make the script executable
chmod +x /tmp/setup-server-env.sh

# Run as root (required to create /etc/baseapi/ and modify systemd)
sudo /tmp/setup-server-env.sh
```

### Step 3: Follow Interactive Prompts

The script will guide you through configuration:

```
===================================
  BaseAPI Production Setup
===================================

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
```

### Step 4: Verify Configuration

```bash
# Check environment file was created with correct permissions
ls -la /etc/baseapi/production.env
# Should show: -rw------- 1 root root

# Verify systemd service references the environment file
sudo systemctl cat netcore-baseapi | grep EnvironmentFile
# Should show: EnvironmentFile=/etc/baseapi/production.env

# Check service status
sudo systemctl status netcore-baseapi
```

## ?? Environment File Structure

The script creates `/etc/baseapi/production.env` with this structure:

```bash
# Database Configuration
DB_HOST=localhost
DB_PORT=5432
DB_NAME=BaseApiDb_Prod
DB_USERNAME=baseapi_user
DB_PASSWORD=SecurePassword123!

# Application Configuration
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://localhost:5000
DOTNET_PRINT_TELEMETRY_MESSAGE=false

# Connection String (automatically built)
ConnectionStrings__DefaultConnection=Host=${DB_HOST};Database=${DB_NAME};Username=${DB_USERNAME};Password=${DB_PASSWORD};Port=${DB_PORT};Pooling=true;
```

> ?? **Security Note:** This file has **600 permissions** (owner read/write only) and is owned by **root**.
> Only root and the systemd service can read it.

## ?? How It Works

### Application Configuration Hierarchy

ASP.NET Core loads configuration in this order (later sources override earlier ones):

1. `appsettings.json` (base settings)
2. `appsettings.{Environment}.json` (e.g., `appsettings.Production.json`)
3. **Environment variables** (highest priority) ? This is where our secrets are!
4. Command-line arguments

### Systemd Service Integration

The systemd service file includes:

```ini
[Service]
EnvironmentFile=/etc/baseapi/production.env
```

This directive tells systemd to:
1. Read `/etc/baseapi/production.env` before starting the service
2. Export all variables as environment variables for the process
3. Make them available to the .NET application

### .NET Configuration Access

In your application, access configuration as usual:

```csharp
// Program.cs - No changes needed!
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
```

The `ConnectionStrings__DefaultConnection` environment variable automatically maps to:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "..."
  }
}
```

## ?? Managing Credentials

### Updating Credentials

**Option 1: Re-run the setup script**

```bash
sudo /tmp/setup-server-env.sh
# Enter new credentials when prompted

# Restart service
sudo systemctl restart netcore-baseapi
```

**Option 2: Manual edit (requires root)**

```bash
# Edit the environment file
sudo nano /etc/baseapi/production.env

# Update the connection string manually after changing credentials
# ConnectionStrings__DefaultConnection=Host=...;Password=NEW_PASSWORD;...

# Reload systemd and restart service
sudo systemctl daemon-reload
sudo systemctl restart netcore-baseapi
```

### Rotating Passwords

1. **Change database password**:
   ```sql
   ALTER USER baseapi_user WITH PASSWORD 'NewSecurePassword456!';
   ```

2. **Update environment file**:
   ```bash
   sudo nano /etc/baseapi/production.env
   # Update DB_PASSWORD and ConnectionStrings__DefaultConnection
   ```

3. **Restart service**:
   ```bash
   sudo systemctl restart netcore-baseapi
   ```

### Viewing Current Configuration

```bash
# View environment file (requires root)
sudo cat /etc/baseapi/production.env

# View environment variables of running process
sudo systemctl show netcore-baseapi --property=Environment
```

## ?? What NOT to Do

### ? Don't Commit Credentials to Git

```bash
# Check if sensitive files are tracked
git ls-files | grep -E "\.env\.deployment$|appsettings\.Production\.json$"

# If they show up, remove them
git rm --cached .env.deployment
git rm --cached appsettings.Production.json
git commit -m "Remove sensitive files"
```

### ? Don't Store Passwords in .env.deployment

The `.env.deployment` file on your Windows machine should ONLY contain:
- SSH connection details (host, user, port)
- Server paths
- Service name

**It should NOT contain:**
- Database passwords
- Connection strings
- API keys
- Any other secrets

### ? Don't Email or Message Credentials

Never send production credentials via:
- Email
- Slack/Teams messages
- Text files
- Screenshots

Instead:
- Use SSH to securely connect to server
- Run setup script directly on server
- Keep credentials on server only

## ?? Additional Security Measures

### File Permissions

Verify and set correct permissions:

```bash
# Environment file should be 600
sudo chmod 600 /etc/baseapi/production.env
sudo chown root:root /etc/baseapi/production.env

# Application files should be owned by service user
sudo chown -R chinodev:chinodev /var/www/netcore-baseapi
sudo chmod -R 755 /var/www/netcore-baseapi
```

### Database User Permissions

Create a dedicated database user with minimal privileges:

```sql
-- Create user
CREATE USER baseapi_user WITH PASSWORD 'SecurePassword123!';

-- Grant only required permissions
GRANT CONNECT ON DATABASE BaseApiDb_Prod TO baseapi_user;
GRANT USAGE ON SCHEMA public TO baseapi_user;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO baseapi_user;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO baseapi_user;

-- Don't grant: SUPERUSER, CREATEDB, CREATEROLE, REPLICATION
```

### Firewall Configuration

Restrict database access:

```bash
# Allow only local connections to PostgreSQL
sudo ufw allow from 127.0.0.1 to any port 5432
sudo ufw deny 5432

# Or allow only from specific IPs
sudo ufw allow from 192.168.100.0/24 to any port 5432
```

### Service User Isolation

Run the service under a dedicated user (not root):

```bash
# Create dedicated user
sudo useradd -r -s /bin/false baseapi

# Update service file
sudo nano /etc/systemd/system/netcore-baseapi.service
# Change: User=baseapi

# Update file ownership
sudo chown -R baseapi:baseapi /var/www/netcore-baseapi
```

## ?? Verification & Testing

### Test Environment Variables

```bash
# Check if environment file is loaded by service
sudo systemctl show netcore-baseapi --property=Environment

# Test connection string manually
sudo -u chinodev bash -c 'source /etc/baseapi/production.env && echo $ConnectionStrings__DefaultConnection'
```

### Test Database Connection

```bash
# Install PostgreSQL client
sudo apt-get install postgresql-client

# Test connection
psql -h localhost -U baseapi_user -d BaseApiDb_Prod
# Enter password when prompted
```

### Test API

```bash
# Health check endpoint
curl http://localhost:5000/health

# Check database connectivity
curl http://localhost:5000/health | jq .
```

## ?? Troubleshooting

### Problem: Service can't connect to database

```bash
# Check environment file exists
ls -la /etc/baseapi/production.env

# Verify systemd references it
sudo systemctl cat netcore-baseapi | grep EnvironmentFile

# Check service logs for connection errors
sudo journalctl -u netcore-baseapi -n 100 | grep -i "connection"
```

### Problem: Environment variables not loaded

```bash
# Reload systemd after changes
sudo systemctl daemon-reload

# Restart service
sudo systemctl restart netcore-baseapi

# Verify status
sudo systemctl status netcore-baseapi
```

### Problem: Permission denied reading environment file

```bash
# Ensure correct ownership and permissions
sudo chown root:root /etc/baseapi/production.env
sudo chmod 600 /etc/baseapi/production.env

# Systemd runs as root initially, so it can read the file
# Then drops privileges to the service user
```

## ?? Additional Resources

### Development vs Production Configuration

| Environment | Configuration Location | Security Level |
|-------------|----------------------|----------------|
| **Development** | `appsettings.Development.json` or `appsettings.Local.json` | Low (local only) |
| **Staging** | Environment variables via systemd | Medium |
| **Production** | `/etc/baseapi/production.env` via systemd | High (600 permissions) |

### Configuration Methods Comparison

| Method | Security | Ease of Rotation | Recommended |
|--------|----------|------------------|-------------|
| Hardcoded in code | ? Very Low | ? Requires redeploy | Never |
| appsettings.json | ? Low | ? Requires redeploy | Development only |
| Environment variables | ? High | ? Service restart only | **YES** |
| Azure Key Vault / HashiCorp Vault | ? Very High | ? Automatic rotation | Enterprise |

### Environment Variable Naming Convention

ASP.NET Core uses double underscores (`__`) to represent nested configuration:

```bash
# Environment variable
ConnectionStrings__DefaultConnection=...

# Maps to JSON
{
  "ConnectionStrings": {
    "DefaultConnection": "..."
  }
}
```

More examples:

```bash
# Environment variables
Logging__LogLevel__Default=Warning
Logging__LogLevel__Microsoft=Error
ApiSettings__MaxRequestSize=10485760

# Maps to JSON
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft": "Error"
    }
  },
  "ApiSettings": {
    "MaxRequestSize": 10485760
  }
}
```

## ? Security Checklist

Use this checklist for each deployment:

- [ ] `.env.deployment` does NOT contain database passwords
- [ ] `.env.deployment` is in `.gitignore`
- [ ] `appsettings.Production.json` does NOT exist in the repository
- [ ] Server environment file (`/etc/baseapi/production.env`) exists with 600 permissions
- [ ] Systemd service has `EnvironmentFile=/etc/baseapi/production.env`
- [ ] SSH key-based authentication is configured
- [ ] Database user has minimal required privileges
- [ ] Firewall rules restrict database access
- [ ] Service runs under dedicated non-root user
- [ ] Logs don't expose sensitive information
- [ ] Credentials are not in version control history (`git log --all --full-history --source --decorate -- "*appsettings.Production.json"`)

---

**Built with ?? Security Best Practices**

**For questions or issues, create an issue in the repository.**
