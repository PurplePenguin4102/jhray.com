using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace jhray.com.Migrations
{
    public partial class AddingRSSHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RSSHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RSSNumber = table.Column<int>(nullable: false),
                    ChannelLink = table.Column<string>(nullable: true),
                    WebMaster = table.Column<string>(nullable: true),
                    ManagingEditor = table.Column<string>(nullable: true),
                    LogoTitle = table.Column<string>(nullable: true),
                    LogoUrl = table.Column<string>(nullable: true),
                    LogoLink = table.Column<string>(nullable: true),
                    ITunesName = table.Column<string>(nullable: true),
                    ITunesEmail = table.Column<string>(nullable: true),
                    ITunesCategory = table.Column<string>(nullable: true),
                    ITunesSubCategory = table.Column<string>(nullable: true),
                    ITunesCategory2 = table.Column<string>(nullable: true),
                    ITunesSubCategory2 = table.Column<string>(nullable: true),
                    ITunesKeywords = table.Column<string>(nullable: true),
                    ITunesExplicit = table.Column<string>(nullable: true),
                    ITunesImage = table.Column<string>(nullable: true),
                    AtomLink = table.Column<string>(nullable: true),
                    PubDate = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Subtitle = table.Column<string>(nullable: true),
                    LastBuildDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RSSHeaders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RSSHeaders");
        }
    }
}
