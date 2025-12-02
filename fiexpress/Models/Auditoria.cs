using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("auditoria")]
    public class Auditoria
    {
        [Key]
        public int idAuditoria { get; set; }

        [ForeignKey("Usuario")]
        public int idAuditoriaUsuario { get; set; }
        public Usuario Usuario { get; set; }

        [Required, MaxLength(50)]
        public string accion { get; set; }

        [MaxLength(50)]
        public string entidad_afectada { get; set; }

        public DateTime fecha_accion { get; set; }

        [Required, MaxLength(50)]
        public string ip { get; set; }

        [Required, MaxLength(50)]
        public string descripcion { get; set; }
    }
}