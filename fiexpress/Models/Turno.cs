using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("turno")]
    public class Turno
    {
        [Key]
        public int idTurno { get; set; }

        [Required, MaxLength(50)]
        public string nombre { get; set; }

        public TimeOnly hora_entrada { get; set; }
        public TimeOnly hora_salida { get; set; }
        public int tolerancia_minutos { get; set; }

        public bool lunes { get; set; }
        public bool martes { get; set; }
        public bool miercoles { get; set; }
        public bool jueves { get; set; }
        public bool viernes { get; set; }
        public bool sabado { get; set; }
        public bool domingo { get; set; }
        public bool activo { get; set; }

        // Navegación
        public ICollection<Horario> Horarios { get; set; }
    }
}