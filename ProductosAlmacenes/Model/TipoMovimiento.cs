using System.ComponentModel.DataAnnotations;

namespace ProductosAlmacenes.Model
{
    public class TipoMovimiento
    {
        [Key] public int TipoMovimientoID { get; set; }
        public string Nombre { get; set; }
        public ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    }
}
