using APIReservify.Controllers;
using APIReservify.Models;
using MongoDB.Driver;
using static APIReservify.ViewModels.VMNegocio;

namespace APIReservify.Services
{
    public class CitaService : ICitaService
    {
        private readonly IMongoCollection<Citas> _citas;
        private readonly ReservifyContext _dbcontext;

        public CitaService(ICitasStoreDataBaseSettings settings, ReservifyContext _context)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _citas = database.GetCollection<Citas>(settings.ReservifyCitasCollectionName);
            _dbcontext = _context;
        }
        public Citas Create(Citas cita)
        {
            _citas.InsertOne(cita);
            return cita;
        }
        public List<Citas> Get()
        {
            return _citas.Find(cita => true).ToList();
        }
        public List<CitasNegocio> GetCitasNegocio(int id)
        {
            try
            {
                List<CitasNegocio> citasNegocio = new List<CitasNegocio>();
                var citas = _citas.Find(cita => cita.Id_negocio == id).ToList();
                foreach (var cita in citas)
                {
                    var usuario = _dbcontext.Usuarios.Find(cita.Id_usuario);
                    var citaNegocio = new CitasNegocio
                    {
                        Id = cita.Id,
                        Fecha = cita.Fecha,
                        Hora = cita.Hora,
                        Id_negocio = cita.Id_negocio,
                        Id_usuario = cita.Id_usuario,
                    };
                    citaNegocio.NombreUsuario = usuario != null ? string.Concat(usuario.Nombre, " ", usuario.Apellidos) : string.Empty;

                    citasNegocio.Add(citaNegocio);
                }
                return citasNegocio;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CitasUsuario> GetCitasUsuario(int id)
        {
            try
            {
                List<CitasUsuario> citasUsuario = new List<CitasUsuario>();
                
                var citas = _citas.Find(cita => cita.Id_usuario == id).ToList();
                foreach (var cita in citas)
                {
                    var negocio = _dbcontext.Negocios.Find(cita.Id_negocio);
                    var citaUsuario = new CitasUsuario
                    {
                        Id = cita.Id,
                        Fecha = cita.Fecha,
                        Hora = cita.Hora,
                        Id_negocio = cita.Id_negocio,
                        Id_usuario = cita.Id_usuario,

                    };
                    if (negocio != null)
                    {
                        citaUsuario.NombreNegocio = negocio.Nombre;
                        citaUsuario.DireccionNegocio = negocio.Direccion;
                        citaUsuario.CategoriaNegocio = negocio.Categoria;                       
                    }

                    citasUsuario.Add(citaUsuario);
                }

                return citasUsuario;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public List<CitasUsuarioApp> GetCitasUsuarioApp(int id)
        {
            try
            {
                List<CitasUsuarioApp> citasUsuario = new List<CitasUsuarioApp>();

                var citas = _citas.Find(cita => cita.Id_usuario == id).ToList();
                foreach (var cita in citas)
                {
                    var negocio = _dbcontext.Negocios.Find(cita.Id_negocio);
                    var citaUsuario = new CitasUsuarioApp
                    {
                        Id = cita.Id,
                        Fecha = cita.Fecha,
                        Hora = cita.Hora,
                        Id_negocio = cita.Id_negocio,
                        Id_usuario = cita.Id_usuario,

                    };
                    if (negocio != null)
                    {
                        citaUsuario.NombreNegocio = negocio.Nombre;
                        citaUsuario.DireccionNegocio = negocio.Direccion;
                        citaUsuario.CategoriaNegocio = negocio.Categoria;
                        citaUsuario.Foto = negocio.Foto;
                    }

                    citasUsuario.Add(citaUsuario);
                }

                return citasUsuario;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public Citas Get(string id)
        {
            return _citas.Find(cita => cita.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _citas.DeleteOne(cita => cita.Id == id);
        }
        public void Update(string id, Citas cita)
        {
            _citas.ReplaceOne(cita => cita.Id == id, cita);
        }
    }
}
