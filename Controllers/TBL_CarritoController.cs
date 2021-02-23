using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProgramaDeRequisas.Models;

namespace ProgramaDeRequisas.Controllers
{
    public class TBL_CarritoController : Controller
    {
        private SITEPLUSEntities2 db = new SITEPLUSEntities2();

        // GET: TBL_Carrito
        public ActionResult Index()
        {
            return View(db.TBL_Carrito.ToList());
        }

        // GET: TBL_Carrito/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_Carrito tBL_Carrito = db.TBL_Carrito.Find(id);
            if (tBL_Carrito == null)
            {
                return HttpNotFound();
            }
            return View(tBL_Carrito);
        }

        // GET: TBL_Carrito/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TBL_Carrito/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Id_Carrito,COD_Material,Descripcion,Cantidad_Pedida,Precio,Cantidad_Aprobada,Cantidad_Disponible")] TBL_Carrito tBL_Carrito)
        {
            if (ModelState.IsValid)
            {
                db.TBL_Carrito.Add(tBL_Carrito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tBL_Carrito);
        }

        // GET: TBL_Carrito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_Carrito tBL_Carrito = db.TBL_Carrito.Find(id);
            if (tBL_Carrito == null)
            {
                return HttpNotFound();
            }
            return View(tBL_Carrito);
        }

        // POST: TBL_Carrito/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Id_Carrito,COD_Material,Descripcion,Cantidad_Pedida,Precio,Cantidad_Aprobada,Cantidad_Disponible")] TBL_Carrito tBL_Carrito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_Carrito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBL_Carrito);
        }

        // GET: TBL_Carrito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_Carrito tBL_Carrito = db.TBL_Carrito.Find(id);
            if (tBL_Carrito == null)
            {
                return HttpNotFound();
            }
            return View(tBL_Carrito);
        }

        // POST: TBL_Carrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_Carrito tBL_Carrito = db.TBL_Carrito.Find(id);
            db.TBL_Carrito.Remove(tBL_Carrito);
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
