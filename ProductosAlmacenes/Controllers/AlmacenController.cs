using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosAlmacenes.DTOs.Almacen;
using ProductosAlmacenes.DTOs.Ubicacion;
using ProductosAlmacenes.Model;

namespace ProductosAlmacenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenController(AlmacenDbContext dbContext, IMapper mapper) : ControllerBase
    {
        private readonly AlmacenDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        [HttpPost("NuevoAlmacen")]
        public async Task<ActionResult<CreadoAlmacenDTO>> CrearNuevoAlmacen(NuevoAlmacenDTO pAlmacen)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var nuevoAlmacen = _mapper.Map<Almacen>(pAlmacen);

                _dbContext.Almacenes.Add(nuevoAlmacen);
                await _dbContext.SaveChangesAsync();

                var ubicacion = new UbicacionAlmacen
                {
                    Activo = true,
                    AlmacenID = nuevoAlmacen.AlmacenID,
                    Pasillo = 1,
                    Estante = 1,
                    Nivel = 1
                };

                _dbContext.UbicacionAlmacenes.Add(ubicacion);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(_mapper.Map<CreadoAlmacenDTO>(nuevoAlmacen));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(ex);
                return BadRequest(false);
            }
        }

        [HttpGet("ObtenerAlmacenPorId")]
        public async Task<ActionResult<CreadoAlmacenDTO>> ObtenerAlmacenPorId(int pId)
        {
            try
            {
                var almacen = _dbContext.Almacenes
                    .Include(almacen => almacen.Ubicaciones.Where(u => u.Activo))
                    .FirstOrDefault(a => a.AlmacenID == pId);
                if (almacen != null)
                {
                    return Ok(_mapper.Map<CreadoAlmacenDTO>(almacen));
                }

                return Ok(new CreadoAlmacenDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("ObtenerAlmacenes")]
        public async Task<ActionResult<List<CreadoAlmacenDTO>>> ObtenerAlmacenes()
        {
            try
            {
                var almacenes = _dbContext.Almacenes
                    .Include(a => a.Ubicaciones)
                    .AsNoTracking();
                return Ok(_mapper.Map<List<CreadoAlmacenDTO>>(almacenes));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("ObtenerUbicacionesPorAlmacenId")]
        public async Task<ActionResult<CreadoAlmacenDTO>> ObtenerUbicacionesPorAlmacenId(int pId)
        {
            try
            {
                var ubicaciones = _dbContext.Almacenes
                    .Include(almacen => almacen.Ubicaciones)
                    .FirstOrDefault(a => a.AlmacenID == pId && a.Activo)?
                    .Ubicaciones.Where(u => u.Activo);

                if (ubicaciones != null)
                {
                    return Ok(_mapper.Map<List<UbicacionDTO>>(ubicaciones));
                }

                return Ok(new List<CreadoAlmacenDTO>());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}