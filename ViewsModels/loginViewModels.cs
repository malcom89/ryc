using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MotoCredito.ViewsModels
{
    public class loginViewModels
    {
       [Required]
        public string userLogin { get; set; }
        [Required]
        public string clave { get; set; }
    }
}