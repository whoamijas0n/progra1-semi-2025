using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    public class Supervisor
    {
        [Key]
        public int idSupervisor { get; set; }
        [ForeignKey("idEmpleado_Supervisor")]
        public int idEmpleado_Supervisor { get; set; }
        public Empleado Empleado { get; set; }
        [ForeignKey("idDepartamento_supervisando")]
        public int idDepartamento_supervisando { get; set; }
        public Departamento Departamento { get; set; }
        public bool activo { get; set; }
    }
}
