using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jhray.com.Migrations
{
    public partial class AddingPodcasts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Podcasts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    ItunesDuration = table.Column<string>(nullable: true),
                    PubDate = table.Column<DateTimeOffset>(nullable: false),
                    LengthInBytes = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podcasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Podcasts_Gems_Id",
                        column: x => x.Id,
                        principalTable: "Gems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Podcasts");
        }
    }
}
