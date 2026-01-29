# 🐘 PostgreSQL Quick Reference

## Connection Details
```

```

## Connection String
```
Host=PRODUCTION_HOST;Database=PRODUCTION_DATABASE;Username=PRODUCTION_USERNAME;Password=PRODUCTION_PASSWORD;Port=5432;Pooling=true;SSL Mode=Require;
```

## Quick Commands

### Test Connection
```powershell
psql -h 1IP -U developer -d BaseApiDb_Dev
```

### Run Application
```powershell
cd C:\Projects\Net\BaseProject\BaseApi\BaseAPI
dotnet run
```

### Test Health
```powershell
Invoke-RestMethod https://localhost:5001/health
```

### Create Migration
```powershell
dotnet ef migrations add MigrationName
```

### Update Database
```powershell
dotnet ef database update
```

### Query Products
```sql
SELECT * FROM "Products";
```

## Files Changed
- ✅ BaseAPI.csproj (packages)
- ✅ appsettings.json (connection string)
- ✅ appsettings.Development.json (dev connection)
- ✅ Infrastructure/DependencyInjection.cs (UseNpgsql)
- ✅ Program.cs (health check)
- ✅ Migrations recreated

## Verification
1. Build: `dotnet build` ✅
2. Run: `dotnet run` 
3. Health: https://localhost:5001/health
4. Swagger: https://localhost:5001/swagger
5. Test API: Create/Read products
