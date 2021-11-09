using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class MedicoController : Controller
    {
        private readonly TurnosContext db;

        public MedicoController(TurnosContext db)
        {
            this.db = db;
        }

        // GET: Medico
        public async Task<IActionResult> Index()
        {
            return View(await db.Medico.ToListAsync());
        }

        // GET: Medico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await db.Medico.Where(m => m.IdMedico == id)
                .Include(me => me.MedicoEspecilidad).ThenInclude(e => e.Especialidad).FirstOrDefaultAsync();
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // GET: Medico/Create
        public IActionResult Create()
        {
            ViewData["ListaEspecialidades"] = new SelectList(db.Especialidad, "IdEspecialidad", "Descripcion");
            return View();
        }

        // POST: Medico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMedico,Nombre,Apellido,Direccion,Telefono,Email,HorarioAtencionDesde,HorarioAtencionHasta")] MedicoModel medico, int IdEspecialidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Add(medico);
                    await db.SaveChangesAsync();

                    var medicoEspecialidad = new MedicoEspecialidadModel();
                    medicoEspecialidad.IdMedico = medico.IdMedico;
                    medicoEspecialidad.IdEspecialidad = IdEspecialidad;

                    db.Add(medicoEspecialidad);
                    await db.SaveChangesAsync();

                    TempData["mensaje"] = $"Se creo correctamente el médico {medico.Nombre} {medico.Apellido}";
                    TempData["status"] = "green lighten-4 green-text text-darken-4";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["mensaje"] = "Hubo un error con los datos del médico. Intenteló más tarde.";
                    TempData["status"] = "red lighten-4 red-text text-darken-4";
                    return RedirectToAction(nameof(Index));
                    //return NotFound();
                }
            }
            return View(medico);
        }

        // GET: Medico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await db.Medico.Where(m => m.IdMedico == id).Include(x => x.MedicoEspecilidad).FirstOrDefaultAsync();
            if (medico == null)
            {
                return NotFound();
            }
            ViewData["ListaEspecialidades"] = new SelectList(db.Especialidad,
                "IdEspecialidad", "Descripcion", medico.MedicoEspecilidad[0].IdEspecialidad);
            return View(medico);
        }

        // POST: Medico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind(
            "IdMedico, Nombre, Apellido, Direccion, Telefono, Email, HorarioAtencionDesde, HorarioAtencionHasta")] MedicoModel medico, int IdEspecialidad)
        {
            if (id == medico.IdMedico)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Update(medico);
                        await db.SaveChangesAsync();

                        var medicoEspecialidad = await db.MedicoEspecialidad.FirstOrDefaultAsync(m => m.IdMedico == id);
                        db.Remove(medicoEspecialidad);
                        await db.SaveChangesAsync();

                        medicoEspecialidad.IdEspecialidad = IdEspecialidad;
                        db.Add(medicoEspecialidad);
                        await db.SaveChangesAsync();
                        TempData["mensaje"] = $"Se actualizó correctamente los datos del médico {medico.Nombre} {medico.Apellido}.";
                        TempData["status"] = "green lighten-4 green-text text-darken-4";
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        if (!MedicoModelExists(medico.IdMedico))
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
            return View(medico);
        }

        // GET: Medico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await db.Medico.FirstOrDefaultAsync(m => m.IdMedico == id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id != null)
            {
                try
                {
                    // Elimina la foreign key
                    var medicoEspecialidad = await db.MedicoEspecialidad.FirstOrDefaultAsync(m => m.IdMedico == id);
                    db.Remove(medicoEspecialidad);
                    await db.SaveChangesAsync();

                    var medico = await db.Medico.FindAsync(id);
                    db.Medico.Remove(medico);
                    await db.SaveChangesAsync();
                    TempData["mensaje"] = "Se eliminó correctamente el médico";
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
                TempData["mensaje"] = "No se puedo eliminar correctamente el médico";
                TempData["status"] = "red lighten-4 red-text text-darken-4";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool MedicoModelExists(int id)
        {
            return db.Medico.Any(e => e.IdMedico == id);
        }

        public string ObtenerAtencionDesde(int idMedico)
        {
            if (idMedico != 0)
            {
                var atencionDesde = db.Medico.Where(m => m.IdMedico == idMedico).FirstOrDefault().HorarioAtencionDesde;
                return $"{atencionDesde.Hour}:{atencionDesde.Minute}";
            }
            else
            {
                return "Selecione un medico";
            }
        }

        public string ObtenerAtencionHasta(int idMedico)
        {
            if (idMedico != 0)
            {
                var atencionHasta = db.Medico.Where(m => m.IdMedico == idMedico).FirstOrDefault().HorarioAtencionHasta;
                return $"{atencionHasta.Hour}:{atencionHasta.Minute}";
            }
            else
            {
                return "Selecione un medico";
            }
        }
    }
}
