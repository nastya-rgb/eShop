using Microsoft.EntityFrameworkCore.Migrations;

namespace mvc.Migrations
{
    public partial class UserNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Positions_PositionId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "StatusWork",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IIN",
                table: "Users",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Positions_PositionId",
                table: "Users",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Positions_PositionId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "StatusWork",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "IIN",
                table: "Users",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 12);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Positions_PositionId",
                table: "Users",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
