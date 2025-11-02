using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("justificacion")]
    public class Justificacion
    {
        [Key]
        public int idJustificacion { get; set; }


        [ForeignKey("Supervisor")]
        public int? idJustificacionSupervisor { get; set; }
        public Supervisor Supervisor { get; set; }
        [ForeignKey("Empleado")]
        public int? idJustificacionEmpleado { get; set; }
        public Empleado Empleado { get; set; }

        [MaxLength(50)]
        public string documento_url { get; set; }

        [Required, MaxLength(50)]
        public string motivo { get; set; }

        [Required, MaxLength(50)]
        public string estado { get; set; }

        public DateTime? fecha_revision { get; set; }
    }
}