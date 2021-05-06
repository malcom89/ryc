using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoCredito.ViewsModels
{
    public class cierresModelsViews
    {
        public int cantidadPrestamos { get; set; }
        public string montoPrestado { get; set; }
        public string montoCobrado { get; set; }
        public string capitalCobrado { get; set; }
        public string interesCobrado { get; set; }
        public string moraCobrada { get; set; }
        public string abonos { get; set; }
        public string totalGastosFijos { get; set; }
        public string totalGastoCajaChica { get; set; }
        public string totalIngresoCajaChica { get; set; }
        public string totalEgresos { get; set; }
    }
}