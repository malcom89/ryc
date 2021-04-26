namespace MotoCredito.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Modelo")]
    public partial class Modelo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modelo()
        {
            Prestamo = new HashSet<Prestamo>();
        }

        public int Id { get; set; }

        public int IdMarca { get; set; }

        [Required]
        [StringLength(10)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(10)]
        public string TipoVehiculo { get; set; }

        public virtual Marca Marca { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestamo> Prestamo { get; set; }
    }
}
