using ProductosAlmacenes.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductosAlmacenes.Model;

namespace ProductosAlmacenes.DTOs.Movimiento
{
    public class CreadoMovimientoDTO
    {
        public int MovimientoID { get; set; }
        public string TipoMovimiento { get; set; }
        public int Cantidad { get; set; } = 0;
        public string NombreProducto { get; set; }
        public string NombreAlmacen { get; set; }
        public int UbicacionID { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;

    }
}