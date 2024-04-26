using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MVC.Controllers
{
    public class PersonaController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var httpCliente = new HttpClient();
            var json = await httpCliente.GetStringAsync("https://localhost:7260/persona");
            var personas = JsonConvert.DeserializeObject<List<Persona>>(json);

            return View(personas);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear( Persona persona)
        {
            if (ModelState.IsValid)
            {
                var httpCliente = new HttpClient();
                var personaSerializable = JsonConvert.SerializeObject(persona);
                var content = new StringContent(personaSerializable, Encoding.UTF8, "application/json");
                await httpCliente.PostAsync("https://localhost:7260/persona", content);


                return RedirectToAction("Index");
            }
            return View(persona);
        }

        public async Task<Persona> encontrar(int? id)
        {
            var httpCliente = new HttpClient();
            var json = await httpCliente.GetStringAsync($"https://localhost:7260/persona/{id}");
            var persona = JsonConvert.DeserializeObject<Persona>(json);
            return persona;
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            Persona persona = await encontrar(id);
            if (persona == null)
            {
                return new NotFoundResult();
            }
            return View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Persona persona)
        {
            if (ModelState.IsValid)
            {
                var httpCliente = new HttpClient();
                var personaSerializable = JsonConvert.SerializeObject(persona);
                var content = new StringContent(personaSerializable, Encoding.UTF8, "application/json");
                await httpCliente.PutAsync($"https://localhost:7260/persona/", content);
                return RedirectToAction("Index");

            }
            return View(persona);
        }

        public async Task<ActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            Persona persona = await encontrar(id);
            if (persona == null)
            {
                return new NotFoundResult();
            }
            return View(persona);
        }

        public async Task<ActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            Persona persona = await encontrar(id);
            if (persona == null)
            {
                return new NotFoundResult();
            }
            return View(persona);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(int id)
        {
            var httpCliente = new HttpClient();
            await httpCliente.DeleteAsync($"https://localhost:7260/persona/{id}");

            return RedirectToAction("Index");
        }

    }
}
