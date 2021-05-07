using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MotoCredito.Entity
{
    [Table("UserLoger")]
    public class UserLoger
    {
        [Key]
        public  int idLoger { get; set; }
        public int idUsuario { get; set; }
        public string ipMaquina { get; set; }
        public string nombreMaquina { get; set; }
        public Nullable<DateTime> expirationSession { get; set; }
        public int estatuSession { get; set; }
    }
}