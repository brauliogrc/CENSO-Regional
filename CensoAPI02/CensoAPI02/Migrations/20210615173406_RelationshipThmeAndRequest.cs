using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class RelationshipThmeAndRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "AnonRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ThemeId",
                table: "Requests",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequests_ThemeId",
                table: "AnonRequests",
                column: "ThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonRequests_Theme_ThemeId",
                table: "AnonRequests",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "tId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Theme_ThemeId",
                table: "Requests",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "tId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonRequests_Theme_ThemeId",
                table: "AnonRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Theme_ThemeId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ThemeId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_AnonRequests_ThemeId",
                table: "AnonRequests");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "AnonRequests");
        }
    }
}
