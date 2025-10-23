using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    public class Empleado
    {
        [Key]
        public int idEmpleado { get; set; }

        [ForeignKey("idDepartamento")]
        public int idDepartamento { get; set; }
        public Departamento Departamento { get; set; }

        [ForeignKey("idRol")]
        public int idRol { get; set; }
        public Rol Rol { get; set; }

        public string codigo_empleado { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }

        public DateOnly fecha_nacimiento { get; set; }
        public DateOnly fecha_ingreso { get; set; }
        public DateOnly? fecha_baja { get; set; }

        public string foto_url { get; set; }
        public bool activo { get; set; }

        public ICollection<Horario> Horarios { get; set; }

    }
}
