using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class AllRelationsipM2MUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_HR_UserTheme_ThemesThemeId",
                table: "HR_UserTheme",
                column: "ThemesThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTheme_ThemesThemeId",
                table: "QuestionTheme",
                column: "ThemesThemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HR_UserTheme");

            migrationBuilder.DropTable(
                name: "QuestionTheme");
        }
    }
}
