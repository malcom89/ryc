namespace MotoCredito.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gastosFijo")]
    public partial class gastosFijo
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string descripcion { get; set; }

        public int tipoGastoFijoId { get; set; }

        public DateTime fecha { get; set; }

        public int usuarioId { get; set; }
        public decimal monto { get; set; }

        public virtual tipoGastosFijo tipoGastosFijo { get; set; }

        public virtual usuarios usuarios { get; set; }
    }
}
