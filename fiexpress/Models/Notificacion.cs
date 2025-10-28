using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("notificacion")]
    public class Notificacion
    {
        [Key]
        public int idNotificacion { get; set; }

        [ForeignKey("Empleado")]
        public int idNotificacionEmpleado { get; set; }
        public Empleado Empleado { get; set; }

        [Required, MaxLength(50)]
        public string titulo { get; set; }

        [Required, MaxLength(50)]
        public string mensaje { get; set; }

        public bool leido { get; set; }

        // ✅ CORREGIDO - fecha_envio según BD
        public DateTime fecha_envio { get; set; }
    }
}