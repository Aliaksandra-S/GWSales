using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GWSales.Data.Migrations
{
    public partial class AddedSizeIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityInStock",
                table: "ProductSizes",
                newName: "Quantity");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_SizeRuName",
                table: "Sizes",
                column: "SizeRuName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sizes_SizeRuName",
                table: "Sizes");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ProductSizes",
                newName: "QuantityInStock");
        }
    }
}
