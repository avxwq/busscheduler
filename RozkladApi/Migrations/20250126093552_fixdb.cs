using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RozkladApi.Migrations
{
    /// <inheritdoc />
    public partial class fixdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "UserBusLines");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "BusStops");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BusLines",
                newName: "LineNumber");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BusLines",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusLineStops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusLineId = table.Column<int>(type: "INTEGER", nullable: false),
                    BusDepartureStopId = table.Column<int>(type: "INTEGER", nullable: false),
                    BusStopId = table.Column<int>(type: "INTEGER", nullable: false),
                    TravelTime = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusLineStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusLineStops_BusLines_BusLineId",
                        column: x => x.BusLineId,
                        principalTable: "BusLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusLineStops_BusStops_BusDepartureStopId",
                        column: x => x.BusDepartureStopId,
                        principalTable: "BusStops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusLineStops_BusStops_BusStopId",
                        column: x => x.BusStopId,
                        principalTable: "BusStops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusLines_UserId",
                table: "BusLines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusLineStops_BusDepartureStopId",
                table: "BusLineStops",
                column: "BusDepartureStopId");

            migrationBuilder.CreateIndex(
                name: "IX_BusLineStops_BusLineId",
                table: "BusLineStops",
                column: "BusLineId");

            migrationBuilder.CreateIndex(
                name: "IX_BusLineStops_BusStopId",
                table: "BusLineStops",
                column: "BusStopId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusLines_Users_UserId",
                table: "BusLines",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusLines_Users_UserId",
                table: "BusLines");

            migrationBuilder.DropTable(
                name: "BusLineStops");

            migrationBuilder.DropIndex(
                name: "IX_BusLines_UserId",
                table: "BusLines");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BusLines");

            migrationBuilder.RenameColumn(
                name: "LineNumber",
                table: "BusLines",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "BusStops",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusLineId = table.Column<int>(type: "INTEGER", nullable: false),
                    BusStopId = table.Column<int>(type: "INTEGER", nullable: false),
                    StopOrder = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_BusLines_BusLineId",
                        column: x => x.BusLineId,
                        principalTable: "BusLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Routes_BusStops_BusStopId",
                        column: x => x.BusStopId,
                        principalTable: "BusStops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusLineId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_BusLines_BusLineId",
                        column: x => x.BusLineId,
                        principalTable: "BusLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBusLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusLineId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBusLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBusLines_BusLines_BusLineId",
                        column: x => x.BusLineId,
                        principalTable: "BusLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBusLines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_BusLineId",
                table: "Routes",
                column: "BusLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_BusStopId",
                table: "Routes",
                column: "BusStopId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_BusLineId",
                table: "Schedules",
                column: "BusLineId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBusLines_BusLineId",
                table: "UserBusLines",
                column: "BusLineId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBusLines_UserId",
                table: "UserBusLines",
                column: "UserId");
        }
    }
}
