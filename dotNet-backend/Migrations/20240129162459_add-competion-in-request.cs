using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotNet_backend.Migrations
{
    /// <inheritdoc />
    public partial class addcompetioninrequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompetitionId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompetitionName",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CompetitionName",
                table: "Requests");
        }
    }
}
