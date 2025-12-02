using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("tarjeta_rfid")]
    public class Rfid
    {
        [Key]
        public int idRfid { get; set; }

        [Required, MaxLength(50)]
        public string codigo_rfid { get; set; }

        [ForeignKey("Empleado")]
        public int idEmpleadoAsignado { get; set; }
        public Empleado Empleado { get; set; }

        public DateOnly fecha_asignacion { get; set; }
        public bool activo { get; set; }
    }
}