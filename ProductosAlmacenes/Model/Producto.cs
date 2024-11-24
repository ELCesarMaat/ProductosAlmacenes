using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductosAlmacenes.Model
{
    public class Producto
    {
        [Key] public int ProductoID { get; set; }
        [Required] [MaxLength(50)] public string UPC { get; set; }
        [Required] [MaxLength(100)] public string Nombre { get; set; }

        public string? Descripcion { get; set; }

        [Column(TypeName = "decimal(10,2)")] public decimal Costo { get; set; }
        [Column(TypeName = "decimal(10,2)")] public decimal Precio { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Relación con Existencias
        public ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    }
}