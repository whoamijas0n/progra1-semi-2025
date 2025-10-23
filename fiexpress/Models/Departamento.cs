using System.ComponentModel.DataAnnotations;

namespace fiexpress.Models
{
    public class Departamento
    {
        [Key]
        public int idDepartamento { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        public bool activo { get; set; }
        public  DateTime fecha_creacion {  get; set; }
        public ICollection<Empleado> Empleado { get; set; }

    }
}
