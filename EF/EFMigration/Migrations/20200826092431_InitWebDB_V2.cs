using Microsoft.EntityFrameworkCore.Migrations;

namespace EFMigration.Migrations
{
    public partial class InitWebDB_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articletag",
                columns: table => new
                {
                    ArticleTagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(nullable: false),
                    TagId = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articletag", x => x.ArticleTagId);
                    table.ForeignKey(
                        name: "FK_articletag_article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_articletag_tags_TagId",
                        column: x => x.TagId,
                        principalTable: "tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_articletag_TagId",
                table: "articletag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_articletag_ArticleId_TagId",
                table: "articletag",
                columns: new[] { "ArticleId", "TagId" },
                unique: true,
                filter: "[TagId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articletag");
        }
    }
}
