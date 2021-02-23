using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using SweetAlert;
using System.Web.Mvc;
using ProgramaDeRequisas.Models;

namespace ProgramaDeRequisas.Controllers
{
    public class TBL_Materiales_X_SociedadController : Controller
    {
        private SITEPLUSEntities2 db = new SITEPLUSEntities2();

        // GET: TBL_Materiales_X_Sociedad
        public ActionResult Index()
        {
            var tBL_Materiales_X_Sociedad = db.TBL_Materiales_X_Sociedad.Include(t => t.TBL_Almacenes).Include(t => t.TBL_Centros);
            return View(tBL_Materiales_X_Sociedad.ToList());
        }

        public ActionResult AddToCart(int id)
        {
            if(Session["carrito"]== null){
                List<ClsShoppingCart> compras = new List<ClsShoppingCart>();
                compras.Add(new ClsShoppingCart(db.TBL_Materiales_X_Sociedad.Find(id),1));
                Session["carrito"] = compras;
            }
            else {
                List<ClsShoppingCart> compras = (List<ClsShoppingCart>)Session["carrito"];

                int IndexExiste = getIndex(id);
                if (IndexExiste == -1)
                {
                    compras.Add(new ClsShoppingCart(db.TBL_Materiales_X_Sociedad.Find(id), 1));
                }
                else
                {
                    compras[IndexExiste].Cantidad++;
                }

                Session["carrito"] = compras;
            }
            return View();
        }

        public ActionResult FinalizarCompra()
        {
            List<ClsShoppingCart> compras = (List<ClsShoppingCart>)Session["carrito"];
            if (compras.Count>0)
            {
                return View("FinalizarCompra");
            }
            else
                return View("CompraError");
        }




        private int getIndex(int id)
        {
            List<ClsShoppingCart> compras = (List<ClsShoppingCart>)Session["carrito"];
            for (int i =0; i<compras.Count; i++)
            {
                if (compras[i].tBL_Materiales_X_Sociedad.ID == id)
                    return i;
            }
            return -1;
        }


        public ActionResult Del(int id)
        {
            List<ClsShoppingCart> compras = (List<ClsShoppingCart>)Session["carrito"];
            compras.RemoveAt(getIndex(id));
            return View("AddToCart");
        }


        // GET: TBL_Materiales_X_Sociedad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_Materiales_X_Sociedad tBL_Materiales_X_Sociedad = db.TBL_Materiales_X_Sociedad.Find(id);
            if (tBL_Materiales_X_Sociedad == null)
            {
                return HttpNotFound();
            }
            return View(tBL_Materiales_X_Sociedad);
        }

        // GET: TBL_Materiales_X_Sociedad/Create
        public ActionResult Create()
        {
            ViewBag.ID_Almacen = new SelectList(db.TBL_Almacenes, "Id_Almacenes", "Id_Almacenes");
            ViewBag.ID_Centro = new SelectList(db.TBL_Centros, "Id_Centros", "NombreCentro");
            return View();
        }

        // POST: TBL_Materiales_X_Sociedad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Grupo_Articulos,Codigo_material,Descripcion,ID_Centro,ID_Almacen,Lote,Lote_Proveedor,FechaCaducidad,Libre_utilizacion,Control_Calidad,Trans_Tras,Valor_Libre_Util,Precio,Pendiente")] TBL_Materiales_X_Sociedad tBL_Materiales_X_Sociedad)
        {
            if (ModelState.IsValid)
            {
                db.TBL_Materiales_X_Sociedad.Add(tBL_Materiales_X_Sociedad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Almacen = new SelectList(db.TBL_Almacenes, "Id_Almacenes", "Id_Almacenes", tBL_Materiales_X_Sociedad.ID_Almacen);
            ViewBag.ID_Centro = new SelectList(db.TBL_Centros, "Id_Centros", "NombreCentro", tBL_Materiales_X_Sociedad.ID_Centro);
            return View(tBL_Materiales_X_Sociedad);
        }

        // GET: TBL_Materiales_X_Sociedad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_Materiales_X_Sociedad tBL_Materiales_X_Sociedad = db.TBL_Materiales_X_Sociedad.Find(id);
            if (tBL_Materiales_X_Sociedad == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Almacen = new SelectList(db.TBL_Almacenes, "Id_Almacenes", "Id_Almacenes", tBL_Materiales_X_Sociedad.ID_Almacen);
            ViewBag.ID_Centro = new SelectList(db.TBL_Centros, "Id_Centros", "NombreCentro", tBL_Materiales_X_Sociedad.ID_Centro);
            return View(tBL_Materiales_X_Sociedad);
        }

        // POST: TBL_Materiales_X_Sociedad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Grupo_Articulos,Codigo_material,Descripcion,ID_Centro,ID_Almacen,Lote,Lote_Proveedor,FechaCaducidad,Libre_utilizacion,Control_Calidad,Trans_Tras,Valor_Libre_Util,Precio,Pendiente")] TBL_Materiales_X_Sociedad tBL_Materiales_X_Sociedad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_Materiales_X_Sociedad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Almacen = new SelectList(db.TBL_Almacenes, "Id_Almacenes", "Id_Almacenes", tBL_Materiales_X_Sociedad.ID_Almacen);
            ViewBag.ID_Centro = new SelectList(db.TBL_Centros, "Id_Centros", "NombreCentro", tBL_Materiales_X_Sociedad.ID_Centro);
            return View(tBL_Materiales_X_Sociedad);
        }

        // GET: TBL_Materiales_X_Sociedad/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TBL_Materiales_X_Sociedad tBL_Materiales_X_Sociedad = db.TBL_Materiales_X_Sociedad.Find(id);
        //    if (tBL_Materiales_X_Sociedad == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tBL_Materiales_X_Sociedad);
        //}

        // POST: TBL_Materiales_X_Sociedad/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    TBL_Materiales_X_Sociedad tBL_Materiales_X_Sociedad = db.TBL_Materiales_X_Sociedad.Find(id);
        //    db.TBL_Materiales_X_Sociedad.Remove(tBL_Materiales_X_Sociedad);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
