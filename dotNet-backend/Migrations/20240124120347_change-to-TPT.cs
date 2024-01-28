using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotNet_backend.Migrations
{
    /// <inheritdoc />
    public partial class changetoTPT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coaches_Email",
                table: "Coaches");

            migrationBuilder.DropIndex(
                name: "IX_Coaches_Username",
                table: "Coaches");

            migrationBuilder.DropIndex(
                name: "IX_Athletes_Email",
                table: "Athletes");

            migrationBuilder.DropIndex(
                name: "IX_Athletes_Username",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Athletes");

            migrationBuilder.AddForeignKey(
                name: "FK_Athletes_Users_Id",
                table: "Athletes",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_Users_Id",
                table: "Coaches",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Athletes_Users_Id",
                table: "Athletes");

            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_Users_Id",
                table: "Coaches");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Coaches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Coaches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Coaches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Coaches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Athletes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Athletes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Athletes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Athletes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Athletes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Athletes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Athletes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_Email",
                table: "Coaches",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_Username",
                table: "Coaches",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_Email",
                table: "Athletes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_Username",
                table: "Athletes",
                column: "Username",
                unique: true);
        }
    }
}
