using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMovies.Migrations
{
    /// <inheritdoc />
    public partial class NewNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Generes",
                table: "Generes");

            migrationBuilder.EnsureSchema(
                name: "movies");

            migrationBuilder.RenameTable(
                name: "Generes",
                newName: "GenresTbl",
                newSchema: "movies");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "movies",
                table: "GenresTbl",
                newName: "GenreName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenresTbl",
                schema: "movies",
                table: "GenresTbl",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GenresTbl",
                schema: "movies",
                table: "GenresTbl");

            migrationBuilder.RenameTable(
                name: "GenresTbl",
                schema: "movies",
                newName: "Generes");

            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Generes",
                newName: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Generes",
                table: "Generes",
                column: "Id");
        }
    }
}
