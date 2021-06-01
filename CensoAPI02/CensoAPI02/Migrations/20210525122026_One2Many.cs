using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class One2Many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HR_Users_Locations_LocationsId",
                table: "HR_Users");

            migrationBuilder.AlterColumn<int>(
                name: "LocationsId",
                table: "HR_Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HR_Users_Locations_LocationsId",
                table: "HR_Users",
                column: "LocationsId",
                principalTable: "Locations",
                principalColumn: "LocationsId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HR_Users_Locations_LocationsId",
                table: "HR_Users");

            migrationBuilder.AlterColumn<int>(
                name: "LocationsId",
                table: "HR_Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_HR_Users_Locations_LocationsId",
                table: "HR_Users",
                column: "LocationsId",
                principalTable: "Locations",
                principalColumn: "LocationsId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
