using ProductosAlmacenes.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductosAlmacenes.DTOs.Almacen
{
    public class NuevoAlmacenDTO
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string UbicacionGeografica { get; set; }
    }
}