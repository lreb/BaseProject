# ?? ORIGINAL ISSUE: Database Connection Failure

## Issue Summary

**Exception**: `System.InvalidOperationException: An exception has been raised that is likely due to a transient failure.`  
**Inner Exception**: `Npgsql.NpgsqlException: No such host is known.`  
**Location**: `GetProductsQuery.Handle()` when calling `_context.Set<Product>().ToListAsync()`

## Root Cause Analysis

### Immediate Cause
The PostgreSQL connection string had an invalid or empty host name, causing DNS resolution to fail.

### Root Cause
Environment variables required to build the connection string were not set:
- `BaseProjectDbServer`
- `BaseProjectDbServerPort`
- `BaseProjectDbServerUser`
- `BaseProjectDbServerPassword`
- `BaseProjectDb`

The old `Startup.cs` (from BaseProjectAPI) used:
```csharp
string dbServer = Environment.GetEnvironmentVariable("BaseProjectDbServer");
// ... other variables ...
StringBuilder connectionString = new StringBuilder($"Server={dbServer};port={dbServerPort};...");
```

When `dbServer` was null or empty, the connection string became: `Server=;port=;...`

## Solutions Implemented

### For Development (Your Current Issue)

**Quick Fix - Set Environment Variables in Visual Studio**:

1. **Right-click project** ? Properties ? Debug ? Environment Variables
2. **Add these variables**:
   ```
   BaseProjectDbServer=localhost
   BaseProjectDbServerPort=5432
   BaseProjectDbServerUser=postgres
   BaseProjectDbServerPassword=your_password
   BaseProjectDb=BaseApiDb_Dev
   ```

**OR - Use appsettings.Local.json** (Recommended for BaseAPI):

1. Create/Edit `BaseAPI/appsettings.Local.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=BaseApiDb_Dev;Username=postgres;Password=your_password;Port=5432;"
     }
   }
   ```

2. This file is:
   - ? Already in `.gitignore`
   - ? Loaded by `Program.cs`
   - ? Overrides base `appsettings.json`

**OR - Use .NET User Secrets** (Most Secure for Dev):

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=BaseApiDb_Dev;Username=postgres;Password=your_password;Port=5432;"
```

### For Production (Security Improvements)

As part of fixing this issue, we also secured the production deployment process:

1. **Created `setup-server-env.sh`**
   - Configures production credentials on Ubuntu server
   - Stores in `/etc/baseapi/production.env` with 600 permissions

2. **Updated `deploy.ps1`**
   - Removed password handling from deployment script
   - Relies on server-side environment variables

3. **Created Documentation**
   - `SECURITY_GUIDE.md` - Complete security guide
   - `DEPLOYMENT_QUICK_REFERENCE.md` - Quick reference
   - Updated `DEPLOYMENT_GUIDE.md`

## Verification Steps

### 1. Verify Configuration is Set

**For Environment Variables**:
```powershell
# In PowerShell
$env:BaseProjectDbServer
# Should output: localhost (or your server)
```

**For appsettings.Local.json**:
```powershell
# Verify file exists
Test-Path BaseAPI/appsettings.Local.json
# Should output: True

# Check content
Get-Content BaseAPI/appsettings.Local.json
# Should show connection string
```

**For User Secrets**:
```powershell
dotnet user-secrets list
# Should show: ConnectionStrings:DefaultConnection = Host=...
```

### 2. Test Database Connection

```powershell
# Try to connect using psql
psql -h localhost -U postgres -d BaseApiDb_Dev
# Enter password when prompted
```

### 3. Run the Application

```powershell
cd BaseAPI
dotnet run
```

Expected output:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### 4. Test the Health Endpoint

```powershell
# Open browser to:
https://localhost:5001/health

# Or use PowerShell:
Invoke-RestMethod https://localhost:5001/health -SkipCertificateCheck
```

Expected response:
```json
{
  "status": "Healthy",
  "checks": [
    {
      "name": "database",
      "status": "Healthy"
    }
  ]
}
```

### 5. Test the Products Endpoint

```powershell
# Open browser to:
https://localhost:5001/swagger

# Or use PowerShell:
Invoke-RestMethod https://localhost:5001/api/v1/products -SkipCertificateCheck
```

## Common Scenarios

### Scenario 1: Using Old Startup.cs Configuration

If you're using the old `Startup.cs` from `BaseProjectAPI`:

**Option A: Set Environment Variables**
```powershell
# In PowerShell (temporary - for current session)
$env:BaseProjectDbServer = "localhost"
$env:BaseProjectDbServerPort = "5432"
$env:BaseProjectDbServerUser = "postgres"
$env:BaseProjectDbServerPassword = "your_password"
$env:BaseProjectDb = "BaseApiDb_Dev"

