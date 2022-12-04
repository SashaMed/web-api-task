using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class storedprocedure2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE update_quatity_to_default @count_of INT OUTPUT
                        AS
                        BEGIN

	                        SET @count_of = 
		                        (SELECT COUNT(Quantity) 
		                        FROM FridgeProducts
		                        WHERE FridgeProducts.Quantity = 0)

	                        IF @count_of = 0
		                        RETURN;

	                        UPDATE FridgeProducts
	                        SET Quantity = (SELECT DefaultQuantity 
					                        FROM Products
					                        WHERE Products.Id = FridgeProducts.ProductId)
	                        FROM
	                        (SELECT * 
	                        FROM FridgeProducts
	                        WHERE FridgeProducts.Quantity = 0) AS Selected 
	                        RETURN @count_of;
                        END";
            migrationBuilder.Sql(sp);


            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("44f65d32-71eb-430e-b83e-35746c85275b"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("bad8224b-4a2a-41e9-b1f6-84cd04999adb"));

            migrationBuilder.InsertData(
                table: "FridgeProducts",
                columns: new[] { "Id", "FridgeId", "ProductId", "Quantity" },
                values: new object[] { new Guid("bce286eb-0fc5-4fca-92bb-fba0e0f5ea74"), new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 3 });

            migrationBuilder.InsertData(
                table: "FridgeProducts",
                columns: new[] { "Id", "FridgeId", "ProductId", "Quantity" },
                values: new object[] { new Guid("c68025fb-afd3-47e4-8606-3725ee023d26"), new Guid("acfff7cb-1804-4ee3-8923-f1e947175f15"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("bce286eb-0fc5-4fca-92bb-fba0e0f5ea74"));

            migrationBuilder.DeleteData(
                table: "FridgeProducts",
                keyColumn: "Id",
                keyValue: new Guid("c68025fb-afd3-47e4-8606-3725ee023d26"));

            migrationBuilder.InsertData(
                table: "FridgeProducts",
                columns: new[] { "Id", "FridgeId", "ProductId", "Quantity" },
                values: new object[] { new Guid("44f65d32-71eb-430e-b83e-35746c85275b"), new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 3 });

            migrationBuilder.InsertData(
                table: "FridgeProducts",
                columns: new[] { "Id", "FridgeId", "ProductId", "Quantity" },
                values: new object[] { new Guid("bad8224b-4a2a-41e9-b1f6-84cd04999adb"), new Guid("acfff7cb-1804-4ee3-8923-f1e947175f15"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), 10 });
        }
    }
}
