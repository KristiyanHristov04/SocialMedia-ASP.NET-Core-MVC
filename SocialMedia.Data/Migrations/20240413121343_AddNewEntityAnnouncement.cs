using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntityAnnouncement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcements_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDate", "SecurityStamp" },
                values: new object[] { "d90a4da5-bd43-4ba0-93ed-a3857a90f952", "AQAAAAIAAYagAAAAEL+DpLQthgdhBEcA9TKSk8R8wvFrxRE6CJ08t3k2Y1ADkFL2OAkg048WRt3aKn4+bA==", new DateTime(2024, 4, 13, 15, 13, 42, 430, DateTimeKind.Local).AddTicks(5768), "673490d4-a945-4be2-a89b-f87424faeb20" });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_UserId",
                table: "Announcements",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDate", "SecurityStamp" },
                values: new object[] { "e766887b-8199-4f39-ae7e-577049c05df5", "AQAAAAIAAYagAAAAEETrpJtp+kfURP+M9YJs6eP4SktgmfZRPgD404FTYjP3tgTHXZTlU2IeAJby/zR2sQ==", new DateTime(2024, 3, 14, 22, 3, 10, 266, DateTimeKind.Local).AddTicks(3762), "05650ca5-7af0-4c94-86b5-9c9357318471" });
        }
    }
}
