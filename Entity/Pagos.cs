namespace MotoCredito.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pagos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pagos()
        {
            Recibo = new HashSet<Recibo>();
        }

        public int Id { get; set; }

        public int IdPrestamo { get; set; }

        [Column(TypeName = "money")]
        public decimal? Capital { get; set; }

        [Column(TypeName = "money")]
        public decimal? Intereses { get; set; }

        [Column(TypeName = "money")]
        public decimal? Mora { get; set; }

        public int NoPago { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaVencimiento { get; set; }

        [Column(TypeName = "money")]
        public decimal? Sobrante { get; set; }

        public bool cubierto { get; set; }

        public virtual Prestamo Prestamo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recibo> Recibo { get; set; }

        [NotMapped]
        public DateTime fechaPago { get; set; }
    }
}
