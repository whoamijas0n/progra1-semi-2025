using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("permiso")]
    public class Permiso
    {
        [Key]
        public int idPermiso { get; set; }

        [ForeignKey("Empleado")]
        public int idPermisoEmpleado { get; set; }
        public Empleado Empleado { get; set; }

        [ForeignKey("Supervisor")]
        public int? idPermisoSupervisor { get; set; }
        public Supervisor Supervisor { get; set; }

        [Required, MaxLength(50)]
        public string tipo { get; set; }

        public DateOnly fecha_inicio { get; set; }
        public DateOnly fecha_fin { get; set; }

        [Required, MaxLength(50)]
        public string estado { get; set; }

        [Required, MaxLength(50)]
        public string motivo { get; set; }

        public DateTime fecha_solicitud { get; set; }
    }
}