using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace solicitud_de_servicios_back.Models
{
    public class DireccionesICEES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DireccionICEESID { get; set; }

        [Required]
        [MaxLength(10)]
        public string Clave { get; set; }

        [Required]
        [MaxLength(80)]
        public string Descripcion { get; set; }

        [MaxLength(50)]
        public string? SubgrupoID { get; set; }

        public int? CenCostosId { get; set; }

        public int? SucursalId { get; set; }

        [Required]
        public bool Activo { get; set; }

        public ICollection<NomEmpleados>? NomEmpleados { get; set; }


    }

}
