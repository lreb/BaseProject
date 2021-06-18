using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

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
                    Name = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DisabledOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2018, 3, 28, 15, 7, 15, 150, DateTimeKind.Unspecified).AddTicks(8943), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2017, 7, 25, 8, 21, 43, 280, DateTimeKind.Unspecified).AddTicks(2598), new TimeSpan(0, -6, 0, 0, 0)), true, "Mr. Sandra Kutch", 3 },
                    { 2L, new DateTimeOffset(new DateTime(2014, 12, 28, 16, 43, 17, 377, DateTimeKind.Unspecified).AddTicks(879), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2012, 7, 21, 19, 1, 3, 65, DateTimeKind.Unspecified).AddTicks(2321), new TimeSpan(0, -6, 0, 0, 0)), false, "Mr. Jenny Shanahan", 5 },
                    { 3L, new DateTimeOffset(new DateTime(2012, 8, 2, 22, 36, 14, 810, DateTimeKind.Unspecified).AddTicks(592), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2017, 8, 24, 12, 50, 39, 865, DateTimeKind.Unspecified).AddTicks(312), new TimeSpan(0, -6, 0, 0, 0)), true, "Miss Beatrice Schultz", 4 },
                    { 4L, new DateTimeOffset(new DateTime(2013, 11, 11, 8, 34, 36, 807, DateTimeKind.Unspecified).AddTicks(8755), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2012, 7, 8, 10, 47, 2, 935, DateTimeKind.Unspecified).AddTicks(2497), new TimeSpan(0, -6, 0, 0, 0)), false, "Sally Reilly II", 3 },
                    { 5L, new DateTimeOffset(new DateTime(2013, 4, 19, 6, 0, 51, 387, DateTimeKind.Unspecified).AddTicks(6307), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 7, 26, 10, 21, 15, 672, DateTimeKind.Unspecified).AddTicks(7169), new TimeSpan(0, -6, 0, 0, 0)), true, "Dr. Marilyn Nicolas", 10 },
                    { 6L, new DateTimeOffset(new DateTime(2013, 7, 24, 17, 26, 50, 585, DateTimeKind.Unspecified).AddTicks(5670), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 3, 21, 5, 6, 11, 926, DateTimeKind.Unspecified).AddTicks(12), new TimeSpan(0, -7, 0, 0, 0)), false, "Mr. Linda Ritchie", 3 },
                    { 7L, new DateTimeOffset(new DateTime(2018, 10, 19, 5, 29, 20, 806, DateTimeKind.Unspecified).AddTicks(9848), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2011, 7, 27, 16, 45, 25, 484, DateTimeKind.Unspecified).AddTicks(4246), new TimeSpan(0, -6, 0, 0, 0)), false, "Dr. Janice Feeney", 2 },
                    { 8L, new DateTimeOffset(new DateTime(2020, 10, 20, 11, 1, 15, 125, DateTimeKind.Unspecified).AddTicks(6559), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2016, 7, 30, 20, 48, 38, 994, DateTimeKind.Unspecified).AddTicks(6573), new TimeSpan(0, -6, 0, 0, 0)), false, "Anne Fritsch DDS", 5 },
                    { 9L, new DateTimeOffset(new DateTime(2016, 11, 27, 18, 20, 16, 548, DateTimeKind.Unspecified).AddTicks(6367), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2012, 8, 22, 1, 52, 15, 835, DateTimeKind.Unspecified).AddTicks(8324), new TimeSpan(0, -6, 0, 0, 0)), true, "Arnold Leannon I", 10 },
                    { 10L, new DateTimeOffset(new DateTime(2012, 11, 29, 0, 57, 50, 574, DateTimeKind.Unspecified).AddTicks(6071), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2015, 3, 8, 4, 39, 35, 146, DateTimeKind.Unspecified).AddTicks(6657), new TimeSpan(0, -7, 0, 0, 0)), true, "Dr. Freda Cormier", 6 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
