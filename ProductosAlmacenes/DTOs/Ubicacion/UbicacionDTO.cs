using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductosAlmacenes.DTOs.Ubicacion
{
    public class UbicacionDTO
    {
        public int UbicacionID { get; set; }
        public int AlmacenID { get; set; }
        public int Pasillo { get; set; }
        public int Estante { get; set; }
        public int Nivel { get; set; }
        public bool Activo { get; set; }
    }
}
