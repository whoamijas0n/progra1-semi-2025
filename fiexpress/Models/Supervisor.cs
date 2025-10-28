using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("supervisor")]
    public class Supervisor
    {
        [Key]
        public int idSupervisor { get; set; }

        // ✅ CORREGIDO - minúscula "supervisor"
        [ForeignKey("Empleado")]
        [Column("idEmpleado_supervisor")]
        public int idEmpleado_supervisor { get; set; }
        public Empleado Empleado { get; set; }

        // ✅ CORREGIDO - minúscula "supervisando"
        [ForeignKey("Departamento")]
        [Column("idDepartamento_supervisando")]
        public int idDepartamento_supervisando { get; set; }
        public Departamento Departamento { get; set; }

        public bool activo { get; set; }

        // Navegación
        public ICollection<Incidencia> Incidencias { get; set; }
        public ICollection<Permiso> Permisos { get; set; }
        public ICollection<Justificacion> Justificaciones { get; set; }
    }
}