# Base API - Clean Architecture Template

> **?? New to this project? Check out the [Complete Project Guide](COMPLETE_PROJECT_GUIDE.md) for comprehensive documentation including setup, troubleshooting, and development workflows.**

A production-ready ASP.NET Core Web API template following Clean Architecture principles with best practices for maintainability, scalability, and testability.

## ?? Documentation

This README provides a quick overview. For detailed information:

- **[?? Complete Project Guide](COMPLETE_PROJECT_GUIDE.md)** - Comprehensive guide (setup to deployment)
- **[?? Startup Troubleshooting](STARTUP_TROUBLESHOOTING.md)** - Common issues & solutions
- **[?? Best Practices](BEST_PRACTICES.md)** - Clean Architecture & coding standards
- **[?? Development Guide](DEVELOPMENT_GUIDE.md)** - Development workflows
- **[? Implementation Summary](IMPLEMENTATION_SUMMARY.md)** - Quick feature reference

## ??? Architecture Overview

This project implements **Clean Architecture** with clear separation of concerns across multiple layers:

```
BaseAPI/
??? Domain/                 # Enterprise Business Logic
?   ??? Common/
?   ?   ??? BaseEntity.cs
?   ?   ??? IAuditableEntity.cs
?   ??? Entities/
?       ??? Product.cs
?
??? Application/            # Application Business Logic
?   ??? Common/
?   ?   ??? Behaviors/      # MediatR Pipeline Behaviors
?   ?   ??? Exceptions/     # Custom Exceptions
?   ?   ??? Interfaces/     # Abstractions
?   ?   ??? Models/         # Shared Model
?   ??? Features/           # Application Features
?       ??? Products/
?       ?   ??? Commands/   # Product Commands
?       ?   ??? Queries/    # Product Queries
?       ?   ??? Events/     # Product Events
?       ??? Orders/
?           ??? Commands/   # Order Commands
?           ??? Queries/    # Order Queries
?           ??? Events/     # Order Events
?
??? Infrastructure/         # Infrastructure & Frameworks
?   ??? Common/
?   ?   ??? Context.cs      # EF Core DbContext
?   ?   ??? Mappings/      # AutoMapper Profiles
?   ??? Persistence/        # Database
?   ?   ??? Configurations/ # EF Core Configuration
?   ?   ??? Repositories/   # Data Repositories
?   ??? Web/                # ASP.NET Core Web API
?   ?   ??? Controllers/    # API Controllers
?   ?   ??? Middleware/     # Custom Middleware
?   ??? HostedServices/      # Background Services
?
??? Tests/                  # Testing Projects
    ??? Application.Tests/  # Application Layer Tests
    ??? Infrastructure.Tests/# Infrastructure Layer Tests
    ??? Web.Tests/          # Web API Tests
```

## ?? Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022/2026 or VS Code

### Quick Start (5 Minutes)

1. **Clone the repository**
   ```bash
   git clone https://github.com/lreb/BaseProject
   cd BaseApi/BaseAPI
   ```

2. **Configure your local database connection**
   
   **Option 1: Using PowerShell Script (Recommended)**
   ```powershell
   .\setup-local-config.ps1
   # Follow the prompts to enter your database credentials
   ```
   
   **Option 2: Manual Setup**
   ```powershell
   # Copy the template
   Copy-Item appsettings.Local.json.template appsettings.Local.json
   
   # Edit with your database credentials
   notepad appsettings.Local.json
   ```
   
   **Option 3: Using .NET User Secrets**
   ```powershell
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=YOUR_HOST;Database=YOUR_DB;Username=YOUR_USER;Password=YOUR_PASS;Port=5432;"
   ```

3. **Restore packages and trust certificate**
   ```bash
   dotnet restore
   dotnet dev-certs https --trust
   ```

4. **Update database** (migrations already created)
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   # Or: dotnet watch run
   ```

6. **Test the API**
   - Swagger: https://localhost:5001/swagger
   - Health: https://localhost:5001/health

**?? Having Issues?** Check the [Troubleshooting Guide](COMPLETE_PROJECT_GUIDE.md#-troubleshooting--common-issues) for solutions.

**?? Configuration Security:** See [Configuration Guide](CONFIGURATION_GUIDE.md) for best practices on managing sensitive settings.

**?? Detailed Setup:** Follow the [5-Minute Setup](COMPLETE_PROJECT_GUIDE.md#5-minute-setup) in the Complete Guide.

### Update connection string

## ??? Adding New Features

### Step-by-Step Guide

1. **Create Domain Entity**
   ```csharp
   // Domain/Entities/YourEntity.cs
   public class YourEntity : BaseEntity { }
   ```

2. **Add Entity Configuration**
   ```csharp
   // Infrastructure/Persistence/Configurations/YourEntityConfiguration.cs
   public class YourEntityConfiguration : IEntityTypeConfiguration<YourEntity> { }
   ```

3. **Create DTOs**
   ```csharp
   // Application/YourFeature/DTOs/YourEntityDto.cs
   ```

4. **Create Commands/Queries**
   ```csharp
   // Application/YourFeature/Commands/CreateYourEntity/CreateYourEntityCommand.cs
   // Application/YourFeature/Queries/GetYourEntity/GetYourEntityQuery.cs
   ```

5. **Add Validators**
   ```csharp
   // In the same file as command/query
   public class CreateYourEntityCommandValidator : AbstractValidator<CreateYourEntityCommand> { }
   ```

6. **Create Controller**
   ```csharp
   // API/Controllers/V1/YourEntitiesController.cs
   ```

**?? Complete Walkthrough:** Follow the detailed [Development Workflow](COMPLETE_PROJECT_GUIDE.md#-development-workflow) with full code examples for adding a Category feature.

## ?? Support

For questions or issues:
- Create an issue in the repository
- Contact the development team
- Check the documentation

### Need Help?

- **?? [Complete Project Guide](COMPLETE_PROJECT_GUIDE.md)** - Full documentation
- **? [FAQ](COMPLETE_PROJECT_GUIDE.md#-faq--support)** - Common questions
- **?? [Troubleshooting](STARTUP_TROUBLESHOOTING.md)** - Known issues
- **?? GitHub Issues** - Report bugs

### Quick Diagnostics

```powershell
# Verify setup
dotnet --version                                    # Check .NET 10
dotnet ef database update                           # Update DB
Invoke-RestMethod https://localhost:5001/health    # Test health

```

## ?? Next Steps

Consider adding:
- [ ] JWT Authentication - [How-to Guide](COMPLETE_PROJECT_GUIDE.md#frequently-asked-questions)
- [ ] Rate Limiting
- [ ] Redis Caching - [Implementation Guide](COMPLETE_PROJECT_GUIDE.md#frequently-asked-questions)
- [ ] Message Queue Integration (RabbitMQ/Azure Service Bus)
- [ ] Docker Support - [Deployment Guide](COMPLETE_PROJECT_GUIDE.md#-production-deployment)
- [ ] CI/CD Pipeline
- [ ] API Gateway Integration
- [ ] Distributed Tracing (OpenTelemetry)
- [ ] GraphQL Support
- [ ] Background Jobs - [Hangfire Setup](COMPLETE_PROJECT_GUIDE.md#frequently-asked-questions)

---

**Built with ?? using Clean Architecture principles**

**?? Ready to start?** Follow the [Quick Start Guide](COMPLETE_PROJECT_GUIDE.md#-quick-start-guide).
