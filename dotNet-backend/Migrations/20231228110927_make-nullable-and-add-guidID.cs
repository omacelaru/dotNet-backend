using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotNet_backend.Migrations
{
    /// <inheritdoc />
    public partial class makenullableandaddguidID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_Clubs_ClubId",
                table: "Coaches");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClubId",
                table: "Coaches",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_Clubs_ClubId",
                table: "Coaches",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_Clubs_ClubId",
                table: "Coaches");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClubId",
                table: "Coaches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_Clubs_ClubId",
                table: "Coaches",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
