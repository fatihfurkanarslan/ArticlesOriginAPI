using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject.API.Migrations
{
    public partial class Categoryname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "Categoryname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Categoryname",
                table: "Categories",
                newName: "Name");
        }
    }
}
