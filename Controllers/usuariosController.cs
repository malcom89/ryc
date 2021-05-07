using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MotoCredito.Entity;
using MotoCredito.Helpers;
using MotoCredito.ViewsModels;

namespace MotoCredito.Controllers
{
    public class usuariosController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: usuarios
        public ActionResult Index()
        {
            return View(db.usuarios.ToList());
        }

        // GET: usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuarios usuarios = db.usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(usuarios usuarios)
        {
            if (usuarios.nuevaClave.Equals(usuarios.confirmarClave))
            {

                usuarios.fechaCreacion = DateTime.Now;
                usuarios.clave = encriptar(usuarios.nuevaClave);
                db.usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index", "Prestamoes", null);

            }

            return View(usuarios);
        }

        // GET: usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuarios usuarios = db.usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellido,UserLogin,clave,fechaCreacion")] usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        // GET: usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuarios usuarios = db.usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuarios usuarios = db.usuarios.Find(id);
            db.usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> login(loginViewModels login)
        {

            var res = new Res<Task>();
            try
            {
                var clave = encriptar(login.clave);
                var user = await db.usuarios.FirstOrDefaultAsync(x => x.UserLogin.Equals(login.userLogin) && x.clave.Equals(clave));

                if (user != null)
                {

                    string nombreMaquina = string.Empty;

                    nombreMaquina = Dns.GetHostName();

                    IPAddress[] ips = Dns.GetHostAddresses(nombreMaquina);

                    string ipMaquina = ips[0].ToString();

                    var isLogers = await db.UserLoger.Where(x => x.nombreMaquina == nombreMaquina && x.ipMaquina == ipMaquina).ToListAsync();


                    if (isLogers.Count > 0)
                    {
                        foreach (var isLoger in isLogers)
                        {
                            db.Entry(isLoger).State = EntityState.Deleted;
                            await db.SaveChangesAsync();
                        }

                        var userLoger = new UserLoger
                        {
                            idLoger = 0,
                            idUsuario = user.Id,
                            nombreMaquina = nombreMaquina,
                            ipMaquina = ipMaquina,
                            estatuSession = 1,
                            expirationSession = DateTime.UtcNow.AddHours(12)


                        };

                        db.UserLoger.Add(userLoger);
                        await db.SaveChangesAsync();

                    }
                    else
                    {


                        var userLoger = new UserLoger
                        {
                            idLoger = 0,
                            idUsuario = user.Id,
                            nombreMaquina = nombreMaquina,
                            ipMaquina = ipMaquina,
                            estatuSession = 1,
                            expirationSession = DateTime.UtcNow.AddHours(12)


                        };

                        db.UserLoger.Add(userLoger);
                        await db.SaveChangesAsync();

                    }




                    System.Web.HttpContext.Current.Session.Add("user", user);
                    res.ok = 200;




                }
                else
                {
                    res.ok = 300;
                    res.err = "USUARIO O CONTRASEÑA SON INCORRECTO INTENTE NUEVAMENTE";
                }

                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception err)
            {

                var serror = err.Message;
                if (err.InnerException != null)
                    serror = serror + " " + err.InnerException.Message;

                res.ok = 404;
                res.err = serror;
                return Json(res, JsonRequestBehavior.AllowGet);
            }



        }

        public static string encriptar(string word)
        {
            byte[] byt = System.Text.Encoding.UTF8.GetBytes(word);
            return Convert.ToBase64String(byt);
        }
        public static string desencriptar(string word)
        {
            byte[] b = Convert.FromBase64String(word);
            return System.Text.Encoding.UTF8.GetString(b);
        }

        [HttpPost]
        public async Task<JsonResult> Logout()
        {

            var res = new Res<Task>();
            string nombreMaquina = string.Empty;

            nombreMaquina = Dns.GetHostName();

            IPAddress[] ips = Dns.GetHostAddresses(nombreMaquina);

            string ipMaquina = ips[0].ToString();

            var isLogers = await db.UserLoger.Where(x => x.nombreMaquina == nombreMaquina && x.ipMaquina == ipMaquina).ToListAsync();


            if (isLogers.Count > 0)
            {
                foreach (var isLoger in isLogers)
                {
                    db.Entry(isLoger).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                }

            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}
