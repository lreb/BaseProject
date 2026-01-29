# ?? Complete Project Guide - BaseAPI

> **A comprehensive guide to understanding, setting up, and working with the BaseAPI Clean Architecture project**

---

## ?? Table of Contents

1. [Project Overview](#-project-overview)
2. [Architecture Deep Dive](#-architecture-deep-dive)
3. [Quick Start Guide](#-quick-start-guide)
4. [Troubleshooting & Common Issues](#-troubleshooting--common-issues)
5. [Dependencies & Packages](#-dependencies--packages)
6. [Database Setup & Migrations](#-database-setup--migrations)
7. [Development Workflow](#-development-workflow)
8. [API Documentation & Testing](#-api-documentation--testing)
9. [Best Practices](#-best-practices)
10. [Production Deployment](#-production-deployment)
11. [FAQ & Support](#-faq--support)

---

## ?? Project Overview

### What is BaseAPI?

BaseAPI is a **production-ready ASP.NET Core Web API template** built on **.NET 10** that implements **Clean Architecture** principles with industry best practices for:

- ? **Maintainability** - Clear separation of concerns
- ? **Scalability** - Stateless design, ready for horizontal scaling
- ? **Testability** - Dependency injection, mockable interfaces
- ? **Performance** - Async operations, caching-ready
- ? **Security** - Input validation, exception handling, CORS

### Key Features

| Feature | Description | Status |
|---------|-------------|--------|
| **Clean Architecture** | 4-layer architecture with clear boundaries | ? Implemented |
| **CQRS Pattern** | Command/Query separation using MediatR | ? Implemented |
| **Repository Pattern** | Generic repository with Unit of Work | ? Implemented |
| **Validation** | FluentValidation with pipeline behavior | ? Implemented |
| **Exception Handling** | Global middleware with custom exceptions | ? Implemented |
| **API Versioning** | URL and header-based versioning | ? Implemented |
| **Health Checks** | Database and application health monitoring | ? Implemented |
| **Logging** | Serilog with console and file sinks | ? Implemented |
| **Swagger/OpenAPI** | Interactive API documentation | ? Implemented |
| **Entity Framework** | Code-first with migrations support | ? Implemented |
| **AutoMapper** | DTO mapping configuration | ? Implemented |
| **Soft Delete** | Global query filters for deleted entities | ? Implemented |

### Technology Stack

```
.NET 10
??? ASP.NET Core Web API
??? Entity Framework Core 10.0.1
??? SQL Server / LocalDB
??? MediatR 12.4.1
??? FluentValidation 11.10.0
??? AutoMapper 12.0.1
??? Serilog 8.0.3
??? Swashbuckle (Swagger) 10.1.0
??? AspNetCore.HealthChecks 9.0.0
```

---

## ??? Architecture Deep Dive

### Clean Architecture Layers

```
???????????????????????????????????????????????????????????????
?                        Presentation                          ?
?                  (BaseAPI - API Layer)                       ?
?  Controllers, Middleware, Filters, Program.cs                ?
???????????????????????????????????????????????????????????????
?                        Application                           ?
?            (Business Logic & Use Cases)                      ?
?  Commands, Queries, DTOs, Validators, Behaviors              ?
???????????????????????????????????????????????????????????????
?                        Infrastructure                        ?
?              (Data Access & External Services)               ?
?  DbContext, Repositories, Configurations                     ?
???????????????????????????????????????????????????????????????
?                          Domain                              ?
?                  (Enterprise Logic)                          ?
?  Entities, Value Objects, Interfaces (NO DEPENDENCIES)       ?
???????????????????????????????????????????????????????????????
```

### Project Structure

```
BaseAPI/
?
??? ?? Domain/                          # Core business logic (no dependencies)
?   ??? Common/
?   ?   ??? BaseEntity.cs              # Base class with Id, audit fields, soft delete
?   ?   ??? IAuditableEntity.cs        # Interface for auditable entities
?   ??? Entities/
?       ??? Product.cs                 # Sample entity
?
??? ?? Application/                     # Application business rules
?   ??? Common/
?   ?   ??? Behaviors/                 # MediatR Pipeline Behaviors
?   ?   ?   ??? ValidationBehavior.cs  # Automatic FluentValidation
?   ?   ?   ??? LoggingBehavior.cs     # Request/response logging
?   ?   ?   ??? PerformanceBehavior.cs # Performance monitoring (>500ms)
?   ?   ??? Exceptions/                # Custom exception types
?   ?   ?   ??? NotFoundException.cs   # 404 errors
?   ?   ?   ??? ValidationException.cs # 400 validation errors
?   ?   ?   ??? BadRequestException.cs # 400 business rule violations
?   ?   ??? Interfaces/                # Abstractions
?   ?   ?   ??? IRepository.cs         # Generic repository interface
?   ?   ?   ??? IUnitOfWork.cs         # Transaction management interface
?   ?   ?   ??? IApplicationDbContext.cs # DbContext abstraction
?   ?   ??? Models/
?   ?       ??? Result.cs              # Standardized response wrapper
?   ??? Products/                      # Feature folder (CQRS pattern)
?   ?   ??? Commands/
?   ?   ?   ??? CreateProduct/
?   ?   ?   ?   ??? CreateProductCommand.cs     # Command + Validator + Handler
?   ?   ?   ??? UpdateProduct/
?   ?   ?   ?   ??? UpdateProductCommand.cs
?   ?   ?   ??? DeleteProduct/
?   ?   ?       ??? DeleteProductCommand.cs
?   ?   ??? Queries/
?   ?   ?   ??? GetProducts/
?   ?   ?   ?   ??? GetProductsQuery.cs         # Query + Handler
?   ?   ?   ??? GetProductById/
?   ?   ?       ??? GetProductByIdQuery.cs
?   ?   ??? DTOs/
?   ?       ??? ProductDto.cs          # Data Transfer Object
?   ?       ??? ProductMappingProfile.cs # AutoMapper configuration
?   ??? DependencyInjection.cs         # Register Application services
?
??? ?? Infrastructure/                  # External concerns implementation
?   ??? Persistence/
?   ?   ??? ApplicationDbContext.cs    # EF Core DbContext
?   ?   ??? Configurations/
?   ?       ??? ProductConfiguration.cs # Entity configuration (Fluent API)
?   ??? Repositories/
?   ?   ??? Repository.cs              # Generic repository implementation
?   ?   ??? UnitOfWork.cs              # Unit of Work implementation
?   ??? DependencyInjection.cs         # Register Infrastructure services
?
??? ?? API/                             # Presentation layer
?   ??? Controllers/
?   ?   ??? V1/
?   ?       ??? ProductsController.cs  # RESTful API endpoints
?   ??? Middleware/
?   ?   ??? ExceptionHandlingMiddleware.cs # Global exception handler
?   ??? Filters/
?   ?   ??? ApiExceptionFilterAttribute.cs # Action filter for exceptions
?   ??? Program.cs                     # Application entry point & configuration
?
??? ?? Migrations/                      # EF Core migrations
?   ??? 20260128200245_InitialCreate.cs
?   ??? 20260128200245_InitialCreate.Designer.cs
?   ??? ApplicationDbContextModelSnapshot.cs
?
??? ?? logs/                            # Serilog log files
?   ??? log-YYYYMMDD.txt               # Daily rolling logs
?
??? ?? appsettings.json                # Main configuration
??? ?? appsettings.Development.json    # Development overrides
??? ?? BaseAPI.csproj                  # Project file with NuGet packages
??? ?? Documentation files (*.md)
```

### Dependency Flow

```
????????????
?   API    ? ??? Uses Application interfaces
????????????
     ?
     ?
????????????
?Application? ??? Defines interfaces, Uses Domain
????????????
     ?
     ?
????????????
?Infrastruc? ??? Implements Application interfaces
?  ture    ?
????????????
     ?
     ?
????????????
?  Domain  ? ??? NO DEPENDENCIES (core business logic)
????????????
```

### Design Patterns Implemented

1. **Clean Architecture** - Onion architecture with dependency inversion
2. **CQRS** - Command Query Responsibility Segregation via MediatR
3. **Repository Pattern** - Data access abstraction
4. **Unit of Work** - Transaction management
5. **Mediator Pattern** - Decoupled request/response handling
6. **Factory Pattern** - Object creation (Result factory methods)
7. **Pipeline Pattern** - MediatR behaviors (validation, logging, performance)
8. **Specification Pattern** - Query specifications via LINQ expressions

---

## ?? Quick Start Guide

### Prerequisites Checklist

Before starting, ensure you have:

- [ ] **.NET 10 SDK** installed
  ```powershell
  dotnet --version  # Should be 10.0 or higher
  ```

- [ ] **SQL Server LocalDB** (included with Visual Studio) or SQL Server
  ```powershell
  sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT @@VERSION"
  ```

- [ ] **Visual Studio 2026** or **VS Code** with C# extension

- [ ] **EF Core Tools** (optional, for command line)
  ```powershell
  dotnet tool install --global dotnet-ef
  dotnet ef --version
  ```

### 5-Minute Setup

#### Step 1: Clone & Restore
```powershell
# Navigate to project directory
cd C:\Projects\Net\BaseProject\BaseApi\BaseAPI

# Restore NuGet packages
dotnet restore

# Trust HTTPS development certificate
dotnet dev-certs https --trust
```

#### Step 2: Database Setup
```powershell
# The database and migrations are already created
# If you need to recreate:
dotnet ef database update
```

#### Step 3: Run the Application
```powershell
# Run with hot reload
dotnet watch run

# Or run normally
dotnet run
```

#### Step 4: Test the API
Open your browser and navigate to:
- **Swagger UI**: https://localhost:5001/swagger
- **Health Check**: https://localhost:5001/health

### First API Test

Using PowerShell:
```powershell
# Create a product
$body = @{
    name = "Test Laptop"
    description = "High-performance laptop"
    price = 1299.99
    stock = 50
    category = "Electronics"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/v1/products" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"

# Get all products
Invoke-RestMethod -Uri "https://localhost:5001/api/v1/products" -Method Get
```

---

## ?? Troubleshooting & Common Issues

### Issue #1: AggregateException During Startup

**Symptoms:**
```
Exception thrown: 'System.AggregateException' in Microsoft.Extensions.DependencyInjection.dll
The program has exited with code 0 (0x0)
```

**Root Cause:** MediatR 12+ changed its registration API. Manual `AddTransient` registration of pipeline behaviors causes DI conflicts.

**Solution:** ? **Already Fixed**
```csharp
// Application/DependencyInjection.cs
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));      // ? Correct
    cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// ? OLD WAY (Don't use)
// services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
```

### Issue #2: Unable to Resolve DbContext

**Symptoms:**
```
Unable to resolve service for type 'Microsoft.EntityFrameworkCore.DbContext' 
while attempting to activate 'Repository<Product>'
```

**Root Cause:** Repository was injecting base `DbContext` instead of concrete `ApplicationDbContext`.

**Solution:** ? **Already Fixed**
```csharp
// Infrastructure/Repositories/Repository.cs
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;  // ? Specific type
    
    public Repository(ApplicationDbContext context)    // ? Correct
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
}
```

### Issue #3: Database Connection Failures

**Symptoms:**
- Application starts but health check fails
- Database-related errors in logs
- "Login failed" or "Server not found" errors

**Solution:**

1. **Verify SQL Server is Running:**
   ```powershell
   # Check LocalDB
   sqllocaldb info
   sqllocaldb start MSSQLLocalDB
   
   # Test connection
   sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT 1"
   ```

2. **Check Connection String:**
   ```json
   // appsettings.json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BaseApiDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Recreate Database:**
   ```powershell
   dotnet ef database drop --force
   dotnet ef database update
   ```

### Issue #4: Port Already in Use

**Symptoms:**
```
Failed to bind to address https://127.0.0.1:5001: address already in use
```

**Solution:**
```powershell
# Find process using port 5001
netstat -ano | findstr :5001

# Kill the process (replace <PID> with actual PID)
taskkill /PID <PID> /F

# Or change port in launchSettings.json
```

### Issue #5: Migration Errors

**Symptoms:**
- "Unable to create DbContext"
- "Build failed" during migration

**Solution:**
```powershell
# Remove last migration
dotnet ef migrations remove

# Clean and rebuild
dotnet clean
dotnet build

# Add migration again
dotnet ef migrations add InitialCreate

# Apply migration
dotnet ef database update
```

### Issue #6: NuGet Package Restore Issues

**Symptoms:**
- Missing package references
- Build errors about missing namespaces

**Solution:**
```powershell
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore with force
dotnet restore --force

# Rebuild solution
dotnet build
```

### Issue #7: HTTPS Certificate Errors

**Symptoms:**
- "Unable to configure HTTPS endpoint"
- Browser shows certificate warnings

**Solution:**
```powershell
# Clean existing certificates
dotnet dev-certs https --clean

# Create and trust new certificate
dotnet dev-certs https --trust
```

### Diagnostic Commands

```powershell
# Check .NET SDK version
dotnet --version

# Check installed SDKs
dotnet --list-sdks

# Check SQL Server
sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT @@VERSION"

# View current migrations
dotnet ef migrations list

# Check current environment
$env:ASPNETCORE_ENVIRONMENT

# Test health endpoint
Invoke-RestMethod -Uri "https://localhost:5001/health"
```

---

## ?? Dependencies & Packages

### Core Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| **Microsoft.EntityFrameworkCore** | 10.0.1 | ORM framework |
| **Microsoft.EntityFrameworkCore.SqlServer** | 10.0.1 | SQL Server provider |
| **Microsoft.EntityFrameworkCore.Design** | 10.0.1 | Design-time tools |
| **MediatR** | 12.4.1 | CQRS implementation |
| **FluentValidation.DependencyInjectionExtensions** | 11.10.0 | Validation |
| **AutoMapper.Extensions.Microsoft.DependencyInjection** | 12.0.1 | Object mapping |

### Logging

| Package | Version | Purpose |
|---------|---------|---------|
| **Serilog.AspNetCore** | 8.0.3 | Logging framework |
| **Serilog.Sinks.Console** | 6.0.0 | Console output |
| **Serilog.Sinks.File** | 6.0.0 | File output |

### API Features

| Package | Version | Purpose |
|---------|---------|---------|
| **Swashbuckle.AspNetCore** | 10.1.0 | Swagger/OpenAPI |
| **Asp.Versioning.Mvc** | 8.1.0 | API versioning |
| **Asp.Versioning.Mvc.ApiExplorer** | 8.1.0 | Version discovery |

### Health Checks

| Package | Version | Purpose |
|---------|---------|---------|
| **AspNetCore.HealthChecks.UI.Client** | 9.0.0 | Health check UI |
| **AspNetCore.HealthChecks.SqlServer** | 9.0.0 | SQL Server health check |

### Package Version Notes

**Important:** The project uses updated versions to match .NET 10:
- HealthChecks packages: 8.x ? **9.0.0** ?
- MediatR: **12.4.1** (requires `AddOpenBehavior` for pipeline behaviors) ?

### Checking for Updates

```powershell
# List all packages
dotnet list package

# List outdated packages
dotnet list package --outdated

# Update specific package
dotnet add package PackageName --version x.x.x

# Update all packages (use with caution)
# Review changes carefully before updating major versions
```

---

## ?? Database Setup & Migrations

### Connection String Configuration

**Development (LocalDB):**
```json
// appsettings.Development.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BaseApiDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

**Production (SQL Server):**
```json
// appsettings.Production.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-server;Database=BaseApiDb;User Id=app_user;Password=StrongPassword123;TrustServerCertificate=True"
  }
}
```

**Using User Secrets (Recommended for development):**
```powershell
# Initialize user secrets
dotnet user-secrets init

# Set connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=...;Password=secret;"

# List secrets
dotnet user-secrets list

# Remove secret
dotnet user-secrets remove "ConnectionStrings:DefaultConnection"
```

### Entity Framework Core Commands

**Create Migration:**
```powershell
# Basic migration
dotnet ef migrations add MigrationName

# With specific project
dotnet ef migrations add MigrationName --project BaseAPI

# With context specified
dotnet ef migrations add MigrationName --context ApplicationDbContext
```

**Apply Migration:**
```powershell
# Update to latest migration
dotnet ef database update

# Update to specific migration
dotnet ef database update MigrationName

# Rollback to previous migration
dotnet ef database update PreviousMigrationName

# View SQL without applying
dotnet ef migrations script
dotnet ef migrations script FromMigration ToMigration
```

**Remove Migration:**
```powershell
# Remove last migration (only if not applied to database)
dotnet ef migrations remove

# Force remove
dotnet ef migrations remove --force
```

**Database Operations:**
```powershell
# Drop database
dotnet ef database drop

# Drop database without confirmation
dotnet ef database drop --force

# List all migrations
dotnet ef migrations list

# Check which migrations have been applied
dotnet ef migrations has-pending-model-changes
```

### Database Schema

The current schema includes:

**Products Table:**
```sql
CREATE TABLE [dbo].[Products] (
    [Id] uniqueidentifier NOT NULL PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    [Name] nvarchar(200) NOT NULL,
    [Description] nvarchar(1000) NULL,
    [Price] decimal(18,2) NOT NULL,
    [Stock] int NOT NULL,
    [Category] nvarchar(100) NOT NULL,
    [IsDeleted] bit NOT NULL DEFAULT 0,
    [CreatedAt] datetime2(7) NOT NULL,
    [CreatedBy] nvarchar(100) NULL,
    [UpdatedAt] datetime2(7) NULL,
    [UpdatedBy] nvarchar(100) NULL
);

CREATE INDEX [IX_Products_IsDeleted] ON [dbo].[Products] ([IsDeleted]);
CREATE INDEX [IX_Products_Category] ON [dbo].[Products] ([Category]);
```

### Soft Delete Implementation

All entities inherit from `BaseEntity` which includes `IsDeleted` flag. A global query filter automatically excludes soft-deleted records:

```csharp
// ApplicationDbContext.cs
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Global query filter for soft delete
    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
        if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
        {
            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var body = Expression.Equal(
                Expression.Property(parameter, nameof(BaseEntity.IsDeleted)),
                Expression.Constant(false));
            modelBuilder.Entity(entityType.ClrType)
                .HasQueryFilter(Expression.Lambda(body, parameter));
        }
    }
}
```

### Database Seeding

To add seed data, create a seeding class:

```csharp
// Infrastructure/Persistence/Seeds/ProductSeeder.cs
public static class ProductSeeder
{
    public static void SeedProducts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sample Product",
                Description = "Sample description",
                Price = 99.99m,
                Stock = 100,
                Category = "Electronics",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            }
        );
    }
}

// Call in ApplicationDbContext
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    ProductSeeder.SeedProducts(modelBuilder);
}
```

### Backup & Restore

```powershell
# Backup database
sqlcmd -S (localdb)\mssqllocaldb -Q "BACKUP DATABASE BaseApiDb TO DISK='C:\Backups\BaseApiDb.bak'"

# Restore database
sqlcmd -S (localdb)\mssqllocaldb -Q "RESTORE DATABASE BaseApiDb FROM DISK='C:\Backups\BaseApiDb.bak' WITH REPLACE"
```

---

## ????? Development Workflow

### Adding a New Feature

Follow these steps to add a new entity (e.g., "Category"):

#### 1. Create Domain Entity
```csharp
// Domain/Entities/Category.cs
public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}
```

#### 2. Add Entity Configuration
```csharp
// Infrastructure/Persistence/Configurations/CategoryConfiguration.cs
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(c => c.Description)
            .HasMaxLength(500);
        
        builder.HasIndex(c => c.Name);
    }
}
```

#### 3. Add DbSet to Context
```csharp
// Infrastructure/Persistence/ApplicationDbContext.cs
public DbSet<Category> Categories => Set<Category>();
```

#### 4. Create and Apply Migration
```powershell
dotnet ef migrations add Add_Categories
dotnet ef database update
```

#### 5. Create DTO
```csharp
// Application/Categories/DTOs/CategoryDto.cs
public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
```

#### 6. Create AutoMapper Profile
```csharp
// Application/Categories/DTOs/CategoryMappingProfile.cs
public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, Category>();
    }
}
```

#### 7. Create Commands

**Create Command:**
```csharp
// Application/Categories/Commands/CreateCategory/CreateCategoryCommand.cs
public record CreateCategoryCommand : IRequest<Result<Guid>>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
}

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<Guid>>
{
    private readonly IRepository<Category> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(
        IRepository<Category> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request);
        
        await _repository.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.SuccessResult(category.Id, "Category created successfully");
    }
}
```

**Update Command:**
```csharp
// Application/Categories/Commands/UpdateCategory/UpdateCategoryCommand.cs
public record UpdateCategoryCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
}

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
        RuleFor(v => v.Name).NotEmpty().MaximumLength(100);
    }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<Unit>>
{
    private readonly IRepository<Category> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(
        IRepository<Category> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Unit>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (category == null)
            throw new NotFoundException(nameof(Category), request.Id);

        _mapper.Map(request, category);
        
        await _repository.UpdateAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Unit>.SuccessResult(Unit.Value, "Category updated successfully");
    }
}
```

**Delete Command:**
```csharp
// Application/Categories/Commands/DeleteCategory/DeleteCategoryCommand.cs
public record DeleteCategoryCommand(Guid Id) : IRequest<Result<Unit>>;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<Unit>>
{
    private readonly IRepository<Category> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(
        IRepository<Category> repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (category == null)
            throw new NotFoundException(nameof(Category), request.Id);

        await _repository.DeleteAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Unit>.SuccessResult(Unit.Value, "Category deleted successfully");
    }
}
```

#### 8. Create Queries

**Get All Query:**
```csharp
// Application/Categories/Queries/GetCategories/GetCategoriesQuery.cs
public record GetCategoriesQuery : IRequest<Result<IEnumerable<CategoryDto>>>;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Result<IEnumerable<CategoryDto>>>
{
    private readonly IRepository<Category> _repository;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(IRepository<Category> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAllAsync(cancellationToken);
        var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
        
        return Result<IEnumerable<CategoryDto>>.SuccessResult(categoriesDto);
    }
}
```

**Get By ID Query:**
```csharp
// Application/Categories/Queries/GetCategoryById/GetCategoryByIdQuery.cs
public record GetCategoryByIdQuery(Guid Id) : IRequest<Result<CategoryDto>>;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
{
    private readonly IRepository<Category> _repository;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IRepository<Category> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (category == null)
            throw new NotFoundException(nameof(Category), request.Id);

        var categoryDto = _mapper.Map<CategoryDto>(category);
        
        return Result<CategoryDto>.SuccessResult(categoryDto);
    }
}
```

#### 9. Create Controller
```csharp
// API/Controllers/V1/CategoriesController.cs
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<CategoryDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetCategoriesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Result<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(id));
        return Ok(result);
    }
}
```

#### 10. Test the API

**Using Swagger:**
1. Run application
2. Navigate to https://localhost:5001/swagger
3. Find "Categories" section
4. Test each endpoint

**Using PowerShell:**
```powershell
# Create category
$body = @{
    name = "Electronics"
    description = "Electronic devices and accessories"
    icon = "??"
} | ConvertTo-Json

$result = Invoke-RestMethod -Uri "https://localhost:5001/api/v1/categories" `
    -Method Post -Body $body -ContentType "application/json"

# Get all categories
Invoke-RestMethod -Uri "https://localhost:5001/api/v1/categories" -Method Get

# Get by ID
Invoke-RestMethod -Uri "https://localhost:5001/api/v1/categories/$($result.data)" -Method Get

# Update category
$updateBody = @{
    id = $result.data
    name = "Electronics & Gadgets"
    description = "Updated description"
    icon = "??"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/v1/categories/$($result.data)" `
    -Method Put -Body $updateBody -ContentType "application/json"

# Delete category
Invoke-RestMethod -Uri "https://localhost:5001/api/v1/categories/$($result.data)" -Method Delete
```

### Development Best Practices Checklist

When adding new features, ensure:

- [ ] Entity inherits from `BaseEntity`
- [ ] Entity configuration created with Fluent API
- [ ] Migration created and applied
- [ ] DTO created (don't expose entities directly)
- [ ] AutoMapper profile configured
- [ ] Commands have validators (FluentValidation)
- [ ] Handlers use repository pattern
- [ ] Controller uses MediatR (thin controllers)
- [ ] Proper HTTP status codes returned
- [ ] Swagger documentation attributes added
- [ ] Unit tests written for handlers
- [ ] Integration tests for controller endpoints

---

## ?? API Documentation & Testing

### Swagger UI

**Access Swagger:**
- URL: https://localhost:5001/swagger
- Only available in Development environment (by default)

**Features:**
- Interactive API testing
- Request/response examples
- Schema documentation
- Authentication testing (when implemented)

### API Endpoints

#### Products API (v1)

**Base URL:** `https://localhost:5001/api/v1/products`

| Method | Endpoint | Description | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| GET | `/` | Get all products | None | `Result<IEnumerable<ProductDto>>` |
| GET | `/{id}` | Get product by ID | None | `Result<ProductDto>` |
| POST | `/` | Create product | `CreateProductCommand` | `Result<Guid>` |
| PUT | `/{id}` | Update product | `UpdateProductCommand` | `Result<Unit>` |
| DELETE | `/{id}` | Delete product (soft) | None | `Result<Unit>` |

**Example Request/Response:**

**POST /api/v1/products**
```json
// Request
{
  "name": "Gaming Laptop",
  "description": "High-performance gaming laptop with RTX 4080",
  "price": 2499.99,
  "stock": 25,
  "category": "Electronics"
}

// Response (201 Created)
{
  "success": true,
  "data": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "message": "Product created successfully",
  "errors": [],
  "statusCode": 200
}
```

**GET /api/v1/products**
```json
// Response (200 OK)
{
  "success": true,
  "data": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "Gaming Laptop",
      "description": "High-performance gaming laptop with RTX 4080",
      "price": 2499.99,
      "stock": 25,
      "category": "Electronics",
      "createdAt": "2026-01-28T10:00:00Z",
      "createdBy": "System"
    }
  ],
  "message": "Products retrieved successfully",
  "errors": [],
  "statusCode": 200
}
```

**Validation Error (400 Bad Request):**
```json
{
  "success": false,
  "message": "One or more validation errors occurred",
  "errors": [
    "Name is required.",
    "Price must be greater than 0."
  ],
  "statusCode": 400
}
```

**Not Found (404 Not Found):**
```json
{
  "success": false,
  "message": "Product with ID '3fa85f64-5717-4562-b3fc-2c963f66afa6' was not found.",
  "errors": [],
  "statusCode": 404
}
```

### Health Check Endpoints

**Full Health Check:** `GET /health`
```json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.0234567",
  "entries": {
    "database": {
      "status": "Healthy",
      "description": null,
      "duration": "00:00:00.0123456",
      "data": {}
    },
    "self": {
      "status": "Healthy",
      "description": null,
      "duration": "00:00:00.0000001",
      "data": {}
    }
  }
}
```

**Liveness Probe:** `GET /health/live`
```json
{
  "status": "Healthy"
}
```

**Readiness Probe:** `GET /health/ready`
```json
{
  "status": "Healthy"
}
```

### Testing with cURL

```bash
# Get all products
curl -X GET "https://localhost:5001/api/v1/products" -k

# Create product
curl -X POST "https://localhost:5001/api/v1/products" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test Product",
    "description": "Test description",
    "price": 99.99,
    "stock": 100,
    "category": "Test"
  }' -k

# Get product by ID
curl -X GET "https://localhost:5001/api/v1/products/{id}" -k

# Update product
curl -X PUT "https://localhost:5001/api/v1/products/{id}" \
  -H "Content-Type: application/json" \
  -d '{
    "id": "{id}",
    "name": "Updated Product",
    "description": "Updated description",
    "price": 149.99,
    "stock": 50,
    "category": "Test"
  }' -k

# Delete product
curl -X DELETE "https://localhost:5001/api/v1/products/{id}" -k

# Health check
curl -X GET "https://localhost:5001/health" -k
```

### Testing with Postman

1. **Import Collection:**
   - Create a new collection named "BaseAPI"
   - Set base URL variable: `{{baseUrl}}` = `https://localhost:5001`

2. **Create Requests:**
   - GET `{{baseUrl}}/api/v1/products`
   - POST `{{baseUrl}}/api/v1/products` (with JSON body)
   - GET `{{baseUrl}}/api/v1/products/{{productId}}`
   - PUT `{{baseUrl}}/api/v1/products/{{productId}}`
   - DELETE `{{baseUrl}}/api/v1/products/{{productId}}`

3. **Environment Variables:**
   - `baseUrl`: https://localhost:5001
   - `productId`: (save from POST response)

---

## ? Best Practices

### Clean Architecture Rules

**DO:**
- ? Keep Domain layer independent (no external dependencies)
- ? Make Application layer define interfaces for Infrastructure
- ? Implement Infrastructure interfaces in Infrastructure layer
- ? Keep API layer thin (delegate to MediatR)

**DON'T:**
- ? Reference Infrastructure from Domain
- ? Put business logic in controllers
- ? Mix concerns across layers
- ? Use concrete types in Application layer

### CQRS Guidelines

**Commands (Write Operations):**
- Use imperative verbs: `CreateProductCommand`, `UpdateProductCommand`
- Single responsibility
- Always validate with FluentValidation
- Return `Result<T>` type

**Queries (Read Operations):**
- Describe what's being retrieved: `GetProductsQuery`, `GetProductByIdQuery`
- Should NOT modify state
- Can bypass repository for complex queries (performance)
- Return DTOs, never entities

### Repository Pattern

**When to Use Generic Repository:**
```csharp
// Simple CRUD operations
var product = await _repository.GetByIdAsync(id);
await _repository.AddAsync(product);
await _repository.UpdateAsync(product);
await _repository.DeleteAsync(product);
```

**When to Create Specific Repository:**
```csharp
// Complex queries, business-specific operations
public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold);
    Task<Product?> GetProductWithReviewsAsync(Guid id);
}
```

### Validation Layers

**1. API Layer (Model Binding):**
```csharp
[Required]
public string Name { get; set; }

[Range(0, 1000000)]
public decimal Price { get; set; }
```

**2. Application Layer (FluentValidation):**
```csharp
RuleFor(x => x.Name)
    .NotEmpty().WithMessage("Name is required")
    .MaximumLength(200).WithMessage("Name too long");

RuleFor(x => x.Price)
    .GreaterThan(0).WithMessage("Price must be positive");
```

**3. Domain Layer (Invariants):**
```csharp
public class Product : BaseEntity
{
    public void ApplyDiscount(decimal percentage)
    {
        if (percentage < 0 || percentage > 100)
            throw new ArgumentException("Invalid discount");
            
        Price = Price * (1 - percentage / 100);
    }
}
```

### Exception Handling

**Custom Exceptions:**
```csharp
// 404 - Resource not found
throw new NotFoundException(nameof(Product), id);

// 400 - Validation errors
throw new ValidationException(validationErrors);

// 400 - Business rule violation
throw new BadRequestException("Insufficient stock");
```

**Global Exception Handler:**
All exceptions are caught by `ExceptionHandlingMiddleware` and converted to appropriate HTTP responses.

### Performance Optimization

**Database Queries:**
```csharp
// ? Good - Project only needed fields
await _context.Products
    .Select(p => new ProductDto { Id = p.Id, Name = p.Name })
    .ToListAsync();

// ? Good - Use pagination
await _context.Products
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToListAsync();

// ? Bad - Load all data
var products = await _context.Products.ToListAsync();
```

**Async/Await:**
```csharp
// ? Always use async for I/O operations
public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken ct)
{
    var product = await _repository.GetByIdAsync(request.Id, ct);
    return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
}
```

### Security Checklist

- [ ] Validate all input
- [ ] Use parameterized queries (EF Core handles this)
- [ ] Never log sensitive data
- [ ] Use HTTPS only
- [ ] Implement authentication/authorization
- [ ] Configure CORS appropriately
- [ ] Use secure connection strings (User Secrets, Azure Key Vault)
- [ ] Implement rate limiting (future)

### Code Quality

**Naming Conventions:**
- Classes: `PascalCase` (Product, ProductService)
- Methods: `PascalCase` (GetProduct, CreateProduct)
- Variables: `camelCase` (product, productId)
- Constants: `UPPER_CASE` (MAX_RETRY_COUNT)
- Interfaces: `IPascalCase` (IRepository, IProductService)

**SOLID Principles:**
- **S**ingle Responsibility - One class, one purpose
- **O**pen/Closed - Open for extension, closed for modification
- **L**iskov Substitution - Derived classes must be substitutable
- **I**nterface Segregation - Many specific interfaces > one general interface
- **D**ependency Inversion - Depend on abstractions, not concretions

---

## ?? Production Deployment

### Pre-Deployment Checklist

- [ ] Update connection string for production database
- [ ] Set `ASPNETCORE_ENVIRONMENT=Production`
- [ ] Disable Swagger in production (already configured)
- [ ] Configure secure CORS policy
- [ ] Implement authentication/authorization
- [ ] Set up logging (Application Insights, Serilog, etc.)
- [ ] Configure health checks for monitoring
- [ ] Run database migrations
- [ ] Test all endpoints
- [ ] Set up CI/CD pipeline
- [ ] Configure secrets management (Azure Key Vault, AWS Secrets Manager)

### Environment Configuration

**Production appsettings:**
```json
// appsettings.Production.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-server;Database=BaseApiDb;User Id=app_user;Password=#{DbPassword}#"
  },
  "AllowedHosts": "yourdomain.com",
  "ApiSettings": {
    "ApiName": "Base API",
    "ApiVersion": "v1"
  }
}
```

### Database Migration in Production

```powershell
# Generate SQL script
dotnet ef migrations script --output migration.sql

# Review SQL script before applying
# Apply to production database using SQL Server Management Studio or:
sqlcmd -S prod-server -d BaseApiDb -i migration.sql
```

### Docker Support (Optional)

**Dockerfile:**
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["BaseAPI/BaseAPI.csproj", "BaseAPI/"]
RUN dotnet restore "BaseAPI/BaseAPI.csproj"
COPY . .
WORKDIR "/src/BaseAPI"
RUN dotnet build "BaseAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BaseAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaseAPI.dll"]
```

**docker-compose.yml:**
```yaml
version: '3.8'

services:
  api:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "5000:80"
      - "5001:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Server=db;Database=BaseApiDb;User Id=sa;Password=YourStrong@Passw0rd
    depends_on:
      - db

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong@Passw0rd
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql

volumes:
  sqldata:
```

### Monitoring & Observability

**Application Insights (Azure):**
```csharp
// Program.cs
builder.Services.AddApplicationInsightsTelemetry();
```

**Health Checks for Kubernetes:**
```yaml
# kubernetes/deployment.yaml
livenessProbe:
  httpGet:
    path: /health/live
    port: 80
  initialDelaySeconds: 10
  periodSeconds: 10

readinessProbe:
  httpGet:
    path: /health/ready
    port: 80
  initialDelaySeconds: 5
  periodSeconds: 5
```

---

## ? FAQ & Support

### Frequently Asked Questions

**Q: Why am I getting "AggregateException" on startup?**
A: This was caused by incorrect MediatR registration. It's fixed in the current version using `AddOpenBehavior`.

**Q: Can I use a different database instead of SQL Server?**
A: Yes, change the provider in `DependencyInjection.cs`:
```csharp
// PostgreSQL
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

// MySQL
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(configuration.GetConnectionString("DefaultConnection")));

// SQLite (for testing)
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
```

**Q: How do I add authentication?**
A: Install `Microsoft.AspNetCore.Authentication.JwtBearer` and configure in `Program.cs`:
```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Then use [Authorize] attribute on controllers/actions
```

**Q: How do I implement caching?**
A: Add caching services and use in handlers:
```csharp
// Program.cs
builder.Services.AddMemoryCache();
builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// In handler
private readonly IMemoryCache _cache;

public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken ct)
{
    var cacheKey = $"product-{request.Id}";
    
    if (!_cache.TryGetValue(cacheKey, out ProductDto productDto))
    {
        var product = await _repository.GetByIdAsync(request.Id, ct);
        productDto = _mapper.Map<ProductDto>(product);
        
        _cache.Set(cacheKey, productDto, TimeSpan.FromMinutes(5));
    }
    
    return Result<ProductDto>.Success(productDto);
}
```

**Q: Can I see actual SQL queries being executed?**
A: Yes, enable EF Core logging:
```json
// appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

**Q: How do I implement pagination?**
A: Create a paginated result model and update queries:
```csharp
public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}

// In query
public record GetProductsQuery(int Page = 1, int PageSize = 10) 
    : IRequest<Result<PaginatedResult<ProductDto>>>;

// In handler
var query = _context.Products.AsQueryable();
var totalCount = await query.CountAsync(ct);
var items = await query
    .Skip((request.Page - 1) * request.PageSize)
    .Take(request.PageSize)
    .ToListAsync(ct);

var result = new PaginatedResult<ProductDto>
{
    Items = _mapper.Map<IEnumerable<ProductDto>>(items),
    PageNumber = request.Page,
    PageSize = request.PageSize,
    TotalCount = totalCount
};
```

**Q: How do I handle file uploads?**
A: Add file upload endpoint:
```csharp
[HttpPost("upload")]
public async Task<IActionResult> Upload(IFormFile file)
{
    if (file == null || file.Length == 0)
        return BadRequest("No file uploaded");

    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
    Directory.CreateDirectory(uploadsFolder);

    var fileName = $"{Guid.NewGuid()}_{file.FileName}";
    var filePath = Path.Combine(uploadsFolder, fileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    return Ok(new { fileName, path = filePath });
}
```

**Q: How do I implement background jobs?**
A: Use Hangfire or IHostedService:
```csharp
// Install: Install-Package Hangfire.AspNetCore

// Program.cs
builder.Services.AddHangfire(configuration => configuration
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

// Add recurring job
RecurringJob.AddOrUpdate("cleanup-job", 
    () => CleanupExpiredRecords(), 
    Cron.Daily);
```

### Getting Help

**Check Logs:**
```powershell
# View recent logs
Get-Content logs/log-*.txt -Tail 50

# Watch logs in real-time
Get-Content logs/log-*.txt -Wait
```

**Common Log Locations:**
- Application logs: `BaseAPI/logs/log-YYYYMMDD.txt`
- Console output: Visual Studio Output window (Debug pane)
- Health checks: `https://localhost:5001/health`

**Diagnostic Endpoints:**
- Health: `/health`
- Swagger: `/swagger` (Development only)

**Community & Support:**
- GitHub Repository: https://github.com/lreb/BaseProject
- Create an issue for bugs or questions
- Check existing issues for common problems

### Useful Resources

**Official Documentation:**
- [ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [MediatR](https://github.com/jbogard/MediatR)
- [FluentValidation](https://docs.fluentvalidation.net)
- [AutoMapper](https://docs.automapper.org)
- [Serilog](https://serilog.net)

**Clean Architecture:**
- [Clean Architecture by Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Microsoft Clean Architecture Template](https://github.com/jasontaylordev/CleanArchitecture)

**Design Patterns:**
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)
- [Repository Pattern](https://martinfowler.com/eaaCatalog/repository.html)

---

## ?? Quick Reference

### Essential Commands

```powershell
# Build & Run
dotnet build
dotnet run
dotnet watch run

# Database
dotnet ef migrations add MigrationName
dotnet ef database update
dotnet ef migrations remove
dotnet ef database drop

# Testing
dotnet test
dotnet test --logger "console;verbosity=detailed"

# Clean & Restore
dotnet clean
dotnet restore
dotnet nuget locals all --clear
```

### Project Checklist

**Starting Development:**
- [ ] SQL Server/LocalDB running
- [ ] .NET 10 SDK installed
- [ ] Database migrations applied
- [ ] Dependencies restored
- [ ] HTTPS certificate trusted

**Before Committing:**
- [ ] Code compiles without errors
- [ ] All tests pass
- [ ] Database migrations included (if schema changed)
- [ ] Logging doesn't contain sensitive data
- [ ] Followed coding conventions

**Before Deploying:**
- [ ] All tests pass
- [ ] Database backup created
- [ ] Connection strings updated
- [ ] Environment variables set
- [ ] Health checks working
- [ ] Monitoring configured

---

## ?? Conclusion

You now have a complete understanding of the BaseAPI project! This guide covers:

? **Architecture** - Clean Architecture with 4 layers  
? **Setup** - Quick start and troubleshooting  
? **Development** - Adding features, best practices  
? **Testing** - API documentation, health checks  
? **Deployment** - Production considerations  
? **Support** - FAQ, resources, commands  

### Next Steps

1. **Explore the code** - Start with `Program.cs` and follow the flow
2. **Create a new feature** - Follow the "Adding a New Feature" guide
3. **Add authentication** - Implement JWT or OAuth
4. **Set up CI/CD** - Automate builds and deployments
5. **Add more features** - Background jobs, file uploads, caching, etc.

### Stay Updated

This project follows:
- ? Clean Architecture principles
- ? SOLID principles
- ? DRY (Don't Repeat Yourself)
- ? KISS (Keep It Simple, Stupid)
- ? Industry best practices

**Happy coding! ??**

---

*Last Updated: January 28, 2026*  
*Version: 1.0*  
*Project: BaseAPI - Clean Architecture Template*
