using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMovies.Migrations
{
    /// <inheritdoc />
    public partial class ExampleToGenreEntity : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "Example2",
                table: "Generes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Example", "Example2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Example", "Example2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Example", "Example2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Example", "Example2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Example", "Example2" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Example",
                table: "Generes");

            migrationBuilder.DropColumn(
                name: "Example2",
                table: "Generes");
        }
    }
}
