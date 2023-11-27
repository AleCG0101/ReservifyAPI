using APIReservify.Models;
using APIReservify.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static APIReservify.ViewModels.VMNegocio;
using Firebase.Auth;
using Firebase.Storage;

namespace APIReservify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NegocioController : ControllerBase
    {
        public readonly ReservifyContext _dbcontext;
        public readonly FirebaseStorage _storage;

        public NegocioController(ReservifyContext _context, FirebaseStorage storage)
        {
            _dbcontext = _context;
            _storage = storage;
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
        public async Task<IActionResult> CrearNegocioAsync([FromForm] CrearNegocio negocio)
        {
            try
            {
                var usuario = _dbcontext.Usuarios.Find(negocio.IdUsuario);
                if (usuario == null)
                    return BadRequest("No se encontro el usuario");
                if (usuario.IdNegocio != 0)
                    return BadRequest("El usuario ya cuenta con un negocio");


                var stream = negocio.Foto.OpenReadStream();
                var fileName = Guid.NewGuid().ToString();
                var path = await _storage.Child("images").Child(fileName).PutAsync(stream);

                var newNegocio = new Negocio
                {
                    Categoria = negocio.Categoria,
                    Nombre = negocio.Nombre,
                    Descripcion = negocio.Descripcion,
                    Direccion = negocio.Direccion,
                    HoraApertura = negocio.HoraApertura,
                    HoraCierre = negocio.HoraCierre,
                    Foto = path,
                };

                _dbcontext.Add(newNegocio);
                _dbcontext.SaveChanges();

                usuario.IdNegocio = newNegocio.IdNegocio;
                _dbcontext.Usuarios.Update(usuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "okay", idNegocio = newNegocio.IdNegocio });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("editarNegocio")]
        public async Task<IActionResult> EditarNegocio([FromForm] EditarNegocio _negocio)
        {
            var negocio = _dbcontext.Negocios.Find(_negocio.IdNegocio);
            if (negocio == null)
                return BadRequest("Negocio no encontrado");
            try
            {
                string path = string.Empty;
                if (_negocio.Foto != null)
                {
                    var stream = _negocio.Foto.OpenReadStream();
                    var fileName = Guid.NewGuid().ToString();
                    path = await _storage.Child("images").Child(fileName).PutAsync(stream);
                }
                negocio.Categoria = string.IsNullOrEmpty(_negocio.Categoria) ? negocio.Categoria : _negocio.Categoria;
                negocio.Nombre = string.IsNullOrEmpty(_negocio.Nombre) ? negocio.Nombre : _negocio.Nombre;
                negocio.Direccion = string.IsNullOrEmpty(_negocio.Direccion) ? negocio.Direccion : _negocio.Direccion;
                negocio.HoraCierre = string.IsNullOrEmpty(_negocio.HoraCierre) ? negocio.HoraCierre : _negocio.HoraCierre;
                negocio.HoraApertura = string.IsNullOrEmpty(_negocio.HoraApertura) ? negocio.HoraApertura : _negocio.HoraApertura;
                negocio.Descripcion = string.IsNullOrEmpty(_negocio.Descripcion) ? negocio.Descripcion : _negocio.Descripcion;
                negocio.Foto = string.IsNullOrEmpty(path) ? negocio.Foto : path;

                _dbcontext.Negocios.Update(negocio);
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
