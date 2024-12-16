using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace solicitud_de_servicios_back.Models
{
    public class Historial
    {
        [Key]
        public int HistorialID { get; set; }

        public DateTime? Fecha { get; set; }

        public string? Comentarios { get; set; }

        public string? Remitente { get; set; }

        [ForeignKey("Request")]
        public int RequestID { get; set; }  // Make sure this is an int and correctly named

        public SS_Solicitud? Request { get; set; }
    }
}
