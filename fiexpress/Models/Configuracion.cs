using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("configuracion")]
    public class Configuracion
    {
        [Key]
        public int idConfiguracion { get; set; }

        [Required, MaxLength(50)]
        public string clave { get; set; }

        [Required, MaxLength(50)]
        public string valor { get; set; }

        [Required, MaxLength(50)]
        public string descripcion { get; set; }

        public DateTime ultima_modificacion { get; set; }
    }
}