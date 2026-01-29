# 🔒 Secure Configuration Management Guide

## Overview

This project uses a multi-layer configuration approach to keep sensitive data (like database credentials) secure and prevent them from being committed to source control.

---

## 📁 Configuration Files Structure

```
BaseAPI/
├── appsettings.json                      ✅ COMMITTED (no secrets)
├── appsettings.Development.json          ❌ NOT COMMITTED (in .gitignore)
├── appsettings.Local.json                ❌ NOT COMMITTED (in .gitignore)
├── appsettings.Production.json           ❌ NOT COMMITTED (in .gitignore)
├── appsettings.Development.json.template ✅ COMMITTED (reference only)
├── appsettings.Local.json.template       ✅ COMMITTED (reference only)
└── appsettings.Production.json.template  ✅ COMMITTED (reference only)
```

---

## 🎯 Configuration Priority (Lowest to Highest)

.NET loads configuration in this order, with later sources overriding earlier ones:

1. **appsettings.json** (base configuration, no secrets)
2. **appsettings.{Environment}.json** (environment-specific, e.g., Development, Production)
3. **appsettings.Local.json** (local development, highest priority)
4. **Environment Variables** (can override any setting)
5. **User Secrets** (alternative to Local file)

---

## 🚀 Quick Start for New Developers

### Step 1: Clone the Repository

```bash
git clone https://github.com/lreb/BaseProject
cd BaseProject/BaseApi/BaseAPI
```

### Step 2: Create Your Local Configuration

Copy the template file:

```powershell
# Windows PowerShell
Copy-Item appsettings.Local.json.template appsettings.Local.json
```

```bash
# Linux/Mac
cp appsettings.Local.json.template appsettings.Local.json
```

### Step 3: Update Your Connection String

Edit `appsettings.Local.json` with your actual database credentials:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=PRODUCTION_HOST;Database=PRODUCTION_DATABASE;Username=PRODUCTION_USERNAME;Password=PRODUCTION_PASSWORD;Port=5432;Pooling=true;SSL Mode=Require;"
  }
}
```

### Step 4: Verify Configuration

Run the application:

```powershell
dotnet run
```

Check that it connects to your database:

```powershell
Invoke-RestMethod https://localhost:5001/health
```

---

## 📝 Configuration Files Explained

### appsettings.json (BASE - COMMITTED)

**Purpose:** Contains default configuration and non-sensitive settings.

**What to include:**
- ✅ Default logging levels
- ✅ API settings (name, version)
- ✅ Feature flags
- ✅ Non-sensitive configuration

**What NOT to include:**
- ❌ Database credentials
- ❌ API keys
- ❌ Passwords
- ❌ Connection strings with credentials

**Example:**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": ""
  },
  "ApiSettings": {
    "ApiName": "Base API",
    "ApiVersion": "v1"
  }
}
```

### appsettings.Local.json (LOCAL DEVELOPMENT - NOT COMMITTED)

**Purpose:** Your personal development configuration. Overrides all other settings.

**Usage:**
- Each developer creates their own from the template
- Contains actual database credentials for local development
- Never committed to Git (in .gitignore)

