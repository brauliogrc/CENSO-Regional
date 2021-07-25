using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CensoAPI02.Migrations
{
    public partial class FirstStableVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Requests_RequestId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_AnonRequestId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_RequestId",
                table: "Answer");

            migrationBuilder.RenameColumn(
                name: "asCrestionDate",
                table: "Answer",
                newName: "asCreationDate");

            migrationBuilder.AlterColumn<int>(
                name: "rModificationUser",
                table: "Requests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "rModificationDate",
                table: "Requests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "Answer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AnonRequestId",
                table: "Answer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "arModificationUser",
                table: "AnonRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "arModificationDate",
                table: "AnonRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Requests_RequestId",
                table: "Answer",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "rId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Requests_RequestId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_AnonRequestId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_RequestId",
                table: "Answer");

            migrationBuilder.RenameColumn(
                name: "asCreationDate",
                table: "Answer",
                newName: "asCrestionDate");

            migrationBuilder.AlterColumn<int>(
                name: "rModificationUser",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "rModificationDate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "Answer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnonRequestId",
                table: "Answer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "arModificationUser",
                table: "AnonRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "arModificationDate",
                table: "AnonRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answer_AnonRequestId",
                table: "Answer",
                column: "AnonRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answer_RequestId",
                table: "Answer",
                column: "RequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Requests_RequestId",
                table: "Answer",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "rId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
