using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class EspecialidadController : Controller
    {
        private readonly TurnosContext db;
        public EspecialidadController(TurnosContext db)
        {
            this.db = db;
        }
        // Por defecto -> Sicronico (empieza y termina)
        // Asincronico -> permite recibir multiples peticiones (!= Hilos en simultaneo)
        public async Task<IActionResult> Index()
        {
            return View(await db.Especialidad.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken] // valida los token enviados desde el formulario (particular)
        public async Task<IActionResult> Create([Bind("IdEspecialidad, Descripcion")] EspecialidadModel especialidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Add(especialidad);
                    await db.SaveChangesAsync();

                    TempData["mensaje"] = $"Se creo correctamente la especialidad {especialidad.Descripcion}";
                    TempData["status"] = "green lighten-4 green-text text-darken-4";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["mensaje"] = "Hubo un error al crear la especialidad. Intenteló más tarde.";
                    TempData["status"] = "red lighten-4 red-text text-darken-4";
                    return RedirectToAction(nameof(Index));
                    //return NotFound();
                }
            }
            return View(especialidad);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var especialidad = await db.Especialidad.FindAsync(id);
            if (especialidad == null)
            {
                return NotFound();
            }
            return View(especialidad);
        }

        [HttpPost] // Metodo edit que hace el UPDATE
        public async Task<IActionResult> Edit(int id, [Bind("IdEspecialidad, Descripcion")] EspecialidadModel especialidad)
        {
            if (id == especialidad.IdEspecialidad)
            {
                try
                {
                    if (ModelState.IsValid) // true -> Se realiza el UPDATE
                    {
                        db.Update(especialidad);
                        await db.SaveChangesAsync();
                        TempData["mensaje"] = $"Se actualizó correctamente la especialidad.";
                        TempData["status"] = "green lighten-4 green-text text-darken-4";
                        return RedirectToAction(nameof(Index)); // redirecciona una vez que hago los cambios
                    }
                }
                catch (Exception)
                {
                    if (!EspecialidadModelExists(especialidad.IdEspecialidad))
                    {
                        TempData["mensaje"] = "La especialidad que acaba de ingresar ya está registrada.";
                        TempData["status"] = "red lighten-4 red-text text-darken-4";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["mensaje"] = "Hubo un error con la especialidad. Intenteló más tarde.";
                        TempData["status"] = "red lighten-4 red-text text-darken-4";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else
            {
                return NotFound();
            }
            return View(especialidad);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var especialidad = await db.Especialidad.FirstOrDefaultAsync(e => e.IdEspecialidad == id);
            if (especialidad == null)
            {
                return NotFound();
            }
            return View(especialidad);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id != null)
            {
                try
                {
                    // Elimina la foreign key
                    var especialidad = await db.Especialidad.FindAsync(id);
                    db.Especialidad.Remove(especialidad);
                    await db.SaveChangesAsync();
                    TempData["mensaje"] = "Se eliminó correctamente la especialidad";
                    TempData["status"] = "green lighten-4 green-text text-darken-4";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e) // execpcion si se produce un error en la conexion a la base de dato o otro tipo de error
                {
                    return View(e.Message);
                }
            }
            else
            {
                TempData["mensaje"] = "No se puedo eliminar correctamente la especialidad";
                TempData["status"] = "red lighten-4 red-text text-darken-4";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool EspecialidadModelExists(int id)
        {
            return db.Medico.Any(e => e.IdMedico == id);
        }
    }
}
