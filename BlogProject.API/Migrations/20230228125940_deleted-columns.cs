using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.API.Migrations
{
    public partial class deletedcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AspNetUsers");
        }
    }
}
