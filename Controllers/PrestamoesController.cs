using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MotoCredito.Entity;
using MotoCredito.ViewsModels;
using Rotativa;

namespace MotoCredito.Controllers
{
    public class PrestamoesController : Controller
    {
        private dbcontext db = new dbcontext();
        readonly usuarios user = (usuarios)System.Web.HttpContext.Current.Session["user"];


        public ActionResult Index()
        {
            //generarMora();
            var prestamos = db.Prestamo.Include(p => p.Clientes).Include(p => p.Modelo).Where(x => x.BalanceRestante >= 1).ToList();

            foreach (var prestamo in prestamos)
            {
                if (prestamo.Pagos.Where(x => !x.cubierto).Any(p => p.Mora >= 1))
                {
                    prestamo.tieneMora = true;
                    prestamo.moraTotal = Convert.ToDecimal(prestamo.Pagos.Sum(x => x.Mora));
                }
            }

            return View(prestamos);
        }

        // GET: Prestamoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);

            foreach (var pago in prestamo.Pagos)
            {
                if (pago.cubierto)
                {
                    pago.fechaPago = pago.Recibo.First().fechaPago;
                }
            }

            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }


        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "Nombres");
            ViewBag.IdModelo = new SelectList(db.Modelo, "Id", "Descripcion");
            ViewBag.IdMarca = new SelectList(db.Marca, "Id", "Descripcion");
            ViewBag.IdTipoPrestamo = new SelectList(db.TipoPrestamo, "Id", "Descripcion");
            return View();
        }

        // POST: Prestamoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(PrestamosModelsViews prestamo)
        {


            if (user == null)
            {
                return RedirectToAction("login", "usuarios");
            }

            var pagos = new List<Pagos>();
            var fechaApertura = prestamo.fecha;


            for (int i = 1; i <= prestamo.Tiempo; i++)
            {

                pagos.Add(new Pagos
                {
                    FechaVencimiento = fechaApertura.AddMonths(i),
                    NoPago = i,
                    Capital = prestamo.capital,
                    Intereses = prestamo.interes

                });
            }

            var prest = new Prestamo()
            {
                BalanceRestante = prestamo.MontoPrestamo,
                color = prestamo.color,
                año = prestamo.año,
                FechaApertura = DateTime.Now,
                IdCliente = prestamo.IdCliente,
                IdModelo = prestamo.IdModelo,
                MontoPrestamo = prestamo.MontoPrestamo,
                NoChasis = prestamo.NoChasis,
                NoPlaca = prestamo.NoPlaca,
                Tiempo = prestamo.Tiempo,
                IdTipoPrestamo = prestamo.IdTipoPrestamo,
                Cuota = prestamo.Cuota,
                interes = prestamo.interes,
                capital = prestamo.capital,
                Pagos = pagos,
                usuarioId = user.Id
            };

            db.Prestamo.Add(prest);

            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult aplicarPago(int id)
        {
            var prestamo = db.Prestamo.FirstOrDefault(x => x.Id == id);

            var ultimoPago = prestamo.Pagos.Where(x => x.cubierto != true).OrderBy(m => m.NoPago).FirstOrDefault();

            PagosModelsViews pago = new PagosModelsViews();


            pago.capital = prestamo.BalanceRestante;
            if (ultimoPago != null)
            {
                pago.abono = ultimoPago.Sobrante;
                pago.cuota = prestamo.Cuota;
                pago.capital = ultimoPago.Capital;
                pago.interes = ultimoPago.Intereses;
                pago.prestamoNumero = prestamo.Id;
                pago.numeroCuota = ultimoPago.NoPago;
                pago.mora = ultimoPago.Mora;
            }
            return View(pago);
        }

        public ActionResult pagosReport()
        {
            return View();
        }

        [HttpPost]
        public JsonResult getPagos(DateTime? desde, DateTime? hasta, bool today)
        {
            var pagos = new List<ReportePagosModelsViews>();
            var recibo = new List<Recibo>();
            if (today)
            {
                var fechaHoy = DateTime.Now;

                recibo = db.Recibo.Where(x => x.fechaPago.Year == fechaHoy.Year && x.fechaPago.Month == fechaHoy.Month && x.fechaPago.Day == fechaHoy.Day).ToList();


            }
            else
            {
                recibo = db.Recibo.Where(x => x.fechaPago <= hasta && x.fechaPago >= desde).ToList();
            }
            foreach (var m in recibo)
            {

                pagos.Add(new ReportePagosModelsViews
                {
                    abono = m.abono.ToString(),
                    capital = m.CapitalPagado.ToString(),
                    interes = m.InteresPagado.ToString(),
                    mora = m.moraPagada.ToString(),
                    totalPagado = m.TotalPagado.ToString(),
                    numeroCuota = m.comentario,
                    prestamoNumero = m.Pagos.First().IdPrestamo.ToString(),
                    fechaPago = m.fechaPago.ToString(),
                    usuarioNombre = m.usuarios.Nombre + " " + m.usuarios.Apellido,
                    nombreCliente = m.Pagos.First().Prestamo.Clientes.Nombres + " " + m.Pagos.First().Prestamo.Clientes.Apellidos
                });
            }



            return Json(pagos);
        }

        [HttpPost]

        public ActionResult aplicarPago(PagosModelsViews pago)
        {
            if (user == null)
            {
                return RedirectToAction("login", "usuarios", null);
            }

            var recibo = new Recibo();

            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {

                    var prestamo = db.Prestamo.First(x => x.Id == pago.prestamoNumero);


                    recibo.TotalPagado = pago.totalPagado;
                    recibo.fechaPago = DateTime.Now;

                    recibo.usuarioId = user.Id;

                    recibo.comentario = "Se aplico pago a cuonta no.: ";


                    var montoTotal = pago.abono == null ? pago.totalPagado : pago.totalPagado + pago.abono;
                    var cuotaAcual = pago.numeroCuota;

                    while (montoTotal > 0)
                    {

                        var pagoAplicar = db.Pagos.FirstOrDefault(x => x.IdPrestamo == pago.prestamoNumero && x.NoPago == cuotaAcual);


                        if (pagoAplicar.FechaVencimiento <= DateTime.Now && pago.cuota <= montoTotal)
                        {
                            //aplica pago a mora
                            if (pagoAplicar.Mora != null && montoTotal >= pagoAplicar.Mora)
                            {
                                recibo.moraPagada = recibo.moraPagada != null ? recibo.moraPagada + pagoAplicar.Mora : pagoAplicar.Mora;
                                montoTotal = montoTotal - pagoAplicar.Mora;
                            }

                            //aplica pago a intereses
                            if (montoTotal >= pagoAplicar.Intereses)
                            {
                                recibo.InteresPagado = recibo.InteresPagado != null ? recibo.InteresPagado + pagoAplicar.Intereses : pagoAplicar.Intereses;
                                montoTotal = montoTotal - pagoAplicar.Intereses;
                            }
                            else if (montoTotal >= 1)
                            {
                                recibo.InteresPagado = recibo.InteresPagado + montoTotal;
                                montoTotal = 0;
                            }

                            //aplica pago a capital
                            if (montoTotal >= pagoAplicar.Capital)
                            {
                                recibo.CapitalPagado = recibo.CapitalPagado != null ? recibo.CapitalPagado + pagoAplicar.Capital : pagoAplicar.Capital;
                                pagoAplicar.cubierto = true;
                                montoTotal = montoTotal - pago.capital;
                                prestamo.BalanceRestante -= (decimal)pago.capital;


                            }
                            else if (montoTotal >= 1)
                            {
                                recibo.CapitalPagado = recibo.CapitalPagado != null ? recibo.CapitalPagado + pagoAplicar.Capital : pagoAplicar.Capital;
                                prestamo.BalanceRestante -= montoTotal;
                                montoTotal = 0;
                            }

                            recibo.comentario += " " + pagoAplicar.NoPago.ToString() + ",";



                        }
                        //aplica abono a proxima cuota
                        else if (montoTotal > 0 && pago.cuota > montoTotal)
                        {

                            pagoAplicar.Sobrante = montoTotal;
                            recibo.abono = montoTotal;
                            montoTotal = 0;
                            recibo.comentario += " " + pagoAplicar.NoPago.ToString();


                        }


                        pagoAplicar.Recibo.Add(recibo);

                        db.Entry(pagoAplicar).State = EntityState.Modified;

                        db.SaveChanges();

                        cuotaAcual++;
                    }


                    db.Entry(prestamo).State = EntityState.Modified;
                    db.Recibo.Add(recibo);
                    db.SaveChanges();


                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error occurred.");
                }
            }










            var datos = new reciboPago
            {
                id = recibo.Id,
                capitalPagado = Convert.ToDecimal(recibo.CapitalPagado).ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-au")),
                interesPagado = Convert.ToDecimal(recibo.InteresPagado).ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-au")),
                moraPagada = Convert.ToDecimal(recibo.moraPagada).ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-au")),
                abono = Convert.ToDecimal(recibo.abono).ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-au")),
                montoRecibido = Convert.ToDecimal(recibo.TotalPagado).ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-au")),
                fechaRecibo = recibo.fechaPago.ToString(),
                nombreCliente = recibo.Pagos.First().Prestamo.Clientes.Nombres,
                usuarioNombre = recibo.Pagos.First().Prestamo.usuarios.Nombre + " " + recibo.Pagos.First().Prestamo.usuarios.Apellido,
                comentario = recibo.comentario

            };

            return RedirectToAction("imprimirRecibo", datos);
        }


        public ActionResult getBalanceGeneral()
        {
            var usuario = user;
            var prestamosActivos = db.Prestamo.Where(x => x.BalanceRestante >= 1).ToList();
            var pagosPendientes = prestamosActivos.Select(x => x.Pagos.Where(h => h.FechaVencimiento <= DateTime.Now && !h.cubierto)).ToList();

            var balance = new balanceGeneralModelView
            {
                cantidadPrestamosActivos = prestamosActivos.Count,
                capitalPrestado = Convert.ToDecimal(prestamosActivos.Sum(x => x.BalanceRestante)).ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-au"))
            };
            return View(balance);

        }

        public ActionResult balanceGeneral()
        {
            var prestamosActivos = db.Prestamo.Where(x => x.BalanceRestante >= 1).ToList();
            List<Pagos> pagosPendientes = new List<Pagos>();
            prestamosActivos.ForEach(delegate (Prestamo prestamo)
           {
               pagosPendientes.AddRange(prestamo.Pagos.Where(h => h.FechaVencimiento <= DateTime.Now && !h.cubierto).ToList());
           }
           );


            var data = new balanceGeneralModelView
            {
                cantidadPrestamosActivos = prestamosActivos.Count,
                capitalPrestado = Convert.ToDecimal(prestamosActivos.Sum(x => x.BalanceRestante)).ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-au")),
                interesesGenerados = Convert.ToDecimal(pagosPendientes.Sum(x => x.Intereses)).ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-au")),
                moraAcumulada = Convert.ToDecimal(pagosPendientes.Sum(x => x.Mora)).ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-au"))


            };




            return View(data);
        }

        public void generarMora()
        {
            var prestamosActivos = db.Prestamo.Where(x => x.BalanceRestante >= 1).ToList();
            List<Pagos> pagosPendientes = new List<Pagos>();
            prestamosActivos.ForEach(delegate (Prestamo prestamo)
            {
                var condicionesPrestamo = prestamo.TipoPrestamo;
                foreach (var pago in prestamo.Pagos)
                {
                    var fechaACobrar = pago.FechaVencimiento.AddDays(condicionesPrestamo.DiasGraciaMora);
                    if (fechaACobrar <= DateTime.Now)
                    {
                        var diferencia = DateTime.Now - fechaACobrar;
                        var tasaPorDia = condicionesPrestamo.InteresMora / 30;
                        var tasaTotal = (tasaPorDia * diferencia.Days) / 100;

                        pago.Mora = prestamo.Cuota * tasaTotal;

                        db.Entry(pago).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                }
            }
           );
        }

        public ActionResult imprimirRecibo(reciboPago recibo)
        {

            

            var report = new ViewAsPdf("imprimirRecibo", recibo)
            {
              
                MinimumFontSize=18,
                PageSize = Rotativa.Options.Size.A8,
                PageMargins= new Rotativa.Options.Margins(0,5,0,5)



            };
            return report;

            //return View();
        }

        public ActionResult prueba()
        {
            return View();
        }
        // GET: Prestamoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "Nombres", prestamo.IdCliente);
            ViewBag.IdModelo = new SelectList(db.Modelo, "Id", "Descripcion", prestamo.IdModelo);
            return View(prestamo);
        }

        // POST: Prestamoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdCliente,NoChasis,NoPlaca,IdModelo,FechaApertura,TasaInteres,TasaMora,TiempoParaMora,MontoPrestamo,Tiempo,PorcentajeInteresAbono,BalanceRestante")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "Id", "Nombres", prestamo.IdCliente);
            ViewBag.IdModelo = new SelectList(db.Modelo, "Id", "Descripcion", prestamo.IdModelo);
            return View(prestamo);
        }

        public ActionResult aplicarPagosMasivos(int id)
        {
            var pagos = db.Pagos.Where(x => x.IdPrestamo == id);

            return View(pagos);
        }
        // GET: Prestamoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // POST: Prestamoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamo prestamo = db.Prestamo.Find(id);
            db.Prestamo.Remove(prestamo);
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

        [HttpPost]
        public JsonResult getModelos(int marcasId)
        {
            return Json(db.Modelo.Where(x => x.IdMarca == marcasId).Select(p => new { p.Id, p.Descripcion }).ToList());
        }

        [HttpPost]
        public JsonResult getClientes(string cedula)
        {
            var data = db.Clientes.FirstOrDefault(x => x.NoIdentificacion.Equals(cedula));
            ClientesViewsmodels clientes = new ClientesViewsmodels();

            if (data == null)
            {
                clientes = new ClientesViewsmodels { succes = false };
            }
            else
            {

                clientes = new ClientesViewsmodels { Id = data.Id, Nombres = data.Nombres + " " + data.Apellidos, succes = true };
            }
            return Json(clientes);
        }

        [HttpPost]
        public JsonResult getTipoPrestamo(int prestamoId)
        {
            var data = db.TipoPrestamo.FirstOrDefault(x => x.Id == prestamoId);

            var tipoPrestamo = new
            {
                data.Id,
                data.DiasGraciaMora,
                data.InteresMora,
                data.InteresPrestamo,
                data.InteresSaldo

            };

            return Json(tipoPrestamo);
        }

        [HttpPost]
        public JsonResult addNuevoCliente(int idTipoIdentificacionNuevoCliente, string noidentificacionNuevoCliente, string nombreNuevoCliente, string apellidoNuevoCliente, string telefonoNuevoCliente,
            string telefonoMovilNuevoCliente, string direccionNuevoCliente, string nombreReferencia1NuevoCliente, string telefonoReferencia1NuevoCliente, string nombreReferencia2NuevoCliente, string telefonoReferencia2NuevoCliente, string nombreReferencia3NuevoCliente, string telefonoReferencia3NuevoCliente)
        {

            var nuevoCliente = new Clientes
            {
                TipoIdentificacion = idTipoIdentificacionNuevoCliente,
                NoIdentificacion = noidentificacionNuevoCliente,
                Nombres = nombreNuevoCliente,
                Apellidos = apellidoNuevoCliente,
                numeroTelefonico1 = telefonoNuevoCliente,
                numeroTelefonico2 = telefonoMovilNuevoCliente,
                direccion = direccionNuevoCliente,
                FechaCreacion = DateTime.Now,
                nombreReferencia1 = nombreReferencia1NuevoCliente,
                numeroTelefonicoReferencia1 = telefonoReferencia1NuevoCliente,
                nombreReferencia2 = nombreReferencia2NuevoCliente,
                numeroTelefonicoReferencia2 = telefonoReferencia2NuevoCliente,
                nombreReferencia3 = nombreReferencia3NuevoCliente,
                numeroTelefonicoReferencia3 = telefonoReferencia3NuevoCliente
            };

            var NuevoClienteId = 0;
            var nombreCliente = "";
            try
            {
                db.Clientes.Add(nuevoCliente);
                db.SaveChanges();
                NuevoClienteId = nuevoCliente.Id;
                nombreCliente = nuevoCliente.Nombres + " " + nuevoCliente.Apellidos;

            }
            catch
            {

            }



            return Json(new { Id = NuevoClienteId, nombreCliente = nombreCliente });
        }

        [HttpPost]
        public JsonResult getTipoIdentificacion()
        {
            var data = db.Tipoidentificacion.Select(x => new { x.Id, x.Descripcion }).ToList();


            return Json(data);
        }


        public void editarWordFile()
        {

        }


        public ActionResult editarMora(int prestamoId)
        {
            var moras = db.Pagos.Where(x => !x.cubierto && x.Mora >= 1 && x.IdPrestamo == prestamoId).ToList();

            return View(moras);
        }

        [HttpPost]
        public JsonResult aplicarDescuentoMora(string montoMora, string noPago, string noPrestamo)
        {
            var pagoNumero = int.Parse(noPago);
            var prestamoNumero = int.Parse(noPrestamo);

            var pago = db.Pagos.FirstOrDefault(x => x.NoPago == pagoNumero && x.IdPrestamo == prestamoNumero);

            var mora = decimal.Parse(montoMora);

            pago.Mora = mora;

            db.Entry(pago).State = EntityState.Modified;

            db.SaveChanges();

            return Json(new { success = true, menssage = "Se Aplico descuento a mora del pago No." + pagoNumero + " del prestamo no." + prestamoNumero + " la nueva mora es de " + mora.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-au")) });
        }


    }
}


