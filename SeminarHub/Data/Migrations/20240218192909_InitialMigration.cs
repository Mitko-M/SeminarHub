using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeminarHub.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Category identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Category name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seminars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Seminar identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Seminar topic"),
                    Lecturer = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Seminar lecturer"),
                    Details = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Seminar details"),
                    OrganizerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Seminar organizer"),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Seminar beginning time and date"),
                    Duration = table.Column<int>(type: "int", nullable: false, comment: "Seminar duration"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Seminar cateofry identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seminars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seminars_AspNetUsers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seminars_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeminarsParticipants",
                columns: table => new
                {
                    SeminarId = table.Column<int>(type: "int", nullable: false, comment: "Seminar identifier"),
                    ParticipantId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Identifier for an application user who is a participant in the seminar")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeminarsParticipants", x => new { x.SeminarId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_SeminarsParticipants_AspNetUsers_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeminarsParticipants_Seminars_SeminarId",
                        column: x => x.SeminarId,
                        principalTable: "Seminars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "edbd1198-8ddc-43ef-b11e-de11606f5b4b", 0, "af558cba-5150-4bec-b2f2-db2c8835b71f", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEIaPDypDSYgVm3o58SW1z13mDQj0c0v99cc+bzzYUntLA7cOD/SN71Qz6Zu1EXzmxw==", null, false, "abc38d25-2124-4eaa-9ad0-1dd7009a4731", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Technology & Innovation" },
                    { 2, "Business & Entrepreneurship" },
                    { 3, "Science & Research" },
                    { 4, "Arts & Culture" }
                });

            migrationBuilder.InsertData(
                table: "Seminars",
                columns: new[] { "Id", "CategoryId", "DateAndTime", "Details", "Duration", "Lecturer", "OrganizerId", "Topic" },
                values: new object[] { 1, 1, new DateTime(2024, 3, 7, 19, 0, 0, 0, DateTimeKind.Unspecified), "", 35, "Yani Lozanova", "edbd1198-8ddc-43ef-b11e-de11606f5b4b", "AI Unmasked: Beyond Myths and Limits" });

            migrationBuilder.InsertData(
                table: "Seminars",
                columns: new[] { "Id", "CategoryId", "DateAndTime", "Details", "Duration", "Lecturer", "OrganizerId", "Topic" },
                values: new object[] { 2, 3, new DateTime(2024, 3, 30, 17, 15, 0, 0, DateTimeKind.Unspecified), "", 45, "WOODY NORRIS", "edbd1198-8ddc-43ef-b11e-de11606f5b4b", "Hypersonic sound and other inventions" });

            migrationBuilder.InsertData(
                table: "Seminars",
                columns: new[] { "Id", "CategoryId", "DateAndTime", "Details", "Duration", "Lecturer", "OrganizerId", "Topic" },
                values: new object[] { 3, 4, new DateTime(2024, 4, 25, 13, 0, 0, 0, DateTimeKind.Unspecified), "", 120, "Sarah Jones", "edbd1198-8ddc-43ef-b11e-de11606f5b4b", "Let's reframe cancel culture" });

            migrationBuilder.CreateIndex(
                name: "IX_Seminars_CategoryId",
                table: "Seminars",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Seminars_OrganizerId",
                table: "Seminars",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_SeminarsParticipants_ParticipantId",
                table: "SeminarsParticipants",
                column: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeminarsParticipants");

            migrationBuilder.DropTable(
                name: "Seminars");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "edbd1198-8ddc-43ef-b11e-de11606f5b4b");
        }
    }
}
