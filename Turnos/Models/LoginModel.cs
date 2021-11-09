using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class LoginModel
    {
        [Key]
        public int LoginId { get; set; }
        [Required(ErrorMessage = "Debe ingresar un {0}")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Debe ingresar una {0}")]
        public string Password { get; set; }
    }
}
