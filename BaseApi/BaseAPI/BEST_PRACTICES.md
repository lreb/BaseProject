# Best Practices Guide

## ?? Table of Contents
1. [Clean Architecture Principles](#clean-architecture-principles)
2. [CQRS Best Practices](#cqrs-best-practices)
3. [Repository Pattern Guidelines](#repository-pattern-guidelines)
4. [Validation Best Practices](#validation-best-practices)
5. [Exception Handling](#exception-handling)
6. [API Design Guidelines](#api-design-guidelines)
7. [Performance Optimization](#performance-optimization)
8. [Security Best Practices](#security-best-practices)
9. [Testing Guidelines](#testing-guidelines)
10. [Code Quality Standards](#code-quality-standards)

---

## Clean Architecture Principles

### Dependency Rule
> **"Source code dependencies must point only inward, toward higher-level policies."**

**DO:**
```csharp
// Application layer depends on Domain abstractions
public interface IRepository<T> { } // In Application layer

// Infrastructure implements Application interfaces
public class Repository<T> : IRepository<T> { } // In Infrastructure layer
```

**DON'T:**
```csharp
// Domain depending on Infrastructure (WRONG!)
public class Product : BaseEntity
{
    public DbContext Context { get; set; } // ? Never!
}
```

### Layer Responsibilities

#### Domain Layer
- Contains enterprise business logic
- Defines entities and value objects
- No external dependencies
- Framework-independent

**Example:**
```csharp
public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    
    public void ApplyDiscount(decimal percentage)
    {
        if (percentage < 0 || percentage > 100)
            throw new ArgumentException("Invalid discount percentage");
            
        Price = Price * (1 - percentage / 100);
    }
}
```

#### Application Layer
- Contains application business logic
- Defines use cases (Commands/Queries)
- Defines interfaces for infrastructure
- Orchestrates domain objects

#### Infrastructure Layer
- Implements Application interfaces
- Database access, file system, external APIs
- Framework-specific implementations

#### Presentation Layer (API)
- HTTP concerns only
- Input validation (basic)
- Response formatting
- Authentication/Authorization

---

## CQRS Best Practices

### Command Naming
Commands should be **imperative verbs** describing actions:
- ? `CreateProductCommand`
- ? `UpdateProductCommand`
- ? `DeleteProductCommand`
- ? `ProductCommand`
- ? `ProductCreation`

### Query Naming
Queries should describe **what data is being retrieved**:
- ? `GetProductsQuery`
- ? `GetProductByIdQuery`
- ? `GetProductsByCategoryQuery`
- ? `ProductQuery`

### Command Structure
```csharp
// Command - What to do
public record CreateProductCommand : IRequest<Result<Guid>>
{
    public string Name { get; init; }
    public decimal Price { get; init; }
}

// Validator - Validation rules
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}

// Handler - How to do it
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken ct)
    {
        // Implementation
    }
}
```

### Separation of Concerns
**DO:**
- Keep commands simple and focused
- One command = One action
- Queries should not modify state

**DON'T:**
- Mix commands and queries
- Put business logic in controllers
- Return different types from same handler

---

## Repository Pattern Guidelines

### Generic Repository
Use for **common CRUD operations**:

```csharp
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
    Task<T> AddAsync(T entity, CancellationToken ct = default);
    Task UpdateAsync(T entity, CancellationToken ct = default);
    Task DeleteAsync(T entity, CancellationToken ct = default);
}
```

### Specific Repository
Create when you need **entity-specific operations**:

```csharp
public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold);
    Task<decimal> GetAveragePriceAsync();
}
```

### When to Use What
- **Generic Repository**: Standard CRUD operations
- **Specific Repository**: Complex queries, business-specific operations
- **Direct DbContext**: Very complex queries, reporting, migrations

---

## Validation Best Practices

### Validation Layers

1. **Model Binding Validation** (API Layer)
   - Data type validation
   - Required fields
   - Basic format validation

2. **FluentValidation** (Application Layer)
   - Business rule validation
   - Complex validation logic
   - Cross-field validation

3. **Domain Validation** (Domain Layer)
   - Invariants
   - Entity-specific rules
   - Business logic validation

### FluentValidation Examples

```csharp
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        // Required fields
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

        // Value range
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .LessThan(1000000).WithMessage("Price cannot exceed 1,000,000");

        // Complex validation
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative")
            .Must((command, stock) => ValidateStockForCategory(command.Category, stock))
            .WithMessage("Invalid stock level for this category");

        // Conditional validation
        When(x => x.Category == "Electronics", () =>
        {
            RuleFor(x => x.WarrantyPeriod).GreaterThan(0);
        });
    }

    private bool ValidateStockForCategory(string category, int stock)
    {
        // Custom validation logic
        return true;
    }
}
```

---

## Exception Handling

### Exception Hierarchy

```
Exception
??? NotFoundException (404)
??? ValidationException (400)
??? BadRequestException (400)
??? UnauthorizedException (401)
??? ForbiddenException (403)
```

### When to Use Custom Exceptions

```csharp
// NOT FOUND - Resource doesn't exist
if (product == null)
    throw new NotFoundException(nameof(Product), id);

// VALIDATION - Business rule violation
if (errors.Any())
    throw new ValidationException(errors);

// BAD REQUEST - Invalid operation
if (product.Stock < quantity)
    throw new BadRequestException("Insufficient stock");

// UNAUTHORIZED - Authentication failed
if (!IsAuthenticated())
    throw new UnauthorizedException();
```

### Exception Handling in Middleware

```csharp
// Global exception handler captures all exceptions
public class ExceptionHandlingMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
}
```

---

## API Design Guidelines

### RESTful Principles

```
GET     /api/v1/products          - Get all products
GET     /api/v1/products/{id}     - Get specific product
POST    /api/v1/products          - Create product
PUT     /api/v1/products/{id}     - Update product
DELETE  /api/v1/products/{id}     - Delete product
```

### HTTP Status Codes

- **200 OK** - Successful GET, PUT
- **201 Created** - Successful POST
- **204 No Content** - Successful DELETE
- **400 Bad Request** - Validation error
- **401 Unauthorized** - Authentication required
- **403 Forbidden** - Access denied
- **404 Not Found** - Resource not found
- **500 Internal Server Error** - Unexpected error

### Response Format

**Success Response:**
```json
{
  "success": true,
  "data": { },
  "message": "Operation successful",
  "statusCode": 200
}
```

**Error Response:**
```json
{
  "success": false,
  "message": "Validation failed",
  "errors": [
    "Name is required",
    "Price must be greater than 0"
  ],
  "statusCode": 400
}
```

### API Versioning

```csharp
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
}
```

---

## Performance Optimization

### Database Queries

**DO:**
```csharp
// Use async/await
await _context.Products.ToListAsync(cancellationToken);

// Project only needed fields
await _context.Products
    .Select(p => new ProductDto { Id = p.Id, Name = p.Name })
    .ToListAsync();

// Use pagination
await _context.Products
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToListAsync();
```

**DON'T:**
```csharp
// Avoid synchronous calls
_context.Products.ToList(); // ?

// Don't load unnecessary data
var products = await _context.Products
    .Include(p => p.Reviews)
    .Include(p => p.Images)
    .ToListAsync(); // Then only use Id and Name ?

// Avoid N+1 queries
foreach (var product in products)
{
    var category = await _context.Categories.FindAsync(product.CategoryId); // ?
}
```

### Caching Strategy

```csharp
// Memory cache for frequently accessed data
public class ProductService
{
    private readonly IMemoryCache _cache;
    
    public async Task<Product> GetProductAsync(Guid id)
    {
        if (!_cache.TryGetValue(id, out Product product))
        {
            product = await _repository.GetByIdAsync(id);
            _cache.Set(id, product, TimeSpan.FromMinutes(5));
        }
        return product;
    }
}
```

### Asynchronous Operations

```csharp
// Always use async for I/O operations
public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken ct)
{
    var product = await _repository.GetByIdAsync(request.Id, ct);
    return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
}
```

---

## Security Best Practices

### Input Validation
- Validate all input data
- Use parameterized queries
- Sanitize user input
- Validate file uploads

### Authentication & Authorization
```csharp
[Authorize]
[ApiController]
public class ProductsController : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        // Only admins can create products
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Anyone can view products
    }
}
```

### Sensitive Data
- Never log sensitive data (passwords, tokens, credit cards)
- Use secure connection strings
- Encrypt sensitive data at rest
- Use HTTPS only

### CORS
```csharp
// Configure CORS appropriately
builder.Services.AddCors(options =>
{
    options.AddPolicy("Production", policy =>
    {
        policy.WithOrigins("https://yourdomain.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

---

## Testing Guidelines

### Unit Tests
Test business logic in isolation:

```csharp
public class CreateProductCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ReturnsSuccessResult()
    {
        // Arrange
        var repository = new Mock<IRepository<Product>>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var handler = new CreateProductCommandHandler(repository.Object, unitOfWork.Object);
        var command = new CreateProductCommand { Name = "Test", Price = 10 };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        repository.Verify(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
```

### Integration Tests
Test API endpoints:

```csharp
public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetProducts_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/v1/products");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}
```

### Test Coverage
Aim for:
- **80%+ code coverage** for business logic
- **100% coverage** for critical paths
- All validation rules tested
- All exception scenarios tested

---

## Code Quality Standards

### Naming Conventions
- **Classes**: PascalCase (Product, ProductService)
- **Methods**: PascalCase (GetProduct, CreateProduct)
- **Variables**: camelCase (product, productId)
- **Constants**: UPPER_CASE (MAX_RETRY_COUNT)
- **Interfaces**: IPascalCase (IRepository, IProductService)

### SOLID Principles

**Single Responsibility Principle**
```csharp
// ? Good - One responsibility
public class ProductValidator
{
    public bool Validate(Product product) { }
}

// ? Bad - Multiple responsibilities
public class ProductManager
{
    public bool Validate(Product product) { }
    public void Save(Product product) { }
    public void SendEmail(Product product) { }
}
```

**Dependency Inversion**
```csharp
// ? Good - Depend on abstractions
public class ProductService
{
    private readonly IRepository<Product> _repository;
}

// ? Bad - Depend on concrete implementations
public class ProductService
{
    private readonly SqlProductRepository _repository;
}
```

### Code Comments
```csharp
// ? Good - Explains WHY
// Using exponential backoff to handle transient failures
await RetryWithExponentialBackoff(operation);

// ? Bad - Explains WHAT (obvious from code)
// Get product by id
var product = await GetProductById(id);
```

---

## Summary Checklist

- [ ] Follow Clean Architecture layers
- [ ] Use CQRS for operations
- [ ] Implement proper validation
- [ ] Handle exceptions globally
- [ ] Follow RESTful principles
- [ ] Optimize database queries
- [ ] Implement security measures
- [ ] Write comprehensive tests
- [ ] Follow SOLID principles
- [ ] Use meaningful names
- [ ] Document complex logic
- [ ] Use async/await properly
- [ ] Implement health checks
- [ ] Use structured logging
- [ ] Version your API

---

**Remember: Best practices are guidelines, not rigid rules. Apply them pragmatically based on your project's needs.**
