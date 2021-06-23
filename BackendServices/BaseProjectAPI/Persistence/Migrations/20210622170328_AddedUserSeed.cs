using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseProjectAPI.Persistence.Migrations
{
    public partial class AddedUserSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 2, 24, 9, 34, 15, 811, DateTimeKind.Unspecified).AddTicks(194), new TimeSpan(0, -7, 0, 0, 0)), null, true, "Barbara", 6 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 5, 1, 22, 14, 8, 203, DateTimeKind.Unspecified).AddTicks(3385), new TimeSpan(0, -6, 0, 0, 0)), null, false, "Cecilia", 8 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 7, 29, 9, 10, 6, 791, DateTimeKind.Unspecified).AddTicks(4506), new TimeSpan(0, -6, 0, 0, 0)), null, true, "Muriel", 3 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2013, 1, 23, 3, 36, 21, 999, DateTimeKind.Unspecified).AddTicks(4355), new TimeSpan(0, -7, 0, 0, 0)), null, "Emma", 7 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2013, 6, 18, 19, 22, 7, 947, DateTimeKind.Unspecified).AddTicks(8365), new TimeSpan(0, -6, 0, 0, 0)), null, "Thelma", 6 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2014, 2, 19, 15, 38, 35, 446, DateTimeKind.Unspecified).AddTicks(3367), new TimeSpan(0, -7, 0, 0, 0)), null, false, "Jaime", 7 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2018, 5, 26, 19, 0, 40, 381, DateTimeKind.Unspecified).AddTicks(6242), new TimeSpan(0, -6, 0, 0, 0)), null, false, "Janice", 5 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 1, 6, 4, 59, 44, 367, DateTimeKind.Unspecified).AddTicks(3687), new TimeSpan(0, -7, 0, 0, 0)), null, "Johanna", 6 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2016, 3, 12, 20, 7, 27, 877, DateTimeKind.Unspecified).AddTicks(4143), new TimeSpan(0, -7, 0, 0, 0)), null, true, "Kelley", 6 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 5, 25, 10, 15, 7, 227, DateTimeKind.Unspecified).AddTicks(36), new TimeSpan(0, -6, 0, 0, 0)), null, true, "Stella", 6 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 3, 15, 15, 51, 25, 713, DateTimeKind.Unspecified).AddTicks(4830), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2015, 6, 21, 22, 28, 26, 224, DateTimeKind.Unspecified).AddTicks(4441), new TimeSpan(0, -6, 0, 0, 0)), false, "Iris Frami I", 5 });

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
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 11, 27, 23, 1, 39, 862, DateTimeKind.Unspecified).AddTicks(7354), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 4, 19, 6, 27, 48, 624, DateTimeKind.Unspecified).AddTicks(7752), new TimeSpan(0, -6, 0, 0, 0)), "Ernest Reynolds Sr.", 4 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 4, 7, 12, 52, 54, 103, DateTimeKind.Unspecified).AddTicks(42), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2017, 12, 2, 0, 49, 45, 453, DateTimeKind.Unspecified).AddTicks(8772), new TimeSpan(0, -7, 0, 0, 0)), false, "Dr. Jackie Grimes", 9 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 6, 11, 1, 16, 41, 705, DateTimeKind.Unspecified).AddTicks(9902), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2018, 2, 3, 14, 53, 16, 934, DateTimeKind.Unspecified).AddTicks(4877), new TimeSpan(0, -7, 0, 0, 0)), false, "Erick Heller Sr.", 8 });
        }
    }
}
