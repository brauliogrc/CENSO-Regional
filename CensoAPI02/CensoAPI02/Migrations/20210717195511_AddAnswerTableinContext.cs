using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class AddAnswerTableinContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerStatus_Requests_RequestId",
                table: "AnswerStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerStatus",
                table: "AnswerStatus");

            migrationBuilder.RenameTable(
                name: "AnswerStatus",
                newName: "Answer");

            migrationBuilder.RenameColumn(
                name: "asUserId",
                table: "Answer",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerStatus_RequestId",
                table: "Answer",
                newName: "IX_Answer_RequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "asId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_UserId",
                table: "Answer",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_HRU_UserId",
                table: "Answer",
                column: "UserId",
                principalTable: "HRU",
                principalColumn: "uId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Requests_RequestId",
                table: "Answer",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "rId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_HRU_UserId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Requests_RequestId",
                table: "Answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_UserId",
                table: "Answer");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "AnswerStatus");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AnswerStatus",
                newName: "asUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_RequestId",
                table: "AnswerStatus",
                newName: "IX_AnswerStatus_RequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerStatus",
                table: "AnswerStatus",
                column: "asId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerStatus_Requests_RequestId",
                table: "AnswerStatus",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "rId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
