# Environment Configuration - Simplified Approach

## Overview

We've simplified the environment configuration to use a **unified structure** for both local development and production deployment.

## File Structure

### ✅ What You Use

| File | Purpose | Contains |
|------|---------|----------|
| **`.env.local`** | Local development | DB credentials for local dev |
| **`.env.production`** | Production deployment | DB credentials + SSH deployment config |

### 📋 Templates (Committed)

| File | Purpose |
|------|---------|
| `.env.template` | Universal template for both local and production |
| `.env.example` | *(Legacy - can be removed)* |
| `.env.deployment.example` | *(Legacy - kept for backwards compatibility)* |

## Quick Start

### For New Setup

```powershell
# 1. Run interactive setup (easiest)
.\setup-env.ps1

# This creates:
# - .env.local (for local development)
# - .env.production (for deployment)
```

### For Existing Projects (Migration)

If you already have `.env.deployment`:

```powershell
# Your existing .env.deployment still works!
# But we recommend migrating to .env.production

# Option 1: Rename it
Rename-Item .env.deployment .env.production

# Option 2: Run setup-env.ps1 to create fresh files
.\setup-env.ps1
```

## Workflows

### Local Development

**Option A: Using existing appsettings.Local.json (Current)**
```powershell
# You can keep using your current workflow
dotnet run
# Uses appsettings.Local.json with DB_HOST=192.168.100.142
```

**Option B: Generate from .env.local (New)**
```powershell
# 1. Create .env.local
.\setup-env.ps1

# 2. Generate appsettings.Local.json from it
.\setup-local-from-env.ps1

# 3. Run
dotnet run
```

### Production Deployment

```powershell
# Uses .env.production (or .env.deployment if .env.production doesn't exist)
.\deploy.ps1
```

## Key Differences

### .env.local (Local Development)
```bash
# Database on remote server (access via external IP)
DB_HOST=192.168.100.142  # ✅ External IP because you're connecting FROM your machine TO server
DB_PORT=5432
DB_NAME=BaseApiDb_Dev
DB_USERNAME=developer
DB_PASSWORD=Dev10*
ASPNETCORE_ENVIRONMENT=Development
```

### .env.production (Production Deployment)
```bash
# SSH Configuration
SSH_USER=chinodev
SSH_HOST=192.168.100.142
SSH_PORT=22
SERVER_APP_PATH=/var/www/netcore-baseapi
SERVICE_NAME=netcore-baseapi

# Database on same server (local connection)
DB_HOST=localhost  # ✅ localhost because app RUNS on same server as database
DB_PORT=5432
DB_NAME=BaseApiDb_Dev
DB_USERNAME=developer
DB_PASSWORD=Dev10*
ASPNETCORE_ENVIRONMENT=Production
```

## Why localhost vs 192.168.100.142?

### Local Development (`appsettings.Local.json`)
```
Your Machine (192.168.1.50) → Network → Database Server (192.168.100.142)
                              ↑
                        Uses external IP
```

### Production (on server)
```
App (192.168.100.142) → Database (192.168.100.142)
      ↑
Both on same machine, use localhost
```

## Comparison with Old Approach

| Old Way | New Way |
|---------|---------|
| `appsettings.Local.json` ✅ | `.env.local` → `appsettings.Local.json` |
| `.env.example` (not used) | `.env.template` (unified) |
| `.env.deployment.example` | `.env.template` (unified) |
| `.env.deployment` | `.env.production` (clearer name) |

## Migration Path

### Step 1: Create .env.production from existing .env.deployment

```powershell
# Copy and rename
Copy-Item .env.deployment .env.production

# Or just rename
Rename-Item .env.deployment .env.production
```

### Step 2: (Optional) Create .env.local for future

```powershell
# Extract from your existing appsettings.Local.json
@"
DB_HOST=192.168.100.142
DB_PORT=5432
DB_NAME=BaseApiDb_Dev
DB_USERNAME=developer
DB_PASSWORD=Dev10*
ASPNETCORE_ENVIRONMENT=Development
"@ | Out-File -FilePath .env.local -Encoding UTF8
```

### Step 3: Deploy with new structure

```powershell
# Works with both .env.production and .env.deployment
.\deploy.ps1
```

## Benefits

1. **Clearer naming**: `.env.local` vs `.env.production` (obvious purpose)
2. **Unified structure**: Same format for both local and prod
3. **Less confusion**: One template (`.env.template`) instead of multiple
4. **Backwards compatible**: Old `.env.deployment` still works
5. **Easy setup**: Interactive `setup-env.ps1` script

## Current State

You can continue using your current approach:
- ✅ `appsettings.Local.json` (works great!)
- ✅ `.env.deployment` (still supported!)

Or migrate to the new simplified approach when convenient.

---

**Recommendation:** Keep your current working setup. The new approach is optional and provides better organization for teams.
