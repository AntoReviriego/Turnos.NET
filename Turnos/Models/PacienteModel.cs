using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class PacienteModel
    {
        [Key]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} permite una longitud de {1} carcateres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} permite una longitud de {1} carcateres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(250, ErrorMessage = "El campo {0} permite una longitud de {1} carcateres")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(20, ErrorMessage = "El campo {0} permite una longitud de {1} carcateres")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage = "El {0} ingresado no es válido")]
        public string Email { get; set; }

        /* --- RELACIONES --- */
        public List<TurnoModel> Turno { get; set; } // 1:n Paciente -> Muchos turnos 

    }
}
