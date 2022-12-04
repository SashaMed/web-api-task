using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class userupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c3edde5-85cb-4698-b40d-6ae950ca3f1e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ded37f60-b0d9-4db5-b1bd-be8b2db4dd3c");

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("48293f82-faef-46c1-a034-7a15a22eb37c"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "Id",
                keyValue: new Guid("6a8d51bd-f8f1-4135-9506-63db93d90554"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("1e82e13d-d9d0-4ed3-a54e-8ac67457f427"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("7900ec74-353e-4571-baf2-5ef26d7f82b1"));

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

            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "AspNetUsers",
                newName: "RefreshToken");

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpires",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TokenExpires",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "AspNetUsers",
                newName: "NickName");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c3edde5-85cb-4698-b40d-6ae950ca3f1e", "71d17f8e-7e78-4334-afe9-226a23e7b00a", "Administrator", "ADMINISTRATOR" },
                    { "ded37f60-b0d9-4db5-b1bd-be8b2db4dd3c", "0fb7a0a8-9bb1-4595-835e-7ded659d0e82", "Manager", "MANAGER" }
                });

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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DefaultQuantity", "Description", "ImagePath", "Name" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), 3, "", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/Parque_Nacional_de_Bras%C3%ADlia_%2814543997474%29.jpg/274px-Parque_Nacional_de_Bras%C3%ADlia_%2814543997474%29.jpg", "milk" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 1, "yummy", "https://www.uavgusta.net/upload/resize_cache/iblock/d5c/740_740_2/15_faktov_o_roli_vody_v_zhizni_cheloveka.jpg", "watermelon" }
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
                values: new object[] { new Guid("1e82e13d-d9d0-4ed3-a54e-8ac67457f427"), new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 3 });

            migrationBuilder.InsertData(
                table: "FridgeProducts",
                columns: new[] { "Id", "FridgeId", "ProductId", "Quantity" },
                values: new object[] { new Guid("7900ec74-353e-4571-baf2-5ef26d7f82b1"), new Guid("acfff7cb-1804-4ee3-8923-f1e947175f15"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), 10 });
        }
    }
}
