using System.ComponentModel.DataAnnotations;

namespace fiexpress.Models
{
    public class Rol
    {
        [Key]
        public int idRol { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
    }
}
