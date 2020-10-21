using Microsoft.EntityFrameworkCore.Migrations;

namespace mvc.Migrations
{
    public partial class UserIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_IIN",
                table: "Users",
                column: "IIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_IIN",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Login",
                table: "Users");
        }
    }
}
