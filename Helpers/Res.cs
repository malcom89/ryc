using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoCredito.Helpers
{
    public class Res<T> where T: class
    {
        public int ok { get; set; }
        public string err { get; set; }
        public T data { get; set; }
    }
}