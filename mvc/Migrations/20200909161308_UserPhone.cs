using Microsoft.EntityFrameworkCore.Migrations;

namespace mvc.Migrations
{
    public partial class UserPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Users");
        }
    }
}
