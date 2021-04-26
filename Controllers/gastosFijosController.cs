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
    public class gastosFijosController : Controller
    {
        private dbcontext db = new dbcontext();
        readonly usuarios user = (usuarios)System.Web.HttpContext.Current.Session["user"];
        // GET: gastosFijos
        public ActionResult Index()
        {
            var gastosFijo = db.gastosFijo.Include(g => g.tipoGastosFijo).Include(g => g.usuarios);
            return View(gastosFijo.ToList());
        }

        // GET: gastosFijos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gastosFijo gastosFijo = db.gastosFijo.Find(id);
            if (gastosFijo == null)
            {
                return HttpNotFound();
            }
            return View(gastosFijo);
        }

        // GET: gastosFijos/Create
        public ActionResult Create()
        {
            ViewBag.tipoGastoFijoId = new SelectList(db.tipoGastosFijo, "id", "descripcion");

            return View();
        }

        // POST: gastosFijos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(gastosFijo gastosFijo)
        {
            gastosFijo.fecha = DateTime.Now;
            gastosFijo.usuarioId = user.Id;

            db.gastosFijo.Add(gastosFijo);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: gastosFijos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gastosFijo gastosFijo = db.gastosFijo.Find(id);
            if (gastosFijo == null)
            {
                return HttpNotFound();
            }
            ViewBag.tipoGastoFijoId = new SelectList(db.tipoGastosFijo, "id", "descripcion", gastosFijo.tipoGastoFijoId);
            ViewBag.usuarioId = new SelectList(db.usuarios, "Id", "Nombre", gastosFijo.usuarioId);
            return View(gastosFijo);
        }

        // POST: gastosFijos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(gastosFijo gastosFijo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gastosFijo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tipoGastoFijoId = new SelectList(db.tipoGastosFijo, "id", "descripcion", gastosFijo.tipoGastoFijoId);
            ViewBag.usuarioId = new SelectList(db.usuarios, "Id", "Nombre", gastosFijo.usuarioId);
            return View(gastosFijo);
        }

        // GET: gastosFijos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gastosFijo gastosFijo = db.gastosFijo.Find(id);
            if (gastosFijo == null)
            {
                return HttpNotFound();
            }
            return View(gastosFijo);
        }

        // POST: gastosFijos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gastosFijo gastosFijo = db.gastosFijo.Find(id);
            db.gastosFijo.Remove(gastosFijo);
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

        public ActionResult createTipoGastos()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createTipoGastos(tipoGastosFijo tipoGastos)
        {
            db.tipoGastosFijo.Add(tipoGastos);
            db.SaveChanges();

            return RedirectToAction("ListTipoGastosFijos");
        }

        public ActionResult GastosFijos()
        {
            return View();
        }
        public ActionResult ListTipoGastosFijos()
        {
            return View(db.tipoGastosFijo.ToList());
        }

        [HttpPost]
        public JsonResult getPeriodosUsados()
        {
            var mesAcutal = DateTime.Now;
            var lastYear = mesAcutal.Year;
            var mes = mesAcutal.Month;
            var meses = new List<object>();
            while (lastYear >= 2020)
            {
                meses.Add(new { mes = mes, ano = lastYear });

                if (mes > 1)
                {
                    mes -= 1;
                }
                else
                {
                    mes = 12;
                    lastYear -= 1;
                }

            }

            return Json(meses);
        }

        public ActionResult createCajaChica()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createCajaChica(cajaChica caja)
        {


            db.cajaChica.Add(caja);
            db.SaveChanges();
            return View();
        }

        public ActionResult mantenimientoCajaChica()
        {
            var movimientosCajaChica = db.movimientosCajaChica.ToList();

            return View(movimientosCajaChica);
        }

        public ActionResult addMovimientoCajaChica()
        {
            ViewBag.tipoMovimientoId = new SelectList(db.tipoMovimientosCajaChica, "id", "descripcion");

            ViewBag.cajaChicaId = new SelectList(db.cajaChica, "id", "descripcion");
            return View();
        }
        [HttpPost]
        public ActionResult addMovimientoCajaChica(movimientosCajaChica movimiento)
        {
            movimiento.usuarioId = user.Id;
            movimiento.fecha = DateTime.Now;

            var caja = db.cajaChica.First(x => x.id == movimiento.cajaChicaId);

            if (movimiento.tipoMovimientoId == 1)
            {
                if (caja.balanceRestante >= movimiento.monto)
                {
                    caja.balanceRestante -= movimiento.monto;
                }
                else
                {
                    return View();
                }
            }
            else if (movimiento.tipoMovimientoId == 2)
            {
                caja.balanceRestante += movimiento.monto;
            }

            db.Entry(caja).State = EntityState.Modified;

            db.movimientosCajaChica.Add(movimiento);
            db.SaveChanges();

            return RedirectToAction("mantenimientoCajaChica");
        }
  

        public ActionResult editarMovimientoCaja(int id)
        {
            ViewBag.tipoMovimientoId = new SelectList(db.tipoMovimientosCajaChica, "id", "descripcion");

            ViewBag.cajaChicaId = new SelectList(db.cajaChica, "id", "descripcion");
            return View(db.movimientosCajaChica.Find(id));
        }
        [HttpPost]
        public ActionResult editarMovimientoCaja(movimientosCajaChica movimiento)
        {
            movimiento.usuarioId = user.Id;
            movimiento.fecha = DateTime.Now;

            var caja = db.cajaChica.First(x => x.id == movimiento.cajaChicaId);

            //var oldMovimiento = db.movimientosCajaChica.First(x => x.id == movimiento.id);


            //if (movimiento.tipoMovimientoId == 1)
            //{

            //    caja.balanceRestante += oldMovimiento.monto;

            //}
            //else if (movimiento.tipoMovimientoId == 2)
            //{
            //    caja.balanceRestante -= oldMovimiento.monto;
            //}


            if (movimiento.tipoMovimientoId == 1)
            {
                if (caja.balanceRestante >= movimiento.monto)
                {
                    caja.balanceRestante -= movimiento.monto;
                }
                else
                {
                    return View();
                }
            }
            else if (movimiento.tipoMovimientoId == 2)
            {
                caja.balanceRestante += movimiento.monto;
            }

            var newMovimiento = db.movimientosCajaChica.Find(movimiento.id);

            newMovimiento = movimiento;


            db.Entry(caja).State = EntityState.Modified;
            db.SaveChanges();
            db.Entry(newMovimiento).State = EntityState.Modified;
         
            db.SaveChanges();

            return RedirectToAction("mantenimientoCajaChica");
        }



  
        public ActionResult eliminarMovimientoCaja(int id)
        {

            var oldMovimiento = db.movimientosCajaChica.Find(id);
            var caja = db.cajaChica.First(x => x.id == oldMovimiento.cajaChicaId);




            if (oldMovimiento.tipoMovimientoId == 1)
            {

                caja.balanceRestante += oldMovimiento.monto;

            }
            else if (oldMovimiento.tipoMovimientoId == 2)
            {
                caja.balanceRestante -= oldMovimiento.monto;
            }



            db.Entry(caja).State = EntityState.Modified;

            db.movimientosCajaChica.Remove(oldMovimiento);
            db.SaveChanges();

            return RedirectToAction("mantenimientoCajaChica");
        }
    }
}
