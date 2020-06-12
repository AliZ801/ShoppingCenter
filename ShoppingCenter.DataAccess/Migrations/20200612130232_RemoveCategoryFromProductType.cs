using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCenter.DataAccess.Migrations
{
    public partial class RemoveCategoryFromProductType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductType_Category_CategoryId",
                table: "ProductType");

            migrationBuilder.DropIndex(
                name: "IX_ProductType_CategoryId",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProductType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ProductType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_CategoryId",
                table: "ProductType",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductType_Category_CategoryId",
                table: "ProductType",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
