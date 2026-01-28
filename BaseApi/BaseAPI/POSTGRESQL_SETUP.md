# ?? PostgreSQL Configuration Guide

## ? Configuration Complete

Your BaseAPI project has been successfully configured to use PostgreSQL!

---

## ?? Configuration Summary

### Connection Details



### Connection Strings

**Production (appsettings.json):**
```
Host=PRODUCTION_HOST;Database=PRODUCTION_DATABASE;Username=PRODUCTION_USERNAME;Password=PRODUCTION_PASSWORD;Port=5432;Pooling=true;SSL Mode=Require;
```

**Development (appsettings.Development.json):**
```
Host=PRODUCTION_HOST;Database=PRODUCTION_DATABASE;Username=PRODUCTION_USERNAME;Password=PRODUCTION_PASSWORD;Port=5432;Pooling=true;SSL Mode=Require;
```

---

## ?? Changes Made

### 1. **NuGet Packages Updated**

**Removed:**
- ? `Microsoft.EntityFrameworkCore.SqlServer` (10.0.1)
- ? `AspNetCore.HealthChecks.SqlServer` (9.0.0)

**Added:**
- ? `Npgsql.EntityFrameworkCore.PostgreSQL` (10.0.0)
- ? `AspNetCore.HealthChecks.Npgsql` (9.0.0)

### 2. **Infrastructure Layer**

**File:** `Infrastructure/DependencyInjection.cs`

```csharp
// Changed from:
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))

// To:
options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
```

### 3. **Health Checks**

**File:** `Program.cs`

```csharp
// Changed from:
.AddSqlServer(connectionString, name: "database", tags: new[] { "db", "sql", "sqlserver" })

// To:
.AddNpgSql(connectionString, name: "database", tags: new[] { "db", "postgresql", "npgsql" })
```

### 4. **Migrations Recreated**

- Old SQL Server migrations removed
- New PostgreSQL migration created: `InitialCreate_PostgreSQL`
- Database schema created on PostgreSQL server

---

## ??? Database Schema

The following database and tables have been created:

### Database: `BaseApiDb_Dev` (Development) / `BaseApiDb` (Production)

### Tables:

**1. Products**
```sql
CREATE TABLE "Products" (
    "Id" uuid NOT NULL PRIMARY KEY,
    "Name" character varying(200) NOT NULL,
    "Description" character varying(1000),
    "Price" numeric(18,2) NOT NULL,
    "Stock" integer NOT NULL,
    "Category" character varying(100) NOT NULL,
    "IsDeleted" boolean NOT NULL DEFAULT false,
    "CreatedAt" timestamp without time zone NOT NULL,
    "CreatedBy" character varying(100) NOT NULL,
    "UpdatedAt" timestamp without time zone,
    "UpdatedBy" character varying(100)
);
```

**2. __EFMigrationsHistory**
```sql
CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL PRIMARY KEY,
    "ProductVersion" character varying(32) NOT NULL
);
```

---

## ?? Testing the Connection

### 1. Run the Application

```powershell
cd C:\Projects\Net\BaseProject\BaseApi\BaseAPI
dotnet run
```

### 2. Check Health Endpoint

```powershell
Invoke-RestMethod -Uri "https://localhost:5001/health"
```

**Expected Response:**
```json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.0234567",
  "entries": {
    "database": {
      "status": "Healthy",
      "description": null,
      "duration": "00:00:00.0123456",
      "tags": ["db", "postgresql", "npgsql"]
    },
    "self": {
      "status": "Healthy"
    }
  }
}
```

### 3. Test API Endpoints

```powershell
# Create a product
$body = @{
    name = "Test Product"
    description = "PostgreSQL Test"
    price = 99.99
    stock = 100
    category = "Electronics"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/v1/products" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"

# Get all products
Invoke-RestMethod -Uri "https://localhost:5001/api/v1/products"
```

---

## ?? Verifying PostgreSQL Database

### Using psql (PostgreSQL CLI)

```bash
# Connect to database
psql -h IP -U developer -d BaseApiDb_Dev

# List tables
\dt

# View Products table structure
\d "Products"

# Query data
SELECT * FROM "Products";

# Exit
\q
```

### Using pgAdmin

1. Create new server connection
2. **Host:** **********
3. **Port:** 5432
4. **Database:** BaseApiDb_Dev
5. **Username:** **********
6. **Password:** ******
7. Browse to **Databases ? BaseApiDb_Dev ? Schemas ? public ? Tables**

### Using Azure Data Studio (with PostgreSQL extension)

1. Install PostgreSQL extension
2. New Connection
3. **Server:** **********
4. **Authentication type:** Password
5. **User name:** ********
6. **Password:** ********
7. **Database:** BaseApiDb_Dev

---

## ?? PostgreSQL vs SQL Server Differences

### Data Types

| SQL Server | PostgreSQL | Notes |
|------------|------------|-------|
| `uniqueidentifier` | `uuid` | GUIDs |
| `nvarchar(n)` | `character varying(n)` | Strings |
| `decimal(18,2)` | `numeric(18,2)` | Decimals |
| `bit` | `boolean` | Boolean |
| `datetime2` | `timestamp without time zone` | DateTime |

### Naming Conventions

- PostgreSQL is **case-sensitive** with quoted identifiers
- Table names in quotes: `"Products"` not `Products`
- EF Core handles this automatically

### Connection String Format

**SQL Server:**
```
Server=localhost;Database=MyDb;User Id=sa;Password=pass;
```

**PostgreSQL:**
```
Host=localhost;Database=MyDb;Username=user;Password=pass;Port=5432;
```

