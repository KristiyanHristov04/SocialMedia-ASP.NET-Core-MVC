using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntityLikedPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikedPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedPosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LikedPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikedPosts_PostId",
                table: "LikedPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedPosts_UserId",
                table: "LikedPosts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikedPosts");
        }
    }
}
