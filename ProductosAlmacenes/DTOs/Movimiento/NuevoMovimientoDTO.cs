using ProductosAlmacenes.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductosAlmacenes.Model;

namespace ProductosAlmacenes.DTOs.Movimiento
{
    public class NuevoMovimientoDTO
    {
        public int ProductoID { get; set; }
        public int UbicacionID { get; set; }
        public int Cantidad { get; set; } = 0;
        public int TipoMovimientoID { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}
