# Development Setup & Quick Start Guide

## ?? Quick Start (5 Minutes)

### 1. Prerequisites Check
```powershell
# Check .NET SDK
dotnet --version  # Should be 10.0 or higher

# Check SQL Server
sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT @@VERSION"
```

### 2. Clone & Restore
```powershell
cd C:\Projects\Net\BaseProject\BaseApi
dotnet restore
```

### 3. Database Setup
```powershell
# Add initial migration
dotnet ef migrations add InitialCreate --project BaseAPI

# Update database
dotnet ef database update --project BaseAPI
```

### 4. Run Application
```powershell
dotnet run --project BaseAPI
```

### 5. Test API
Open browser: `https://localhost:5001/swagger`

---

## ?? Detailed Setup

### Required Tools

1. **.NET 10 SDK**
   - Download: https://dotnet.microsoft.com/download
   - Verify: `dotnet --version`

2. **SQL Server**
   - LocalDB (included with Visual Studio)
   - Or SQL Server Express
   - Or SQL Server Developer Edition

3. **IDE (Choose One)**
   - Visual Studio 2022 (17.8+)
   - Visual Studio Code + C# extension
   - JetBrains Rider

4. **Optional Tools**
   - Git
   - Postman or Insomnia (API testing)
   - SQL Server Management Studio (SSMS)
   - Azure Data Studio

### Environment Setup

#### Windows
```powershell
# Set environment variables (optional)
$env:ASPNETCORE_ENVIRONMENT = "Development"
$env:ConnectionStrings__DefaultConnection = "Server=(localdb)\mssqllocaldb;Database=BaseApiDb;Trusted_Connection=True;"

# Trust HTTPS certificate
dotnet dev-certs https --trust
```

#### macOS/Linux
```bash
# Set environment variables
export ASPNETCORE_ENVIRONMENT=Development
export ConnectionStrings__DefaultConnection="Server=localhost;Database=BaseApiDb;User Id=sa;Password=YourPassword;"

# Trust HTTPS certificate
dotnet dev-certs https --trust
```

---

## ??? Database Setup

### Using LocalDB (Windows)
```powershell
# Check LocalDB instances
sqllocaldb info

# Create instance if needed
sqllocaldb create MSSQLLocalDB

# Start instance
sqllocaldb start MSSQLLocalDB

# Connection string in appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BaseApiDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### Using SQL Server
```json
// appsettings.Development.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BaseApiDb;User Id=sa;Password=YourStrongPassword123;TrustServerCertificate=True"
  }
}
```

### Entity Framework Migrations

```powershell
# Install EF Core tools globally
dotnet tool install --global dotnet-ef

# Verify installation
dotnet ef

# Add migration
dotnet ef migrations add InitialCreate --project BaseAPI

# View migration SQL (without applying)
dotnet ef migrations script --project BaseAPI

# Apply migration
dotnet ef database update --project BaseAPI

# Rollback migration
dotnet ef database update PreviousMigrationName --project BaseAPI

# Remove last migration (if not applied)
dotnet ef migrations remove --project BaseAPI

# Drop database
dotnet ef database drop --project BaseAPI
```

---

## ?? Running the Application

### Development Mode

```powershell
# Run with hot reload
dotnet watch run --project BaseAPI

# Run without hot reload
dotnet run --project BaseAPI

# Run with specific environment
dotnet run --project BaseAPI --environment Production

# Run with specific port
dotnet run --project BaseAPI --urls "https://localhost:5001;http://localhost:5000"
```

### Visual Studio
1. Open `BaseAPI.sln`
2. Set `BaseAPI` as startup project
3. Press F5 (Debug) or Ctrl+F5 (Run without debugging)

### VS Code
1. Open folder: `BaseApi\BaseAPI`
2. Press F5 (creates launch.json automatically)
3. Or use terminal: `dotnet run`

---

## ?? Testing the API

### Using Swagger UI
1. Navigate to `https://localhost:5001/swagger`
2. Expand endpoint
3. Click "Try it out"
4. Enter parameters
5. Click "Execute"

### Using PowerShell

```powershell
# Get all products
Invoke-RestMethod -Uri "https://localhost:5001/api/v1/products" -Method Get

# Create product
$body = @{
    name = "Test Product"
    description = "Test Description"
    price = 99.99
    stock = 100
    category = "Electronics"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/v1/products" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"

# Get product by ID
Invoke-RestMethod -Uri "https://localhost:5001/api/v1/products/{id}" -Method Get

# Update product
$updateBody = @{
    id = "your-guid-here"
    name = "Updated Product"
    description = "Updated Description"
    price = 149.99
    stock = 50
    category = "Electronics"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/v1/products/{id}" `
    -Method Put `
    -Body $updateBody `
    -ContentType "application/json"

# Delete product
Invoke-RestMethod -Uri "https://localhost:5001/api/v1/products/{id}" -Method Delete
```

### Using cURL

```bash
# Get all products
curl -X GET "https://localhost:5001/api/v1/products"

