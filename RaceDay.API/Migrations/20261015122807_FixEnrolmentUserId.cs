using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceDay.API.Migrations
{
    /// <inheritdoc />
    public partial class FixEnrolmentUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Users_ParticipantId",
                table: "Enrolments");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Enrolments_ParticipantId_CategoryId",
                table: "Enrolments");

            migrationBuilder.DropIndex(
                name: "IX_Categories_EventId_CategoryName",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "Enrolments",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Enrolments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrolments_CategoryId1",
                table: "Enrolments",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolments_UserId",
                table: "Enrolments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EventId",
                table: "Categories",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Categories_CategoryId1",
                table: "Enrolments",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Users_UserId",
                table: "Enrolments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Categories_CategoryId1",
                table: "Enrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Users_UserId",
                table: "Enrolments");

            migrationBuilder.DropIndex(
                name: "IX_Enrolments_CategoryId1",
                table: "Enrolments");

            migrationBuilder.DropIndex(
                name: "IX_Enrolments_UserId",
                table: "Enrolments");

            migrationBuilder.DropIndex(
                name: "IX_Categories_EventId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Enrolments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Enrolments",
                newName: "ParticipantId");

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnrolmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    FinishTime = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    Position = table.Column<int>(type: "INTEGER", nullable: true),
                    ResultStatus = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Results_Enrolments_EnrolmentId",
                        column: x => x.EnrolmentId,
                        principalTable: "Enrolments",
                        principalColumn: "EnrolmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrolments_ParticipantId_CategoryId",
                table: "Enrolments",
                columns: new[] { "ParticipantId", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EventId_CategoryName",
                table: "Categories",
                columns: new[] { "EventId", "CategoryName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_EnrolmentId",
                table: "Results",
                column: "EnrolmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Users_ParticipantId",
                table: "Enrolments",
                column: "ParticipantId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
