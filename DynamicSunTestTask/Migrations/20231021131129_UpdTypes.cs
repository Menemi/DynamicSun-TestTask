using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicSunTestTask.Migrations
{
    /// <inheritdoc />
    public partial class UpdTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Weathers",
                newName: "DateAndTime");

            migrationBuilder.AlterColumn<double>(
                name: "Rh",
                table: "Weathers",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateAndTime",
                table: "Weathers",
                newName: "DateTime");

            migrationBuilder.AlterColumn<int>(
                name: "Rh",
                table: "Weathers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
