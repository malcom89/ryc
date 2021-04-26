using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MotoCredito.Entity;

namespace MotoCredito.Controllers
{
    public class TipoPrestamoesController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: TipoPrestamoes
        public ActionResult Index()
        {
            return View(db.TipoPrestamo.ToList());
        }

        // GET: TipoPrestamoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrestamo tipoPrestamo = db.TipoPrestamo.Find(id);
            if (tipoPrestamo == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrestamo);
        }

        // GET: TipoPrestamoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPrestamoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,InteresPrestamo,InteresMora,InteresSaldo,DiasGraciaMora")] TipoPrestamo tipoPrestamo)
        {
            if (ModelState.IsValid)
            {
                db.TipoPrestamo.Add(tipoPrestamo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoPrestamo);
        }

        // GET: TipoPrestamoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrestamo tipoPrestamo = db.TipoPrestamo.Find(id);
            if (tipoPrestamo == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrestamo);
        }

        // POST: TipoPrestamoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InteresPrestamo,InteresMora,InteresSaldo,DiasGraciaMora")] TipoPrestamo tipoPrestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPrestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoPrestamo);
        }

        // GET: TipoPrestamoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrestamo tipoPrestamo = db.TipoPrestamo.Find(id);
            if (tipoPrestamo == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrestamo);
        }

        // POST: TipoPrestamoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPrestamo tipoPrestamo = db.TipoPrestamo.Find(id);
            db.TipoPrestamo.Remove(tipoPrestamo);
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
