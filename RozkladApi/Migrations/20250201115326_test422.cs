using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RozkladApi.Migrations
{
    /// <inheritdoc />
    public partial class test422 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_BusLines_LineId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Stops_StopId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Routes_RouteId",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Stops_RouteId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Stops");

            migrationBuilder.CreateTable(
                name: "RouteStop",
                columns: table => new
                {
                    RouteId = table.Column<string>(type: "TEXT", nullable: false),
                    StopId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteStop", x => new { x.RouteId, x.StopId });
                    table.ForeignKey(
                        name: "FK_RouteStop_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteStop_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteStop_StopId",
                table: "RouteStop",
                column: "StopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_BusLines_LineId",
                table: "Schedules",
                column: "LineId",
                principalTable: "BusLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Stops_StopId",
                table: "Schedules",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_BusLines_LineId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Stops_StopId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "RouteStop");

            migrationBuilder.AddColumn<string>(
                name: "RouteId",
                table: "Stops",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stops_RouteId",
                table: "Stops",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_BusLines_LineId",
                table: "Schedules",
                column: "LineId",
                principalTable: "BusLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Stops_StopId",
                table: "Schedules",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Routes_RouteId",
                table: "Stops",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id");
        }
    }
}
