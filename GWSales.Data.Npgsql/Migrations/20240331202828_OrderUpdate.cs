using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GWSales.Data.Migrations
{
    public partial class OrderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AppliedDiscountPercentage",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "Subtotal",
                table: "OrderDetails",
                newName: "SubtotalAmount");

            migrationBuilder.RenameColumn(
                name: "DiscountPercentage",
                table: "CustomerDiscounts",
                newName: "DiscountRate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "OrderDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AppliedDiscountRate",
                table: "OrderDetails",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "CustomerDiscounts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AppliedDiscountRate",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "SubtotalAmount",
                table: "OrderDetails",
                newName: "Subtotal");

            migrationBuilder.RenameColumn(
                name: "DiscountRate",
                table: "CustomerDiscounts",
                newName: "DiscountPercentage");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedOn",
                table: "Orders",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "OrderDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<decimal>(
                name: "AppliedDiscountPercentage",
                table: "OrderDetails",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "CustomerDiscounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
