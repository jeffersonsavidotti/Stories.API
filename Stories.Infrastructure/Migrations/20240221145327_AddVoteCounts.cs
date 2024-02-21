using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stories.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVoteCounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NegativeVotesCount",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositiveVotesCount",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NegativeVotesCount",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositiveVotesCount",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NegativeVotesCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PositiveVotesCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NegativeVotesCount",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "PositiveVotesCount",
                table: "Stories");
        }
    }
}
