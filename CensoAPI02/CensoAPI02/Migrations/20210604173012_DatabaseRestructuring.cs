using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class DatabaseRestructuring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HR_UserTheme");

            migrationBuilder.DropTable(
                name: "HRU_Theme");

            migrationBuilder.DropTable(
                name: "LocationsTheme");

            migrationBuilder.DropTable(
                name: "Question_Theme");

            migrationBuilder.DropTable(
                name: "QuestionTheme");

            migrationBuilder.DropTable(
                name: "HR_Users");

            migrationBuilder.DropColumn(
                name: "Request_Answer_Status",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Request_Area",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Request_User_Name",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "Theme_Status",
                table: "Theme",
                newName: "tStatus");

            migrationBuilder.RenameColumn(
                name: "Theme_Name",
                table: "Theme",
                newName: "tName");

            migrationBuilder.RenameColumn(
                name: "Theme_Modification_date",
                table: "Theme",
                newName: "tModificationDate");

            migrationBuilder.RenameColumn(
                name: "Theme_Modification_User",
                table: "Theme",
                newName: "tModificationUser");

            migrationBuilder.RenameColumn(
                name: "Theme_Creation_User",
                table: "Theme",
                newName: "tCreationUser");

            migrationBuilder.RenameColumn(
                name: "Theme_Creation_Date",
                table: "Theme",
                newName: "tCreationDate");

            migrationBuilder.RenameColumn(
                name: "ThemeId",
                table: "Theme",
                newName: "tId");

            migrationBuilder.RenameColumn(
                name: "Request_Theme",
                table: "Requests",
                newName: "rModificationUser");

            migrationBuilder.RenameColumn(
                name: "Request_Modification_User",
                table: "Requests",
                newName: "rEmployeeType");

            migrationBuilder.RenameColumn(
                name: "Request_Modification_Date",
                table: "Requests",
                newName: "rModificationDate");

            migrationBuilder.RenameColumn(
                name: "Request_Issue",
                table: "Requests",
                newName: "rIssue");

            migrationBuilder.RenameColumn(
                name: "Request_Employee_Type",
                table: "Requests",
                newName: "rEmployeeLeader");

            migrationBuilder.RenameColumn(
                name: "Request_Employee_Leader",
                table: "Requests",
                newName: "rCreationUser");

            migrationBuilder.RenameColumn(
                name: "Request_Creation_User",
                table: "Requests",
                newName: "rArea");

            migrationBuilder.RenameColumn(
                name: "Request_Creation_Date",
                table: "Requests",
                newName: "rCreationDate");

            migrationBuilder.RenameColumn(
                name: "Request_Attachement",
                table: "Requests",
                newName: "rAttachement");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "Requests",
                newName: "rId");

            migrationBuilder.RenameColumn(
                name: "Question_Status",
                table: "Questions",
                newName: "qStatus");

            migrationBuilder.RenameColumn(
                name: "Question_Name",
                table: "Questions",
                newName: "qName");

            migrationBuilder.RenameColumn(
                name: "Question_Modification_User",
                table: "Questions",
                newName: "qModificationUser");

            migrationBuilder.RenameColumn(
                name: "Question_Modification_Date",
                table: "Questions",
                newName: "qModificationDate");

            migrationBuilder.RenameColumn(
                name: "Question_Creation_User",
                table: "Questions",
                newName: "qCreationUser");

            migrationBuilder.RenameColumn(
                name: "Question_Creation_Date",
                table: "Questions",
                newName: "qCreationDate");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Questions",
                newName: "qId");

            migrationBuilder.RenameColumn(
                name: "Location_Status",
                table: "Locations",
                newName: "lStatus");

            migrationBuilder.RenameColumn(
                name: "Location_Name",
                table: "Locations",
                newName: "lName");

            migrationBuilder.RenameColumn(
                name: "Location_Modification_User",
                table: "Locations",
                newName: "lModificationUser");

            migrationBuilder.RenameColumn(
                name: "Location_Modification_Date",
                table: "Locations",
                newName: "lModificationDate");

            migrationBuilder.RenameColumn(
                name: "Location_Creation_User",
                table: "Locations",
                newName: "lCreationuser");

            migrationBuilder.RenameColumn(
                name: "Location_Creation_Date",
                table: "Locations",
                newName: "lCreationDate");

            migrationBuilder.RenameColumn(
                name: "LocationsId",
                table: "Locations",
                newName: "lId");

            migrationBuilder.CreateTable(
                name: "AnonRequest",
                columns: table => new
                {
                    arId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    arEmployeeType = table.Column<int>(type: "int", nullable: false),
                    arShip = table.Column<int>(type: "int", nullable: false),
                    arIssue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    arAttachement = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    arCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    arModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    arMoficationUser = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonRequest", x => x.arId);
                    table.ForeignKey(
                        name: "FK_AnonRequest_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "qId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerStatus",
                columns: table => new
                {
                    asId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    asUserId = table.Column<int>(type: "int", nullable: false),
                    asAnswer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    asCrestionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerStatus", x => x.asId);
                    table.ForeignKey(
                        name: "FK_AnswerStatus_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "rId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HRU",
                columns: table => new
                {
                    uId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    uEmail = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    uRol = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    uCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    uCreationUser = table.Column<int>(type: "int", nullable: false),
                    uModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lModificationUser = table.Column<int>(type: "int", nullable: false),
                    uStatus = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRU", x => x.uId);
                    table.ForeignKey(
                        name: "FK_HRU_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "lId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationsThemes",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    ThemeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationsThemes", x => new { x.ThemeId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_LocationsThemes_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "lId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationsThemes_Theme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
                        principalColumn: "tId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsThemes",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    ThemeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsThemes", x => new { x.ThemeId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_QuestionsThemes_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "qId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsThemes_Theme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
                        principalColumn: "tId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerAnonStatus",
                columns: table => new
                {
                    anId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    anAnswer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    anUserId = table.Column<int>(type: "int", nullable: false),
                    anCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    anRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerAnonStatus", x => x.anId);
                    table.ForeignKey(
                        name: "FK_AnswerAnonStatus_AnonRequest_anRequestId",
                        column: x => x.anRequestId,
                        principalTable: "AnonRequest",
                        principalColumn: "arId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HRUsersThemes",
                columns: table => new
                {
                    HRUId = table.Column<int>(type: "int", nullable: false),
                    ThemeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRUsersThemes", x => new { x.ThemeId, x.HRUId });
                    table.ForeignKey(
                        name: "FK_HRUsersThemes_HRU_HRUId",
                        column: x => x.HRUId,
                        principalTable: "HRU",
                        principalColumn: "uId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HRUsersThemes_Theme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
                        principalColumn: "tId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequest_QuestionId",
                table: "AnonRequest",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerAnonStatus_anRequestId",
                table: "AnswerAnonStatus",
                column: "anRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerStatus_RequestId",
                table: "AnswerStatus",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HRU_LocationId",
                table: "HRU",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_HRUsersThemes_HRUId",
                table: "HRUsersThemes",
                column: "HRUId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationsThemes_LocationId",
                table: "LocationsThemes",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsThemes_QuestionId",
                table: "QuestionsThemes",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerAnonStatus");

            migrationBuilder.DropTable(
                name: "AnswerStatus");

            migrationBuilder.DropTable(
                name: "HRUsersThemes");

            migrationBuilder.DropTable(
                name: "LocationsThemes");

            migrationBuilder.DropTable(
                name: "QuestionsThemes");

            migrationBuilder.DropTable(
                name: "AnonRequest");

            migrationBuilder.DropTable(
                name: "HRU");

            migrationBuilder.RenameColumn(
                name: "tStatus",
                table: "Theme",
                newName: "Theme_Status");

            migrationBuilder.RenameColumn(
                name: "tName",
                table: "Theme",
                newName: "Theme_Name");

            migrationBuilder.RenameColumn(
                name: "tModificationUser",
                table: "Theme",
                newName: "Theme_Modification_User");

            migrationBuilder.RenameColumn(
                name: "tModificationDate",
                table: "Theme",
                newName: "Theme_Modification_date");

            migrationBuilder.RenameColumn(
                name: "tCreationUser",
                table: "Theme",
                newName: "Theme_Creation_User");

            migrationBuilder.RenameColumn(
                name: "tCreationDate",
                table: "Theme",
                newName: "Theme_Creation_Date");

            migrationBuilder.RenameColumn(
                name: "tId",
                table: "Theme",
                newName: "ThemeId");

            migrationBuilder.RenameColumn(
                name: "rModificationUser",
                table: "Requests",
                newName: "Request_Theme");

            migrationBuilder.RenameColumn(
                name: "rModificationDate",
                table: "Requests",
                newName: "Request_Modification_Date");

            migrationBuilder.RenameColumn(
                name: "rIssue",
                table: "Requests",
                newName: "Request_Issue");

            migrationBuilder.RenameColumn(
                name: "rEmployeeType",
                table: "Requests",
                newName: "Request_Modification_User");

            migrationBuilder.RenameColumn(
                name: "rEmployeeLeader",
                table: "Requests",
                newName: "Request_Employee_Type");

            migrationBuilder.RenameColumn(
                name: "rCreationUser",
                table: "Requests",
                newName: "Request_Employee_Leader");

            migrationBuilder.RenameColumn(
                name: "rCreationDate",
                table: "Requests",
                newName: "Request_Creation_Date");

            migrationBuilder.RenameColumn(
                name: "rAttachement",
                table: "Requests",
                newName: "Request_Attachement");

            migrationBuilder.RenameColumn(
                name: "rArea",
                table: "Requests",
                newName: "Request_Creation_User");

            migrationBuilder.RenameColumn(
                name: "rId",
                table: "Requests",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "qStatus",
                table: "Questions",
                newName: "Question_Status");

            migrationBuilder.RenameColumn(
                name: "qName",
                table: "Questions",
                newName: "Question_Name");

            migrationBuilder.RenameColumn(
                name: "qModificationUser",
                table: "Questions",
                newName: "Question_Modification_User");

            migrationBuilder.RenameColumn(
                name: "qModificationDate",
                table: "Questions",
                newName: "Question_Modification_Date");

            migrationBuilder.RenameColumn(
                name: "qCreationUser",
                table: "Questions",
                newName: "Question_Creation_User");

            migrationBuilder.RenameColumn(
                name: "qCreationDate",
                table: "Questions",
                newName: "Question_Creation_Date");

            migrationBuilder.RenameColumn(
                name: "qId",
                table: "Questions",
                newName: "QuestionId");

            migrationBuilder.RenameColumn(
                name: "lStatus",
                table: "Locations",
                newName: "Location_Status");

            migrationBuilder.RenameColumn(
                name: "lName",
                table: "Locations",
                newName: "Location_Name");

            migrationBuilder.RenameColumn(
                name: "lModificationUser",
                table: "Locations",
                newName: "Location_Modification_User");

            migrationBuilder.RenameColumn(
                name: "lModificationDate",
                table: "Locations",
                newName: "Location_Modification_Date");

            migrationBuilder.RenameColumn(
                name: "lCreationuser",
                table: "Locations",
                newName: "Location_Creation_User");

            migrationBuilder.RenameColumn(
                name: "lCreationDate",
                table: "Locations",
                newName: "Location_Creation_Date");

            migrationBuilder.RenameColumn(
                name: "lId",
                table: "Locations",
                newName: "LocationsId");

            migrationBuilder.AddColumn<string>(
                name: "Request_Answer_Status",
                table: "Requests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Request_Area",
                table: "Requests",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Request_User_Name",
                table: "Requests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HR_Users",
                columns: table => new
                {
                    HR_UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationsId = table.Column<int>(type: "int", nullable: false),
                    User_Creation_User = table.Column<int>(type: "int", nullable: false),
                    User_Creeation_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    User_Modification_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_Modification_User = table.Column<int>(type: "int", nullable: false),
                    User_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    User_Rol = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    User_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_Users", x => x.HR_UserId);
                    table.ForeignKey(
                        name: "FK_HR_Users_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "LocationsId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "QuestionTheme",
                columns: table => new
                {
                    QuestionsQuestionId = table.Column<int>(type: "int", nullable: false),
                    ThemesThemeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTheme", x => new { x.QuestionsQuestionId, x.ThemesThemeId });
                    table.ForeignKey(
                        name: "FK_QuestionTheme_Questions_QuestionsQuestionId",
                        column: x => x.QuestionsQuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionTheme_Theme_ThemesThemeId",
                        column: x => x.ThemesThemeId,
                        principalTable: "Theme",
                        principalColumn: "ThemeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HR_UserTheme",
                columns: table => new
                {
                    HR_UsersHR_UserId = table.Column<int>(type: "int", nullable: false),
                    ThemesThemeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_UserTheme", x => new { x.HR_UsersHR_UserId, x.ThemesThemeId });
                    table.ForeignKey(
                        name: "FK_HR_UserTheme_HR_Users_HR_UsersHR_UserId",
                        column: x => x.HR_UsersHR_UserId,
                        principalTable: "HR_Users",
                        principalColumn: "HR_UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HR_UserTheme_Theme_ThemesThemeId",
                        column: x => x.ThemesThemeId,
                        principalTable: "Theme",
                        principalColumn: "ThemeId",
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

            migrationBuilder.CreateIndex(
                name: "IX_HR_Users_LocationsId",
                table: "HR_Users",
                column: "LocationsId");

            migrationBuilder.CreateIndex(
                name: "IX_HR_UserTheme_ThemesThemeId",
                table: "HR_UserTheme",
                column: "ThemesThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_HRU_Theme_themeId",
                table: "HRU_Theme",
                column: "themeId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationsTheme_ThemesThemeId",
                table: "LocationsTheme",
                column: "ThemesThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Theme_themeId",
                table: "Question_Theme",
                column: "themeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTheme_ThemesThemeId",
                table: "QuestionTheme",
                column: "ThemesThemeId");
        }
    }
}
