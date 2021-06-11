using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class RelationshipCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonRequest_Questions_QuestionId",
                table: "AnonRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerAnonStatus_AnonRequest_anRequestId",
                table: "AnswerAnonStatus");

            migrationBuilder.DropIndex(
                name: "IX_Requests_QuestionId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnonRequest",
                table: "AnonRequest");

            migrationBuilder.DropIndex(
                name: "IX_AnonRequest_QuestionId",
                table: "AnonRequest");

            migrationBuilder.RenameTable(
                name: "AnonRequest",
                newName: "AnonRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnonRequests",
                table: "AnonRequests",
                column: "arId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_QuestionId",
                table: "Requests",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequests_QuestionId",
                table: "AnonRequests",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonRequests_Questions_QuestionId",
                table: "AnonRequests",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "qId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerAnonStatus_AnonRequests_anRequestId",
                table: "AnswerAnonStatus",
                column: "anRequestId",
                principalTable: "AnonRequests",
                principalColumn: "arId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonRequests_Questions_QuestionId",
                table: "AnonRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerAnonStatus_AnonRequests_anRequestId",
                table: "AnswerAnonStatus");

            migrationBuilder.DropIndex(
                name: "IX_Requests_QuestionId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnonRequests",
                table: "AnonRequests");

            migrationBuilder.DropIndex(
                name: "IX_AnonRequests_QuestionId",
                table: "AnonRequests");

            migrationBuilder.RenameTable(
                name: "AnonRequests",
                newName: "AnonRequest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnonRequest",
                table: "AnonRequest",
                column: "arId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_QuestionId",
                table: "Requests",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnonRequest_QuestionId",
                table: "AnonRequest",
                column: "QuestionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnonRequest_Questions_QuestionId",
                table: "AnonRequest",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "qId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerAnonStatus_AnonRequest_anRequestId",
                table: "AnswerAnonStatus",
                column: "anRequestId",
                principalTable: "AnonRequest",
                principalColumn: "arId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
