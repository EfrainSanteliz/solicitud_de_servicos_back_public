using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace solicitud_de_servicios_back.Models
{
    public class SS_Solicitud_de_servicio
    {

       [Key]
       public int SS_Solicitud_de_servicio_id { get; set; }

       public string? DescripcionSolicitud_De_Servicio { get; set; }

       public bool? Habilitado { get; set; }

       public ICollection<SS_Solicitud>? SS_Solicitudes { get; set; }



    }
}
