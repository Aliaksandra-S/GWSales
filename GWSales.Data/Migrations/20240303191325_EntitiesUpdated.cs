using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GWSales.Data.Migrations
{
    public partial class EntitiesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDiscounts_Customers_CustomerId",
                table: "CustomerDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDiscounts_CustomerId",
                table: "CustomerDiscounts");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerDiscounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "OrderDate",
                table: "Orders",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedOn",
                table: "Orders",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "OrderDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Customers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "CustomerDiscounts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DiscountId",
                table: "Customers",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerDiscounts_DiscountId",
                table: "Customers",
                column: "DiscountId",
                principalTable: "CustomerDiscounts",
                principalColumn: "DiscountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerDiscounts_DiscountId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_DiscountId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "CustomerDiscounts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CustomerDiscounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDiscounts_CustomerId",
                table: "CustomerDiscounts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDiscounts_Customers_CustomerId",
                table: "CustomerDiscounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
