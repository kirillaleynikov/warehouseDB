using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warehouse.Migrations
{
    public partial class mgg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TovarDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Razmer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false),
                    kolvo = table.Column<int>(type: "int", nullable: false),
                    minpr = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TovarDB", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TovarDB");
        }
    }
}
