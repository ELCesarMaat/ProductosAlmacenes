using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductosAlmacenes.Model
{
    public class UbicacionAlmacen
    {
        [Key] public int UbicacionID { get; set; }
        public int Pasillo { get; set; }
        public int Estante { get; set; }
        public int Nivel { get; set; }
        public bool Activo { get; set; } = true;
        [Required] public int AlmacenID { get; set; }
        [ForeignKey(nameof(AlmacenID))] public Almacen Almacen { get; set; }
        public ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();


    }
}