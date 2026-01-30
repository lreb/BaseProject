# ?? Quick Version Update Guide

## How to Update API Version

### Step 1: Update appsettings.json

Edit `BaseAPI/appsettings.json` and change the `ApiVersion`:

```json
{
  "ApiSettings": {
    "ApiName": "Base API",
    "ApiVersion": "1.24.0",  ?? CHANGE THIS
    "ApiDescription": "..."
  }
}
```

### Step 2: Commit and Tag

```bash
git add appsettings.json
git commit -m "chore: bump version to 1.24.0"
git tag v1.24.0
git push && git push --tags
```

### Step 3: Verify

Run the application and check:

1. **Swagger UI**: Navigate to `/swagger` - version appears next to the title
2. **Version Endpoint**: `GET /api/v1/version` 
3. **Response Headers**: Check `X-API-Version` header
4. **Logs**: Look for "API Version:" in startup logs

## ?? Version Format

Follow **Semantic Versioning**: `MAJOR.MINOR.PATCH`

- **MAJOR** (2.0.0) - Breaking changes
- **MINOR** (1.24.0) - New features (backward compatible  
- **PATCH** (1.23.1) - Bug fixes

## ?? Where Version Appears

? Swagger UI title (e.g., "Base API v1.23.1")  
? Swagger UI header badge (green badge)  
? Browser tab title  
? HTTP Response Header (`X-API-Version`)  
? `/api/v1/version` endpoint  
? Application startup logs  
? Swagger JSON (`/swagger/v1/swagger.json`)

## ?? Tips

- Use `-dev`, `-beta`, `-rc` suffixes for pre-releases
- Always update before deploying
- Keep VERSION_MANAGEMENT.md updated with history
- Tag every release in Git

## Example Updates

### Bug Fix
```json
"ApiVersion": "1.23.2"  // Was 1.23.1
```

### New Feature
```json
"ApiVersion": "1.24.0"  // Was 1.23.1
```

### Breaking Change
```json
"ApiVersion": "2.0.0"  // Was 1.23.1
```

### Development Build
```json
"ApiVersion": "1.24.0-dev"
```

---

**That's it! Just update one line in appsettings.json and you're done!** ??
