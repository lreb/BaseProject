using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BaseProjectAPI.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    DisabledOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DisabledOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedOn", "Description", "DisabledOn", "IsEnabled", "Name", "Quantity", "UpdatedOn" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2020, 10, 25, 17, 7, 58, 318, DateTimeKind.Unspecified).AddTicks(9891), new TimeSpan(0, -7, 0, 0, 0)), "Senior", null, true, "Doris", 9, null },
                    { 9L, new DateTimeOffset(new DateTime(2012, 4, 17, 11, 19, 18, 316, DateTimeKind.Unspecified).AddTicks(6451), new TimeSpan(0, -6, 0, 0, 0)), "Investor", null, false, "Marie", 4, null },
                    { 8L, new DateTimeOffset(new DateTime(2012, 11, 4, 1, 34, 20, 399, DateTimeKind.Unspecified).AddTicks(5342), new TimeSpan(0, -7, 0, 0, 0)), "Human", null, true, "Eloise", 7, null },
                    { 7L, new DateTimeOffset(new DateTime(2020, 2, 28, 3, 47, 8, 897, DateTimeKind.Unspecified).AddTicks(9335), new TimeSpan(0, -7, 0, 0, 0)), "Lead", null, false, "Betsy", 6, null },
                    { 6L, new DateTimeOffset(new DateTime(2018, 7, 24, 20, 25, 6, 561, DateTimeKind.Unspecified).AddTicks(7912), new TimeSpan(0, -6, 0, 0, 0)), "Future", null, true, "Carolyn", 5, null },
                    { 10L, new DateTimeOffset(new DateTime(2013, 1, 24, 20, 24, 32, 400, DateTimeKind.Unspecified).AddTicks(4971), new TimeSpan(0, -7, 0, 0, 0)), "Regional", null, true, "Karen", 8, null },
                    { 4L, new DateTimeOffset(new DateTime(2012, 5, 30, 16, 32, 10, 608, DateTimeKind.Unspecified).AddTicks(6138), new TimeSpan(0, -6, 0, 0, 0)), "Central", null, true, "Angel", 1, null },
                    { 3L, new DateTimeOffset(new DateTime(2016, 11, 23, 18, 22, 38, 824, DateTimeKind.Unspecified).AddTicks(1580), new TimeSpan(0, -7, 0, 0, 0)), "Internal", null, false, "Roxanne", 3, null },
                    { 2L, new DateTimeOffset(new DateTime(2020, 9, 26, 12, 11, 8, 99, DateTimeKind.Unspecified).AddTicks(8929), new TimeSpan(0, -6, 0, 0, 0)), "Corporate", null, false, "Susan", 8, null },
                    { 5L, new DateTimeOffset(new DateTime(2019, 1, 28, 9, 11, 40, 845, DateTimeKind.Unspecified).AddTicks(9077), new TimeSpan(0, -7, 0, 0, 0)), "Internal", null, false, "Bertha", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "DisabledOn", "Email", "FirstName", "IsEnabled", "LastName", "UpdatedOn" },
                values: new object[,]
                {
                    { 9, new DateTimeOffset(new DateTime(2015, 6, 1, 22, 48, 54, 61, DateTimeKind.Unspecified).AddTicks(5624), new TimeSpan(0, -6, 0, 0, 0)), null, "Leland29@hotmail.com", "Leland", true, "Weissnat", null },
                    { 1, new DateTimeOffset(new DateTime(2021, 1, 26, 3, 42, 1, 7, DateTimeKind.Unspecified).AddTicks(4706), new TimeSpan(0, -7, 0, 0, 0)), null, "Shannon.Trantow@hotmail.com", "Shannon", true, "Trantow", null },
                    { 2, new DateTimeOffset(new DateTime(2019, 5, 1, 18, 48, 58, 709, DateTimeKind.Unspecified).AddTicks(4451), new TimeSpan(0, -6, 0, 0, 0)), null, "Ramiro_Fahey55@hotmail.com", "Ramiro", false, "Fahey", null },
                    { 3, new DateTimeOffset(new DateTime(2013, 12, 3, 1, 0, 36, 354, DateTimeKind.Unspecified).AddTicks(3405), new TimeSpan(0, -7, 0, 0, 0)), null, "Myron_Hammes96@yahoo.com", "Myron", false, "Hammes", null },
                    { 4, new DateTimeOffset(new DateTime(2019, 4, 28, 0, 12, 59, 533, DateTimeKind.Unspecified).AddTicks(1902), new TimeSpan(0, -6, 0, 0, 0)), null, "Rafael.Schuster@hotmail.com", "Rafael", true, "Schuster", null },
                    { 5, new DateTimeOffset(new DateTime(2020, 7, 16, 22, 51, 10, 531, DateTimeKind.Unspecified).AddTicks(2360), new TimeSpan(0, -6, 0, 0, 0)), null, "Abraham66@gmail.com", "Abraham", false, "Koelpin", null },
                    { 6, new DateTimeOffset(new DateTime(2020, 6, 14, 1, 20, 15, 467, DateTimeKind.Unspecified).AddTicks(8023), new TimeSpan(0, -6, 0, 0, 0)), null, "John_Leannon44@hotmail.com", "John", true, "Leannon", null },
                    { 7, new DateTimeOffset(new DateTime(2020, 3, 21, 3, 7, 51, 768, DateTimeKind.Unspecified).AddTicks(620), new TimeSpan(0, -7, 0, 0, 0)), null, "Tommy.Bartoletti70@yahoo.com", "Tommy", false, "Bartoletti", null },
                    { 8, new DateTimeOffset(new DateTime(2017, 11, 14, 9, 54, 7, 437, DateTimeKind.Unspecified).AddTicks(6752), new TimeSpan(0, -7, 0, 0, 0)), null, "Ernesto49@hotmail.com", "Ernesto", false, "Tillman", null },
                    { 10, new DateTimeOffset(new DateTime(2014, 2, 21, 16, 52, 6, 113, DateTimeKind.Unspecified).AddTicks(3044), new TimeSpan(0, -7, 0, 0, 0)), null, "Bradley.Balistreri14@gmail.com", "Bradley", false, "Balistreri", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name_Quantity_CreatedOn",
                table: "Items",
                columns: new[] { "Name", "Quantity", "CreatedOn" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
