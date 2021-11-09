using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class TurnoController : Controller
    {
        private readonly TurnosContext db;
        private IConfiguration configuration;

        public TurnoController(TurnosContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        // GET: Turno
        public IActionResult Index()
        {
            // ListBox de Medicos
            ViewData["IdMedico"] = new SelectList((from m in db.Medico.ToList()
                                                   select new
                                                   {
                                                       IdMedico = m.IdMedico,
                                                       NombreApellido = $"{m.Nombre} {m.Apellido}"
                                                   }), "IdMedico", "NombreApellido");
            // ListBox de Paciente
            ViewData["IdPaciente"] = new SelectList((from p in db.Paciente.ToList()
                                                     select new
                                                     {
                                                         IdPaciente = p.IdPaciente,
                                                         NombreApellido = $"{p.Nombre} {p.Apellido}"
                                                     }), "IdPaciente", "NombreApellido");
            return View();
        }

        public JsonResult ObtenerTurnos(int idMedico)
        {
            var turno = db.Turno.Where(t => t.IdMedico == idMedico)
                .Select(t => new
                {
                    t.IdTurno,
                    t.IdMedico,
                    t.IdPaciente,
                    t.FechaHoraInicio,
                    t.FechaHoraFin,
                    paciente = $"{ t.Paciente.Nombre} { t.Paciente.Apellido }"
                }).ToList(); // Listo turnos que tenga el medico 
            return Json(turno); // coleccion turnos -> formato JSON
        }

        [HttpPost]
        public JsonResult GuardarTurno(TurnoModel turno)
        {
            var ok = false;
            try
            {
                db.Turno.Add(turno);
                db.SaveChanges();
                ok = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Excepcion encontrada: {e}");
            }
            var jsonResult = new { ok = ok };
            return Json(jsonResult);
        }

        [HttpPost]
        public JsonResult DeleteTurno(int idTurno)
        {
            var ok = false;
            try
            {
                var deleteTurno = db.Turno.Where(t => t.IdTurno == idTurno).FirstOrDefault();
                if (deleteTurno != null)
                {
                    db.Turno.Remove(deleteTurno);
                    db.SaveChanges();
                    ok = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Excepcion encontrada: {e}");
            }
            var jsonResult = new { ok = ok };
            return Json(jsonResult);
        }
    }
}
