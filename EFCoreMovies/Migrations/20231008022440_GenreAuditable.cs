using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMovies.Migrations
{
    /// <inheritdoc />
    public partial class GenreAuditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Generes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Generes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateBy", "ModifiedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateBy", "ModifiedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateBy", "ModifiedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateBy", "ModifiedBy" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Generes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateBy", "ModifiedBy" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Generes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Generes");
        }
    }
}
