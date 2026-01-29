# Generate appsettings.Local.json from .env.local
# Run this for local development setup

$ErrorActionPreference = "Stop"

Write-Host "====================================" -ForegroundColor Cyan
Write-Host "  Local Configuration Setup" -ForegroundColor Cyan
Write-Host "====================================" -ForegroundColor Cyan
Write-Host ""

if (-not (Test-Path ".env.local")) {
    Write-Host "❌ .env.local not found!" -ForegroundColor Red
    Write-Host ""
    Write-Host "Run: .\setup-env.ps1 first" -ForegroundColor Yellow
    exit 1
}

# Load .env.local
Write-Host "Loading .env.local..." -ForegroundColor Gray
$envVars = @{}
Get-Content ".env.local" | ForEach-Object {
    $line = $_.Trim()
    if ($line -ne "" -and -not $line.StartsWith("#")) {
        if ($line -match "^([^=]+)=(.*)$") {
            $key = $matches[1].Trim()
            $value = $matches[2].Trim()
            $envVars[$key] = $value
        }
    }
}

# Build connection string
$connectionString = "Host=$($envVars['DB_HOST']);Database=$($envVars['DB_NAME']);Username=$($envVars['DB_USERNAME']);Password=$($envVars['DB_PASSWORD']);Port=$($envVars['DB_PORT']);Pooling=true;"

# Create appsettings.Local.json
$settings = @{
    "Logging" = @{
        "LogLevel" = @{
            "Default" = "Information"
            "Microsoft.AspNetCore" = "Warning"
            "Microsoft.EntityFrameworkCore" = "Information"
        }
    }
    "ConnectionStrings" = @{
        "DefaultConnection" = $connectionString
    }
}

$settingsJson = $settings | ConvertTo-Json -Depth 10
$settingsJson | Out-File -FilePath "appsettings.Local.json" -Encoding UTF8

Write-Host "✅ Created appsettings.Local.json" -ForegroundColor Green
Write-Host ""
Write-Host "Connection: $($envVars['DB_USERNAME'])@$($envVars['DB_HOST']):$($envVars['DB_PORT'])/$($envVars['DB_NAME'])" -ForegroundColor Gray
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "  1. dotnet restore" -ForegroundColor White
Write-Host "  2. dotnet ef database update" -ForegroundColor White
Write-Host "  3. dotnet run" -ForegroundColor White
