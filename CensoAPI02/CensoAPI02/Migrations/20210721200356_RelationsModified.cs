using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class RelationsModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Locations_locationId",
                table: "Areas");

            migrationBuilder.DropTable(
                name: "AnswerAnonStatus");

            migrationBuilder.DropIndex(
                name: "IX_Areas_locationId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "locationId",
                table: "Areas");

            migrationBuilder.AddColumn<bool>(
                name: "aStatus",
                table: "Areas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AnonRequestId",
                table: "Answer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AreasLocations",
                columns: table => new
                {
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasLocations", x => new { x.LocationId, x.AreaId });
                    table.ForeignKey(
                        name: "FK_AreasLocations_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "aId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AreasLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "lId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_rModificationUser",
                table: "Requests",
                column: "rModificationUser");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_AnonRequestId",
                table: "Answer",
                column: "AnonRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequests_arModificationUser",
                table: "AnonRequests",
                column: "arModificationUser");

            migrationBuilder.CreateIndex(
                name: "IX_AreasLocations_AreaId",
                table: "AreasLocations",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonRequests_HRU_arModificationUser",
                table: "AnonRequests",
                column: "arModificationUser",
                principalTable: "HRU",
                principalColumn: "uId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_AnonRequests_AnonRequestId",
                table: "Answer",
                column: "AnonRequestId",
                principalTable: "AnonRequests",
                principalColumn: "arId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_HRU_rModificationUser",
                table: "Requests",
                column: "rModificationUser",
                principalTable: "HRU",
                principalColumn: "uId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonRequests_HRU_arModificationUser",
                table: "AnonRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_AnonRequests_AnonRequestId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_HRU_rModificationUser",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "AreasLocations");

            migrationBuilder.DropIndex(
                name: "IX_Requests_rModificationUser",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Answer_AnonRequestId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_AnonRequests_arModificationUser",
                table: "AnonRequests");

            migrationBuilder.DropColumn(
                name: "aStatus",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "AnonRequestId",
                table: "Answer");

            migrationBuilder.AddColumn<int>(
                name: "locationId",
                table: "Areas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnswerAnonStatus",
                columns: table => new
                {
                    anId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    anAnswer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    anCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    anRequestId = table.Column<int>(type: "int", nullable: false),
                    anUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerAnonStatus", x => x.anId);
                    table.ForeignKey(
                        name: "FK_AnswerAnonStatus_AnonRequests_anRequestId",
                        column: x => x.anRequestId,
                        principalTable: "AnonRequests",
                        principalColumn: "arId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_locationId",
                table: "Areas",
                column: "locationId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerAnonStatus_anRequestId",
                table: "AnswerAnonStatus",
                column: "anRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Locations_locationId",
                table: "Areas",
                column: "locationId",
                principalTable: "Locations",
                principalColumn: "lId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
