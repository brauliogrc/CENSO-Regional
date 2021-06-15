using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class DataTypeCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lModificationUser",
                table: "HRU",
                newName: "uModificationUser");

            migrationBuilder.RenameColumn(
                name: "arMoficationUser",
                table: "AnonRequests",
                newName: "arModificationUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "uModificationUser",
                table: "HRU",
                newName: "lModificationUser");

            migrationBuilder.RenameColumn(
                name: "arModificationUser",
                table: "AnonRequests",
                newName: "arMoficationUser");
        }
    }
}
