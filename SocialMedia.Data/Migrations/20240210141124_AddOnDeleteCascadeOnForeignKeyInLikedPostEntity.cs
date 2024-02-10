using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOnDeleteCascadeOnForeignKeyInLikedPostEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikedPosts_Posts_PostId",
                table: "LikedPosts");

            migrationBuilder.AddForeignKey(
                name: "FK_LikedPosts_Posts_PostId",
                table: "LikedPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikedPosts_Posts_PostId",
                table: "LikedPosts");

            migrationBuilder.AddForeignKey(
                name: "FK_LikedPosts_Posts_PostId",
                table: "LikedPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
