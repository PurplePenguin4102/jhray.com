using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace jhray.com.Migrations
{
    public partial class AddingPodcastToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Podcasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LengthInBytes = table.Column<long>(type: "int8", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    PubDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: true)
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
