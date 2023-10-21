using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicSunTestTask.Migrations
{
    /// <inheritdoc />
    public partial class SplitDatetoDayMonthYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Weathers");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Weathers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Weathers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Weathers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Weathers");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Weathers");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Weathers");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Weathers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
