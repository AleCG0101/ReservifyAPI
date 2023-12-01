using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIReservify.Models;
using System.Security.Cryptography;
using System.Text;

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
        public static string GetSHA256(string str)
        {
            SHA256 sHA256 = SHA256.Create();
            ASCIIEncoding encoding = new();
            byte[] stream;
            StringBuilder sb = new StringBuilder();
            stream = sHA256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
            return sb.ToString();
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
        [Route("acceder/{correo}/{pass}")]
        public IActionResult Acceder(string correo, string pass)
        {
            var usuario = new Usuario();
            try
            {
                string password = GetSHA256(pass);
                usuario = _dbcontext.Usuarios.Where(u => u.Correo == correo && u.Pass == password).FirstOrDefault();
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
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }
        }

        [HttpPost]
        [Route("registrarUsuario")]
        public IActionResult RegistrarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                usuario.Pass = GetSHA256(usuario.Pass);
                _dbcontext.Usuarios.Add(usuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "okay"});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }     

        [HttpPut]
        [Route("editarUsuario")]
        public IActionResult EditarUsuario([FromBody] Usuario _usuario)
        {
            var usuario = _dbcontext.Usuarios.Find(_usuario.IdUsuario);
            if (usuario == null)
                return BadRequest("Usuario no encontrado");
            try
            {
                usuario.Nombre = string.IsNullOrEmpty(_usuario.Nombre) ? usuario.Nombre : _usuario.Nombre;
                usuario.Apellidos = string.IsNullOrEmpty(_usuario.Apellidos) ? usuario.Apellidos : _usuario.Apellidos;
                usuario.IdNegocio = _usuario.IdNegocio == null || _usuario.IdNegocio == 0 ? 0 : _usuario.IdNegocio;
                usuario.Correo = string.IsNullOrEmpty(_usuario.Correo) ? usuario.Correo : _usuario.Correo;
                usuario.Telefono = string.IsNullOrEmpty(_usuario.Telefono) ? usuario.Telefono : _usuario.Telefono;
                usuario.Pass = string.IsNullOrEmpty(_usuario.Pass) ? usuario.Pass : GetSHA256(_usuario.Pass);

                _dbcontext.Usuarios.Update(usuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "okay" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("editarPass/{id}/{pass}")]
        public IActionResult EditarPass(int id, string pass)
        {
            var usuario = _dbcontext.Usuarios.Find(id);
            if (usuario == null)
                return BadRequest("Usuario no encontrado");
            try
            {
                usuario.Pass = GetSHA256(pass);
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
