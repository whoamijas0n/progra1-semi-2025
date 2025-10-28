using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("horario")]
    public class Horario
    {
        [Key]
        public int idHorario { get; set; }

        [ForeignKey("Empleado")]
        public int idHorario_De_Empleado { get; set; }
        public Empleado Empleado { get; set; }

        [ForeignKey("Turno")]
        public int idTurno { get; set; }
        public Turno Turno { get; set; }

        public DateOnly fecha_inicio { get; set; }
        public DateOnly? fecha_fin { get; set; }
        public bool activo { get; set; }
    }
}