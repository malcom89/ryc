namespace MotoCredito.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cajaChica")]
    public partial class cajaChica
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cajaChica()
        {
            movimientosCajaChica = new HashSet<movimientosCajaChica>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string descripcion { get; set; }

        [Column(TypeName = "money")]
        public decimal montoMaximo { get; set; }

        [Column(TypeName = "money")]
        public decimal balanceRestante { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<movimientosCajaChica> movimientosCajaChica { get; set; }
    }
}
