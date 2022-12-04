using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class dbinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "de82649f-4b9e-48e5-a2c7-e4718184808d", "11032647-3a1d-4176-b80c-37d9eeefc95f", "Manager", "MANAGER" },
                    { "fea8f1bc-782c-41fa-8eca-4642414a9e06", "bc3cf69f-ee73-44de-92d5-888817332920", "Administrator", "ADMINISTRATOR" }
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
                values: new object[] { new Guid("4497d9a5-b561-4898-a5fc-c6c6ef57b048"), new Guid("acfff7cb-1804-4ee3-8923-f1e947175f15"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), 10 });

            migrationBuilder.InsertData(
                table: "FridgeProducts",
                columns: new[] { "Id", "FridgeId", "ProductId", "Quantity" },
                values: new object[] { new Guid("dfca294a-f1b7-4ed7-b821-9a077f8de1ec"), new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de82649f-4b9e-48e5-a2c7-e4718184808d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fea8f1bc-782c-41fa-8eca-4642414a9e06");

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
                keyValue: new Guid("4497d9a5-b561-4898-a5fc-c6c6ef57b048"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("dfca294a-f1b7-4ed7-b821-9a077f8de1ec"));

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
        }
    }
}
