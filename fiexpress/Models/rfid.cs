using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    public class rfid
    {
        [Key]

        public int idRfid { get; set; }
        public string codigo_rfid { get; set; }
        public int idEmpleado { get; set; }
        [ForeignKey("idEmpleado")]
        public Empleado Empleado { get; set; }
        public int fecha_asignacion { get; set; }







    }

}
