using APIReservify.Models;
using APIReservify.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static APIReservify.ViewModels.VMNegocio;

namespace APIReservify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NegocioController : ControllerBase
    {
        public readonly ReservifyContext _dbcontext;

        public NegocioController(ReservifyContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        public IActionResult GetAllNegocios()
        {
            List<Negocio> negocios = new List<Negocio>();

            try
            {
                negocios = _dbcontext.Negocios.ToList();
                if (negocios == null)
                    return BadRequest("Sin resultados");
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "okay", response = negocios });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }
        }

        [HttpGet]
        [Route("negocioDetalle/{id}")]
        public IActionResult GetNegocioDetalle(int id)
        {
            var negocio = new Negocio();
            
            try
            {
                negocio = _dbcontext.Negocios.Find(id);
                if (negocio == null)
                    return BadRequest("Negocio no encontrado");
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "okay", response = negocio });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});

            }
        }

        [HttpGet]
        [Route("negocioUsuario/{id}")]
        public IActionResult GetNegocioUsuario(int id)
        {
            var negocio = new Negocio();
            var usuario = _dbcontext.Usuarios.Find(id);
            if (usuario == null)
                return BadRequest("No existe el usuario");
            try
            {
                negocio = _dbcontext.Negocios.Where(n => n.IdNegocio == usuario.IdNegocio).FirstOrDefault();
                if (negocio == null)
                    return BadRequest("Negocio no encontrado");
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "okay", response = negocio });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }
        }

        [HttpPost]
        [Route("crearNegocio")]
        public IActionResult CrearNegocio([FromBody] CrearNegocio negocio)
        {
            try
            {
                var usuario = _dbcontext.Usuarios.Find(negocio.IdUsuario);
                if (usuario == null)
                    return BadRequest("No se encontro el usuario");
                if (usuario.IdNegocio != 0)
                    return BadRequest("El usuario ya cuenta con un negocio");

                var newNegocio = new Negocio();
                newNegocio.Categoria = negocio.Categoria;
                newNegocio.Nombre = negocio.Nombre;
                newNegocio.Descripcion = negocio.Descripcion;
                newNegocio.Direccion = negocio.Direccion;
                newNegocio.HoraApertura = negocio.HoraApertura;
                newNegocio.HoraCierre = negocio.HoraCierre;

                _dbcontext.Add(newNegocio);
                _dbcontext.SaveChanges();

                usuario.IdNegocio = newNegocio.IdNegocio;
                _dbcontext.Usuarios.Update(usuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "okay" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


    }
}
