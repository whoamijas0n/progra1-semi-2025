using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fiexpress.Models
{
    [Table("empleado")]
    public class Empleado
    {
        [Key]
        public int idEmpleado { get; set; }

        [ForeignKey("Departamento")]
        public int idDepartamento { get; set; }
        public Departamento Departamento { get; set; }

        [ForeignKey("Rol")]
        public int idRol { get; set; }
        public Rol Rol { get; set; }

        [Required, MaxLength(35)]
        public string codigo_empleado { get; set; }

        [Required, MaxLength(65)]
        public string nombre { get; set; }

        // ❌ ELIMINADO: apellido (no existe en BD)
        // public string apellido { get; set; }

        [Required, EmailAddress, MaxLength(50)]
        public string email { get; set; }

        [Required, MaxLength(9)]
        public string telefono { get; set; }

        public DateOnly fecha_nacimiento { get; set; }
        public DateOnly fecha_ingreso { get; set; }
        public DateOnly? fecha_baja { get; set; }

        [MaxLength(150)]
        public string? foto_url { get; set; }

        public bool activo { get; set; }
        public long? telegram_chat_id { get; set; }


    
        public ICollection<Horario> Horarios { get; set; }
        public ICollection<Fichaje> Fichajes { get; set; }
        public ICollection<Estadistica> Estadisticas { get; set; }
        public ICollection<Permiso> Permisos { get; set; }
        public ICollection<Rfid> Rfids { get; set; }
        public ICollection<Notificacion> Notificaciones { get; set; }


        public ICollection<Incidencia> Incidencias { get; set; }
        public ICollection<Justificacion> Justificaciones { get; set; }

    
        public Supervisor Supervisor { get; set; }
        public Usuario Usuario { get; set; }
    }
}