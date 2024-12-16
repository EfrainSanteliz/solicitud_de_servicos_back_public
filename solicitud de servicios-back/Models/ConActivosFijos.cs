using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace solicitud_de_servicios_back.Models
{
    public class ConActivosFijos
    {
        [Key]
        public int ActivoFijoID { get; set; }

        [Required]
        public int AFTipoActivoFijoID { get; set; }

        [MaxLength(10)]
        public string AFClave { get; set; }

        [MaxLength(50)]
        public string AFNombre { get; set; }

        [MaxLength(100)]
        public string AFDescripcion { get; set; }

        public int? AFEmpleadoID { get; set; }

        public string AFDatosAdicionales { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime AFFechaFactura { get; set; }

        [MaxLength(15)]
        public string AFFactura { get; set; }

        [MaxLength(50)]
        public string AFMarca { get; set; }

        [MaxLength(50)]
        public string AFModelo { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string AFValorBien { get; set; } // Aquí lo mapeamos como VARCHAR(10)

        [MaxLength(40)]
        public string AFNoSerie { get; set; }

        public byte AFEstatus { get; set; }

        [MaxLength(255)]
        public string AFObservaciones { get; set; }

        public int AFSucursalId { get; set; }

        public int AFCuentaContableID { get; set; }

        public int AFFolio { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime AFFechaRegistro { get; set; }

        public int DireccionICEESID { get; set; }

        public int AFPropietarioBienId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? AFFechaAsignacion { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? AFFechaCancelacion { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? AFFechaDonacion { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal? AFValorDonacion { get; set; }

        public bool? AFesDonacion { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal? AFDonacionValuacion { get; set; }

        public int? AFBienMuebleID { get; set; }

       // [JsonIgnore]  // Evitar referencia circular
       // public ICollection<SS_Solicitud> SS_Solicitudes { get; set; }



    }
}