# Run application
dotnet run
```

**Option B: Create launchSettings.json**

Edit `BaseAPI/Properties/launchSettings.json`:
```json
{
  "profiles": {
    "BaseAPI": {
      "commandName": "Project",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "BaseProjectDbServer": "localhost",
        "BaseProjectDbServerPort": "5432",
        "BaseProjectDbServerUser": "postgres",
        "BaseProjectDbServerPassword": "your_password",
        "BaseProjectDb": "BaseApiDb_Dev"
      }
    }
  }
}
```

### Scenario 2: Using New Program.cs Configuration (BaseAPI)

The new `BaseAPI/Program.cs` uses standard ASP.NET Core configuration:

**Recommended: Use appsettings.Local.json**

1. Copy template:
   ```powershell
   Copy-Item BaseAPI/appsettings.json.template BaseAPI/appsettings.Local.json
   ```

2. Edit with your credentials:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=BaseApiDb_Dev;Username=postgres;Password=your_password;Port=5432;"
     }
   }
   ```

3. Run:
   ```powershell
   dotnet run
   ```

## Troubleshooting

### Issue: Still getting "No such host is known"

**Check 1: Verify PostgreSQL is running**
```powershell
# Windows
Get-Service postgresql*

# Should show "Running"
```

**Check 2: Verify connection string**
```powershell
# Check what connection string the app is using
# (Add logging to see the actual connection string)
```

**Check 3: Test direct connection**
```powershell
psql -h localhost -p 5432 -U postgres -d postgres
```

### Issue: "Password authentication failed"

- Verify username and password are correct
- Check PostgreSQL pg_hba.conf allows local connections
- Try connecting with psql first

### Issue: "Database does not exist"

```powershell
# Create database
psql -h localhost -U postgres -c "CREATE DATABASE BaseApiDb_Dev;"

# Run migrations
dotnet ef database update
```

## Project Configuration Comparison

### Old Configuration (BaseProjectAPI)

**Location**: `Startup.cs`
**Method**: Environment variables
**Variables**:
- `BaseProjectDbServer`
- `BaseProjectDbServerPort`
- `BaseProjectDbServerUser`
- `BaseProjectDbServerPassword`
- `BaseProjectDb`

**Connection String Building**:
```csharp
StringBuilder connectionString = new StringBuilder(
    $"Server={dbServer};port={dbServerPort};user id={dbServerUser};password={dbServerPassword};database={db};pooling=true"
);
```

### New Configuration (BaseAPI)

**Location**: `Program.cs` + `Infrastructure/DependencyInjection.cs`
**Method**: ASP.NET Core Configuration System
**Sources** (in priority order):
1. `appsettings.json` (base)
2. `appsettings.{Environment}.json`
3. `appsettings.Local.json` (development)
4. Environment variables
5. User secrets (development)

**Connection String Access**:
```csharp
builder.Configuration.GetConnectionString("DefaultConnection")
```

## Next Steps

1. **? Fix your immediate issue**:
   - Set environment variables OR
   - Create `appsettings.Local.json` OR
   - Use user secrets

2. **? Verify database connection**:
   - Test with psql
   - Run application
   - Check health endpoint

3. **?? Review documentation**:
   - [SECURITY_GUIDE.md](SECURITY_GUIDE.md) - Configuration best practices
   - [README.md](README.md) - Getting started guide
   - [DEPLOYMENT_GUIDE.md](DEPLOYMENT_GUIDE.md) - Production deployment

4. **?? Secure production** (when ready to deploy):
   - Follow [DEPLOYMENT_GUIDE.md](DEPLOYMENT_GUIDE.md)
   - Use `setup-server-env.sh` on server
   - Never commit credentials to Git

## Quick Fix Commands

```powershell
# Create appsettings.Local.json with your credentials
@"
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=BaseApiDb_Dev;Username=postgres;Password=your_password;Port=5432;"
  }
}
"@ | Out-File -FilePath BaseAPI/appsettings.Local.json -Encoding UTF8

# Run the application
cd BaseAPI
dotnet run

# Test the API
Invoke-RestMethod https://localhost:5001/health -SkipCertificateCheck
```

## Support

If you're still having issues:

1. **Check logs** in `BaseAPI/logs/` folder
2. **Enable verbose logging** in `appsettings.json`:
   ```json
   "Logging": {
     "LogLevel": {
       "Default": "Debug",
       "Microsoft.EntityFrameworkCore": "Debug"
     }
   }
   ```
3. **Create GitHub issue** with:
   - Full error message
   - Connection string format (without password!)
   - PostgreSQL version
   - Environment (Dev/Prod)

---

**Issue Resolved**: Configuration-based connection string setup  
**Security Improved**: Production credential management  
**Documentation Added**: Comprehensive guides for both scenarios
