namespace MotoCredito.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("movimientosCajaChica")]
    public partial class movimientosCajaChica
    {
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string descripcion { get; set; }

        public int tipoMovimientoId { get; set; }

        [Column(TypeName = "money")]
        public decimal monto { get; set; }

        public DateTime fecha { get; set; }

        public int usuarioId { get; set; }

        public int cajaChicaId { get; set; }

        public virtual cajaChica cajaChica { get; set; }

        public virtual tipoMovimientosCajaChica tipoMovimientosCajaChica { get; set; }

        public virtual usuarios usuarios { get; set; }
    }
}
