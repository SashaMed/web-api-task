using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class ForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_FridgeModels_FridgeModelId",
                table: "Fridges");

            migrationBuilder.DropIndex(
                name: "IX_Fridges_FridgeModelId",
                table: "Fridges");

            migrationBuilder.DropColumn(
                name: "FridgeModelId",
                table: "Fridges");

            migrationBuilder.CreateIndex(
                name: "IX_Fridges_FridgeId",
                table: "Fridges",
                column: "FridgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_FridgeModels_FridgeId",
                table: "Fridges",
                column: "FridgeId",
                principalTable: "FridgeModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_FridgeModels_FridgeId",
                table: "Fridges");

            migrationBuilder.DropIndex(
                name: "IX_Fridges_FridgeId",
                table: "Fridges");

            migrationBuilder.AddColumn<Guid>(
                name: "FridgeModelId",
                table: "Fridges",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Fridges_FridgeModelId",
                table: "Fridges",
                column: "FridgeModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_FridgeModels_FridgeModelId",
                table: "Fridges",
                column: "FridgeModelId",
                principalTable: "FridgeModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
