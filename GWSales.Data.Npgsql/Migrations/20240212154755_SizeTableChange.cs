using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GWSales.Data.Migrations
{
    public partial class SizeTableChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Sizes_SizeID",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "SizeNameEng",
                table: "Sizes");

            migrationBuilder.RenameColumn(
                name: "SizeNameRu",
                table: "Sizes",
                newName: "SizeRuName");

            migrationBuilder.RenameColumn(
                name: "SizeID",
                table: "ProductSizes",
                newName: "SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizes_SizeID",
                table: "ProductSizes",
                newName: "IX_ProductSizes_SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Sizes_SizeId",
                table: "ProductSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Sizes_SizeId",
                table: "ProductSizes");

            migrationBuilder.RenameColumn(
                name: "SizeRuName",
                table: "Sizes",
                newName: "SizeNameRu");

            migrationBuilder.RenameColumn(
                name: "SizeId",
                table: "ProductSizes",
                newName: "SizeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizes_SizeId",
                table: "ProductSizes",
                newName: "IX_ProductSizes_SizeID");

            migrationBuilder.AddColumn<string>(
                name: "SizeNameEng",
                table: "Sizes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Sizes_SizeID",
                table: "ProductSizes",
                column: "SizeID",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
