﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductosAlmacenes.DTOs.Producto
{
    public class NuevoProductoDTO
    {
        public string Nombre { get; set; }
        public string UPC { get; set; }
        public string? Descripcion { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
    }
}