using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogProject.API.Migrations
{
    public partial class publicidforphoto2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "Photos",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PublicId",
                table: "Photos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
