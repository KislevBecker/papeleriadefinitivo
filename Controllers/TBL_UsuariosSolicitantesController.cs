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
    public class TBL_UsuariosSolicitantesController : Controller
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



        // GET: TBL_UsuariosSolicitantes
        [Authorize]
        public ActionResult Index()
        {
            if (Permisos() == true)
            {
                List<ClsUsuariosSolicitantes> lst = (from t in db.VSP_usuariosPapeleria
                                             orderby t.ID
                                             select new ClsUsuariosSolicitantes
                                             {
                                                 ID = t.ID,
                                                 nombreSolicitante = t.nombreSolicitante,
                                                 pass = t.pass,
                                                 NombreSociedad = t.NombreSociedad,
                                                 NombreCentro = t.NombreCentro,
                                                 ID_Almacen = t.Id_Almacenes,
                                                 Nombre_CECO=t.Nombre_CECO,
                                                 Estado = t.Estado,
                                                 Rol = t.Rol,
                                                 TipoSolicitante = t.TipoSolicitante
                                             }).ToList();
                return View(lst);

            }
            else
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }
        }


        // GET: TBL_UsuariosSolicitantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_UsuariosSolicitantes tBL_UsuariosSolicitantes = db.TBL_UsuariosSolicitantes.Find(id);
            if (tBL_UsuariosSolicitantes == null)
            {
                return HttpNotFound();
            }
            return View(tBL_UsuariosSolicitantes);
        }

        // GET: TBL_UsuariosSolicitantes/Create
        public ActionResult Create()
        {
            List<ClsUsuariosSolicitantes> lst = new List<ClsUsuariosSolicitantes>();
            ViewBag.Id_Sociedades = new SelectList(db.TBL_Sociedades, "Id_Sociedades", "NombreSociedad");
            ViewBag.Id_Tipo = new SelectList(db.TBL_TipoSolicitante, "Id_Tipo", "TipoSolicitante");
            ViewBag.ID_Centro = new SelectList(db.TBL_Centros, "Id_Centros", "NombreCentro");

            //var lst_soc = lst.Select(a => new SelectListItem
            //{
            //    Value = a.ID_Centro.ToString(),
            //    Text = a.NombreCentro
            //}).ToList();
            //ViewBag.ID_Centro = lst_soc;
            //int sociedad_id;
            //ViewBag.ID_Centro = new SelectList(db.TBL_Centros.SqlQuery("select distinct cn.* from TBL_Centros CN INNER JOIN TBL_UsuariosSolicitantes L ON cn.Id_Centros = l.ID_Centro INNER JOIN TBL_UsuariosSolicitantes M ON cn.Id_Sociedades= m.Id_Sociedades where m.Id_Sociedades = {0}", sociedad_id).ToList());

            ViewBag.ID_Almacen = new SelectList(db.TBL_Almacenes, "Id_Almacenes", "Id_Almacenes");
            ViewBag.ID_Rol = new SelectList(db.TBL_Roles_Uuarios, "ID_Rol", "Rol");
            ViewBag.ID_Estado = new SelectList(db.TBL_Estado_Uuarios, "ID_Estado", "Estado");
            ViewBag.ID_CECO = new SelectList(db.TBL_CECO, "ID_CECO", "Nombre_CECO");
            return View();
        }

        // POST: TBL_UsuariosSolicitantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,nombreSolicitante,pass,ID_Estado,Id_Tipo,Id_Sociedades,ID_Centro,ID_Almacen,ID_CECO,ID_Rol")] TBL_UsuariosSolicitantes tBL_UsuariosSolicitantes)
        {
            if (ModelState.IsValid)
            {
                db.TBL_UsuariosSolicitantes.Add(tBL_UsuariosSolicitantes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Sociedades = new SelectList(db.TBL_Sociedades, "Id_Sociedades", "NombreSociedad", tBL_UsuariosSolicitantes.Id_Sociedades);
            ViewBag.Id_Tipo = new SelectList(db.TBL_TipoSolicitante, "Id_Tipo", "TipoSolicitante", tBL_UsuariosSolicitantes.Id_Tipo);
            return View(tBL_UsuariosSolicitantes);
        }

        // GET: TBL_UsuariosSolicitantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_UsuariosSolicitantes tBL_UsuariosSolicitantes = db.TBL_UsuariosSolicitantes.Find(id);
            if (tBL_UsuariosSolicitantes == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Sociedades = new SelectList(db.TBL_Sociedades, "Id_Sociedades", "NombreSociedad", tBL_UsuariosSolicitantes.Id_Sociedades);
            ViewBag.Id_Tipo = new SelectList(db.TBL_TipoSolicitante, "Id_Tipo", "TipoSolicitante", tBL_UsuariosSolicitantes.Id_Tipo);
            ViewBag.ID_Centro = new SelectList(db.TBL_Centros, "Id_Centros", "NombreCentro", tBL_UsuariosSolicitantes.ID_Centro);
            ViewBag.ID_Almacen = new SelectList(db.TBL_Almacenes, "Id_Almacenes", "Id_Almacenes", tBL_UsuariosSolicitantes.ID_Almacen);
            ViewBag.ID_Rol = new SelectList(db.TBL_Roles_Uuarios, "ID_Rol", "Rol", tBL_UsuariosSolicitantes.ID_Rol);
            ViewBag.ID_Estado = new SelectList(db.TBL_Estado_Uuarios, "ID_Estado", "Estado", tBL_UsuariosSolicitantes.ID_Estado);
            ViewBag.ID_CECO = new SelectList(db.TBL_CECO, "ID_CECO", "Nombre_CECO", tBL_UsuariosSolicitantes.ID_CECO);
            return View(tBL_UsuariosSolicitantes);
        }

        // POST: TBL_UsuariosSolicitantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,nombreSolicitante,pass,ID_Estado,Id_Tipo,Id_Sociedades,ID_Centro,ID_Almacen,ID_CECO,ID_Rol")] TBL_UsuariosSolicitantes tBL_UsuariosSolicitantes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_UsuariosSolicitantes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Sociedades = new SelectList(db.TBL_Sociedades, "Id_Sociedades", "NombreSociedad", tBL_UsuariosSolicitantes.Id_Sociedades);
            ViewBag.Id_Tipo = new SelectList(db.TBL_TipoSolicitante, "Id_Tipo", "TipoSolicitante", tBL_UsuariosSolicitantes.Id_Tipo);
            return View(tBL_UsuariosSolicitantes);
        }

        // GET: TBL_UsuariosSolicitantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_UsuariosSolicitantes tBL_UsuariosSolicitantes = db.TBL_UsuariosSolicitantes.Find(id);
            if (tBL_UsuariosSolicitantes == null)
            {
                return HttpNotFound();
            }
            return View(tBL_UsuariosSolicitantes);
        }

        // POST: TBL_UsuariosSolicitantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_UsuariosSolicitantes tBL_UsuariosSolicitantes = db.TBL_UsuariosSolicitantes.Find(id);
            db.TBL_UsuariosSolicitantes.Remove(tBL_UsuariosSolicitantes);
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

        public JsonResult GetSociedades()
        {
            List<SelectListItem> Lst = (from s in db.TBL_Sociedades
                                        select new SelectListItem
                                        { Value = s.Id_Sociedades.ToString(), Text = s.NombreSociedad }).ToList();
            return Json(Lst, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCentros(int sociedad_id)
        {
            List<SelectListItem> Lst = (from c in db.TBL_Centros
                                        where c.Id_Sociedades == sociedad_id
                                        select new SelectListItem
                                        { Value = c.Id_Centros.ToString(), Text = c.NombreCentro }).ToList();
            return Json(Lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAlmacenes(int centro_id)
        {
            List<SelectListItem> Lst = (from a in db.TBL_Almacenes
                                        where a.Id_Centros == centro_id
                                        select new SelectListItem
                                        { Value = a.Id_Almacenes.ToString(), Text = a.Id_Almacenes.ToString() }).ToList();
            return Json(Lst, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCeco(int sociedad_id)
        {
            List<SelectListItem> Lst = (from cc in db.TBL_CECO
                                            //orderby cc.Nombre_CECO
                                        where cc.ID_Sociedades == sociedad_id
                                        select new SelectListItem
                                        { Value = cc.ID_CECO.ToString(), Text = cc.Nombre_CECO }).ToList();
            return Json(Lst, JsonRequestBehavior.AllowGet);
        }

    }
}
