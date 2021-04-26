using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoCredito.ViewsModels
{
    public class reciboPago
    {
        public int id { get; set; }
        public string nombreCliente { get; set; }
        public string nunPrestamo { get; set; }
        public string montoRecibido { get; set; }
        public string capitalPagado { get; set; }
        public string interesPagado { get; set; }
        public string moraPagada { get; set; }
        public string abono { get; set; }
        public string fechaRecibo { get; set; }
        public string usuarioNombre { get; set; }
        public string comentario { get; set; }
    }
}