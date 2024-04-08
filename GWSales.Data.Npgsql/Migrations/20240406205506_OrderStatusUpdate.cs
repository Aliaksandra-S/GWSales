using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GWSales.Data.Migrations
{
    public partial class OrderStatusUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "OrderStatus",
            table: "Orders");
        }
    }
}
