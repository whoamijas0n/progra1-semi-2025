using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webappacademica.Models
{
    public class Periodo
    {
        [Key]
        public int idPeriodo { get; set; }
        public DateTime fecha { get; set; }
        public string periodo { get; set; }
        //Relacion con la tabla matriculas
        public ICollection<Matricula> Matriculas { get; set; }
    }
}