namespace MotoCredito.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clientes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clientes()
        {
            Prestamo = new HashSet<Prestamo>();
            TelefonosDeContactos = new HashSet<TelefonosDeContactos>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; }

        public int TipoIdentificacion { get; set; }

        [Required]
        [StringLength(25)]
        public string NoIdentificacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaCreacion { get; set; }

        [StringLength(11)]
        public string numeroIdentificacion { get; set; }

        [StringLength(11)]
        public string numeroTelefonico1 { get; set; }

        [StringLength(11)]
        public string numeroTelefonico2 { get; set; }

        [StringLength(11)]
        public string numeroTelefonicoReferencia1 { get; set; }

        [StringLength(11)]
        public string numeroTelefonicoReferencia2 { get; set; }

        [StringLength(11)]
        public string numeroTelefonicoReferencia3 { get; set; }

        [StringLength(50)]
        public string direccion { get; set; }

        [StringLength(30)]
        public string nombreReferencia1 { get; set; }

        [StringLength(30)]
        public string nombreReferencia2 { get; set; }

        [StringLength(30)]
        public string nombreReferencia3 { get; set; }

        [StringLength(255)]
        public string fotoUrl { get; set; }

        public virtual Tipoidentificacion Tipoidentificacion1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestamo> Prestamo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TelefonosDeContactos> TelefonosDeContactos { get; set; }

        [NotMapped]
        public System.Web.HttpPostedFileBase Foto { get; set; }

    }
}
