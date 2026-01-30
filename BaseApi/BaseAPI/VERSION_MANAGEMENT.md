# API Version Management Guide

## ?? Overview

This guide explains how to manage and update the API version following **Semantic Versioning (SemVer)** principles.

## ?? Semantic Versioning Format

The API uses semantic versioning in the format: **MAJOR.MINOR.PATCH** (e.g., `1.23.1`)

```
1.23.1
? ?  ?
? ?  ???? PATCH version (bug fixes, patches)
? ??????? MINOR version (new features, backward compatible)
????????? MAJOR version (breaking changes)
```

### Version Increment Rules

| Change Type | Version to Increment | Example | When to Use |
|------------|---------------------|---------|-------------|
| **Breaking Change** | MAJOR | 1.23.1 ? 2.0.0 | API contract changes, removed endpoints, changed request/response structures |
| **New Feature** | MINOR | 1.23.1 ? 1.24.0 | New endpoints, new optional parameters, new features |
| **Bug Fix** | PATCH | 1.23.1 ? 1.23.2 | Bug fixes, security patches, performance improvements |

## ?? How to Update the Version

### Method 1: Update appsettings.json (Recommended)

**File:** `BaseAPI/appsettings.json`

```json
{
  "ApiSettings": {
    "ApiName": "Base API",
    "ApiVersion": "1.24.0",  // <--- Update this line
    "ApiDescription": "A scalable and maintainable ASP.NET Core Web API following Clean Architecture principles"
  }
}
```

### Method 2: Environment-Specific Versions

You can override the version per environment:

**appsettings.Development.json:**
```json
{
  "ApiSettings": {
    "ApiVersion": "1.24.0-dev"
  }
}
```

**appsettings.Production.json:**
```json
{
  "ApiSettings": {
    "ApiVersion": "1.24.0"
  }
}
```

### Method 3: Environment Variables

Override via environment variable:
```bash
export ApiSettings__ApiVersion="1.24.0"
```

Or in Docker:
```yaml
environment:
  - ApiSettings__ApiVersion=1.24.0
```

## ?? Where the Version Appears

### 1. Swagger UI
- **Title**: Shows in the browser tab and header
- **Version Badge**: Displayed prominently at the top
- **Description**: Includes version in the API description

Access: `https://your-api/swagger`

### 2. Swagger JSON
The OpenAPI specification includes the version:
```json
{
  "openapi": "3.0.1",
  "info": {
    "title": "Base API",
    "version": "v1.24.0"
  }
}
```

Access: `https://your-api/swagger/v1/swagger.json`

### 3. Version Endpoint
Programmatic access to version information:

```http
GET /api/v1/version
```

**Response:**
```json
{
  "apiVersion": "1.24.0",
  "apiName": "Base API",
  "assemblyVersion": "1.0.0.0",
  "buildDate": "2024-01-28T10:00:00Z",
  "environment": "Production",
  "frameworkVersion": "10.0.0"
}
```

### 4. Application Logs
Version is logged on startup:
```
[INF] API Version: 1.24.0
[INF] Application started successfully - Version: 1.24.0
```

### 5. HTTP Response Headers
Add version to response headers (optional):

```csharp
app.Use(async (context, next) =>
{
    var apiSettings = context.RequestServices.GetRequiredService<IOptions<ApiSettings>>().Value;
    context.Response.Headers.Add("X-API-Version", apiSettings.ApiVersion);
    await next();
});
```

## ?? Version Update Workflow

### For Bug Fixes (PATCH)

1. **Update Version**
   ```json
   "ApiVersion": "1.23.2"  // Was 1.23.1
   ```

2. **Update Version History in ConfigureSwaggerOptions.cs**
   ```csharp
   Description = $"{_apiSettings.ApiDescription}\n\n**Current Version:** {version}\n\n" +
                 "### Version History\n" +
                 "- **1.23.2** - Fixed product deletion bug\n" +
                 "- **1.23.1** - Bug fixes and performance improvements\n" +
                 "- **1.23.0** - New features added"
   ```

3. **Commit Changes**
   ```bash
   git add appsettings.json API/Configuration/ConfigureSwaggerOptions.cs
   git commit -m "chore: bump version to 1.23.2"
   git tag v1.23.2
   git push && git push --tags
   ```

### For New Features (MINOR)

1. **Update Version**
   ```json
   "ApiVersion": "1.24.0"  // Was 1.23.1
   ```

2. **Update Description**
   ```csharp
   "- **1.24.0** - Added category management endpoints\n" +
   "- **1.23.1** - Bug fixes and performance improvements"
   ```

3. **Commit and Tag**
   ```bash
   git commit -m "feat: add category management (v1.24.0)"
   git tag v1.24.0
   ```

