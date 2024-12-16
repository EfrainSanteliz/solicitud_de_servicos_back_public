using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace solicitud_de_servicios_back.Models
{
    public class SS_HistorialPrioridad
    {

        [Key]
        public int SS_HistorialPrioridadId { get; set; }

        public string? Quien { get; set; }

        public int? Prioridad { get; set; }

        public DateTime? FechaPrioridad { get; set; }



        [ForeignKey("Request")]
        public int SS_SolicitudId { get; set; }  // Make sure this is an int and correctly named

        public SS_Solicitud? SS_Solicitudes { get; set; }
    }
}
