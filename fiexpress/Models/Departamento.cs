using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("departamento")]
    public class Departamento
    {
        [Key]
        public int idDepartamento { get; set; }

        [Required, MaxLength(70)]
        public string nombre { get; set; }

        [Required, MaxLength(50)]
        public string codigo { get; set; }

        public bool activo { get; set; }

        public DateTime fecha_creacion { get; set; }

        // Navegación
        public ICollection<Empleado> Empleados { get; set; }
        public ICollection<Supervisor> Supervisores { get; set; }
    }
}
