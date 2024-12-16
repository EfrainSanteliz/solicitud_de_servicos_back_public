using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace solicitud_de_servicios_back.Models
{
    public class SS_HistorialStatus
    {
        [Key]
        public int SS_HistorialStatusId { get; set; }

        public string? Quien { get; set; }

        public int? Status { get; set; }

        public DateTime? FechaStatus { get; set; }




        [ForeignKey("Request")]
        public int SS_SolicitudId { get; set; }  // Make sure this is an int and correctly named

        public SS_Solicitud? SS_Solicitudes { get; set; }


    }
}
