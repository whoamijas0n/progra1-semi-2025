using fiexpress.Models;
using Microsoft.EntityFrameworkCore;

namespace fiexpress.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }

        // DbSets
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<Configuracion> Configuraciones { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Estadistica> Estadisticas { get; set; }
        public DbSet<Fichaje> Fichajes { get; set; }
        public DbSet<Historial> Historiales { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<Justificacion> Justificaciones { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Rfid> Rfids { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Supervisor> Supervisores { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================================
            // RELACIONES
            // ========================================

            // Empleado -> Departamento y Rol
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Departamento)
                .WithMany(d => d.Empleados)
                .HasForeignKey(e => e.idDepartamento)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Rol)
                .WithMany(r => r.Empleados)
                .HasForeignKey(e => e.idRol)
                .OnDelete(DeleteBehavior.Restrict);

            // Horario -> Empleado y Turno
            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Empleado)
                .WithMany(e => e.Horarios)
                .HasForeignKey(h => h.idHorario_De_Empleado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Turno)
                .WithMany(t => t.Horarios)
                .HasForeignKey(h => h.idTurno)
                .OnDelete(DeleteBehavior.Restrict);

            // Fichaje -> Empleado
            modelBuilder.Entity<Fichaje>()
                .HasOne(f => f.Empleado)
                .WithMany(e => e.Fichajes)
                .HasForeignKey(f => f.idEmpleadoFichaje)
                .OnDelete(DeleteBehavior.Restrict);

            // Estadistica -> Empleado
            modelBuilder.Entity<Estadistica>()
                .HasOne(s => s.Empleado)
                .WithMany(e => e.Estadisticas)
                .HasForeignKey(s => s.idEmpleadoEstadistica)
                .OnDelete(DeleteBehavior.Restrict);

            // Permiso -> Empleado y Supervisor
            modelBuilder.Entity<Permiso>()
                .HasOne(p => p.Empleado)
                .WithMany(e => e.Permisos)
                .HasForeignKey(p => p.idPermisoEmpleado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Permiso>()
                .HasOne(p => p.Supervisor)
                .WithMany(s => s.Permisos)
                .HasForeignKey(p => p.idPermisoSupervisor)
                .OnDelete(DeleteBehavior.Restrict);

            // Rfid -> Empleado
            modelBuilder.Entity<Rfid>()
                .HasOne(r => r.Empleado)
                .WithMany(e => e.Rfids)
                .HasForeignKey(r => r.idEmpleadoAsignado)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ Incidencia -> Empleado y Supervisor
            modelBuilder.Entity<Incidencia>()
                .HasOne(i => i.Empleado)
                .WithMany(e => e.Incidencias)
                .HasForeignKey(i => i.idIncidenciaEmpleado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Incidencia>()
                .HasOne(i => i.Supervisor)
                .WithMany(s => s.Incidencias)
                .HasForeignKey(i => i.idIncidenciaSupervisor)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ Justificacion -> Incidencia y Supervisor
  

            modelBuilder.Entity<Justificacion>()
                .HasOne(j => j.Supervisor)
                .WithMany(s => s.Justificaciones)
                .HasForeignKey(j => j.idJustificacionSupervisor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Justificacion>()
                .HasOne(j => j.Empleado)
                .WithMany(s => s.Justificaciones)
                .HasForeignKey(j => j.idJustificacionEmpleado)
                .OnDelete(DeleteBehavior.Restrict);

            // Notificacion -> Empleado
            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Empleado)
                .WithMany(e => e.Notificaciones)
                .HasForeignKey(n => n.idNotificacionEmpleado)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ Supervisor -> Empleado (1:1) y Departamento
            modelBuilder.Entity<Supervisor>()
                .HasOne(s => s.Empleado)
                .WithOne(e => e.Supervisor)
                .HasForeignKey<Supervisor>(s => s.idEmpleado_supervisor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supervisor>()
                .HasOne(s => s.Departamento)
                .WithMany(d => d.Supervisores)
                .HasForeignKey(s => s.idDepartamento_supervisando)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ Usuario -> Empleado (1:1)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Empleado)
                .WithOne(e => e.Usuario)
                .HasForeignKey<Usuario>(u => u.idUsuarioEmpleado)
                .OnDelete(DeleteBehavior.Restrict);

            // Auditoria -> Usuario
            modelBuilder.Entity<Auditoria>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Auditorias)
                .HasForeignKey(a => a.idAuditoriaUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Historial -> Usuario
            modelBuilder.Entity<Historial>()
                .HasOne(h => h.Usuario)
                .WithMany(u => u.Historiales)
                .HasForeignKey(h => h.idHistorialUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // ========================================
            // ÍNDICES PARA PERFORMANCE
            // ========================================

            // Índices únicos
            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.codigo_empleado)
                .IsUnique();

            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.email)
                .IsUnique();

            modelBuilder.Entity<Rfid>()
                .HasIndex(r => r.codigo_rfid)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.username)
                .IsUnique();

            // Índices compuestos para búsquedas frecuentes
            modelBuilder.Entity<Fichaje>()
                .HasIndex(f => new { f.idEmpleadoFichaje, f.fecha });

            modelBuilder.Entity<Estadistica>()
                .HasIndex(e => new { e.idEmpleadoEstadistica, e.fecha });

            modelBuilder.Entity<Horario>()
                .HasIndex(h => new { h.idHorario_De_Empleado, h.activo });

            // ========================================
            // VALORES POR DEFECTO
            // ========================================

            modelBuilder.Entity<Empleado>()
                .Property(e => e.activo)
                .HasDefaultValue(true);

            modelBuilder.Entity<Notificacion>()
                .Property(n => n.leido)
                .HasDefaultValue(false);

            modelBuilder.Entity<Notificacion>()
                .Property(n => n.fecha_envio)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Departamento>()
                .Property(d => d.fecha_creacion)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Auditoria>()
                .Property(a => a.fecha_accion)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Historial>()
                .Property(h => h.fecha_cambio)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}