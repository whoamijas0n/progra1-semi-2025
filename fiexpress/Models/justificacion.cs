using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    public class justificacion
    {
        [Key]
        public int idJustificacion { get; set; }
        public int incidenciaId { get; set; }
        [ForeignKey("incidenciaId")]
        public incidencia Incidencia { get; set; }
        public string motivo { get; set; }
        public int estado { get; set; }
    }
}
