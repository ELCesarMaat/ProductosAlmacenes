using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductosAlmacenes.Model;

public class Movimiento
{
    [Key] public int MovimientoID { get; set; }

    [Required] public int ProductoID { get; set; }

    [Required] public int UbicacionID { get; set; }

    [Required] public int Cantidad { get; set; } = 0;

    [Required] public int TipoMovimientoID { get; set; }

    public DateTime FechaActualizacion { get; set; } = DateTime.Now;

    [ForeignKey(nameof(ProductoID))] public Producto Producto { get; set; }
    [ForeignKey(nameof(UbicacionID))] public UbicacionAlmacen Ubicacion { get; set; }
    [ForeignKey(nameof(TipoMovimientoID))] public TipoMovimiento TipoMovimiento { get; set; }
}