---

## ?? Security Best Practices

### 1. Use Environment Variables (Recommended)

Instead of hardcoding credentials, use:

```powershell
# Set environment variable


# Or use User Secrets for development

```

### 2. Production Configuration

**appsettings.Production.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=#{DB_HOST}#;Database=#{DB_NAME}#;Username=#{DB_USER}#;Password=#{DB_PASSWORD}#;Port=5432;SSL Mode=Require;"
  }
}
```

Replace `#{}` tokens during deployment.

### 3. SSL/TLS Connection

For production, enable SSL:
```

```

---

## ?? Troubleshooting

### Connection Refused

**Problem:** Can't connect to PostgreSQL server

**Solutions:**
1. Verify PostgreSQL is running on **********
2. Check firewall allows port 5432
3. Verify pg_hba.conf allows connections from your IP
4. Test connection: `telnet ********2 5432`

### Authentication Failed

**Problem:** Password authentication failed for user "developer"

**Solutions:**
1. Verify username: `developer` (not `Developer`)
2. Verify password: `Dev10*`
3. Check pg_hba.conf authentication method (should be `md5` or `scram-sha-256`)

### Database Does Not Exist

**Problem:** Database "BaseApiDb_Dev" does not exist

**Solution:**
```powershell
# Create database
psql -h IP -U developer -c "CREATE DATABASE \"BaseApiDb_Dev\";"

# Or run migrations again
dotnet ef database update
```

### Migration Errors

**Problem:** Error applying migrations

**Solution:**
```powershell
# Drop and recreate database
dotnet ef database drop --force
dotnet ef database update

# Or manually:
psql -h IP -U developer -c "DROP DATABASE IF EXISTS \"BaseApiDb_Dev\";"
psql -h IP -U developer -c "CREATE DATABASE \"BaseApiDb_Dev\";"
dotnet ef database update
```

### Case Sensitivity Issues

**Problem:** Table or column not found

**Solution:**
PostgreSQL is case-sensitive. Entity Framework Core automatically quotes identifiers, so:
- ? Use: `"Products"` (with quotes)
- ? Don't use: `products` or `PRODUCTS` (without quotes)

---

## ?? Common Tasks

### Create New Migration

```powershell
dotnet ef migrations add MigrationName
```

### Apply Migrations

```powershell
dotnet ef database update
```

### Rollback Migration

```powershell
dotnet ef database update PreviousMigrationName
```

### Generate SQL Script

```powershell
dotnet ef migrations script --output migration.sql
```

### View Connection String

```powershell
# From appsettings
Get-Content appsettings.json | Select-String "ConnectionStrings" -Context 0,2

# Test connection
psql -h IP -U developer -d BaseApiDb_Dev -c "SELECT version();"
```

---

## ?? Performance Optimization

### Connection Pooling

Already enabled in connection string:
```
Pooling=true;
```

### Connection Pool Settings

For high-load scenarios, customize:
```
Host=PRODUCTION_HOST;Database=PRODUCTION_DATABASE;Username=PRODUCTION_USERNAME;Password=PRODUCTION_PASSWORD;Port=5432;Pooling=true;SSL Mode=Require;
```

### Index Recommendations

For better query performance:

```sql
-- Index on Category for filtering
CREATE INDEX "IX_Products_Category" ON "Products" ("Category");

-- Index on IsDeleted for soft delete filtering
CREATE INDEX "IX_Products_IsDeleted" ON "Products" ("IsDeleted");

-- Composite index for common queries
CREATE INDEX "IX_Products_Category_IsDeleted" ON "Products" ("Category", "IsDeleted");
```

---

## ?? Reverting to SQL Server (If Needed)

If you need to switch back:

### 1. Update BaseAPI.csproj
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="10.0.1" />
<PackageReference Include="AspNetCore.HealthChecks.SqlServer" Version="9.0.0" />
```

### 2. Update Connection String
```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BaseApiDb;Trusted_Connection=True;"
```

### 3. Update DependencyInjection.cs
```csharp
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
```

### 4. Update Health Check
```csharp
.AddSqlServer(connectionString, name: "database")
```

### 5. Recreate Migrations
```powershell
Remove-Item -Path "Migrations" -Recurse -Force
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## ?? Additional Resources

### Official Documentation
- [Npgsql Entity Framework Core Provider](https://www.npgsql.org/efcore/)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)
- [ASP.NET Core Health Checks](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks)

### PostgreSQL Tools
- [pgAdmin](https://www.pgadmin.org/) - GUI management tool
- [Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio/) - Cross-platform DB tool
- [DBeaver](https://dbeaver.io/) - Universal database tool

### Connection String Reference
- [Npgsql Connection String Parameters](https://www.npgsql.org/doc/connection-string-parameters.html)

---

## ? Success Checklist

- [x] PostgreSQL NuGet packages installed
- [x] Connection string updated with PostgreSQL credentials
- [x] Infrastructure layer configured for PostgreSQL
- [x] Health checks updated to use PostgreSQL
- [x] Old SQL Server migrations removed
- [x] New PostgreSQL migrations created
- [x] Database created on PostgreSQL server ()
- [x] Application builds successfully

## ?? Next Steps

1. **Run the application:** `dotnet run`
2. **Test health check:** Visit https://localhost:5001/health
3. **Test Swagger:** Visit https://localhost:5001/swagger
4. **Create test data:** Use Swagger UI or PowerShell commands
5. **Verify in PostgreSQL:** Use psql or pgAdmin to check data

---

**? Your BaseAPI is now running on PostgreSQL!**

*Configuration completed on: January 28, 2026*
