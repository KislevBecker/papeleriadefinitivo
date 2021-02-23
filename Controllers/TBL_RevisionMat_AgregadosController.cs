using System;
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
    public class TBL_RevisionMat_AgregadosController : Controller
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



        // GET: TBL_RevisionMat_Agregados
        public ActionResult Index()
        {
            if (Permisos() == true)
            {
                return View(db.TBL_RevisionMat_Agregados.ToList());
            }
            else
            {
                return RedirectToAction("AccesoDenegado", "Home");
            }
        }

        // GET: TBL_RevisionMat_Agregados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_RevisionMat_Agregados tBL_RevisionMat_Agregados = db.TBL_RevisionMat_Agregados.Find(id);
            if (tBL_RevisionMat_Agregados == null)
            {
                return HttpNotFound();
            }
            return View(tBL_RevisionMat_Agregados);
        }

        // GET: TBL_RevisionMat_Agregados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TBL_RevisionMat_Agregados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Revision,MATNR,MAKTX,Sociedad,Centro,Ceco,Almacen,Cantidad_Solicitada,Cantidad_Aprobada,Fecha_Solicitud,Fecha_Cambio_Estado,usuario_Solicitante,Estado,Comentario")] TBL_RevisionMat_Agregados tBL_RevisionMat_Agregados)
        {
            if (ModelState.IsValid)
            {
                db.TBL_RevisionMat_Agregados.Add(tBL_RevisionMat_Agregados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tBL_RevisionMat_Agregados);
        }

        // GET: TBL_RevisionMat_Agregados/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TBL_RevisionMat_Agregados tBL_RevisionMat_Agregados = db.TBL_RevisionMat_Agregados.Find(id);
        //    if (tBL_RevisionMat_Agregados == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tBL_RevisionMat_Agregados);
        //}

        public ActionResult Edit(string MATNR, int? Cantidad, string MAKTX)
        {
            try
            {
                //string codigoSociedad = Session["SociedadesID"] as string;
                //var pSoc = new SqlParameter
                //{
                //    ParameterName = "soc",
                //    SqlDbType = System.Data.SqlDbType.VarChar,
                //    Value = soc,
                //};

                //string centro = Session["centro"] as string;
                //var pCentro = new SqlParameter
                //{
                //    ParameterName = "centro",
                //    SqlDbType = System.Data.SqlDbType.VarChar,
                //    Value = centro,
                //};

                //string almacen = Session["almacen"] as string;
                //var pAlmacen = new SqlParameter
                //{
                //    ParameterName = "almacen",
                //    SqlDbType = System.Data.SqlDbType.Int,
                //    Value = almacen,
                //};

                //string ceco = Session["ceco"] as string;
                //var pCeco = new SqlParameter
                //{
                //    ParameterName = "ceco",
                //    SqlDbType = System.Data.SqlDbType.Varchar,
                //    Value = ceco,
                //};

                string user = Session["usuario"] as string;
                var pUsuario = new SqlParameter
                {
                    ParameterName = "usuario",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = user,
                };

         
                var lst = (from t in db.TBL_Materiales_Agregados
                           where t.MATNR == MATNR
                           select t
                           ).ToList();
                if (lst.Count > 0)
                {
                    ViewBag.Message = "El material ya ha sido agregado...";
                }
                else
                {
                    db.sp_Insertar_TBL_Materiales_Agregados(MATNR, MAKTX, Cantidad, user).ToString();
                    ViewBag.Message = "Material guardado correctamente...";
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            //string codigoSociedad = Session["SociedadesID"] as string;
            Session["mensaje_me"] = ViewBag.Message;
            return RedirectToAction("Index", "TBL_RevisionMat_Agregados"/*, new { SociedadesID = codigoSociedad }*/);
        }



        // POST: TBL_RevisionMat_Agregados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Revision,MATNR,MAKTX,Sociedad,Centro,Ceco,Almacen,Cantidad_Solicitada,Cantidad_Aprobada,Fecha_Solicitud,Fecha_Cambio_Estado,usuario_Solicitante,Estado,Comentario")] TBL_RevisionMat_Agregados tBL_RevisionMat_Agregados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_RevisionMat_Agregados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBL_RevisionMat_Agregados);
        }

        // GET: TBL_RevisionMat_Agregados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_RevisionMat_Agregados tBL_RevisionMat_Agregados = db.TBL_RevisionMat_Agregados.Find(id);
            if (tBL_RevisionMat_Agregados == null)
            {
                return HttpNotFound();
            }
            return View(tBL_RevisionMat_Agregados);
        }

        // POST: TBL_RevisionMat_Agregados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_RevisionMat_Agregados tBL_RevisionMat_Agregados = db.TBL_RevisionMat_Agregados.Find(id);
            db.TBL_RevisionMat_Agregados.Remove(tBL_RevisionMat_Agregados);
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
