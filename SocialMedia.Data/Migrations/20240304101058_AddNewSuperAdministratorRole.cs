using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewSuperAdministratorRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 2, 19, 18, 7, 728, DateTimeKind.Local).AddTicks(1779));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2de99791-ebc7-452d-b2d1-c4ea8a548136", null, "SuperAdministrator", "SUPERADMINISTRATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDate", "SecurityStamp" },
                values: new object[] { "15fc381d-143e-4037-9215-2c8044b4dfe2", "AQAAAAIAAYagAAAAEOO0mEcx1pvmchcLwF/3rChEy+4xjx1U+9zBeDTuZ4nTXaWj1lf5Ua0xHqnh2zlZfw==", new DateTime(2024, 3, 4, 12, 10, 57, 433, DateTimeKind.Local).AddTicks(4132), "efffe1c4-9b41-41bc-bafa-5836f93d8761" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2de99791-ebc7-452d-b2d1-c4ea8a548136", "7ca7b293-0236-47cf-88d8-5165102b89ad" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2de99791-ebc7-452d-b2d1-c4ea8a548136", "7ca7b293-0236-47cf-88d8-5165102b89ad" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2de99791-ebc7-452d-b2d1-c4ea8a548136");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 2, 19, 18, 7, 728, DateTimeKind.Local).AddTicks(1779),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ca7b293-0236-47cf-88d8-5165102b89ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDate", "SecurityStamp" },
                values: new object[] { "621257b2-8deb-450b-8456-1e44bbe3a872", "AQAAAAIAAYagAAAAEGwdfhqaiEPUJIpJsFiqzvCArO6wtHPWCquT23uh0iycOJ713GdmUIpWNLUwLySevQ==", new DateTime(2024, 3, 2, 19, 18, 7, 728, DateTimeKind.Local).AddTicks(1779), "bf8fe289-92b4-47a1-b5fa-4c148918feba" });
        }
    }
}
