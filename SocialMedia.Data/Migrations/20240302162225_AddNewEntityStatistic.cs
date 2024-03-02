using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntityStatistic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportedPostsDeletedCount = table.Column<int>(type: "int", nullable: false),
                    AllTimeUsersCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5728d47b-637f-485f-a926-c60ddbee0412", "AQAAAAIAAYagAAAAEDRIsol6JE0p8fG1Zx+rsspKNVJxxyAWdk9xOf3hHqxfQZbUP6MDW5jFyatCjZDFjw==", "e8397ac6-3f63-45a4-a187-62b77b3b82d8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe6d5553-3d35-4d9b-95b6-5c8a80cdbe34", "AQAAAAIAAYagAAAAEHJ6OuIQSC5j+Pv4X0bdwpzNg4htPBB1IV3fHnTbZ7gKCG5Q4TB9fdqtwI5AU6PLKg==", "8c6e8d16-4298-424b-94f8-d0cd3ca5b911" });
        }
    }
}
