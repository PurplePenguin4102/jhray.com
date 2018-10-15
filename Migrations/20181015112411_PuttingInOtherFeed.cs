using Microsoft.EntityFrameworkCore.Migrations;

namespace jhray.com.Migrations
{
    public partial class PuttingInOtherFeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeedId",
                table: "Podcasts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "Pictures",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedId",
                table: "Podcasts");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "Pictures");
        }
    }
}
