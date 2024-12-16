using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace solicitud_de_servicios_back.Models
{
    public class SS_Solicitud
    {

        public int SS_SolicitudId { get; set; }

        public int? ServicioSolicitado { get; set; }

        //public string? SolicitudDeServicioARealizar { get; set; }

        public DateTime? FechaSolicitada { get; set; }

        public string? Descripcion { get; set; }

        public int? Estatus { get; set; }

        public string? FirmaEmpleado {  get; set; }
        public string? FirmaJefeDepartamento { get; set; }
        public string? FirmaJefe { get; set; }
        public string? Archivo { get; set; }
        public int? Prioridad { get; set; }

        public bool? RevisadoSub { get; set; }

        public int? UltimoStatus { get; set; }

        // Relaciones
        public int? EmpleadoID { get; set; }
       // public NomEmpleados? NomEmpleados { get; set; }

        public int? ActivoFijoID { get; set; }
       // public ConActivosFijos? ConActivosFijos { get; set; }
        // Update the following line


        public int? SS_Servicio_solicitado_Id { get; set; }
        public SS_Servicio_Solicitado? SS_Servicio_Solicitados { get; set; }




        public int? SS_Solicitud_de_servicio_id { get; set; }

        public SS_Solicitud_de_servicio? SS_Solicitud_de_servicios { get; set; }



        // **Relación con Historials**
        public ICollection<SS_HistorialComentarios>? HistorialComentarios { get; set; }

        public ICollection<SS_HistorialStatus>? HistorialStatus { get; set; }

        public ICollection<SS_HistorialPrioridad>? HistorialPrioridads { get; set; }


    }
}
