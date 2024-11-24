using System.ComponentModel.DataAnnotations;

namespace ProductosAlmacenes.Model
{
    public class Almacen
    {
        [Key] public int AlmacenID { get; set; }

        [Required] [MaxLength(100)] public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string UbicacionGeografica { get; set; }

        public DateTime FechaDeAlta { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;
        public ICollection<UbicacionAlmacen> Ubicaciones { get; set; } = new List<UbicacionAlmacen>();


    }
}