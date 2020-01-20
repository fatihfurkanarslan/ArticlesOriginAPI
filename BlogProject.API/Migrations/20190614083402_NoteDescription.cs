using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject.API.Migrations
{
    public partial class NoteDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Notes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Notes");
        }
    }
}
