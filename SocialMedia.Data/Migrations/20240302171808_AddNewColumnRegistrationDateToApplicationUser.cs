using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Data.Data.Migrations
{
    /// <inheritdoc />
    /// 
    [ExcludeFromCodeCoverage]
    public partial class AddNewColumnRegistrationDateToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 2, 19, 18, 7, 728, DateTimeKind.Local).AddTicks(1779));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "621257b2-8deb-450b-8456-1e44bbe3a872", "AQAAAAIAAYagAAAAEGwdfhqaiEPUJIpJsFiqzvCArO6wtHPWCquT23uh0iycOJ713GdmUIpWNLUwLySevQ==", "bf8fe289-92b4-47a1-b5fa-4c148918feba" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b89378b-254b-4ffd-aa47-6d21fc4d1ae9", "AQAAAAIAAYagAAAAEGDNSW9yTPWwXF4HlE4d2Y+82PeSePnaVdvlbax/GtlqK7u0utwMxJIctunq3GJz1w==", "d8eb8cdc-7d9d-4eea-aae4-9d2a76ea9225" });
        }
    }
}
