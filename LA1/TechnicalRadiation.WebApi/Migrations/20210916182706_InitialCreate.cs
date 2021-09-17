using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalRadiation.WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ProfileImgSource = table.Column<string>(type: "TEXT", nullable: true),
                    Bio = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Slug = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    ImgSource = table.Column<string>(type: "TEXT", nullable: true),
                    ShortDescription = table.Column<string>(type: "TEXT", nullable: true),
                    LongDescription = table.Column<string>(type: "TEXT", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorNewsItem",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewsItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorNewsItem", x => new { x.AuthorsId, x.NewsItemsId });
                    table.ForeignKey(
                        name: "FK_AuthorNewsItem_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorNewsItem_NewsItem_NewsItemsId",
                        column: x => x.NewsItemsId,
                        principalTable: "NewsItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryNewsItem",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    NewsItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryNewsItem", x => new { x.CategoriesId, x.NewsItemsId });
                    table.ForeignKey(
                        name: "FK_CategoryNewsItem_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryNewsItem_NewsItem_NewsItemsId",
                        column: x => x.NewsItemsId,
                        principalTable: "NewsItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorNewsItem_NewsItemsId",
                table: "AuthorNewsItem",
                column: "NewsItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryNewsItem_NewsItemsId",
                table: "CategoryNewsItem",
                column: "NewsItemsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorNewsItem");

            migrationBuilder.DropTable(
                name: "CategoryNewsItem");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "NewsItem");
        }
    }
}
