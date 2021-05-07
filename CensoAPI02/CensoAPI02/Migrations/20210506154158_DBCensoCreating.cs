using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class DBCensoCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HR_Users",
                columns: table => new
                {
                    HR_UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    User_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    User_Rol = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    User_Creeation_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_Creation_User = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    User_Modification_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_Modification_User = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    User_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_Users", x => x.HR_UserId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location_Creation_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location_Creation_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location_Modification_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location_Modification_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationsId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question_Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Question_Creation_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Question_Creation_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question_Modification_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Question_Modification_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    ThemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Theme_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Theme_Creation_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Theme_Creation_User = table.Column<int>(type: "int", nullable: false),
                    Theme_Modification_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Theme_Modification_User = table.Column<int>(type: "int", nullable: false),
                    Theme_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.ThemeId);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Request_Theme = table.Column<int>(type: "int", nullable: false),
                    Request_User_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Request_Employee_Type = table.Column<int>(type: "int", nullable: false),
                    Request_Issue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Request_Area = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Request_Employee_Leader = table.Column<int>(type: "int", nullable: false),
                    Request_Answer_Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Request_Attachement = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Request_Creation_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Request_Creation_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Request_Modification_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Request_Modification_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Requests_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HRU_Theme",
                columns: table => new
                {
                    hruserId = table.Column<int>(type: "int", nullable: false),
                    themeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRU_Theme", x => new { x.hruserId, x.themeId });
                    table.ForeignKey(
                        name: "FK_HRU_Theme_HR_Users_hruserId",
                        column: x => x.hruserId,
                        principalTable: "HR_Users",
                        principalColumn: "HR_UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HRU_Theme_Theme_themeId",
                        column: x => x.themeId,
                        principalTable: "Theme",
                        principalColumn: "ThemeId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Question_Theme",
                columns: table => new
                {
                    questionId = table.Column<int>(type: "int", nullable: false),
                    themeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Theme", x => new { x.questionId, x.themeId });
                    table.ForeignKey(
                        name: "FK_Question_Theme_Questions_questionId",
                        column: x => x.questionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Question_Theme_Theme_themeId",
                        column: x => x.themeId,
                        principalTable: "Theme",
                        principalColumn: "ThemeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HRU_Theme_themeId",
                table: "HRU_Theme",
                column: "themeId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Theme_LocationsId",
                table: "Location_Theme",
                column: "LocationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Theme_themeId",
                table: "Location_Theme",
                column: "themeId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Theme_themeId",
                table: "Question_Theme",
                column: "themeId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_QuestionId",
                table: "Requests",
                column: "QuestionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HRU_Theme");

            migrationBuilder.DropTable(
                name: "Location_Theme");

            migrationBuilder.DropTable(
                name: "Question_Theme");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "HR_Users");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