# Create product
curl -X POST "https://localhost:5001/api/v1/products" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test Product",
    "description": "Test Description",
    "price": 99.99,
    "stock": 100,
    "category": "Electronics"
  }'
```

---

## ?? Health Checks

```powershell
# Full health check
Invoke-RestMethod -Uri "https://localhost:5001/health"

# Liveness probe
Invoke-RestMethod -Uri "https://localhost:5001/health/live"

# Readiness probe
Invoke-RestMethod -Uri "https://localhost:5001/health/ready"
```

Expected response:
```json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.0234567",
  "entries": {
    "database": {
      "status": "Healthy",
      "duration": "00:00:00.0123456"
    },
    "self": {
      "status": "Healthy",
      "duration": "00:00:00.0000001"
    }
  }
}
```

---

## ?? Logging

### View Logs

**Console Output:**
- Logs appear in terminal/console window
- Colored output for different log levels

**File Logs:**
- Location: `BaseAPI/logs/`
- File pattern: `log-YYYYMMDD.txt`
- Rolling daily logs

**Filter Logs:**
```json
// appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information",
      "BaseAPI": "Debug"
    }
  }
}
```

---

## ?? Debugging

### Visual Studio
1. Set breakpoints (F9)
2. Start debugging (F5)
3. Use Debug windows:
   - Locals (Ctrl+Alt+V, L)
   - Watch (Ctrl+Alt+W, 1-4)
   - Call Stack (Ctrl+Alt+C)
   - Immediate (Ctrl+Alt+I)

### VS Code
1. Set breakpoints (click left of line number)
2. Press F5 to start debugging
3. Use Debug Console (Ctrl+Shift+Y)

### Common Debugging Scenarios

**Database Connection Issues:**
```powershell
# Test connection string
dotnet ef database update --project BaseAPI --verbose

# Check SQL Server is running
sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT 1"
```

**Port Already in Use:**
```powershell
# Find process using port 5001
netstat -ano | findstr :5001

# Kill process (replace PID)
taskkill /PID <PID> /F

# Or change port in launchSettings.json
```

**Certificate Issues:**
```powershell
# Trust dev certificate
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

---

## ?? Common Development Tasks

### Add New Entity

```powershell
# 1. Create entity class
# Domain/Entities/YourEntity.cs

# 2. Add DbSet to context
# Infrastructure/Persistence/ApplicationDbContext.cs

# 3. Create migration
dotnet ef migrations add Add_YourEntity --project BaseAPI

# 4. Update database
dotnet ef database update --project BaseAPI
```

### Add New Feature

```powershell
# 1. Create feature folder structure
mkdir Application/YourFeature
mkdir Application/YourFeature/Commands
mkdir Application/YourFeature/Queries
mkdir Application/YourFeature/DTOs

# 2. Create command/query files
# Application/YourFeature/Commands/CreateYourEntity/CreateYourEntityCommand.cs
# Application/YourFeature/Queries/GetYourEntity/GetYourEntityQuery.cs

# 3. Create controller
# API/Controllers/V1/YourEntitiesController.cs
```

### Update Dependencies

```powershell
# List outdated packages
dotnet list BaseAPI package --outdated

# Update specific package
dotnet add BaseAPI package PackageName

# Update all packages
dotnet restore BaseAPI
```

---

## ?? Configuration

### Development vs Production

**appsettings.Development.json:**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BaseApiDb_Dev;..."
  }
}
```

**appsettings.Production.json:**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-server;Database=BaseApiDb;..."
  }
}
```

### User Secrets (for sensitive data)

```powershell
# Initialize user secrets
dotnet user-secrets init --project BaseAPI

# Set secret
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=...;Password=secret;" --project BaseAPI

# List secrets
dotnet user-secrets list --project BaseAPI

# Remove secret
dotnet user-secrets remove "ConnectionStrings:DefaultConnection" --project BaseAPI
```

---

## ?? Performance Profiling

### Using dotnet-counters

```powershell
# Install tool
dotnet tool install --global dotnet-counters

# Monitor application
dotnet-counters monitor --process-id <PID>

# Or monitor by name
dotnet-counters monitor -n BaseAPI
```

### Using dotnet-trace

```powershell
# Install tool
dotnet tool install --global dotnet-trace

# Collect trace
dotnet-trace collect --process-id <PID>
```

---

## ?? Troubleshooting

### Application won't start
```powershell
# Check for errors in logs
cat logs/log-*.txt

# Verify configuration
dotnet run --project BaseAPI --launch-profile "BaseAPI"

# Check port availability
netstat -an | findstr "5001"
```

### Database errors
```powershell
# Verify connection string
# Check appsettings.json

# Test database connection
sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT 1"

# Recreate database
dotnet ef database drop --project BaseAPI --force
dotnet ef database update --project BaseAPI
```

### NuGet restore issues
```powershell
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore BaseAPI --force
```

---

## ?? Getting Help

- Check logs in `logs/` folder
- Review Swagger UI for API documentation
- Check health endpoints for system status
- Review exception details in API responses
- Enable verbose logging in appsettings.json

---

**Happy Coding! ??**
