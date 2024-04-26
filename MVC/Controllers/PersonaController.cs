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
        //public async Task<ActionResult> Index()
        //{
        //    var httpCliente = new HttpClient();
        //    var json = await httpCliente.GetStringAsync("https://localhost:7260/persona");
        //    var personas = JsonConvert.DeserializeObject<List<Persona>>(json);

        //    return View(personas);
        //}
        public async Task<ActionResult> Index()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://localhost:7260/persona");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var personas = JsonConvert.DeserializeObject<List<Persona>>(json);
                    return View(personas);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error al comunicarse con el servidor remoto: " + ex.Message);
                ModelState.AddModelError("", "Error al comunicarse con el servidor remoto.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error inesperado: " + ex.Message);
                return RedirectToAction("Error");
            }

            return View(new List<Persona>());
        }


        public ActionResult Crear()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Crear( Persona persona)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var httpCliente = new HttpClient();
        //        var personaSerializable = JsonConvert.SerializeObject(persona);
        //        var content = new StringContent(personaSerializable, Encoding.UTF8, "application/json");
        //        await httpCliente.PostAsync("https://localhost:7260/persona", content);


        //        return RedirectToAction("Index");
        //    }
        //    return View(persona);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(Persona persona)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(persona);
                }

                using (var httpClient = new HttpClient())
                {
                    var personaJson = JsonConvert.SerializeObject(persona);
                    var content = new StringContent(personaJson, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("https://localhost:7260/persona", content);
                    response.EnsureSuccessStatusCode();
                }

                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error al comunicarse con el servidor remoto: " + ex.Message);
                ModelState.AddModelError("", "Error al comunicarse con el servidor remoto.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error inesperado: " + ex.Message);
                return RedirectToAction("Error");
            }

            return View(persona);
        }

        //public async Task<Persona> encontrar(int? id)
        //{
        //    var httpCliente = new HttpClient();
        //    var json = await httpCliente.GetStringAsync($"https://localhost:7260/persona/{id}");
        //    var persona = JsonConvert.DeserializeObject<Persona>(json);
        //    return persona;
        //}

        public async Task<Persona> encontrar(int? id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync($"https://localhost:7260/persona/{id}");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var persona = JsonConvert.DeserializeObject<Persona>(json);
                    return persona;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error al comunicarse con el servidor remoto: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error inesperado: " + ex.Message);
                return null;
            }
        }


        //public async Task<ActionResult> Editar(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new BadRequestResult();
        //    }
        //    Persona persona = await encontrar(id);
        //    if (persona == null)
        //    {
        //        return new NotFoundResult();
        //    }
        //    return View(persona);
        //}

        public async Task<ActionResult> Editar(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var persona = await encontrar(id);

                if (persona == null)
                {
                    return NotFound();
                }

                return View(persona);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error inesperado: " + ex.Message);
                return RedirectToAction("Error");
            }
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Editar(Persona persona)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var httpCliente = new HttpClient();
        //        var personaSerializable = JsonConvert.SerializeObject(persona);
        //        var content = new StringContent(personaSerializable, Encoding.UTF8, "application/json");
        //        await httpCliente.PutAsync($"https://localhost:7260/persona/", content);
        //        return RedirectToAction("Index");

        //    }
        //    return View(persona);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Persona persona)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(persona);
                }

                using (var httpClient = new HttpClient())
                {
                    var personaJson = JsonConvert.SerializeObject(persona);
                    var content = new StringContent(personaJson, Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync("https://localhost:7260/persona/", content);
                    response.EnsureSuccessStatusCode();
                }

                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error al comunicarse con el servidor remoto: " + ex.Message);
                ModelState.AddModelError("", "Error al comunicarse con el servidor remoto.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error inesperado: " + ex.Message);
                return RedirectToAction("Error");
            }

            return View(persona);
        }



        //public async Task<ActionResult> Detalles(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new BadRequestResult();
        //    }
        //    Persona persona = await encontrar(id);
        //    if (persona == null)
        //    {
        //        return new NotFoundResult();
        //    }
        //    return View(persona);
        //}

        public async Task<ActionResult> Detalles(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var persona = await encontrar(id);

                if (persona == null)
                {
                    return NotFound();
                }

                return View(persona);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error inesperado: " + ex.Message);
                return RedirectToAction("Error");
            }
        }


        //public async Task<ActionResult> Eliminar(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new BadRequestResult();
        //    }
        //    Persona persona = await encontrar(id);
        //    if (persona == null)
        //    {
        //        return new NotFoundResult();
        //    }
        //    return View(persona);
        //}
        public async Task<ActionResult> Eliminar(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var persona = await encontrar(id);

                if (persona == null)
                {
                    return NotFound();
                }

                return View(persona);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error inesperado: " + ex.Message);
                return RedirectToAction("Error");
            }
        }


        //[HttpPost, ActionName("Eliminar")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var httpCliente = new HttpClient();
        //    await httpCliente.DeleteAsync($"https://localhost:7260/persona/{id}");

        //    return RedirectToAction("Index");
        //}

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmarEliminar(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync($"https://localhost:7260/persona/{id}");
                    response.EnsureSuccessStatusCode();
                }

                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error al comunicarse con el servidor remoto: " + ex.Message);
                ModelState.AddModelError("", "Error al comunicarse con el servidor remoto.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error inesperado: " + ex.Message);
                return RedirectToAction("Error");
            }

            return RedirectToAction("Index");
        }


    }
}
