using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class CreationTableofAreaandRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rShip",
                table: "Requests",
                newName: "AreaId");

            migrationBuilder.RenameColumn(
                name: "arShip",
                table: "AnonRequests",
                newName: "AreaId");

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    aId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    locationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.aId);
                    table.ForeignKey(
                        name: "FK_Areas_Locations_locationId",
                        column: x => x.locationId,
                        principalTable: "Locations",
                        principalColumn: "lId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AreaId",
                table: "Requests",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequests_AreaId",
                table: "AnonRequests",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_locationId",
                table: "Areas",
                column: "locationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonRequests_Areas_AreaId",
                table: "AnonRequests",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "aId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Areas_AreaId",
                table: "Requests",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "aId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonRequests_Areas_AreaId",
                table: "AnonRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Areas_AreaId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Requests_AreaId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_AnonRequests_AreaId",
                table: "AnonRequests");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Requests",
                newName: "rShip");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "AnonRequests",
                newName: "arShip");
        }
    }
}
