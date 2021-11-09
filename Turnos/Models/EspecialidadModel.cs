using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class EspecialidadModel
    {
        [Key]
        public int IdEspecialidad { get; set; } // primary key

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(200, ErrorMessage = "El campo {0} debe tener un maximo de {1} caracteres")]
        [Display(Name = "Descripción", Prompt = "Ingrese una descripción")]
        public string Descripcion { get; set; }

        /* --- RELACIONES --- */
        public List<MedicoEspecialidadModel> MedicoEspecilidad { get; set; } // obtiene lista de medicos con la especialiadad

    }
}