**Example:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=PRODUCTION_HOST;Database=PRODUCTION_DATABASE;Username=PRODUCTION_USERNAME;Password=PRODUCTION_PASSWORD;Port=5432;Pooling=true;SSL Mode=Require;"
  },
  "Logging": {
    "LogLevel": {
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

**Benefits:**
- ✅ Highest priority (overrides everything)
- ✅ Works across all environments (Development, Production, etc.)
- ✅ Simple to use
- ✅ No need to change ASPNETCORE_ENVIRONMENT

### appsettings.Development.json (DEVELOPMENT - NOT COMMITTED)

**Purpose:** Shared development environment settings.

**When to use:**
- Team wants consistent development settings
- CI/CD pipeline for development environment

**Example:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=dev-server;Database=DevDb;Username=devuser;Password=devpass;"
  },
  "Logging": {
    "LogLevel": {
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

### appsettings.Production.json (PRODUCTION - NOT COMMITTED)

**Purpose:** Production environment configuration.

**Security:**
- ❌ Never committed to Git
- ✅ Created during deployment
- ✅ Uses environment variables or secret managers

**Example:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=prod-server;Database=ProdDb;Username=produser;Password=${DB_PASSWORD};"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  }
}
```

### *.template Files (TEMPLATES - COMMITTED)

**Purpose:** Reference templates for creating actual configuration files.

**Usage:**
```powershell
# Copy template to create your local file
Copy-Item appsettings.Local.json.template appsettings.Local.json

# Edit with your values
notepad appsettings.Local.json
```

---

## 🔐 Alternative: Using User Secrets (.NET)

For even better security, you can use .NET User Secrets for local development:

### Setup User Secrets

```powershell
# Initialize user secrets
dotnet user-secrets init

# Set connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=PRODUCTION_HOST;Database=PRODUCTION_DATABASE;Username=PRODUCTION_USERNAME;Password=PRODUCTION_PASSWORD;Port=5432;Pooling=true;SSL Mode=Require;"

# List all secrets
dotnet user-secrets list

# Remove a secret
dotnet user-secrets remove "ConnectionStrings:DefaultConnection"

# Clear all secrets
dotnet user-secrets clear
```

### Where Are User Secrets Stored?

**Windows:**
```
%APPDATA%\Microsoft\UserSecrets\{user-secrets-id}\secrets.json
```

**Linux/Mac:**
```
~/.microsoft/usersecrets/{user-secrets-id}/secrets.json
```

### Benefits of User Secrets

- ✅ Stored outside project directory
- ✅ Encrypted by user account
- ✅ Per-user configuration
- ✅ Works automatically in Visual Studio
- ✅ Never accidentally committed

### Limitations

- ❌ Only works in Development environment
- ❌ Not available in Production
- ❌ Requires setup on each machine

---

## 🌍 Environment Variables (Production)

For production deployments, use environment variables:

### Setting Environment Variables

**Windows (PowerShell):**
```powershell
$env:ConnectionStrings__DefaultConnection = "Host=prod-server;Database=ProdDb;Username=produser;Password=prodpass;"
```

**Linux/Mac:**
```bash
export ConnectionStrings__DefaultConnection="Host=prod-server;Database=ProdDb;Username=produser;Password=prodpass;"
```

**Docker:**
```dockerfile
ENV ConnectionStrings__DefaultConnection="Host=prod-server;Database=ProdDb;Username=produser;Password=prodpass;"
```

**Azure App Service:**
- Navigate to Configuration → Application Settings
- Add: `ConnectionStrings__DefaultConnection`

**Kubernetes:**
```yaml
env:
  - name: ConnectionStrings__DefaultConnection
    valueFrom:
      secretKeyRef:
        name: database-secret
        key: connection-string
```

### Note on Naming

Use double underscore `__` for nested configuration:

```
ConnectionStrings__DefaultConnection  →  ConnectionStrings:DefaultConnection
ApiSettings__ApiName                  →  ApiSettings:ApiName
```

---

## 🔒 Azure Key Vault / AWS Secrets Manager (Recommended for Production)

### Azure Key Vault

**Install Package:**
```powershell
dotnet add package Azure.Extensions.AspNetCore.Configuration.Secrets
```

**Configure in Program.cs:**
```csharp
builder.Configuration.AddAzureKeyVault(
    new Uri($"https://{keyVaultName}.vault.azure.net/"),
    new DefaultAzureCredential());
```

### AWS Secrets Manager

**Install Package:**
```powershell
dotnet add package Amazon.Extensions.Configuration.SystemsManager
```

**Configure in Program.cs:**
```csharp
builder.Configuration.AddSystemsManager("/my-app/");
```

---

## 🎯 Recommended Configuration Strategy

### For Local Development:

**Option 1: appsettings.Local.json (Simplest)**
```powershell
Copy-Item appsettings.Local.json.template appsettings.Local.json
# Edit with your credentials
```

**Option 2: User Secrets (More Secure)**
```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
```

### For Team Development Environment:

1. Store credentials in shared secret manager (Azure Key Vault)
2. Or use environment variables in deployment pipeline
3. Never commit `appsettings.Development.json`

### For Production:

1. **Best:** Azure Key Vault / AWS Secrets Manager
2. **Good:** Environment variables in deployment platform
3. **Acceptable:** appsettings.Production.json created during deployment

---

## ✅ Security Checklist

Before committing, verify:

- [ ] `appsettings.json` has NO credentials or secrets
- [ ] `appsettings.Local.json` is in `.gitignore`
- [ ] `appsettings.Development.json` is in `.gitignore`
- [ ] `appsettings.Production.json` is in `.gitignore`
- [ ] Only `.template` files are committed
- [ ] No passwords in any committed file
- [ ] `.gitignore` includes all sensitive files

### Verify with Git

```powershell
# Check what will be committed
git status

# Search for credentials in tracked files
git grep -i "password"
git grep -i "connectionstring"

# Check .gitignore
cat .gitignore | Select-String "appsettings"
```

---

## 🚨 What to Do If You Accidentally Commit Secrets

### Immediate Actions:

1. **Change the password/credentials immediately**
2. **Remove from Git history:**

```powershell
# For most recent commit
git reset --soft HEAD~1
git restore --staged appsettings.json
git commit -m "Remove sensitive data"

# For older commits (use BFG Repo-Cleaner)
# Download from: https://rtyley.github.io/bfg-repo-cleaner/
java -jar bfg.jar --replace-text passwords.txt
git reflog expire --expire=now --all
git gc --prune=now --aggressive
```

3. **Force push (if not shared yet):**
```powershell
git push --force
```

4. **Notify team if already pushed**

---

## 📚 Configuration Loading Order Example

Given these files:

**appsettings.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=base;"
  },
  "Logging": { "LogLevel": { "Default": "Information" } }
}
```

**appsettings.Development.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=dev;"
  }
}
```

**appsettings.Local.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=local;"
  }
}
```

