using Microsoft.EntityFrameworkCore;

namespace solicitud_de_servicios_back.Models
{
    public class UserContextPlasalcees : DbContext
    {

        public UserContextPlasalcees(DbContextOptions<UserContextPlasalcees> options) : base(options) { }

        public DbSet<NomEmpleados> NomEmpleados { get; set; }
 
        public DbSet<ConActivosFijos> ConActivosFijos { get; set; }


        public DbSet<DireccionesICEES> DireccionesICEES { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Configuración de la relación uno a muchos entre NomEmpleados y Request
             ///  modelBuilder.Entity<SS_Solicitud>()
                //  .HasOne(s => s.NomEmpleados)
                 // .WithMany(n => n.SS_Solicitudes)
                 // .HasForeignKey(s => s.EmpleadoID)
                 // .OnDelete(DeleteBehavior.Cascade); // Define el comportamiento de eliminación en cascada

            // Configuración de la relación uno a muchos entre ConActivosFijos y Request
             // modelBuilder.Entity<SS_Solicitud>()
                 // .HasOne(r => r.ConActivosFijos)
               //   .WithMany(c => c.SS_Solicitudes)  // Cambiado a WithMany para reflejar uno a muchos
                //   .HasForeignKey(r => r.ActivoFijoID)
                //  .OnDelete(DeleteBehavior.SetNull);



            // Configuración de la relación uno a muchos entre DireccionesICESS y NomEmpleados
            modelBuilder.Entity<NomEmpleados>()
              .HasOne(ne => ne.DireccionesICEES)
             .WithMany(d => d.NomEmpleados)
             .HasForeignKey(ne => ne.NomEmpAdscripcionID)
             .OnDelete(DeleteBehavior.Cascade);


            // modelBuilder.Entity<SS_Usuarios>()
            //.HasOne(u => u.NomEmpleados)
           // .WithOne(n => n.Usuario)
            // .HasForeignKey<SS_Usuarios>(u => u.EmpleadoID) // Foreign key in Usuarios
            // .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);





        }
    }
}
