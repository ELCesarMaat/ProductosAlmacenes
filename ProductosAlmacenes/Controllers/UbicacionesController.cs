using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductosAlmacenes.DTOs.Ubicacion;
using ProductosAlmacenes.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductosAlmacenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionesController(AlmacenDbContext db, IMapper mapper) : ControllerBase
    {
        private readonly AlmacenDbContext _dbContext = db;
        private readonly IMapper _mapper = mapper;

        [HttpPost("NuevaUbicacion")]
        public async Task<ActionResult<CreadoUbicacionDTO>> NuevaUbicacion(NuevoUbicacionDTO pUbicacion)
        {
            try
            {
                var nuevaUbicacion = _mapper.Map<UbicacionAlmacen>(pUbicacion);
                _dbContext.UbicacionAlmacenes.Add(nuevaUbicacion);
                await _dbContext.SaveChangesAsync();

                var nombreAlmacen = _dbContext.Almacenes
                    .FirstOrDefault(a => a.AlmacenID == nuevaUbicacion.AlmacenID)?.Nombre;

                var ubicacionCreada = new CreadoUbicacionDTO
                {
                    UbicacionID = nuevaUbicacion.UbicacionID,
                    NombreAlmacen = nombreAlmacen!,
                    Estante = nuevaUbicacion.Estante,
                    Nivel = nuevaUbicacion.Nivel,
                    Pasillo = nuevaUbicacion.Pasillo
                };
                return Ok(ubicacionCreada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}