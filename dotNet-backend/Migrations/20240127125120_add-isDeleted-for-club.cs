using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotNet_backend.Migrations
{
    /// <inheritdoc />
    public partial class addisDeletedforclub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Clubs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Clubs");
        }
    }
}
