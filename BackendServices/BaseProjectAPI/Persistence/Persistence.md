Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
Npgsql
Npgsql.EntityFrameworkCore.PostgreSQL

Install-Package Microsoft.EntityFrameworkCore.Tools
Update-Package Microsoft.EntityFrameworkCore.Tools
Get-Help about_EntityFrameworkCore



Add-Migration Init -o .\Persistence\Migrations

