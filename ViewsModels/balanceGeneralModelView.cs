using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoCredito.ViewsModels
{
    public class balanceGeneralModelView
    {
        public int id { get; set; }
        public string capitalPrestado { get; set; }
        public string interesesGenerados { get; set; }
        public string moraAcumulada { get; set; }
        public int cantidadPrestamosActivos { get; set; }

    }
}