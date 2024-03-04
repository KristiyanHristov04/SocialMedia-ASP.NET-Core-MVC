using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedOnlySuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "22a1d95e-a505-4934-9f76-178c3798d64a", "7ca7b293-0236-47cf-88d8-5165102b89ad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDate", "SecurityStamp" },
                values: new object[] { "266e042d-8a52-4ad8-a005-193ad1a3fe2b", "AQAAAAIAAYagAAAAEKTzg0+X8FnugvbY//HTlIs4tT3enJSRjZHfqRhWl1w8xvZnnXSraJoctBYJPnGLrA==", new DateTime(2024, 3, 4, 15, 30, 25, 715, DateTimeKind.Local).AddTicks(5384), "8ead58cb-a4c4-430e-9bf4-082614ddd2d7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "22a1d95e-a505-4934-9f76-178c3798d64a", "7ca7b293-0236-47cf-88d8-5165102b89ad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDate", "SecurityStamp" },
                values: new object[] { "15fc381d-143e-4037-9215-2c8044b4dfe2", "AQAAAAIAAYagAAAAEOO0mEcx1pvmchcLwF/3rChEy+4xjx1U+9zBeDTuZ4nTXaWj1lf5Ua0xHqnh2zlZfw==", new DateTime(2024, 3, 4, 12, 10, 57, 433, DateTimeKind.Local).AddTicks(4132), "efffe1c4-9b41-41bc-bafa-5836f93d8761" });
        }
    }
}
