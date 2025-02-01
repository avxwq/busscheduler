using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RozkladApi.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseInit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusStops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusLineId = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                name: "UserBusLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    BusLineId = table.Column<int>(type: "INTEGER", nullable: false),
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "UserBusLines");

            migrationBuilder.DropTable(
                name: "BusStops");

            migrationBuilder.DropTable(
                name: "BusLines");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