### For Breaking Changes (MAJOR)

1. **Update Version**
   ```json
   "ApiVersion": "2.0.0"  // Was 1.23.1
   ```

2. **Update Description with Migration Guide**
   ```csharp
   Description = $"{_apiSettings.ApiDescription}\n\n**Current Version:** {version}\n\n" +
                 "### ?? Breaking Changes in v2.0.0\n" +
                 "- Product endpoint now requires authentication\n" +
                 "- Changed response format for all endpoints\n" +
                 "- See migration guide: /docs/v2-migration\n\n" +
                 "### Version History\n" +
                 "- **2.0.0** - Major API redesign (BREAKING)\n" +
                 "- **1.23.1** - Final v1 release"
   ```

3. **Create Migration Guide**
   Create `MIGRATION_V2.md` documenting all breaking changes

4. **Commit and Tag**
   ```bash
   git commit -m "feat!: v2.0.0 major API redesign (BREAKING CHANGE)"
   git tag v2.0.0
   ```

## ?? Deployment Best Practices

### Pre-Release Versions

For testing before production:

```json
"ApiVersion": "1.24.0-beta.1"
"ApiVersion": "1.24.0-rc.1"
"ApiVersion": "1.24.0-alpha.1"
```

### Version in CI/CD

**GitHub Actions Example:**
```yaml
name: Deploy API

on:
  push:
    tags:
      - 'v*'

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      
      - name: Extract version
        id: version
        run: echo "VERSION=${GITHUB_REF#refs/tags/v}" >> $GITHUB_OUTPUT
      
      - name: Update appsettings
        run: |
          jq '.ApiSettings.ApiVersion = "${{ steps.version.outputs.VERSION }}"' \
            appsettings.json > tmp.json && mv tmp.json appsettings.json
      
      - name: Build and Deploy
        run: dotnet publish -c Release
```

### Docker with Version

**Dockerfile:**
```dockerfile
ARG VERSION=1.0.0
ENV ApiSettings__ApiVersion=${VERSION}
```

**Build:**
```bash
docker build --build-arg VERSION=1.24.0 -t myapi:1.24.0 .
```

## ?? Version Monitoring

### Health Check with Version
```http
GET /health
```

**Response includes version:**
```json
{
  "status": "Healthy",
  "version": "1.24.0",
  "entries": {
    "database": { "status": "Healthy" }
  }
}
```

### Logging Version in Requests

Add middleware to log version with each request:
```csharp
app.Use(async (context, next) =>
{
    var apiSettings = context.RequestServices.GetRequiredService<IOptions<ApiSettings>>().Value;
    using (LogContext.PushProperty("ApiVersion", apiSettings.ApiVersion))
    {
        await next();
    }
});
```

## ?? Changelog Maintenance

Maintain a `CHANGELOG.md` file:

```markdown
# Changelog

All notable changes to this project will be documented in this file.

## [1.24.0] - 2024-01-28

### Added
- Category management endpoints
- Bulk product import feature

### Changed
- Improved product search performance

### Fixed
- Fixed pagination bug in product listing

## [1.23.1] - 2024-01-15

### Fixed
- Fixed product deletion error
- Security patch for authentication
```

## ?? Version Control Best Practices

1. **Always update version before release**
2. **Create Git tags for each version**
   ```bash
   git tag -a v1.24.0 -m "Release version 1.24.0"
   ```

3. **Use conventional commits**
   - `feat:` for new features (MINOR)
   - `fix:` for bug fixes (PATCH)
   - `feat!:` or `BREAKING CHANGE:` for breaking changes (MAJOR)

4. **Document breaking changes clearly**

5. **Keep version history in Swagger description**

## ?? Quick Reference

| Task | Command / File |
|------|---------------|
| Update version | Edit `appsettings.json` ? `ApiSettings.ApiVersion` |
| View in Swagger | Navigate to `/swagger` |
| Get version programmatically | `GET /api/v1/version` |
| Check in logs | Look for "API Version:" in startup logs |
| Override for environment | Set `ApiSettings__ApiVersion` env variable |
| Create release tag | `git tag v1.24.0 && git push --tags` |

## ?? Troubleshooting

### Version not updating in Swagger
1. Clear browser cache
2. Restart application
3. Check `appsettings.json` is being deployed
4. Verify configuration binding in logs

### Different versions in different environments
- Check environment-specific appsettings files
- Verify environment variables
- Check Docker/Kubernetes configuration

### Version endpoint returns wrong version
- Ensure `ApiSettings` is properly configured in DI container
- Check configuration file is being read correctly

---

**Remember:** Always update the version number BEFORE deploying to production!
