using System;
using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class TurnoModel

    {
        [Key]
        public int IdTurno { get; set; }

        public int IdPaciente { get; set; }

        public int IdMedico { get; set; }

        [Display(Name = "Fecha hora Ini.")]
        public DateTime FechaHoraInicio { get; set; }

        [Display(Name = "Fecha hora Fin")]
        public DateTime FechaHoraFin { get; set; }

        public PacienteModel Paciente { get; set; }
        public MedicoModel Medico { get; set; }
    }
}
