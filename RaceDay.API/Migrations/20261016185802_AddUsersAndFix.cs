using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceDay.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersAndFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Weathers_EventId",
                table: "Weathers");

            migrationBuilder.DropIndex(
                name: "IX_Results_EnrolmentId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "RouteDescription",
                table: "EventRoutes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Enrolments");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Events",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "RouteName",
                table: "EventRoutes",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "TEXT",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ElevationGainM",
                table: "EventRoutes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<double>(
                name: "DistanceKm",
                table: "EventRoutes",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_EventId",
                table: "Weathers",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_EnrolmentId",
                table: "Results",
                column: "EnrolmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_OrganizerId",
                table: "Events",
                column: "OrganizerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_OrganizerId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Weathers_EventId",
                table: "Weathers");

            migrationBuilder.DropIndex(
                name: "IX_Results_EnrolmentId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Events_OrganizerId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Events",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "EventRoutes",
                newName: "RouteName");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ElevationGainM",
                table: "EventRoutes",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DistanceKm",
                table: "EventRoutes",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RouteDescription",
                table: "EventRoutes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Enrolments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_EventId",
                table: "Weathers",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_EnrolmentId",
                table: "Results",
                column: "EnrolmentId",
                unique: true);
        }
    }
}
