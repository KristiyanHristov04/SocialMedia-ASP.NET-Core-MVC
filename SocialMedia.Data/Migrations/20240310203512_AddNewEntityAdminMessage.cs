using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntityAdminMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminMessages", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDate", "SecurityStamp" },
                values: new object[] { "bcae76f4-185a-4ad0-8a94-92be4c7c7cdf", "AQAAAAIAAYagAAAAEINqReLIJMfTXPBCf7teoLsdEY1/DnVCmoSuSfsmay6fDTVkUcF82+lXYGm+BtsPXQ==", new DateTime(2024, 3, 10, 22, 35, 11, 426, DateTimeKind.Local).AddTicks(2443), "8ce39547-94c0-4cef-af82-ef1065d1b775" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminMessages");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDate", "SecurityStamp" },
                values: new object[] { "266e042d-8a52-4ad8-a005-193ad1a3fe2b", "AQAAAAIAAYagAAAAEKTzg0+X8FnugvbY//HTlIs4tT3enJSRjZHfqRhWl1w8xvZnnXSraJoctBYJPnGLrA==", new DateTime(2024, 3, 4, 15, 30, 25, 715, DateTimeKind.Local).AddTicks(5384), "8ead58cb-a4c4-430e-9bf4-082614ddd2d7" });
        }
    }
}
