using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMedia.Data.Data.Migrations
{
    /// <inheritdoc />
    /// 
    [ExcludeFromCodeCoverage]
    public partial class SeedAdminAndUserRolesAndSeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "22a1d95e-a505-4934-9f76-178c3798d64a", null, "Administrator", "ADMINISTRATOR" },
                    { "23fd43c7-113f-4b97-8d21-6cd8661b96b1", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CountryId", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7ca7b293-0236-47cf-88d8-5165102b89ad", 0, "e7426f2b-7ae1-49f4-87db-621de62f0553", 26, "admin@socialmedia.com", true, "Georgi", "Ivanov", false, null, "ADMIN@SOCIALMEDIA.COM", "ADMIN", "AQAAAAIAAYagAAAAEK7mJaQBv2iXJ9su7ZyP+1JqUaZ7WYTdBmqJY2jgjF/wVMa8vNxuQDpUl6pRN1JkZA==", null, false, "d002a8bf-4504-4c15-85d0-69fad23bba88", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "22a1d95e-a505-4934-9f76-178c3798d64a", "7ca7b293-0236-47cf-88d8-5165102b89ad" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23fd43c7-113f-4b97-8d21-6cd8661b96b1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "22a1d95e-a505-4934-9f76-178c3798d64a", "7ca7b293-0236-47cf-88d8-5165102b89ad" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22a1d95e-a505-4934-9f76-178c3798d64a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad");
        }
    }
}
