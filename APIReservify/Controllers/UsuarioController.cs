using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIReservify.Models;

namespace APIReservify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly ReservifyContext _dbcontext;

        public UsuarioController(ReservifyContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuarioInfo(int id)
        {
            var usuario = new Usuario();
            try
            {
                usuario = _dbcontext.Usuarios.Find(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "okay", response = usuario });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = usuario });
            }

        }

        [HttpGet]
        [Route("acceder/{correo}{pass}")]
        public IActionResult Acceder(string correo, string pass)
        {
            var usuario = new Usuario();
            try
            {
                usuario = _dbcontext.Usuarios.Where(u => u.Correo == correo && u.Pass == pass).FirstOrDefault();
                if (usuario == null)
                {
                    return BadRequest("Usuario no encontrado");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "okay", response = usuario });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = usuario });
            }
        }
    }
}
