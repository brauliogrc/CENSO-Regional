using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class AddUserNameFieldinRequestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lCreationuser",
                table: "Locations",
                newName: "lCreationUser");

            migrationBuilder.AddColumn<string>(
                name: "rUserName",
                table: "Requests",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rUserName",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "lCreationUser",
                table: "Locations",
                newName: "lCreationuser");
        }
    }
}
