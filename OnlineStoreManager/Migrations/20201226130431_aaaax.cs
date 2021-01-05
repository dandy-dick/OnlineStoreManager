using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStoreManager.Migrations
{
    public partial class aaaax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnStore",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnStore",
                table: "Products");
        }
    }
}
