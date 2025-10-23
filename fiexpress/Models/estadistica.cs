using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    public class estadistica
    {
        [Key]
        public int idEstadistica { get; set; }
        public int idEmpleado { get; set; }
        [ForeignKey("idEmpleado")]
        public Empleado Empleado { get; set; }
        public DateTime fecha { get; set; }
       public int minutos_trabajadores { get; set; }
        public int minutos_retraso { get; set; }
        public int minutos_extras { get; set; }
        public int asistencias { get; set; }
        public int estado_dia { get; set; }
    }
}
