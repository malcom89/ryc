namespace MotoCredito.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoPrestamo")]
    public partial class TipoPrestamo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoPrestamo()
        {
            Prestamo = new HashSet<Prestamo>();
        }

        public int Id { get; set; }

        public decimal InteresPrestamo { get; set; }

        public decimal InteresMora { get; set; }

        public decimal InteresSaldo { get; set; }

        public int DiasGraciaMora { get; set; }

        [Required]
        [StringLength(30)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestamo> Prestamo { get; set; }
    }
}
