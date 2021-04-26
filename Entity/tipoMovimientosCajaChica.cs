namespace MotoCredito.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tipoMovimientosCajaChica")]
    public partial class tipoMovimientosCajaChica
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipoMovimientosCajaChica()
        {
            movimientosCajaChica = new HashSet<movimientosCajaChica>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<movimientosCajaChica> movimientosCajaChica { get; set; }
    }
}
