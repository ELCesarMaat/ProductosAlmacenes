using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductosAlmacenes.Migrations
{
    /// <inheritdoc />
    public partial class Migracion3Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TipoMovimientos",
                columns: new[] { "TipoMovimientoID", "Nombre" },
                values: new object[,]
                {
                    { 1, "Entrada" },
                    { 2, "Salida" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TipoMovimientos",
                keyColumn: "TipoMovimientoID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoMovimientos",
                keyColumn: "TipoMovimientoID",
                keyValue: 2);
        }
    }
}
