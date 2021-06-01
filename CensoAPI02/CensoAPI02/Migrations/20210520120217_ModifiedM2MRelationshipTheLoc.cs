using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class ModifiedM2MRelationshipTheLoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location_Theme");

            migrationBuilder.CreateTable(
                name: "LocationsTheme",
                columns: table => new
                {
                    LocationsId = table.Column<int>(type: "int", nullable: false),
                    ThemesThemeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationsTheme", x => new { x.LocationsId, x.ThemesThemeId });
                    table.ForeignKey(
                        name: "FK_LocationsTheme_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "LocationsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationsTheme_Theme_ThemesThemeId",
                        column: x => x.ThemesThemeId,
                        principalTable: "Theme",
                        principalColumn: "ThemeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationsTheme_ThemesThemeId",
                table: "LocationsTheme",
                column: "ThemesThemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationsTheme");

            migrationBuilder.CreateTable(
                name: "Location_Theme",
                columns: table => new
                {
                    locationId = table.Column<int>(type: "int", nullable: false),
                    themeId = table.Column<int>(type: "int", nullable: false),
                    LocationsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location_Theme", x => new { x.locationId, x.themeId });
                    table.ForeignKey(
                        name: "FK_Location_Theme_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "LocationsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_Theme_Theme_themeId",
                        column: x => x.themeId,
                        principalTable: "Theme",
                        principalColumn: "ThemeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_Theme_LocationsId",
                table: "Location_Theme",
                column: "LocationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Theme_themeId",
                table: "Location_Theme",
                column: "themeId");
        }
    }
}
