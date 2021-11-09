using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class PacienteController : Controller
    {
        private readonly TurnosContext db;

        public PacienteController(TurnosContext db)
        {
            this.db = db;
        }

        // GET: PacienteController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await db.Paciente.ToListAsync());
        }

        // GET: PacienteController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var paciente = await db.Paciente.FirstOrDefaultAsync(p => p.IdPaciente == id);

            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // GET: PacienteController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PacienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPaciente, Nombre, Apellido, Direccion, Telefono, Email")] PacienteModel paciente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Add(paciente);
                    await db.SaveChangesAsync();
                    TempData["mensaje"] = $"Se creo correctamente el paciente {paciente.Nombre} {paciente.Apellido}";
                    TempData["status"] = "green lighten-4 green-text text-darken-4";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["mensaje"] = "Hubo un error con los datos del paciente. Intenteló más tarde.";
                    TempData["status"] = "red lighten-4 red-text text-darken-4";
                    return RedirectToAction(nameof(Index));
                    //return NotFound();
                }
            }

            return View(paciente); // si el modelo no es valido.         
        }

        // GET: PacienteController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await db.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: PacienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPaciente, Nombre, Apellido, Direccion, Telefono, Email")] PacienteModel paciente)
        {
            if (id == paciente.IdPaciente)
            {
                if (ModelState.IsValid) // valdaciones de formulario validas?
                {
                    try
                    {
                        db.Update(paciente);
                        await db.SaveChangesAsync();
                        TempData["mensaje"] = $"Se actualizó correctamente los datos del paciente {paciente.Nombre} {paciente.Apellido}.";
                        TempData["status"] = "green lighten-4 green-text text-darken-4";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception) // execpcion si se produce un error en la conexion a la base de dato o otro tipo de error
                    {
                        if (!PacienteModelExists(paciente.IdPaciente))
                        {
                            TempData["mensaje"] = "El médico que acaba de ingresar ya está registrado.";
                            TempData["status"] = "red lighten-4 red-text text-darken-4";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            TempData["mensaje"] = "Hubo un error con los datos del medico. Intenteló más tarde.";
                            TempData["status"] = "red lighten-4 red-text text-darken-4";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            else
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: PacienteController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var paciente = await db.Paciente.FirstOrDefaultAsync(p => p.IdPaciente == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: PacienteController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id != null)
            {
                try
                {
                    var paciente = await db.Paciente.FindAsync(id);
                    db.Paciente.Remove(paciente);
                    await db.SaveChangesAsync();
                    TempData["mensaje"] = "Se eliminó correctamente el paciente";
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
                TempData["mensaje"] = "No se puedo eliminar correctamente el paciente";
                TempData["status"] = "red lighten-4 red-text text-darken-4";
                return RedirectToAction(nameof(Index));
            }

        }

        private bool PacienteModelExists(int id)
        {
            return db.Medico.Any(e => e.IdMedico == id);
        }
    }
}