**Result when running:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=local;"  // ✅ Local wins!
  },
  "Logging": { "LogLevel": { "Default": "Information" } }  // From base
}
```

---

## 🛠️ Troubleshooting

### "My connection string isn't working"

**Check priority:**
```powershell
# View all loaded configuration
dotnet run --urls "http://localhost:5000"
# Check logs for "ConnectionString: ..."
```

**Verify file is loaded:**
1. Check file exists: `appsettings.Local.json`
2. Check syntax is valid JSON
3. Check Program.cs includes: `AddJsonFile("appsettings.Local.json")`

### "I don't see my appsettings.Local.json changes"

**Solutions:**
1. Restart application (hot reload doesn't always pick up appsettings changes)
2. Check JSON syntax (use JSON validator)
3. Verify file is in same directory as appsettings.json

### "Git is tracking my appsettings.Local.json"

**Fix:**
```powershell
# Remove from Git tracking
git rm --cached appsettings.Local.json

# Commit the removal
git commit -m "Remove appsettings.Local.json from tracking"

# Verify it's in .gitignore
cat .gitignore | Select-String "appsettings.Local"
```

---

## 📖 Additional Resources

- [ASP.NET Core Configuration](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/)
- [Safe Storage of App Secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)
- [Azure Key Vault Configuration Provider](https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration)
- [.gitignore Best Practices](https://github.com/github/gitignore)

---

## 🎉 Summary

**For your setup:**

1. ✅ Keep `appsettings.Local.json` for local development (NOT COMMITTED)
2. ✅ Use `appsettings.Local.json.template` as reference (COMMITTED)
3. ✅ Remove secrets from `appsettings.json` (base file)
4. ✅ Add all sensitive files to `.gitignore`
5. ✅ Use User Secrets or Environment Variables for production

**Your current configuration is now secure! 🔒**
