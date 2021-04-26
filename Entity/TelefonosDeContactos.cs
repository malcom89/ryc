namespace MotoCredito.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TelefonosDeContactos
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string TipoTelefono { get; set; }

        public int IdCliente { get; set; }

        public virtual Clientes Clientes { get; set; }
    }
}
