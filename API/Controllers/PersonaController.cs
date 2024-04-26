using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class PersonaController : Controller
    {
        private readonly IPersonaServicio personaServicio;

        public PersonaController(IPersonaServicio personaServicio)
        {
            this.personaServicio = personaServicio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> ObtenerTodoPersona()
        {
            try
            {
                return await personaServicio.Obtener();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno del servidor: " + ex.Message);
            }
        }

        [HttpGet("{personaId}")]
        public async Task<ActionResult<Persona>> ObtenerPersona(int personaId)
        {
            try
            {
                var estado = await personaServicio.ObtenerPorId(personaId);
                if (estado.PersonaId == 0)
                {
                    return NotFound("No se encontro la persona");
                }

                return estado;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno del servidor: " + ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Persona>> AgregarPersona(Persona persona)
        {
            try 
            { 
                await personaServicio.Insertar(persona);

                return CreatedAtAction("ObtenerPersona", new { PersonaId = persona.PersonaId }, persona);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno del servidor: " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarPersona( Persona persona)
        {
            try
            {
                var estado = await personaServicio.ObtenerPorId(persona.PersonaId);
                if (estado.PersonaId == 0)
                {
                    return NotFound("No se encontro la persona");
                }

                await personaServicio.Actualizar(persona);
               
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno del servidor: " + ex.Message);
            }
        }

        [HttpDelete("{personaId}")]
        public async Task<IActionResult> EliminarPersona(int personaId)
        {
            try
            {
                var estado = await personaServicio.ObtenerPorId(personaId);
                if (estado.PersonaId == 0)
                {
                    return NotFound("No se encontro la persona");
                }

                await personaServicio.EliminarPorId(personaId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error interno del servidor: " + ex.Message);
            }
        }
    }
}
