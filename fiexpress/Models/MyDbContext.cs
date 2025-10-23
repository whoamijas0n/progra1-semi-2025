using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using fiexpress.Models;

namespace fiexpress.Models
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        // 🔹 Tablas (DbSet)
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Supervisor> Supervisores { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<rfid> rfids { get; set; }
        public DbSet<fichaje>Fichajes  { get; set; }
        public DbSet<estadistica> Estadisticas { get; set; }
        public DbSet<incidencia> Incidencias { get; set; }
        public DbSet<justificacion> Justificaciones { get; set; }
        public DbSet<permiso> Permisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Rol → Empleado (1 a muchos)
            modelBuilder.Entity<Rol>()
                .HasMany(r => r.Empleado)
                .WithOne(e => e.Rol)
                .HasForeignKey(e => e.idRol)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Departamento → Empleado (1 a muchos)
            modelBuilder.Entity<Departamento>()
                .HasMany(d => d.Empleado)
                .WithOne(e => e.Departamento)
                .HasForeignKey(e => e.idDepartamento)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Empleado → Horarios (1 a muchos)
            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.Horarios)
                .WithOne(h => h.Empleado)
                .HasForeignKey(h => h.idHorario_De_Empleado)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Turno → Horarios (1 a muchos)
            modelBuilder.Entity<Turno>()
                .HasMany(t => t.Horario)
                .WithOne(h => h.Turno)
                .HasForeignKey(h => h.idTurno)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Supervisor → Empleado y Departamento
            modelBuilder.Entity<Supervisor>()
                .HasOne(s => s.Empleado)
                .WithMany()
                .HasForeignKey(s => s.idEmpleado_Supervisor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supervisor>()
                .HasOne(s => s.Departamento)
                .WithMany()
                .HasForeignKey(s => s.idDepartamento_supervisando)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<rfid>()
                .HasOne(r => r.Empleado)
                .WithMany()
                .HasForeignKey(r => r.idEmpleado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Empleado)
                .WithMany(e => e.Horarios)
                .HasForeignKey(h => h.idHorario_De_Empleado)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<fichaje>()
                .HasOne(f => f.Empleado)
                .WithMany()
                .HasForeignKey(f => f.idEmpleado)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<estadistica>()
                .HasOne(es => es.Empleado)
                .WithMany()
                .HasForeignKey(es => es.idEmpleado)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<incidencia>()
                .HasOne(i => i.Empleado)
                .WithMany()
                .HasForeignKey(i => i.idEmpleado)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<justificacion>()
                .HasOne(j => j.Empleado)
                .WithMany()
                .HasForeignKey(j => j.idEmpleado)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<permiso>()
                .HasOne(p => p.Empleado)
                .WithMany()
                .HasForeignKey(p => p.idpleado)
                .OnDelete(DeleteBehavior.Restrict);



            // 🔹 Propiedades específicas
            modelBuilder.Entity<Rol>().Property(r => r.nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Departamento>().Property(d => d.nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Empleado>().Property(e => e.nombre).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Turno>().Property(t => t.nombre).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<rfid>().Property(r => r.idRfid).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<fichaje>().Property(f => f.idFichaje).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<estadistica>().Property(es => es.idEstadistica).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<incidencia>().Property(i => i.tipo).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<justificacion>().Property(j => j.motivo).IsRequired().HasMaxLength(250);
            modelBuilder.Entity<permiso>().Property(p => p.estado).IsRequired().HasMaxLength(50);
        }
    }

}

