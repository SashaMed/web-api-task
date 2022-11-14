using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("bce286eb-0fc5-4fca-92bb-fba0e0f5ea74"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("c68025fb-afd3-47e4-8606-3725ee023d26"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Id",
                keyValue: new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "Id",
                keyValue: new Guid("acfff7cb-1804-4ee3-8923-f1e947175f15"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Products");

            migrationBuilder.InsertData(
                table: "FridgeModels",
                columns: new[] { "Id", "Name", "Year" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "samsung ldt123", 2023 },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "super atlant 40Byn/month", 2018 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DefaultQuantity", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), 3, "", "milk" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 1, "yummy", "watermelon" }
                });

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "Id", "FridgeModelId", "Name", "OwnerName" },
                values: new object[] { new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "atlant", "sasha" });

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "Id", "FridgeModelId", "Name", "OwnerName" },
                values: new object[] { new Guid("acfff7cb-1804-4ee3-8923-f1e947175f15"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "samsung", "anna" });

            migrationBuilder.InsertData(
                table: "FridgeProducts",
                columns: new[] { "Id", "FridgeId", "ProductId", "Quantity" },
                values: new object[] { new Guid("bce286eb-0fc5-4fca-92bb-fba0e0f5ea74"), new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 3 });

            migrationBuilder.InsertData(
                table: "FridgeProducts",
                columns: new[] { "Id", "FridgeId", "ProductId", "Quantity" },
                values: new object[] { new Guid("c68025fb-afd3-47e4-8606-3725ee023d26"), new Guid("acfff7cb-1804-4ee3-8923-f1e947175f15"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), 10 });
        }
    }
}
