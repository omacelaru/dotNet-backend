using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotNet_backend.Migrations
{
    /// <inheritdoc />
    public partial class makenullablecoachinathlete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Athletes_Coaches_CoachId",
                table: "Athletes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CoachId",
                table: "Athletes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Athletes_Coaches_CoachId",
                table: "Athletes",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Athletes_Coaches_CoachId",
                table: "Athletes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CoachId",
                table: "Athletes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Athletes_Coaches_CoachId",
                table: "Athletes",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
