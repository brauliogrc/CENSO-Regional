using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class ModifiedHRUId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    aId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    aStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.aId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    lId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lCreationUser = table.Column<int>(type: "int", nullable: false),
                    lModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lModificationUser = table.Column<int>(type: "int", nullable: false),
                    lStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.lId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    qId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    qName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    qCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    qCreationUser = table.Column<int>(type: "int", nullable: false),
                    qModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    qModificationUser = table.Column<int>(type: "int", nullable: false),
                    qStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.qId);
                });

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

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    tId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tCreationUser = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    tModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tModificationUser = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    tStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.tId);
                });

            migrationBuilder.CreateTable(
                name: "AreasLocations",
                columns: table => new
                {
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasLocations", x => new { x.LocationId, x.AreaId });
                    table.ForeignKey(
                        name: "FK_AreasLocations_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "aId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AreasLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "lId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HRU",
                columns: table => new
                {
                    uEmployeeNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    uEmail = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    uSupervisorNumber = table.Column<long>(type: "bigint", nullable: false),
                    uCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    uCreationUser = table.Column<int>(type: "int", nullable: false),
                    uModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    uModificationUser = table.Column<int>(type: "int", nullable: true),
                    uStatus = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRU", x => x.uEmployeeNumber);
                    table.ForeignKey(
                        name: "FK_HRU_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "lId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HRU_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "rolId",
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
                name: "AnonRequests",
                columns: table => new
                {
                    arId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    arEmployeeType = table.Column<int>(type: "int", nullable: false),
                    arIssue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    arAttachement = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    arCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    arModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    arModificationUser = table.Column<long>(type: "bigint", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    ThemeId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonRequests", x => x.arId);
                    table.ForeignKey(
                        name: "FK_AnonRequests_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "aId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnonRequests_HRU_arModificationUser",
                        column: x => x.arModificationUser,
                        principalTable: "HRU",
                        principalColumn: "uEmployeeNumber");
                    table.ForeignKey(
                        name: "FK_AnonRequests_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "lId");
                    table.ForeignKey(
                        name: "FK_AnonRequests_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "qId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnonRequests_RequestStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "RequestStatus",
                        principalColumn: "rsId");
                    table.ForeignKey(
                        name: "FK_AnonRequests_Theme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
                        principalColumn: "tId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HRUsersThemes",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ThemeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRUsersThemes", x => new { x.ThemeId, x.UserId });
                    table.ForeignKey(
                        name: "FK_HRUsersThemes_HRU_UserId",
                        column: x => x.UserId,
                        principalTable: "HRU",
                        principalColumn: "uEmployeeNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HRUsersThemes_Theme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
                        principalColumn: "tId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    rId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rUserId = table.Column<int>(type: "int", nullable: false),
                    rUserName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    rIssue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    rAttachement = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    rEmployeeType = table.Column<int>(type: "int", nullable: false),
                    rEmployeeLeader = table.Column<int>(type: "int", nullable: false),
                    rCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    rModificationUser = table.Column<long>(type: "bigint", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    ThemeId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.rId);
                    table.ForeignKey(
                        name: "FK_Requests_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "aId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_HRU_rModificationUser",
                        column: x => x.rModificationUser,
                        principalTable: "HRU",
                        principalColumn: "uEmployeeNumber");
                    table.ForeignKey(
                        name: "FK_Requests_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "lId");
                    table.ForeignKey(
                        name: "FK_Requests_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "qId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_RequestStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "RequestStatus",
                        principalColumn: "rsId");
                    table.ForeignKey(
                        name: "FK_Requests_Theme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
                        principalColumn: "tId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    asId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    asAnswer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    asAttachement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    asCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: true),
                    AnonRequestId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.asId);
                    table.ForeignKey(
                        name: "FK_Answer_AnonRequests_AnonRequestId",
                        column: x => x.AnonRequestId,
                        principalTable: "AnonRequests",
                        principalColumn: "arId");
                    table.ForeignKey(
                        name: "FK_Answer_HRU_UserId",
                        column: x => x.UserId,
                        principalTable: "HRU",
                        principalColumn: "uEmployeeNumber");
                    table.ForeignKey(
                        name: "FK_Answer_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "rId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequests_AreaId",
                table: "AnonRequests",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequests_arModificationUser",
                table: "AnonRequests",
                column: "arModificationUser");

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequests_LocationId",
                table: "AnonRequests",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequests_QuestionId",
                table: "AnonRequests",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequests_StatusId",
                table: "AnonRequests",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequests_ThemeId",
                table: "AnonRequests",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_AnonRequestId",
                table: "Answer",
                column: "AnonRequestId",
                unique: true,
                filter: "[AnonRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_RequestId",
                table: "Answer",
                column: "RequestId",
                unique: true,
                filter: "[RequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_UserId",
                table: "Answer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AreasLocations_AreaId",
                table: "AreasLocations",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_HRU_LocationId",
                table: "HRU",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_HRU_RoleId",
                table: "HRU",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_HRUsersThemes_UserId",
                table: "HRUsersThemes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationsThemes_LocationId",
                table: "LocationsThemes",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsThemes_QuestionId",
                table: "QuestionsThemes",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AreaId",
                table: "Requests",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_LocationId",
                table: "Requests",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_QuestionId",
                table: "Requests",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_rModificationUser",
                table: "Requests",
                column: "rModificationUser");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_StatusId",
                table: "Requests",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ThemeId",
                table: "Requests",
                column: "ThemeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "AreasLocations");

            migrationBuilder.DropTable(
                name: "HRUsersThemes");

            migrationBuilder.DropTable(
                name: "LocationsThemes");

            migrationBuilder.DropTable(
                name: "QuestionsThemes");

            migrationBuilder.DropTable(
                name: "AnonRequests");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "HRU");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
