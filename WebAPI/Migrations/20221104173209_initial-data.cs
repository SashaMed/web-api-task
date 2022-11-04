using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class initialdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FridgeModels",
                columns: new[] { "Id", "Name", "Year" },
                values: new object[] { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "samsung ldt123", 2023 });

            migrationBuilder.InsertData(
                table: "FridgeModels",
                columns: new[] { "Id", "Name", "Year" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "super atlant 40Byn/month", 2018 });

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "Id", "FridgeId", "Name", "OwnerName" },
                values: new object[] { new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "samsung", "anna" });

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "Id", "FridgeId", "Name", "OwnerName" },
                values: new object[] { new Guid("acfff7cb-1804-4ee3-8923-f1e947175f15"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "atlant", "sasha" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Id",
                keyValue: new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Id",
                keyValue: new Guid("acfff7cb-1804-4ee3-8923-f1e947175f15"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
