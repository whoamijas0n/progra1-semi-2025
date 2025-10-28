using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("fichaje")]
    public class Fichaje
    {
        [Key]
        [Column("idFIchaje")] // ⚠️ Nota: Con I mayúscula en BD
        public int idFichaje { get; set; }

        [ForeignKey("Empleado")]
        public int idEmpleadoFichaje { get; set; }
        public Empleado Empleado { get; set; }

        public DateOnly fecha { get; set; }
        public TimeOnly hora { get; set; }

        [Required, MaxLength(50)]
        public string tipo { get; set; }

        [MaxLength(50)]
        public string ip { get; set; }

        [MaxLength(100)]
        public string observacion { get; set; }
    }
}
