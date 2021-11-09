using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class MedicoModel
    {
        [Key]
        public int IdMedico { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} permite una longitud de {1} carcateres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} permite una longitud de {1} carcateres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campoa {0} es obligatorio")]
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

        [Display(Name = "Atención desde")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime HorarioAtencionDesde { get; set; }

        [Display(Name = "Atención hasta")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime HorarioAtencionHasta { get; set; }

        /* --- RELACIONES --- */
        public List<MedicoEspecialidadModel> MedicoEspecilidad { get; set; } // obtiene lista de especialidades que tiene un medico
        public List<TurnoModel> Turno { get; set; } // 1:n Medico -> Muchos turnos


    }
}
