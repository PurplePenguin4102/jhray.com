using Microsoft.EntityFrameworkCore.Migrations;

namespace jhray.com.Migrations
{
    public partial class Capitals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "BlogPosts",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BlogPosts",
                newName: "id");
        }
    }
}
