using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class AddTableRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uRol",
                table: "HRU");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "HRU",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    rolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rolName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.rolId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HRU_RoleId",
                table: "HRU",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_HRU_Roles_RoleId",
                table: "HRU",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "rolId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRU_Roles_RoleId",
                table: "HRU");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_HRU_RoleId",
                table: "HRU");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "HRU");

            migrationBuilder.AddColumn<string>(
                name: "uRol",
                table: "HRU",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");
        }
    }
}
