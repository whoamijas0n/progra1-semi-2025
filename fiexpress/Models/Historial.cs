using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("historial")]
    public class Historial
    {
        [Key]
        public int idHistorial { get; set; }

        [ForeignKey("Usuario")]
        public int idHistorialUsuario { get; set; }
        public Usuario Usuario { get; set; }

        [MaxLength(50)]
        public string tabla_afectada { get; set; }

        [MaxLength(50)]
        public string tipo_cambio { get; set; }

        public DateTime fecha_cambio { get; set; }

        [MaxLength(50)]
        public string descripcion { get; set; }
    }
}