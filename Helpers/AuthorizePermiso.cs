using MotoCredito.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private dbcontext db = new dbcontext();
        public AuthorizePermiso(string name)
        {

            _name = name;

        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(ComprobarPermisos(filterContext))
            {

            }else
            {
                filterContext.HttpContext.Response.Redirect("/usuarios/login");
            }
        }


        private bool ComprobarPermisos(AuthorizationContext filterContext)
        {
            bool tienePermiso = true;
            // Lógica de comprobación de autorización
            // ....
            string nombreMaquina = string.Empty;

            nombreMaquina = Dns.GetHostName();

            IPAddress[] ips = Dns.GetHostAddresses(nombreMaquina);

            string ipMaquina = ips[0].ToString();

  
            var estaLogueado =  db.UserLoger.FirstOrDefault(x =>
            x.nombreMaquina == nombreMaquina
            && x.ipMaquina == ipMaquina
            && x.estatuSession == 1);

            
            if(estaLogueado != null)
            {
                var usuario = db.usuarios.Include(x => x.Permisos).Where(x => x.Id == estaLogueado.idUsuario).FirstOrDefault();
                if(DateTime.Now > estaLogueado.expirationSession || usuario.Permisos.FirstOrDefault(x => x.descripcion == _name) == null)
                {
                    tienePermiso = false;
                } else
                {

                }
            } else
            {
                tienePermiso = false;
            }
            return tienePermiso;
        }
    }
}