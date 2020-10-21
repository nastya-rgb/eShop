using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mvc.Migrations
{
    public partial class PhoneYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Phones");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Phones",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Phones");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Phones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
