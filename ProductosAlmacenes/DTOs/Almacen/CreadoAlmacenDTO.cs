using ProductosAlmacenes.DTOs.Ubicacion;

namespace ProductosAlmacenes.DTOs.Almacen
{
    public class CreadoAlmacenDTO
    {
        public int AlmacenID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string UbicacionGeografica { get; set; }
        public ICollection<UbicacionDTO> Ubicaciones { get; set; }
    }
}