using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    public class incidencia
    {
        [Key]
        public int idIncidencia { get; set; }
        public int idEmpleado { get; set; }
        [ForeignKey("idEmpleado")]
        public Empleado Empleado { get; set; }
        public DateTime fecha { get; set; }
        public string tipo { get; set; }
        public int idsupervisor { get; set; }
        [ForeignKey("idsupervisor")]
        public Empleado Supervisor { get; set; }
        public int resuelta { get; set; }
        public DateTime? fecha_resuelta { get; set; }
    }
}
