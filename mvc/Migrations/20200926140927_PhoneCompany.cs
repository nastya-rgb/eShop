using Microsoft.EntityFrameworkCore.Migrations;

namespace mvc.Migrations
{
    public partial class PhoneCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Phones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Phones",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_CompanyId",
                table: "Phones",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_UserId",
                table: "Phones",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Companies_CompanyId",
                table: "Phones",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Users_UserId",
                table: "Phones",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Companies_CompanyId",
                table: "Phones");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Users_UserId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_CompanyId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_UserId",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Phones");
        }
    }
}
