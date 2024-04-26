using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public interface IPersonaServicio
    {
        public Task<List<Persona>> Obtener();
        public Task<Persona> ObtenerPorId(int personaId);
        public Task Insertar(Persona persona);
        public Task Actualizar(Persona persona);
        public Task EliminarPorId(int personaId);

    }
}
