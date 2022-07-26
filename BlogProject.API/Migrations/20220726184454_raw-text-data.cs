using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.API.Migrations
{
    public partial class rawtextdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RAWText",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RAWText",
                table: "Notes");
        }
    }
}
