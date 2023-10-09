using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMovies.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "invoice");

            migrationBuilder.CreateSequence<int>(
                name: "InvoiceNumber",
                schema: "invoice");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceNumber",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR invoice.InvoiceNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Invoices");

            migrationBuilder.DropSequence(
                name: "InvoiceNumber",
                schema: "invoice");
        }
    }
}
