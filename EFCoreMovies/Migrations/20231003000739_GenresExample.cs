using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMovies.Migrations
{
    /// <inheritdoc />
    public partial class GenresExample : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Example",
                table: "Generes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Example",
                value: null);

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Example",
                value: null);

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Example",
                value: null);

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Example",
                value: null);

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Example",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Example",
                table: "Generes");
        }
    }
}
