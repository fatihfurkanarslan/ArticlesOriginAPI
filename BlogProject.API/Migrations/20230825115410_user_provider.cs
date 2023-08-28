using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.API.Migrations
{
    public partial class user_provider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Provider",
                table: "AspNetUsers");
        }
    }
}
