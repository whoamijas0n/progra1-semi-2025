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

        [Required, MaxLength(100)]
        public string titulo { get; set; }

        [Required, MaxLength(500)]
        public string mensaje { get; set; }

        [Required]
        public bool leido { get; set; } = false; // ✅ Valor por defecto

        public DateTime fecha_envio { get; set; }
    }
}