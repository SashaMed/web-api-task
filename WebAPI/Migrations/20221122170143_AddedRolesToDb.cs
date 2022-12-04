using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("48293f82-faef-46c1-a034-7a15a22eb37c"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("6a8d51bd-f8f1-4135-9506-63db93d90554"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0edb7847-2b75-4bf6-9d97-ad6ed4854008", "c026c51b-ba06-432d-a29b-dd495b044e0d", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eac416bd-8791-41ba-ae02-a7bfab61a3ea", "d791fc30-d0f6-480c-9821-e283b65556cb", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0edb7847-2b75-4bf6-9d97-ad6ed4854008");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eac416bd-8791-41ba-ae02-a7bfab61a3ea");

            migrationBuilder.InsertData(
                table: "FridgeModels",
                columns: new[] { "Id", "Name", "Year" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "samsung ldt123", 2023 },
                    { new Guid("48293f82-faef-46c1-a034-7a15a22eb37c"), "bekko", 2022 },
                    { new Guid("6a8d51bd-f8f1-4135-9506-63db93d90554"), "LG g14", 2020 },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "super atlant 40Byn/month", 2018 }
                });
        }
    }
}
