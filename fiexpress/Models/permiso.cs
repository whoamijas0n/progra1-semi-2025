using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    public class permiso
    {
        [Key]
        public int idPermiso { get; set; }
        public int idpleado { get; set; }
        [ForeignKey]("idEmpleado")
            public Empleado dmpleado { get; set; }
        public int tipo { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public int estado { get; set; }
        public int perimiso_supervisor { get; set; }
        [ForeignKey("perimiso_supervisor")]
            public Empleado Supervisor { get; set; }
        public int motivo { get; set; }
    }
}
