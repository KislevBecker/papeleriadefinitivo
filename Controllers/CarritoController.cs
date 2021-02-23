using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProgramaDeRequisas.Data;
using ProgramaDeRequisas.Models;

namespace ProgramaDeRequisas.Controllers
{
    public class RequisasController : Controller
    {
        private ProgramaDeRequisasContext db = new ProgramaDeRequisasContext();

        // GET: Requisas
        public ActionResult Index()
        {
            //return View(db.Requisas.ToList());
            return View();
        }

        // GET: Requisas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisas requisas = db.Requisas.Find(id);
            if (requisas == null)
            {
                return HttpNotFound();
            }
            return View(requisas);
        }

        // GET: Requisas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Requisas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,descripcion,fecha_requisicion,cantidad,estado,usuario")] Requisas requisas)
        {
            if (ModelState.IsValid)
            {
                db.Requisas.Add(requisas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requisas);
        }

        // GET: Requisas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisas requisas = db.Requisas.Find(id);
            if (requisas == null)
            {
                return HttpNotFound();
            }
            return View(requisas);
        }

        // POST: Requisas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,descripcion,fecha_requisicion,cantidad,estado,usuario")] Requisas requisas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requisas);
        }

        // GET: Requisas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisas requisas = db.Requisas.Find(id);
            if (requisas == null)
            {
                return HttpNotFound();
            }
            return View(requisas);
        }

        // POST: Requisas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Requisas requisas = db.Requisas.Find(id);
            db.Requisas.Remove(requisas);
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
