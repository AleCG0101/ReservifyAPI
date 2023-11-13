using APIReservify.Models;
using static APIReservify.ViewModels.VMNegocio;

namespace APIReservify.Services
{
    public interface ICitaService
    {
        List<Citas> Get();
        List<Citas> GetCitasNegocio(int id);
        List<CitasUsuario> GetCitasUsuario(int id);
        Citas Get(string id);
        Citas Create(Citas cita);
        void Update (string id, Citas cita);
        void Remove (string id);
    }
}
