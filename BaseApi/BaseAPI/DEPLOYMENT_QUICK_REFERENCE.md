# ?? Production Deployment Quick Reference

## ?? First-Time Setup (One-Time Tasks)

### 1. Configure Ubuntu Server (5 minutes)

```bash
# On Windows dev machine - upload setup script
scp setup-server-env.sh chinodev@192.168.100.142:/tmp/

# SSH into server
ssh chinodev@192.168.100.142

# Run setup script
chmod +x /tmp/setup-server-env.sh
sudo /tmp/setup-server-env.sh
```

**You'll be prompted for:**
- Database host (default: localhost)
- Database port (default: 5432)
- Database name
- Database username
- Database password (entered securely, not displayed)

### 2. Configure Deployment Settings (2 minutes)

```powershell
# On Windows dev machine
Copy-Item .env.deployment.example .env.deployment
notepad .env.deployment
```

**Edit with your values:**
```bash
SSH_USER=chinodev
SSH_HOST=192.168.100.142
SSH_PORT=22
SERVER_APP_PATH=/var/www/netcore-baseapi
SERVICE_NAME=netcore-baseapi
```

**DO NOT add database credentials here!**

### 3. Setup SSH Keys (Optional but Recommended)

```powershell
# Generate SSH key
ssh-keygen -t rsa -b 4096

# Copy to server
type $env:USERPROFILE\.ssh\id_rsa.pub | ssh chinodev@192.168.100.142 "cat >> ~/.ssh/authorized_keys"

# Add to .env.deployment
# SSH_KEY_PATH=~/.ssh/id_rsa
```

## ?? Regular Deployment (2-3 minutes)

### Deploy to Production

```powershell
# From BaseAPI directory
.\deploy.ps1
```

That's it! The script will:
1. ? Load configuration
2. ?? Run tests
3. ?? Build Release version
4. ?? Package as ZIP
5. ?? Upload to server
6. ?? Stop service, deploy, restart

### Deploy Options

```powershell
# Skip tests (faster)
.\deploy.ps1 -SkipTests

# Skip build (use existing publish folder)
.\deploy.ps1 -SkipBuild

# Verbose output
.\deploy.ps1 -Verbose

# Combine options
.\deploy.ps1 -SkipTests -Verbose
```

## ?? Verification Commands

### Check Service Status

```powershell
ssh chinodev@192.168.100.142 'sudo systemctl status netcore-baseapi'
```

### View Logs

```powershell
# Real-time logs
ssh chinodev@192.168.100.142 'sudo journalctl -u netcore-baseapi -f'

# Last 50 lines
ssh chinodev@192.168.100.142 'sudo journalctl -u netcore-baseapi -n 50'
```

### Test API

```powershell
# Health check
Invoke-RestMethod http://192.168.100.142/health

# Or with curl
curl http://192.168.100.142/health
```

## ??? Common Management Tasks

### Restart Service

```powershell
ssh chinodev@192.168.100.142 'sudo systemctl restart netcore-baseapi'
```

### Update Database Credentials

```bash
# On server
sudo /tmp/setup-server-env.sh
sudo systemctl restart netcore-baseapi
```

### View Environment Configuration

```bash
# On server (requires root)
sudo cat /etc/baseapi/production.env
```

### Check Service Configuration

```bash
# On server
sudo systemctl cat netcore-baseapi
```

## ?? Troubleshooting

### Service Won't Start

```bash
# Check logs
sudo journalctl -u netcore-baseapi -n 100

# Check service status
sudo systemctl status netcore-baseapi

# Verify files exist
ls -la /var/www/netcore-baseapi/
```

### Connection String Issues

```bash
# Verify environment file
sudo cat /etc/baseapi/production.env

# Check systemd references it
sudo systemctl cat netcore-baseapi | grep EnvironmentFile

# Reload and restart
sudo systemctl daemon-reload
sudo systemctl restart netcore-baseapi
```

### Deployment Script Fails

```powershell
# Verify configuration
Get-Content .env.deployment

# Test SSH connection
ssh chinodev@192.168.100.142 'echo "Connected!"'

# Check if publish folder exists (if using -SkipBuild)
Test-Path ./publish
```

## ?? Pre-Deployment Checklist

Before each deployment:

- [ ] All tests passing locally
- [ ] Database migrations created (if needed)
- [ ] `.env.deployment` configured correctly
- [ ] Server environment variables configured (first time only)
- [ ] SSH access working
- [ ] Previous deployment was successful

## ?? Security Reminders

**? DO:**
- Store database credentials on server in `/etc/baseapi/production.env`
- Use SSH key authentication
- Keep `.env.deployment` in `.gitignore`
- Verify file permissions (600 for production.env)

**? DON'T:**
- Commit database passwords to Git
- Store passwords in `.env.deployment`
- Email or message production credentials
- Use same passwords for dev and production

## ?? Typical Deployment Timeline

| Step | Time | Description |
|------|------|-------------|
| Tests | 30-60s | Unit and integration tests |
| Build | 30-45s | Release build and publish |
| Package | 5-10s | Create ZIP archive |
| Upload | 10-30s | Transfer to server (depends on file size) |
| Deploy | 15-20s | Stop, extract, start service |
| **Total** | **2-3 minutes** | End-to-end deployment |

## ?? Quick Tips

1. **Use `-SkipTests` during rapid iterations** (but run tests before final deployment)
2. **Keep terminal open** to see real-time deployment progress
3. **Check logs immediately** after deployment to catch issues early
4. **Use health endpoint** to verify service is running
5. **Bookmark useful SSH commands** for quick access

## ?? Getting Help

If something goes wrong:

1. **Check deployment logs** in PowerShell output
2. **View service logs**: `ssh chinodev@192.168.100.142 'sudo journalctl -u netcore-baseapi -n 100'`
3. **Review deployment guide**: [DEPLOYMENT_GUIDE.md](DEPLOYMENT_GUIDE.md)
4. **Check security guide**: [SECURITY_GUIDE.md](SECURITY_GUIDE.md)
5. **Create GitHub issue** with error details

## ?? Related Documentation

- [Full Deployment Guide](DEPLOYMENT_GUIDE.md) - Detailed deployment instructions
- [Security Guide](SECURITY_GUIDE.md) - Credential management best practices
- [Complete Project Guide](COMPLETE_PROJECT_GUIDE.md) - Comprehensive documentation

---

**Last Updated**: January 29, 2026

**Quick Access Commands**:
- Deploy: `.\deploy.ps1`
- Status: `ssh chinodev@192.168.100.142 'sudo systemctl status netcore-baseapi'`
- Logs: `ssh chinodev@192.168.100.142 'sudo journalctl -u netcore-baseapi -f'`
