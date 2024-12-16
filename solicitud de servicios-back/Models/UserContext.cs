using Microsoft.EntityFrameworkCore;
using SocialMediaApi.Controllers;
using solicitud_de_servicios_back.Controllers;

namespace solicitud_de_servicios_back.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        //public DbSet<NomEmpleados> NomEmpleados { get; set; }
        public DbSet<SS_Solicitud> SS_Solicitudes { get; set; }
        //public DbSet<ConActivosFijos> ConActivosFijos { get; set; }

        public DbSet<SS_HistorialComentarios> SS_HistorialComentarios { get; set; }

        //public DbSet<DireccionesICEES> DireccionesICEES { get; set; }

        public DbSet<SS_Servicio_Solicitado> SS_Servicio_Solicitados { get; set; }

        public DbSet<SS_Solicitud_de_servicio> SS_Solicitud_De_Servicios { get; set; }

        public DbSet<SS_Usuarios> SS_Usuarios { get; set; }

        public DbSet<SS_HistorialPrioridad> SS_HistorialPrioridads { get; set; }

        public DbSet<SS_HistorialStatus> SS_HistorialStatus { get; set; }

        public DbSet<SS_UserDetailsResult> UserDetailsResult { get; set; }

        public DbSet<SS_solicitudDetails> SS_solicitudDetails { get; set; }

        public DbSet<SS_showSolicitudes> SS_showSolicitudes { get; set; }

        public DbSet<SS_listEmpleadoAndUsers> SS_ListEmpleadoAndUsers { get; set; }

        public DbSet<SS_ListEmpleados> SS_ListEmpleados { get; set; }

        public DbSet<SS_ConActivosFijosSP> SS_ConActivosFijosSP { get; set; }

      //  public DbSet<SS_Codigo> SS_Codigo { get; set; }

        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configuración de la relación uno a muchos entre NomEmpleados y Request
         //   modelBuilder.Entity<SS_Solicitud>()
          //      .HasOne(s => s.NomEmpleados)
           //     .WithMany(n => n.SS_Solicitudes)
          //      .HasForeignKey(s => s.EmpleadoID)
          //      .OnDelete(DeleteBehavior.Cascade); // Define el comportamiento de eliminación en cascada

            // Configuración de la relación uno a muchos entre ConActivosFijos y Request
          //  modelBuilder.Entity<SS_Solicitud>()
          //      .HasOne(r => r.ConActivosFijos)
          //      .WithMany(c => c.SS_Solicitudes)  // Cambiado a WithMany para reflejar uno a muchos
         //       .HasForeignKey(r => r.ActivoFijoID)
          //      .OnDelete(DeleteBehavior.SetNull);

            // Configuración de la relación uno a muchos entre Request y Historial
            modelBuilder.Entity<SS_HistorialComentarios>()
                .HasOne(h => h.SS_Solicitudes)
                .WithMany(r => r.HistorialComentarios)  // Configura la relación inversa
                .HasForeignKey(h => h.SS_SolicitudId)
                .OnDelete(DeleteBehavior.Cascade);  // Comportamiento de eliminación en cascada

            modelBuilder.Entity<SS_HistorialStatus>()
               .HasOne(h => h.SS_Solicitudes)
               .WithMany(r => r.HistorialStatus)  // Configura la relación inversa
               .HasForeignKey(h => h.SS_SolicitudId)
               .OnDelete(DeleteBehavior.Cascade);  // Comportamiento de eliminación en cascada


            modelBuilder.Entity<SS_HistorialPrioridad>()
               .HasOne(h => h.SS_Solicitudes)
               .WithMany(r => r.HistorialPrioridads)  // Configura la relación inversa
               .HasForeignKey(h => h.SS_SolicitudId)
               .OnDelete(DeleteBehavior.Cascade);  // Comportamiento de eliminación en cascada


            // Configuración de la relación uno a muchos entre DireccionesICESS y NomEmpleados
            //modelBuilder.Entity<NomEmpleados>()
              //  .HasOne(r => r.DireccionesICEES)
               // .WithMany(n => n.NomEmpleados)
               // .HasForeignKey(r => r.NomEmpAdscripcionID)
               // .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SS_Solicitud>()
                .HasOne(r => r.SS_Solicitud_de_servicios)
                .WithMany(s => s.SS_Solicitudes)
                .HasForeignKey(r => r.SS_Solicitud_de_servicio_id)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<SS_Solicitud>()
                .HasOne(r => r.SS_Servicio_Solicitados)
                .WithMany(s => s.SS_Solicitudes)
                .HasForeignKey(r => r.SS_Servicio_solicitado_Id)
               .OnDelete(DeleteBehavior.Cascade);

            //  modelBuilder.Entity<SS_Usuarios>()
            //.HasOne(u => u.NomEmpleados)
            //.WithOne(n => n.Usuario)
            // .HasForeignKey<SS_Usuarios>(u => u.EmpleadoID) // Foreign key in Usuarios
            /// .OnDelete(DeleteBehavior.Cascade);
            /// 

           // modelBuilder.Entity<SS_Usuarios>()
           //.HasOne(u => u.SS_Codigo)
           //.WithOne(c => c.SS_Usuarios)
           //.HasForeignKey<SS_Codigo>(c => c.SS_UsuarioId)
           //.OnDelete(DeleteBehavior.Cascade);
            /// 
            modelBuilder.Entity<SS_UserDetailsResult>().HasNoKey();
            modelBuilder.Entity<SS_solicitudDetails>().HasNoKey();
            modelBuilder.Entity<SS_showSolicitudes>().HasNoKey(); 
            modelBuilder.Entity<SS_listEmpleadoAndUsers>().HasNoKey();
            modelBuilder.Entity<SS_ListEmpleados>().HasNoKey();
            modelBuilder.Entity<SS_ConActivosFijosSP>().HasNoKey();

            base.OnModelCreating(modelBuilder);





        }
    }
}