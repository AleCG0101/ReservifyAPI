using APIReservify.Models;
using MongoDB.Driver;

namespace APIReservify.Services
{
    public class CitaService : ICitaService
    {
        private readonly IMongoCollection<Citas> _citas;

        public CitaService(ICitasStoreDataBaseSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _citas = database.GetCollection<Citas>(settings.ReservifyCitasCollectionName);
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
        public List<Citas> GetCitasNegocio(int id)
        {
            return _citas.Find(cita => cita.Id_negocio == id).ToList();
        }
        public List<Citas> GetCitasUsuario(int id)
        {
            return _citas.Find(cita => cita.Id_usuario == id).ToList();
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
