using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop_API.Migrations
{
    public partial class errors_table_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ERRORS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KEY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ERROR_AZ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ERROR_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ERROR_RU = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERRORS", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ERRORS");
        }
    }
}
