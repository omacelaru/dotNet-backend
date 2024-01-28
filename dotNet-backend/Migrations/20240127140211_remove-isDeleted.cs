using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotNet_backend.Migrations
{
    /// <inheritdoc />
    public partial class removeisDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Coaches_OldCoachId",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_OldCoachId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "OldCoachId",
                table: "Clubs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Clubs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "OldCoachId",
                table: "Clubs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_OldCoachId",
                table: "Clubs",
                column: "OldCoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Coaches_OldCoachId",
                table: "Clubs",
                column: "OldCoachId",
                principalTable: "Coaches",
                principalColumn: "Id");
        }
    }
}
