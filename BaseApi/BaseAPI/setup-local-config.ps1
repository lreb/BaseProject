# 🚀 Quick Setup Script for New Developers

Write-Host "====================================" -ForegroundColor Cyan
Write-Host "  BaseAPI Configuration Setup" -ForegroundColor Cyan
Write-Host "====================================" -ForegroundColor Cyan
Write-Host ""

# Check if appsettings.Local.json already exists
if (Test-Path "appsettings.Local.json") {
    Write-Host "⚠️  appsettings.Local.json already exists!" -ForegroundColor Yellow
    Write-Host ""
    $overwrite = Read-Host "Do you want to overwrite it? (y/n)"
    if ($overwrite -ne "y") {
        Write-Host "❌ Setup cancelled." -ForegroundColor Red
        exit
    }
}

# Copy template to actual file
Write-Host "📁 Creating appsettings.Local.json from template..." -ForegroundColor Green
Copy-Item "appsettings.Local.json.template" "appsettings.Local.json"

Write-Host "✅ File created successfully!" -ForegroundColor Green
Write-Host ""

# Prompt for database configuration
Write-Host "Please enter your database connection details:" -ForegroundColor Cyan
Write-Host ""

$host = Read-Host "Database Host (e.g., localhost or 192.168.....)"
$port = Read-Host "Database Port (default: 5432)"
if ([string]::IsNullOrWhiteSpace($port)) { $port = "5432" }

$database = Read-Host "Database Name (e.g., BaseApiDb_Dev)"
$username = Read-Host "Database Username"
$password = Read-Host "Database Password" -AsSecureString
$passwordPlain = [Runtime.InteropServices.Marshal]::PtrToStringAuto(
    [Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))

# Build connection string
$connectionString = "Host=$host;Database=$database;Username=$username;Password=$passwordPlain;Port=$port;Pooling=true;"

# Read template content
$jsonContent = Get-Content "appsettings.Local.json" -Raw | ConvertFrom-Json

# Update connection string
$jsonContent.ConnectionStrings.DefaultConnection = $connectionString

# Save updated JSON
$jsonContent | ConvertTo-Json -Depth 10 | Set-Content "appsettings.Local.json"

Write-Host ""
Write-Host "✅ Configuration saved to appsettings.Local.json" -ForegroundColor Green
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "  1. Restore packages: dotnet restore" -ForegroundColor White
Write-Host "  2. Update database: dotnet ef database update" -ForegroundColor White
Write-Host "  3. Run application: dotnet run" -ForegroundColor White
Write-Host "  4. Test health: Invoke-RestMethod https://localhost:5001/health" -ForegroundColor White
Write-Host ""
Write-Host "⚠️  IMPORTANT: appsettings.Local.json is in .gitignore and will NOT be committed!" -ForegroundColor Yellow
Write-Host ""

# Verify .gitignore
if (Select-String -Path ".gitignore" -Pattern "appsettings.Local.json" -Quiet) {
    Write-Host "✅ Verified: appsettings.Local.json is in .gitignore" -ForegroundColor Green
} else {
    Write-Host "⚠️  WARNING: appsettings.Local.json is NOT in .gitignore!" -ForegroundColor Red
    Write-Host "   Add it to .gitignore before committing!" -ForegroundColor Red
}

Write-Host ""
Write-Host "Setup complete! 🎉" -ForegroundColor Green
