using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    public class Horario
    {
        [Key]
        public int idHorario { get; set; }
        [ForeignKey("idHorario_De_Empleado")]
        public int idHorario_De_Empleado { get; set; }
        public Empleado Empleado { get; set; }
        [ForeignKey("idTurno")]
        public int idTurno { get; set; }
        public Turno Turno { get; set; }
        public DateOnly fecha_inicio { get; set; }
        public DateOnly? fecha_fin { get; set; }
        public bool activo { get; set; }

    }
}
