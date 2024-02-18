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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seminars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lecturer = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OrganizerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SeminarsParticipants",
                columns: table => new
                {
                    SeminarId = table.Column<int>(type: "int", nullable: false),
                    ParticipantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeminarsParticipants", x => new { x.SeminarId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_SeminarsParticipants_AspNetUsers_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeminarsParticipants_Seminars_SeminarId",
                        column: x => x.SeminarId,
                        principalTable: "Seminars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e642c1df-5c1c-4957-a8a6-9654ad56bfd4", 0, "d4f20ca8-377e-44a2-8a03-b9c452ec41dd", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEALRMv6abgoUyI82zIzCpBXv86VWP1oTa+cLGEKwHnQ5RkGUUTPN9E7glSwUZcyflQ==", null, false, "cbe3d4a0-7077-420a-adbd-ba166eaa13d7", false, "test@softuni.bg" });

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
                values: new object[] { 1, 1, new DateTime(2024, 3, 7, 19, 0, 0, 0, DateTimeKind.Unspecified), "", 35, "Yani Lozanova", "e642c1df-5c1c-4957-a8a6-9654ad56bfd4", "AI Unmasked: Beyond Myths and Limits" });

            migrationBuilder.InsertData(
                table: "Seminars",
                columns: new[] { "Id", "CategoryId", "DateAndTime", "Details", "Duration", "Lecturer", "OrganizerId", "Topic" },
                values: new object[] { 2, 3, new DateTime(2024, 3, 30, 17, 15, 0, 0, DateTimeKind.Unspecified), "", 45, "WOODY NORRIS", "e642c1df-5c1c-4957-a8a6-9654ad56bfd4", "Hypersonic sound and other inventions" });

            migrationBuilder.InsertData(
                table: "Seminars",
                columns: new[] { "Id", "CategoryId", "DateAndTime", "Details", "Duration", "Lecturer", "OrganizerId", "Topic" },
                values: new object[] { 3, 4, new DateTime(2024, 4, 25, 13, 0, 0, 0, DateTimeKind.Unspecified), "", 120, "Sarah Jones", "e642c1df-5c1c-4957-a8a6-9654ad56bfd4", "Let's reframe cancel culture" });

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
                keyValue: "e642c1df-5c1c-4957-a8a6-9654ad56bfd4");
        }
    }
}
