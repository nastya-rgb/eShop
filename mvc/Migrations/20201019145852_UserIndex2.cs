using Microsoft.EntityFrameworkCore.Migrations;

namespace mvc.Migrations
{
    public partial class UserIndex2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name_LastName",
                table: "Users",
                columns: new[] { "Name", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_XIN",
                table: "Companies",
                column: "XIN",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Name_LastName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Companies_XIN",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }
    }
}
