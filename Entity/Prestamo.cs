namespace MotoCredito.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prestamo")]
    public partial class Prestamo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prestamo()
        {
            Pagos = new HashSet<Pagos>();
        }

        public int Id { get; set; }

        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string NoChasis { get; set; }

        [Required]
        [StringLength(10)]
        public string NoPlaca { get; set; }

        public int IdModelo { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaApertura { get; set; }

        [Column(TypeName = "money")]
        public decimal? MontoPrestamo { get; set; }

        public int Tiempo { get; set; }

        public int IdTipoPrestamo { get; set; }

        [Column(TypeName = "money")]
        public decimal? BalanceRestante { get; set; }

        [Column(TypeName = "money")]
        public decimal? Cuota { get; set; }

        [Column(TypeName = "money")]
        public decimal? interes { get; set; }

        [Column(TypeName = "money")]
        public decimal? capital { get; set; }

        [StringLength(10)]
        public string color { get; set; }

        public int? año { get; set; }

        public int? usuarioId { get; set; }

        public virtual Clientes Clientes { get; set; }

        public virtual Modelo Modelo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pagos> Pagos { get; set; }

        public virtual TipoPrestamo TipoPrestamo { get; set; }

        public virtual usuarios usuarios { get; set; }

        [NotMapped]
        public bool tieneMora { get; set; }
        [NotMapped]
        public decimal moraTotal { get; set; }
    }
}
