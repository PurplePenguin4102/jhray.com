using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace jhray.com.Migrations
{
    public partial class AddingBlogPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Hashtags = table.Column<string>(nullable: true),
                    MarkdownContent = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    SubTitle = table.Column<string>(nullable: true),
                    Published = table.Column<DateTime>(nullable: false),
                    RSSHeaderId = table.Column<int>(nullable: false),
                    AuthorId = table.Column<int>(nullable: false),
                    AuthorId1 = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_ChilledUser_AuthorId1",
                        column: x => x.AuthorId1,
                        principalTable: "ChilledUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPosts_RSSHeaders_RSSHeaderId",
                        column: x => x.RSSHeaderId,
                        principalTable: "RSSHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PictureLinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BlogId = table.Column<int>(nullable: false),
                    PictureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PictureLinks_BlogPosts_BlogId",
                        column: x => x.BlogId,
                        principalTable: "BlogPosts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PictureLinks_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_AuthorId1",
                table: "BlogPosts",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_RSSHeaderId",
                table: "BlogPosts",
                column: "RSSHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_PictureLinks_BlogId",
                table: "PictureLinks",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_PictureLinks_PictureId",
                table: "PictureLinks",
                column: "PictureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PictureLinks");

            migrationBuilder.DropTable(
                name: "BlogPosts");
        }
    }
}
