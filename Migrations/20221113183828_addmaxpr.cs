using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace warehouse.Migrations
{
    public partial class addmaxpr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "maxpr",
                table: "TovarDB",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxpr",
                table: "TovarDB");
        }
    }
}
