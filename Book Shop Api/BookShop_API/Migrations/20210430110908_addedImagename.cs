using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop_API.Migrations
{
    public partial class addedImagename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagename",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagename",
                table: "Books");
        }
    }
}
