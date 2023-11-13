﻿using APIReservify.Models;

namespace APIReservify.Services
{
    public interface ICitaService
    {
        List<Citas> Get();
        List<Citas> GetCitasNegocio(int id);
        List<Citas> GetCitasUsuario(int id);
        Citas Get(string id);
        Citas Create(Citas cita);
        void Update (string id, Citas cita);
        void Remove (string id);
    }
}
