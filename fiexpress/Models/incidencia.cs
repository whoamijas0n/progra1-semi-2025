using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("incidencia")]
    public class Incidencia
    {
        [Key]
        public int idIncidencia { get; set; }

        [ForeignKey("Empleado")]
        public int idIncidenciaEmpleado { get; set; }
        public Empleado Empleado { get; set; }

        [ForeignKey("Supervisor")]
        public int idIncidenciaSupervisor { get; set; }
        public Supervisor Supervisor { get; set; }

        public DateOnly fecha { get; set; }

        [MaxLength(100)]
        public string descripcion { get; set; }

        [Required, MaxLength(50)]
        public string tipo { get; set; }

        public bool resuelta { get; set; }
        public DateOnly? fecha_de_resolucion { get; set; }

        // ✅ AGREGADA - Colección de justificaciones
        public ICollection<Justificacion> Justificaciones { get; set; }
    }
}