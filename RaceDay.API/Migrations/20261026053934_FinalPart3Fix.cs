using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceDay.API.Migrations
{
    /// <inheritdoc />
    public partial class FinalPart3Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "EventRoutes",
                newName: "RouteName");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteDescription",
                table: "EventRoutes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Enrolments");

            migrationBuilder.RenameColumn(
                name: "RouteName",
                table: "EventRoutes",
                newName: "CreatedAt");

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
        }
    }
}
