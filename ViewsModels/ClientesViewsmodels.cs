using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoCredito.ViewsModels
{
    public class ClientesViewsmodels
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public int tipoIdentificacion { get; set; }
        public string numeroIdentificacion { get; set; }
        public string numeroTelefonico1 { get; set; }
        public string numeroTelefonico2 { get; set; }
        public string numeroTelefonicoReferencia1 { get; set; }
        public string numeroTelefonicoReferencia2 { get; set; }
        public string numeroTelefonicoReferencia3 { get; set; }
        public string direccion { get; set; }
        public bool succes { get; set; }
    }
}