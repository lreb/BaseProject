# Startup Troubleshooting Guide

## Issues Fixed

### 1. MediatR Registration Issue (AggregateException)
**Problem**: Application was throwing `AggregateException` during startup when trying to register MediatR pipeline behaviors.

**Root Cause**: MediatR version 12+ changed its registration API. The old approach of manually registering behaviors with `AddTransient` was causing DI conflicts.

**Solution**: Updated `Application/DependencyInjection.cs` to use `AddOpenBehavior` method:
```csharp
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
```

### 2. Repository DI Resolution Issue
**Problem**: Application couldn't resolve `DbContext` when trying to activate `Repository<T>`.

**Error Message**: 
```
Unable to resolve service for type 'Microsoft.EntityFrameworkCore.DbContext' 
while attempting to activate 'BaseAPI.Infrastructure.Repositories.Repository`1[BaseAPI.Domain.Entities.Product]'
```

**Root Cause**: The `Repository<T>` class was injecting the base `DbContext` class instead of the concrete `ApplicationDbContext`.

**Solution**: Updated `Infrastructure/Repositories/Repository.cs` to inject `ApplicationDbContext`:
```csharp
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
}
```

### 3. Database Migration Setup
**Problem**: No database migrations existed, and the database wasn't created.

**Solution**: 
- Installed EF Core tools globally: `dotnet tool install --global dotnet-ef`
- Created initial migration: `dotnet ef migrations add InitialCreate`
- Applied migration: `dotnet ef database update`

### 4. Package Version Warnings
**Problem**: NuGet warnings about HealthChecks packages version mismatch.

**Solution**: Updated packages in `BaseAPI.csproj`:
- `AspNetCore.HealthChecks.UI.Client`: 8.0.2 ? 9.0.0
- `AspNetCore.HealthChecks.SqlServer`: 8.1.0 ? 9.0.0

## Running the Application

### Prerequisites
1. .NET 10 SDK installed
2. SQL Server LocalDB installed (comes with Visual Studio)
3. EF Core tools installed globally: `dotnet tool install --global dotnet-ef`

### First Time Setup
1. Open the solution in Visual Studio 2026
2. Restore NuGet packages
3. Database should already be created with migrations
4. Press F5 or click the Run button

### Verify Setup
Once running, you can verify the application health:
- **Health Check**: https://localhost:[port]/health
- **Swagger UI**: https://localhost:[port]/swagger
- **API Endpoints**: https://localhost:[port]/api/v1/products

### Manual Database Setup (if needed)
If you need to recreate the database:

```bash
# Navigate to the project directory
cd BaseAPI

# Remove existing migrations (if any issues)
dotnet ef database drop
dotnet ef migrations remove

# Create new migration
dotnet ef migrations add InitialCreate

# Apply migration
dotnet ef database update
```

### Connection String
Default connection string (in `appsettings.json`):
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BaseApiDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

## Common Errors

### Error: "Unable to create a 'DbContext' of type..."
- **Cause**: DI configuration issue
- **Fix**: Ensure `Repository<T>` injects `ApplicationDbContext` not `DbContext`

### Error: "AggregateException in Microsoft.Extensions.DependencyInjection.dll"
- **Cause**: MediatR registration issue
- **Fix**: Use `AddOpenBehavior` method in MediatR configuration

### Error: Database connection failures during startup
- **Cause**: Database doesn't exist or migrations not applied
- **Fix**: Run `dotnet ef database update`

### Error: LocalDB not found
- **Cause**: SQL Server LocalDB not installed
- **Fix**: Install SQL Server LocalDB through Visual Studio Installer or standalone installer

## Architecture Summary

The project follows Clean Architecture principles:

```
BaseAPI (Presentation)
??? API/
?   ??? Controllers/     # API Controllers
?   ??? Middleware/      # Custom middleware
?   ??? Filters/         # Exception filters
??? Application/         # Business Logic
?   ??? Common/
?   ?   ??? Behaviors/   # MediatR pipeline behaviors
?   ?   ??? Interfaces/  # Application interfaces
?   ?   ??? Models/      # Result types
?   ??? Products/        # Feature folders
??? Domain/              # Entities & Value Objects
??? Infrastructure/      # Data Access & External Services
    ??? Persistence/     # EF Core DbContext
    ??? Repositories/    # Repository implementations
```

## Next Steps

1. Review the Clean Architecture structure
2. Add authentication/authorization if needed
3. Configure logging sinks (currently console + file)
4. Add additional features following the Products example
5. Set up CI/CD pipeline
6. Configure production connection strings

## Support

If you encounter other issues:
1. Check Visual Studio Output window (Debug pane)
2. Review Serilog logs in the `logs/` folder
3. Ensure all NuGet packages are restored
4. Verify .NET 10 SDK is properly installed
