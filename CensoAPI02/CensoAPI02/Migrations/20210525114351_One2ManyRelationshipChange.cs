using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class One2ManyRelationshipChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationsId",
                table: "HR_Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HR_Users_LocationsId",
                table: "HR_Users",
                column: "LocationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_HR_Users_Locations_LocationsId",
                table: "HR_Users",
                column: "LocationsId",
                principalTable: "Locations",
                principalColumn: "LocationsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HR_Users_Locations_LocationsId",
                table: "HR_Users");

            migrationBuilder.DropIndex(
                name: "IX_HR_Users_LocationsId",
                table: "HR_Users");

            migrationBuilder.DropColumn(
                name: "LocationsId",
                table: "HR_Users");
        }
    }
}
