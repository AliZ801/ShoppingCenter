using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCenter.DataAccess.Migrations
{
    public partial class AddStatusToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Products",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Products");
        }
    }
}
