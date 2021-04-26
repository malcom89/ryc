using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoCredito.ViewsModels
{
    public class PrestamosModelsViews
    {
        public int IdCliente { get; set; }
        public int IdModelo { get; set; }
        public int IdTipoPrestamo { get; set; }
        public string NoChasis { get; set; }
        public string NoPlaca { get; set; }
        public decimal MontoPrestamo { get; set; }
        public decimal TasaInteres { get; set; }
        public decimal TasaMora { get; set; }
        public int TiempoParaMora { get; set; }
        public int Tiempo { get; set; }
        public decimal PorcentajeInteresAbono { get; set; }
        public decimal Cuota { get; set; }
        public decimal interes { get; set; }
        public decimal capital { get; set; }
        public int? año { get; set; }
        public string color { get; set; }

        public DateTime fecha { get; set; }
    }
}