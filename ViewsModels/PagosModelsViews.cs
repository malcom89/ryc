using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoCredito.ViewsModels
{
    public class PagosModelsViews
    {
        public decimal? cuota { get; set; }
        public decimal? interes { get; set; }
        public decimal? capital { get; set; }
        public decimal? totalPagado { get; set; }
        public decimal? mora { get; set; }
        public decimal? abono { get; set; }
        public int numeroCuota { get; set; }
        public string comentario { get; set; }
        public int prestamoNumero { get; set; }
    }

    public class ReportePagosModelsViews
    {
        public string cuota { get; set; }
        public string interes { get; set; }
        public string capital { get; set; }
        public string totalPagado { get; set; }
        public string mora { get; set; }
        public string abono { get; set; }
        public string numeroCuota { get; set; }
        public string fechaPago { get; set; }
        public string prestamoNumero { get; set; }
        public string usuarioNombre { get; set; }
        public string nombreCliente { get; set; }
    }
}