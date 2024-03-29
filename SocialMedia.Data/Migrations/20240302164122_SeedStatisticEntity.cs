using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace SocialMedia.Data.Data.Migrations
{
    /// <inheritdoc />
    /// 
    [ExcludeFromCodeCoverage]
    public partial class SeedStatisticEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b89378b-254b-4ffd-aa47-6d21fc4d1ae9", "AQAAAAIAAYagAAAAEGDNSW9yTPWwXF4HlE4d2Y+82PeSePnaVdvlbax/GtlqK7u0utwMxJIctunq3GJz1w==", "d8eb8cdc-7d9d-4eea-aae4-9d2a76ea9225" });

            migrationBuilder.InsertData(
                table: "Statistics",
                columns: new[] { "Id", "AllTimeUsersCount", "ReportedPostsDeletedCount" },
                values: new object[] { 1, 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5728d47b-637f-485f-a926-c60ddbee0412", "AQAAAAIAAYagAAAAEDRIsol6JE0p8fG1Zx+rsspKNVJxxyAWdk9xOf3hHqxfQZbUP6MDW5jFyatCjZDFjw==", "e8397ac6-3f63-45a4-a187-62b77b3b82d8" });
        }
    }
}
