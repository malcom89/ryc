namespace MotoCredito.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Recibo")]
    public partial class Recibo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Recibo()
        {
            Pagos = new HashSet<Pagos>();
        }

        public int Id { get; set; }

        public int PagoReciboId { get; set; }

        [Column(TypeName = "money")]
        public decimal? InteresPagado { get; set; }

        [Column(TypeName = "money")]
        public decimal? moraPagada { get; set; }

        [Column(TypeName = "money")]
        public decimal? CapitalPagado { get; set; }

        [Column(TypeName = "money")]
        public decimal? abono { get; set; }

        [Column(TypeName = "date")]
        public DateTime fechaPago { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalPagado { get; set; }

        public int? usuarioId { get; set; }

        [StringLength(50)]
        public string comentario { get; set; }

        public virtual usuarios usuarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pagos> Pagos { get; set; }
    }
}
