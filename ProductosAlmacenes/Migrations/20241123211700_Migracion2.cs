using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductosAlmacenes.Migrations
{
    /// <inheritdoc />
    public partial class Migracion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Existencias");

            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "Almacenes");

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Almacenes",
                newName: "FechaDeAlta");

            migrationBuilder.AddColumn<decimal>(
                name: "Costo",
                table: "Productos",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UPC",
                table: "Productos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Almacenes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Almacenes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UbicacionGeografica",
                table: "Almacenes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TipoMovimientos",
                columns: table => new
                {
                    TipoMovimientoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMovimientos", x => x.TipoMovimientoID);
                });

            migrationBuilder.CreateTable(
                name: "UbicacionAlmacenes",
                columns: table => new
                {
                    UbicacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pasillo = table.Column<int>(type: "int", nullable: false),
                    Estante = table.Column<int>(type: "int", nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    AlmacenID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionAlmacenes", x => x.UbicacionID);
                    table.ForeignKey(
                        name: "FK_UbicacionAlmacenes_Almacenes_AlmacenID",
                        column: x => x.AlmacenID,
                        principalTable: "Almacenes",
                        principalColumn: "AlmacenID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    MovimientoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    UbicacionID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    TipoMovimientoID = table.Column<int>(type: "int", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.MovimientoID);
                    table.ForeignKey(
                        name: "FK_Movimientos_Productos_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Productos",
                        principalColumn: "ProductoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimientos_TipoMovimientos_TipoMovimientoID",
                        column: x => x.TipoMovimientoID,
                        principalTable: "TipoMovimientos",
                        principalColumn: "TipoMovimientoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimientos_UbicacionAlmacenes_UbicacionID",
                        column: x => x.UbicacionID,
                        principalTable: "UbicacionAlmacenes",
                        principalColumn: "UbicacionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_ProductoID",
                table: "Movimientos",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_TipoMovimientoID",
                table: "Movimientos",
                column: "TipoMovimientoID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_UbicacionID",
                table: "Movimientos",
                column: "UbicacionID");

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionAlmacenes_AlmacenID",
                table: "UbicacionAlmacenes",
                column: "AlmacenID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropTable(
                name: "TipoMovimientos");

            migrationBuilder.DropTable(
                name: "UbicacionAlmacenes");

            migrationBuilder.DropColumn(
                name: "Costo",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "UPC",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Almacenes");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Almacenes");

            migrationBuilder.DropColumn(
                name: "UbicacionGeografica",
                table: "Almacenes");

            migrationBuilder.RenameColumn(
                name: "FechaDeAlta",
                table: "Almacenes",
                newName: "FechaCreacion");

            migrationBuilder.AddColumn<string>(
                name: "Ubicacion",
                table: "Almacenes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Existencias",
                columns: table => new
                {
                    ExistenciaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlmacenID = table.Column<int>(type: "int", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Existencias", x => x.ExistenciaID);
                    table.ForeignKey(
                        name: "FK_Existencias_Almacenes_AlmacenID",
                        column: x => x.AlmacenID,
                        principalTable: "Almacenes",
                        principalColumn: "AlmacenID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Existencias_Productos_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Productos",
                        principalColumn: "ProductoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Existencias_AlmacenID",
                table: "Existencias",
                column: "AlmacenID");

            migrationBuilder.CreateIndex(
                name: "IX_Existencias_ProductoID",
                table: "Existencias",
                column: "ProductoID");
        }
    }
}
