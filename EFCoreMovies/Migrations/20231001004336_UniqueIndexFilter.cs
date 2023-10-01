using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMovies.Migrations
{
    /// <inheritdoc />
    public partial class UniqueIndexFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Generes_Name",
                table: "Generes");

            migrationBuilder.CreateIndex(
                name: "IX_Generes_Name",
                table: "Generes",
                column: "Name",
                unique: true,
                filter: "isDeleted = 'false'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Generes_Name",
                table: "Generes");

            migrationBuilder.CreateIndex(
                name: "IX_Generes_Name",
                table: "Generes",
                column: "Name",
                unique: true);
        }
    }
}
