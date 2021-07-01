using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class RelationshipLocationsAndRequestAndAnonRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "AnonRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_LocationId",
                table: "Requests",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequests_LocationId",
                table: "AnonRequests",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonRequests_Locations_LocationId",
                table: "AnonRequests",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "lId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Locations_LocationId",
                table: "Requests",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "lId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonRequests_Locations_LocationId",
                table: "AnonRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Locations_LocationId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_LocationId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_AnonRequests_LocationId",
                table: "AnonRequests");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "AnonRequests");
        }
    }
}
