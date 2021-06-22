using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BaseProjectAPI.Persistence.Migrations
{
    public partial class AddedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DisabledOn",
                table: "Items",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

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
                    DisabledOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2014, 3, 17, 5, 16, 26, 270, DateTimeKind.Unspecified).AddTicks(8102), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 12, 19, 9, 21, 41, 832, DateTimeKind.Unspecified).AddTicks(3441), new TimeSpan(0, -7, 0, 0, 0)), false, "Miss Hector Schumm", 10 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2013, 11, 13, 11, 23, 15, 213, DateTimeKind.Unspecified).AddTicks(8553), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2014, 9, 11, 13, 25, 34, 630, DateTimeKind.Unspecified).AddTicks(2580), new TimeSpan(0, -6, 0, 0, 0)), true, "Lorena Rau I", 5 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 3, 15, 15, 51, 25, 713, DateTimeKind.Unspecified).AddTicks(4830), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2015, 6, 21, 22, 28, 26, 224, DateTimeKind.Unspecified).AddTicks(4441), new TimeSpan(0, -6, 0, 0, 0)), "Iris Frami I", 5 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 9, 7, 15, 44, 34, 876, DateTimeKind.Unspecified).AddTicks(4480), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2012, 4, 15, 22, 5, 28, 107, DateTimeKind.Unspecified).AddTicks(8837), new TimeSpan(0, -6, 0, 0, 0)), "Brandon Effertz I", 10 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2017, 1, 30, 17, 37, 43, 459, DateTimeKind.Unspecified).AddTicks(3079), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2014, 12, 26, 4, 32, 50, 726, DateTimeKind.Unspecified).AddTicks(660), new TimeSpan(0, -7, 0, 0, 0)), "Delores Heller II", 9 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2017, 3, 10, 15, 0, 21, 19, DateTimeKind.Unspecified).AddTicks(7572), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2017, 5, 5, 16, 58, 35, 303, DateTimeKind.Unspecified).AddTicks(7919), new TimeSpan(0, -6, 0, 0, 0)), true, "Anita Dach DDS", 4 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 9, 22, 9, 20, 40, 486, DateTimeKind.Unspecified).AddTicks(1605), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2015, 7, 21, 19, 9, 8, 885, DateTimeKind.Unspecified).AddTicks(6105), new TimeSpan(0, -6, 0, 0, 0)), true, "Miss Penny Christiansen", 2 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 11, 27, 23, 1, 39, 862, DateTimeKind.Unspecified).AddTicks(7354), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 4, 19, 6, 27, 48, 624, DateTimeKind.Unspecified).AddTicks(7752), new TimeSpan(0, -6, 0, 0, 0)), "Ernest Reynolds Sr." });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 4, 7, 12, 52, 54, 103, DateTimeKind.Unspecified).AddTicks(42), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2017, 12, 2, 0, 49, 45, 453, DateTimeKind.Unspecified).AddTicks(8772), new TimeSpan(0, -7, 0, 0, 0)), false, "Dr. Jackie Grimes" });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 6, 11, 1, 16, 41, 705, DateTimeKind.Unspecified).AddTicks(9902), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 2, 3, 14, 53, 16, 934, DateTimeKind.Unspecified).AddTicks(4877), new TimeSpan(0, -7, 0, 0, 0)), "Erick Heller Sr." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DisabledOn",
                table: "Items",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 6, 12, 2, 17, 31, 594, DateTimeKind.Unspecified).AddTicks(4628), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 2, 2, 4, 19, 48, 802, DateTimeKind.Unspecified).AddTicks(9589), new TimeSpan(0, -7, 0, 0, 0)), true, "Miss Lori Keebler", 9 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 3, 3, 12, 50, 59, 244, DateTimeKind.Unspecified).AddTicks(6891), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 6, 3, 14, 53, 15, 104, DateTimeKind.Unspecified).AddTicks(3619), new TimeSpan(0, -6, 0, 0, 0)), false, "Ms. Billie Huels", 8 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 6, 4, 10, 41, 27, 719, DateTimeKind.Unspecified).AddTicks(817), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2013, 4, 26, 18, 12, 28, 68, DateTimeKind.Unspecified).AddTicks(5580), new TimeSpan(0, -6, 0, 0, 0)), "Faith Hane MD", 3 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 2, 21, 20, 36, 23, 518, DateTimeKind.Unspecified).AddTicks(6510), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 11, 3, 1, 42, 58, 636, DateTimeKind.Unspecified).AddTicks(6673), new TimeSpan(0, -7, 0, 0, 0)), "Mrs. Joanna Wolff", 6 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2013, 1, 24, 5, 43, 18, 469, DateTimeKind.Unspecified).AddTicks(3102), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 3, 23, 13, 43, 44, 802, DateTimeKind.Unspecified).AddTicks(7295), new TimeSpan(0, -7, 0, 0, 0)), "Dr. Edmond Rohan", 5 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2016, 4, 20, 6, 16, 0, 343, DateTimeKind.Unspecified).AddTicks(9682), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2013, 5, 23, 4, 46, 37, 833, DateTimeKind.Unspecified).AddTicks(9261), new TimeSpan(0, -6, 0, 0, 0)), false, "Ms. Elizabeth Steuber", 6 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 4, 10, 9, 41, 35, 188, DateTimeKind.Unspecified).AddTicks(7201), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2015, 11, 28, 14, 40, 57, 174, DateTimeKind.Unspecified).AddTicks(625), new TimeSpan(0, -7, 0, 0, 0)), false, "Mrs. Kathy Kshlerin", 9 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2016, 3, 14, 23, 41, 32, 936, DateTimeKind.Unspecified).AddTicks(3085), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 3, 1, 22, 27, 34, 460, DateTimeKind.Unspecified).AddTicks(2409), new TimeSpan(0, -7, 0, 0, 0)), "Luther Hand II" });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2011, 12, 13, 15, 19, 7, 217, DateTimeKind.Unspecified).AddTicks(3315), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2012, 7, 3, 17, 13, 58, 656, DateTimeKind.Unspecified).AddTicks(3991), new TimeSpan(0, -6, 0, 0, 0)), true, "Conrad Collins DDS" });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2013, 12, 16, 16, 1, 9, 57, DateTimeKind.Unspecified).AddTicks(4091), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 7, 18, 8, 45, 5, 331, DateTimeKind.Unspecified).AddTicks(9602), new TimeSpan(0, -6, 0, 0, 0)), "Mrs. Ruby Connelly" });
        }
    }
}
