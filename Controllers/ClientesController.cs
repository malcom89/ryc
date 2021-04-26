using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MotoCredito.Entity;
using System.IO;

namespace MotoCredito.Controllers
{
    public class ClientesController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: Clientes
        public ActionResult Index()
        {
            var clientes = db.Clientes.Include(c => c.Tipoidentificacion1);
            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.TipoIdentificacion = new SelectList(db.Tipoidentificacion, "Id", "Descripcion");
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Clientes clientes)
        {

            string path = Path.Combine(Server.MapPath("~/FotosClientes"),
                                       Path.GetFileName(clientes.Foto.FileName));
            clientes.Foto.SaveAs(path);

            clientes.fotoUrl = path;
            clientes.FechaCreacion = DateTime.Now;
                db.Clientes.Add(clientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            

            ViewBag.TipoIdentificacion = new SelectList(db.Tipoidentificacion, "Id", "Descripcion", clientes.TipoIdentificacion);
            return View(clientes);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoIdentificacion = new SelectList(db.Tipoidentificacion, "Id", "Descripcion", clientes.TipoIdentificacion);
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Clientes clientes)
        {

            if (clientes.Foto != null)
            {
                string path = Path.Combine(Server.MapPath("~/FotosClientes"),
                                                Path.GetFileName(clientes.Foto.FileName));
                clientes.Foto.SaveAs(path);

                clientes.fotoUrl = "/FotosClientes/" + clientes.Foto.FileName;
            }
            db.Entry(clientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
       
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clientes clientes = db.Clientes.Find(id);
            db.Clientes.Remove(clientes);
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
    }
}
