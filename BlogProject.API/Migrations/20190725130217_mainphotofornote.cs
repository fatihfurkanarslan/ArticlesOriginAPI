using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject.API.Migrations
{
    public partial class mainphotofornote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainPhotourl",
                table: "Notes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainPhotourl",
                table: "Notes");
        }
    }
}
