using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RozkladApi.Migrations
{
    /// <inheritdoc />
    public partial class fixfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusLineStops");

            migrationBuilder.DropTable(
                name: "BusStops");

            migrationBuilder.RenameColumn(
                name: "LineNumber",
                table: "BusLines",
                newName: "Stops");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "BusLines",
                newName: "Number");

            migrationBuilder.CreateTable(
                name: "Departures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Weekdays = table.Column<string>(type: "TEXT", nullable: false),
                    Weekends = table.Column<string>(type: "TEXT", nullable: false),
                    Holidays = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    StartPoint = table.Column<string>(type: "TEXT", nullable: false),
                    EndPoint = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Zone = table.Column<string>(type: "TEXT", nullable: false),
                    DeparturesId = table.Column<string>(type: "TEXT", nullable: false),
                    RouteId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stops_Departures_DeparturesId",
                        column: x => x.DeparturesId,
                        principalTable: "Departures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stops_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LineId = table.Column<int>(type: "INTEGER", nullable: false),
                    StopId = table.Column<int>(type: "INTEGER", nullable: false),
                    Weekdays = table.Column<string>(type: "TEXT", nullable: false),
                    Saturdays = table.Column<string>(type: "TEXT", nullable: false),
                    Sundays = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_BusLines_LineId",
                        column: x => x.LineId,
                        principalTable: "BusLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_LineId",
                table: "Schedules",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_StopId",
                table: "Schedules",
                column: "StopId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_DeparturesId",
                table: "Stops",
                column: "DeparturesId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_RouteId",
                table: "Stops",
                column: "RouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "Departures");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.RenameColumn(
                name: "Stops",
                table: "BusLines",
                newName: "LineNumber");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "BusLines",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "BusStops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusLineStops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusDepartureStopId = table.Column<int>(type: "INTEGER", nullable: false),
                    BusLineId = table.Column<int>(type: "INTEGER", nullable: false),
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
        }
    }
}
