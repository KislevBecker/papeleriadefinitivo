﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProgramaDeRequisas.Models;

namespace ProgramaDeRequisas.Controllers
{
    public class RevisionRequisasController : Controller
    {
        private SITEPLUSEntities2 db = new SITEPLUSEntities2();


        public bool Permisos()
        {
            string user = Session["Usuario"] as string;

            var Lst = db.TBL_UsuariosSolicitantes.SqlQuery("select distinct * from TBL_UsuariosSolicitantes where nombreSolicitante = {0}", user).ToList();

            int? rol_id = Lst[0].ID_Rol;

            var Lst2 = db.TBL_Roles_Uuarios.SqlQuery("select * from TBL_Roles_Uuarios where ID_Rol = {0}", rol_id).ToList();

            if (Lst2[0].Rol == "Administrador")
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        // GET: TBL_Material
        public ActionResult Index()
        {
            if (Permisos() == true)
            {
                var tBL_Material = db.TBL_Material.Include(t => t.TBL_Material_X_Almacen);
                return View(tBL_Material.ToList());
            }
            else
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }
        }

        // GET: TBL_Material/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_Material tBL_Material = db.TBL_Material.Find(id);
            if (tBL_Material == null)
            {
                return HttpNotFound();
            }
            return View(tBL_Material);
        }

        // GET: TBL_Material/Create
        public ActionResult Create()
        {

            ViewBag.ID = new SelectList(db.TBL_Material_X_Almacen, "ID", "Lote");
            return View();
        }

        // POST: TBL_Material/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GrupoArticulos,COD_Material,Descripcion,Fecha_Cad,Cantidad_Disponible,Precio")] TBL_Material tBL_Material)
        {
            if (ModelState.IsValid)
            {
                db.TBL_Material.Add(tBL_Material);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.TBL_Material_X_Almacen, "ID", "Lote", tBL_Material.ID);
            return View(tBL_Material);
        }

        // GET: TBL_Material/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_Material tBL_Material = db.TBL_Material.Find(id);
            if (tBL_Material == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.TBL_Material_X_Almacen, "ID", "Lote", tBL_Material.ID);
            return View(tBL_Material);
        }

        // POST: TBL_Material/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GrupoArticulos,COD_Material,Descripcion,Fecha_Cad,Cantidad_Disponible,Precio")] TBL_Material tBL_Material)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_Material).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.TBL_Material_X_Almacen, "ID", "Lote", tBL_Material.ID);
            return View(tBL_Material);
        }

        // GET: TBL_Material/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_Material tBL_Material = db.TBL_Material.Find(id);
            if (tBL_Material == null)
            {
                return HttpNotFound();
            }
            return View(tBL_Material);
        }

        // POST: TBL_Material/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_Material tBL_Material = db.TBL_Material.Find(id);
            db.TBL_Material.Remove(tBL_Material);
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