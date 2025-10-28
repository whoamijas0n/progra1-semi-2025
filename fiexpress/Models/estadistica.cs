using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("estadistica")]
    public class Estadistica
    {
        [Key]
        public int idEstadistica { get; set; }

        [ForeignKey("Empleado")]
        public int idEmpleadoEstadistica { get; set; }
        public Empleado Empleado { get; set; }

        public DateOnly fecha { get; set; }
        public int? minutos_trabajados { get; set; }
        public int? minutos_retraso { get; set; }
        public int? minutos_extra { get; set; }
        public bool asistencia { get; set; }

        [MaxLength(50)]
        public string estado_dia { get; set; }
    }
}