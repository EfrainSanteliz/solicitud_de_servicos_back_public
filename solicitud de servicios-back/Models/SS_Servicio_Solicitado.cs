using System.ComponentModel.DataAnnotations;

namespace solicitud_de_servicios_back.Models
{
    public class SS_Servicio_Solicitado
    {
        [Key]
        public int SS_Servicio_solicitado_Id { get; set; }

        public string? DescripcionServicio_Solicitado {  get; set; }

        public bool? HabilitadoServicio_Solicitado { get; set; }

        public ICollection<SS_Solicitud>? SS_Solicitudes { get; set; }


    }
}
