using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop_API.Migrations
{
    public partial class SeconMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PIN",
                table: "PERSON");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PIN",
                table: "PERSON",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
