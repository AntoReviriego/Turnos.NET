namespace Turnos.Models
{
    public class MedicoEspecialidadModel
    {
        public int IdMedico { get; set; }
        public int IdEspecialidad { get; set; }

        /*  CORRESPONDEN A LOS MODELOS MEDICO-ESPECIALIDAD*/
        public MedicoModel Medico { get; set; }
        public EspecialidadModel Especialidad { get; set; }
    }
}
