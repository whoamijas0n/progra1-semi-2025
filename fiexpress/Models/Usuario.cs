using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }

        [ForeignKey("Empleado")]
        public int idUsuarioEmpleado { get; set; }
        public Empleado Empleado { get; set; }

        [Required, MaxLength(50)]
        public string username { get; set; }

        [Required, MaxLength(35)]
        public string password { get; set; }

        public DateTime? ultimo_login { get; set; }
        public bool activo { get; set; }

        // Navegación
        public ICollection<Auditoria> Auditorias { get; set; }
        public ICollection<Historial> Historiales { get; set; }
    }
}