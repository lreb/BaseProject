using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseProjectAPI.Persistence.Migrations
{
    public partial class AddedUserSeed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2014, 3, 22, 17, 6, 10, 982, DateTimeKind.Unspecified).AddTicks(9280), new TimeSpan(0, -7, 0, 0, 0)), "Juana", 5 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2016, 5, 21, 9, 20, 23, 876, DateTimeKind.Unspecified).AddTicks(6498), new TimeSpan(0, -6, 0, 0, 0)), true, "Kelli", 10 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 12, 6, 20, 52, 46, 690, DateTimeKind.Unspecified).AddTicks(2521), new TimeSpan(0, -7, 0, 0, 0)), "Sophie", 2 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 3, 1, 0, 55, 25, 748, DateTimeKind.Unspecified).AddTicks(7293), new TimeSpan(0, -7, 0, 0, 0)), true, "Louise", 8 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2014, 8, 13, 3, 23, 53, 391, DateTimeKind.Unspecified).AddTicks(8699), new TimeSpan(0, -6, 0, 0, 0)), true, "Traci", 9 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 6, 25, 4, 30, 25, 505, DateTimeKind.Unspecified).AddTicks(2432), new TimeSpan(0, -6, 0, 0, 0)), true, "Alison", 9 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 5, 27, 3, 27, 9, 400, DateTimeKind.Unspecified).AddTicks(5467), new TimeSpan(0, -6, 0, 0, 0)), "Christie", 7 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 2, 8, 9, 45, 56, 234, DateTimeKind.Unspecified).AddTicks(3179), new TimeSpan(0, -7, 0, 0, 0)), "Elizabeth", 5 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 12, 23, 0, 47, 30, 612, DateTimeKind.Unspecified).AddTicks(7101), new TimeSpan(0, -7, 0, 0, 0)), "Angelica", 2 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedOn", "IsEnabled", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 8, 26, 15, 26, 55, 334, DateTimeKind.Unspecified).AddTicks(1416), new TimeSpan(0, -6, 0, 0, 0)), false, "Rachel" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "DisabledOn", "Email", "FirstName", "IsEnabled", "LastName" },
                values: new object[,]
                {
                    { 10, new DateTimeOffset(new DateTime(2019, 4, 9, 20, 24, 38, 150, DateTimeKind.Unspecified).AddTicks(9647), new TimeSpan(0, -6, 0, 0, 0)), null, "Frederick.Lehner66@hotmail.com", "Frederick", true, "Lehner" },
                    { 8, new DateTimeOffset(new DateTime(2018, 6, 10, 13, 16, 44, 96, DateTimeKind.Unspecified).AddTicks(2352), new TimeSpan(0, -6, 0, 0, 0)), null, "Orlando26@gmail.com", "Orlando", false, "Mitchell" },
                    { 7, new DateTimeOffset(new DateTime(2019, 7, 3, 0, 42, 46, 701, DateTimeKind.Unspecified).AddTicks(7860), new TimeSpan(0, -6, 0, 0, 0)), null, "Alexander21@gmail.com", "Alexander", true, "Mraz" },
                    { 6, new DateTimeOffset(new DateTime(2017, 2, 15, 17, 28, 1, 500, DateTimeKind.Unspecified).AddTicks(8706), new TimeSpan(0, -7, 0, 0, 0)), null, "Alfonso.Nolan@gmail.com", "Alfonso", false, "Nolan" },
                    { 5, new DateTimeOffset(new DateTime(2016, 5, 28, 15, 0, 36, 554, DateTimeKind.Unspecified).AddTicks(5502), new TimeSpan(0, -6, 0, 0, 0)), null, "Fernando_Goodwin92@gmail.com", "Fernando", false, "Goodwin" },
                    { 4, new DateTimeOffset(new DateTime(2014, 2, 27, 10, 56, 49, 820, DateTimeKind.Unspecified).AddTicks(2746), new TimeSpan(0, -7, 0, 0, 0)), null, "Cecil2@gmail.com", "Cecil", false, "Koepp" },
                    { 3, new DateTimeOffset(new DateTime(2016, 6, 21, 0, 37, 40, 486, DateTimeKind.Unspecified).AddTicks(3629), new TimeSpan(0, -6, 0, 0, 0)), null, "Levi.Cummings5@hotmail.com", "Levi", true, "Cummings" },
                    { 2, new DateTimeOffset(new DateTime(2012, 4, 27, 15, 58, 52, 824, DateTimeKind.Unspecified).AddTicks(5992), new TimeSpan(0, -6, 0, 0, 0)), null, "Gabriel.Hirthe@gmail.com", "Gabriel", false, "Hirthe" },
                    { 9, new DateTimeOffset(new DateTime(2013, 7, 25, 6, 8, 4, 138, DateTimeKind.Unspecified).AddTicks(7320), new TimeSpan(0, -6, 0, 0, 0)), null, "Ron_Connelly@yahoo.com", "Ron", false, "Connelly" },
                    { 1, new DateTimeOffset(new DateTime(2019, 3, 30, 20, 26, 34, 705, DateTimeKind.Unspecified).AddTicks(9877), new TimeSpan(0, -7, 0, 0, 0)), null, "Earnest_Runolfsdottir21@gmail.com", "Earnest", false, "Runolfsdottir" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 2, 24, 9, 34, 15, 811, DateTimeKind.Unspecified).AddTicks(194), new TimeSpan(0, -7, 0, 0, 0)), "Barbara", 6 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2012, 5, 1, 22, 14, 8, 203, DateTimeKind.Unspecified).AddTicks(3385), new TimeSpan(0, -6, 0, 0, 0)), false, "Cecilia", 8 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 7, 29, 9, 10, 6, 791, DateTimeKind.Unspecified).AddTicks(4506), new TimeSpan(0, -6, 0, 0, 0)), "Muriel", 3 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2013, 1, 23, 3, 36, 21, 999, DateTimeKind.Unspecified).AddTicks(4355), new TimeSpan(0, -7, 0, 0, 0)), false, "Emma", 7 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2013, 6, 18, 19, 22, 7, 947, DateTimeKind.Unspecified).AddTicks(8365), new TimeSpan(0, -6, 0, 0, 0)), false, "Thelma", 6 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedOn", "IsEnabled", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2014, 2, 19, 15, 38, 35, 446, DateTimeKind.Unspecified).AddTicks(3367), new TimeSpan(0, -7, 0, 0, 0)), false, "Jaime", 7 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2018, 5, 26, 19, 0, 40, 381, DateTimeKind.Unspecified).AddTicks(6242), new TimeSpan(0, -6, 0, 0, 0)), "Janice", 5 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2015, 1, 6, 4, 59, 44, 367, DateTimeKind.Unspecified).AddTicks(3687), new TimeSpan(0, -7, 0, 0, 0)), "Johanna", 6 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedOn", "Name", "Quantity" },
                values: new object[] { new DateTimeOffset(new DateTime(2016, 3, 12, 20, 7, 27, 877, DateTimeKind.Unspecified).AddTicks(4143), new TimeSpan(0, -7, 0, 0, 0)), "Kelley", 6 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedOn", "IsEnabled", "Name" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 5, 25, 10, 15, 7, 227, DateTimeKind.Unspecified).AddTicks(36), new TimeSpan(0, -6, 0, 0, 0)), true, "Stella" });
        }
    }
}
