using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("rol")]
    public class Rol
    {
        [Key]
        public int idRol { get; set; }

        [Required, MaxLength(50)]
        public string nombre { get; set; }

        [Required, MaxLength(100)]
        public string descripcion { get; set; }

        public bool activo { get; set; }

        // Navegación
        public ICollection<Empleado> Empleados { get; set; }
    }
}