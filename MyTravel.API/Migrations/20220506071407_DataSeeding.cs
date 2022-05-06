using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTravel.API.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureTime", "Description", "DiscountPresent", "Features", "Fees", "Notes", "OriginalPrice", "Title", "UpdateTime" },
                values: new object[] { new Guid("830949f1-419f-4381-b1b3-5f98ed142698"), new DateTime(2022, 5, 6, 15, 14, 7, 397, DateTimeKind.Local).AddTicks(663), null, "shuoming", null, null, null, null, 0m, "ceshititle", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("830949f1-419f-4381-b1b3-5f98ed142698"));
        }
    }
}
