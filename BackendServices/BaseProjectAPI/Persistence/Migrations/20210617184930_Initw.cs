using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseProjectAPI.Persistence.Migrations
{
    public partial class Initw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 6, 12, 2, 17, 31, 594, DateTimeKind.Unspecified).AddTicks(4628), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 2, 2, 4, 19, 48, 802, DateTimeKind.Unspecified).AddTicks(9589), new TimeSpan(0, -7, 0, 0, 0)), "Miss Lori Keebler", 9 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 3, 3, 12, 50, 59, 244, DateTimeKind.Unspecified).AddTicks(6891), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 6, 3, 14, 53, 15, 104, DateTimeKind.Unspecified).AddTicks(3619), new TimeSpan(0, -6, 0, 0, 0)), "Ms. Billie Huels", 8 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 6, 4, 10, 41, 27, 719, DateTimeKind.Unspecified).AddTicks(817), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2013, 4, 26, 18, 12, 28, 68, DateTimeKind.Unspecified).AddTicks(5580), new TimeSpan(0, -6, 0, 0, 0)), false, "Faith Hane MD", 3 });

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
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2013, 1, 24, 5, 43, 18, 469, DateTimeKind.Unspecified).AddTicks(3102), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 3, 23, 13, 43, 44, 802, DateTimeKind.Unspecified).AddTicks(7295), new TimeSpan(0, -7, 0, 0, 0)), false, "Dr. Edmond Rohan", 5 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2016, 4, 20, 6, 16, 0, 343, DateTimeKind.Unspecified).AddTicks(9682), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2013, 5, 23, 4, 46, 37, 833, DateTimeKind.Unspecified).AddTicks(9261), new TimeSpan(0, -6, 0, 0, 0)), "Ms. Elizabeth Steuber", 6 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 4, 10, 9, 41, 35, 188, DateTimeKind.Unspecified).AddTicks(7201), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2015, 11, 28, 14, 40, 57, 174, DateTimeKind.Unspecified).AddTicks(625), new TimeSpan(0, -7, 0, 0, 0)), "Mrs. Kathy Kshlerin", 9 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2016, 3, 14, 23, 41, 32, 936, DateTimeKind.Unspecified).AddTicks(3085), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 3, 1, 22, 27, 34, 460, DateTimeKind.Unspecified).AddTicks(2409), new TimeSpan(0, -7, 0, 0, 0)), "Luther Hand II", 4 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2011, 12, 13, 15, 19, 7, 217, DateTimeKind.Unspecified).AddTicks(3315), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2012, 7, 3, 17, 13, 58, 656, DateTimeKind.Unspecified).AddTicks(3991), new TimeSpan(0, -6, 0, 0, 0)), "Conrad Collins DDS", 9 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2013, 12, 16, 16, 1, 9, 57, DateTimeKind.Unspecified).AddTicks(4091), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 7, 18, 8, 45, 5, 331, DateTimeKind.Unspecified).AddTicks(9602), new TimeSpan(0, -6, 0, 0, 0)), false, "Mrs. Ruby Connelly", 8 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2018, 3, 28, 15, 7, 15, 150, DateTimeKind.Unspecified).AddTicks(8943), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2017, 7, 25, 8, 21, 43, 280, DateTimeKind.Unspecified).AddTicks(2598), new TimeSpan(0, -6, 0, 0, 0)), "Mr. Sandra Kutch", 3 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2014, 12, 28, 16, 43, 17, 377, DateTimeKind.Unspecified).AddTicks(879), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2012, 7, 21, 19, 1, 3, 65, DateTimeKind.Unspecified).AddTicks(2321), new TimeSpan(0, -6, 0, 0, 0)), "Mr. Jenny Shanahan", 5 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 8, 2, 22, 36, 14, 810, DateTimeKind.Unspecified).AddTicks(592), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2017, 8, 24, 12, 50, 39, 865, DateTimeKind.Unspecified).AddTicks(312), new TimeSpan(0, -6, 0, 0, 0)), true, "Miss Beatrice Schultz", 4 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2013, 11, 11, 8, 34, 36, 807, DateTimeKind.Unspecified).AddTicks(8755), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2012, 7, 8, 10, 47, 2, 935, DateTimeKind.Unspecified).AddTicks(2497), new TimeSpan(0, -6, 0, 0, 0)), "Sally Reilly II", 3 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2013, 4, 19, 6, 0, 51, 387, DateTimeKind.Unspecified).AddTicks(6307), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 7, 26, 10, 21, 15, 672, DateTimeKind.Unspecified).AddTicks(7169), new TimeSpan(0, -6, 0, 0, 0)), true, "Dr. Marilyn Nicolas", 10 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2013, 7, 24, 17, 26, 50, 585, DateTimeKind.Unspecified).AddTicks(5670), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 3, 21, 5, 6, 11, 926, DateTimeKind.Unspecified).AddTicks(12), new TimeSpan(0, -7, 0, 0, 0)), "Mr. Linda Ritchie", 3 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2018, 10, 19, 5, 29, 20, 806, DateTimeKind.Unspecified).AddTicks(9848), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2011, 7, 27, 16, 45, 25, 484, DateTimeKind.Unspecified).AddTicks(4246), new TimeSpan(0, -6, 0, 0, 0)), "Dr. Janice Feeney", 2 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2020, 10, 20, 11, 1, 15, 125, DateTimeKind.Unspecified).AddTicks(6559), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2016, 7, 30, 20, 48, 38, 994, DateTimeKind.Unspecified).AddTicks(6573), new TimeSpan(0, -6, 0, 0, 0)), "Anne Fritsch DDS", 5 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedOn", "DisabledOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2016, 11, 27, 18, 20, 16, 548, DateTimeKind.Unspecified).AddTicks(6367), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2012, 8, 22, 1, 52, 15, 835, DateTimeKind.Unspecified).AddTicks(8324), new TimeSpan(0, -6, 0, 0, 0)), "Arnold Leannon I", 10 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedOn", "DisabledOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 11, 29, 0, 57, 50, 574, DateTimeKind.Unspecified).AddTicks(6071), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2015, 3, 8, 4, 39, 35, 146, DateTimeKind.Unspecified).AddTicks(6657), new TimeSpan(0, -7, 0, 0, 0)), true, "Dr. Freda Cormier", 6 });
        }
    }
}
