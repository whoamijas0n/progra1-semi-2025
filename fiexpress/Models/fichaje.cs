using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    public class fichaje
    {
        [Key]
        public int idFichaje { get; set; }
        public int idEmpleado { get; set; }
        [ForeignKey("idEmpleado")]
        public Empleado Empleado { get; set; }
        public DateTime fecha { get; set; }
        public DateTime hora { get; set; }
        public int ip { get; set; }
        public int observaciones { get; set; }


    }
}
