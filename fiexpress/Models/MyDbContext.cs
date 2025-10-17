using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using fiexpress.Models;

namespace fiexpress.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() { }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Aqui vamos a agregar los modelos de las entidades que creemos para cada tabla
            //Un ejemplo;
            //modelBuilder.Entity<nombre de la calse>().HasKey(a => a.idDeLaTabla);

            //EJEMPLO PARA LA TABLA ROL
            modelBuilder.Entity<Rol>().HasKey(a => a.idRol);


        }
        public DbSet<fiexpress.Models.Rol> Rol { get; set; } = default!;

    }
}
