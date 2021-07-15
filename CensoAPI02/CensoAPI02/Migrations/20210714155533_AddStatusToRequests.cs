using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class AddStatusToRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "AnonRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RequestStatus",
                columns: table => new
                {
                    rsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rsStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatus", x => x.rsId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_StatusId",
                table: "Requests",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequests_StatusId",
                table: "AnonRequests",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonRequests_RequestStatus_StatusId",
                table: "AnonRequests",
                column: "StatusId",
                principalTable: "RequestStatus",
                principalColumn: "rsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestStatus_StatusId",
                table: "Requests",
                column: "StatusId",
                principalTable: "RequestStatus",
                principalColumn: "rsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonRequests_RequestStatus_StatusId",
                table: "AnonRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestStatus_StatusId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropIndex(
                name: "IX_Requests_StatusId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_AnonRequests_StatusId",
                table: "AnonRequests");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "AnonRequests");
        }
    }
}
