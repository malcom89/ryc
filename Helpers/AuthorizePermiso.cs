using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace MotoCredito.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizePermiso: AuthorizeAttribute
    {
        public readonly string _name;
        public AuthorizePermiso(string name)
        {

            _name = name;

        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(ComprobarPermisos())
            {

            }else
            {
                filterContext.HttpContext.Response.Redirect("/usuarios/login");
            }
        }


        private bool ComprobarPermisos()
        {
            bool tienePermiso = true;
            // Lógica de comprobación de autorización
            // ....
            return tienePermiso;
        }
    }
}