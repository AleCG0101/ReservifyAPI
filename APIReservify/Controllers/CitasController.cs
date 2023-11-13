using APIReservify.Models;
using APIReservify.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIReservify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService citaService;

        public CitasController(ICitaService citaService)
        { 
            this.citaService = citaService;
        }
        // GET: api/<CitasController>
        [HttpGet]
        public ActionResult<List<Citas>> Get()
        {
            return citaService.Get();
        }

        // GET: api/<CitasController>/GetCitasNegocio/2       
        [HttpGet]
        [Route("GetCitasNegocio/{id}")]
        public ActionResult<List<Citas>> GetCitasNegocio(int id)
        {
            return citaService.GetCitasNegocio(id);
        }

        [HttpGet]
        [Route("GetCitasUsuario/{id}")]
        public ActionResult<List<Citas>> GetCitasUsuario(int id)
        {
            return citaService.GetCitasUsuario(id);
        }

        // GET api/<CitasController>/5
        [HttpGet("{id}")]
        public ActionResult<Citas> Get(string id)
        {
            var cita = citaService.Get(id);
            if (cita == null)
            {
                return NotFound($"Cita no encontrada");
            }
            return cita;
        }

        // POST api/<CitasController>
        [HttpPost]
        public ActionResult<Citas> Post([FromBody] Citas cita)
        {
            citaService.Create(cita);
            return CreatedAtAction(nameof(Get), new {id = cita.Id}, cita);
        }

        // PUT api/<CitasController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Citas cita)
        {
            var citaExistente = citaService.Get(id);

            if (citaExistente == null)
            {
                return NotFound("Cita no encontrada");
            }
            citaService.Update(id, cita);
            return NoContent();
        }

        // DELETE api/<CitasController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var cita = citaService.Get(id);

            if (cita == null)
            {
                return NotFound("Cita no encontrada");
            }
            citaService.Remove(cita.Id);

            return Ok("Cita eliminada");
        }
    }
}
