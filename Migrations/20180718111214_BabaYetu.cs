using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jhray.com.Migrations
{
    public partial class BabaYetu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArtistName",
                table: "Pictures",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Pictures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistName",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Pictures");
        }
    }
}
