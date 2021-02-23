using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProgramaDeRequisas.Models;
using SweetAlert;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using SAP.Middleware.Connector;

namespace ProgramaDeRequisas.Controllers
{
    public class VSP_Sociedades_Centros_AlmacenesController : Controller
    {
        private SITEPLUSEntities2 db = new SITEPLUSEntities2();

        // GET: VSP_Sociedades_Centros_Almacenes
        //[Authorize]
        public ActionResult Index()
        {
            string user = Session["Usuario"] as string;
            List<ClsMaterialesAgregadosIndex> lstIndex = (from t in db.TBL_Materiales_Agregados
                                                          where t.usuario_solicitante==user
                                                          orderby t.MATNR
                                                          select new ClsMaterialesAgregadosIndex
                                                          {
                                                              MATNR = t.MATNR,
                                                              MAKTX = t.MAKTX,
                                                              Cantidad_Solicitada = (int)t.Cantidad,
                                                              Fecha_Solicitud = (DateTime)t.Fecha_Solicitud,
                                                              usuario_solicitante = t.usuario_solicitante,
                                                          }).ToList();
            return View(lstIndex);
        }


        // GET: VSP_Sociedades_Centros_Almacenes/Details/5
        public ActionResult Details() /*string id*/
        {
            return View();
        }

        
        // GET: VSP_Sociedades_Centros_Almacenes/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: VSP_Sociedades_Centros_Almacenes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NombreSociedad,NombreCentro,Id_Almacenes")] VSP_Sociedades_Centros_Almacenes vSP_Sociedades_Centros_Almacenes)
        {
            if (ModelState.IsValid)
            {
                db.VSP_Sociedades_Centros_Almacenes.Add(vSP_Sociedades_Centros_Almacenes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vSP_Sociedades_Centros_Almacenes);
        }

        // GET: VSP_Sociedades_Centros_Almacenes/Edit/5
        //[Authorize]
        public ActionResult Edit() /*string id*/
        {
            return View();
        }

        // POST: VSP_Sociedades_Centros_Almacenes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NombreSociedad,NombreCentro,Id_Almacenes")] VSP_Sociedades_Centros_Almacenes vSP_Sociedades_Centros_Almacenes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vSP_Sociedades_Centros_Almacenes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vSP_Sociedades_Centros_Almacenes);
        }

        // GET: VSP_Sociedades_Centros_Almacenes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VSP_Sociedades_Centros_Almacenes vSP_Sociedades_Centros_Almacenes = db.VSP_Sociedades_Centros_Almacenes.Find(id);
            if (vSP_Sociedades_Centros_Almacenes == null)
            {
                return HttpNotFound();
            }
            return View(vSP_Sociedades_Centros_Almacenes);
        }

        // POST: VSP_Sociedades_Centros_Almacenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VSP_Sociedades_Centros_Almacenes vSP_Sociedades_Centros_Almacenes = db.VSP_Sociedades_Centros_Almacenes.Find(id);
            db.VSP_Sociedades_Centros_Almacenes.Remove(vSP_Sociedades_Centros_Almacenes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost,ActionName("filtrarLista")]
        public ActionResult filtrarLista(string SociedadesID)
        {
            List<codigossap> lst = new List<codigossap>();
            if (!string.IsNullOrEmpty(SociedadesID))
            {
                DataTable dt = PostArticulos(SociedadesID);
              
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new codigossap
                    {
                        MATNR = Convert.ToString(row["MATNR"]),
                        MAKTX = Convert.ToString(row["MAKTX"])
                    });
                }

                var lst_soc = lst.Select(a => new SelectListItem
                {
                    Value = a.MATNR,
                    Text = a.MAKTX
                }).ToList();
                //ViewBag.Materiales = lst_soc;
                //ViewBag.Materiales = lst_soc.AsEnumerable();

                //for (int i=0; i<=lst_soc.Count-1; i++)
                //{
                //    ViewBag.MATNR = lst_soc[i].Value;
                //    ViewBag.MAKTX = lst_soc[i].Text;
                //}

            }
            else
            {
                DataTable dt = PostArticulos(SociedadesID);
               
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new codigossap
                    {
                        MATNR = Convert.ToString(row["MATNR"]),
                        MAKTX = Convert.ToString(row["MAKTX"])
                    });
                }

                var lst_soc = lst.Select(a => new SelectListItem
                {
                    Value = a.MATNR,
                    Text = a.MAKTX
                }).ToList();
                //ViewBag.Materiales = lst_soc;
                //ViewBag.Materiales = lst_soc.AsEnumerable();
                //for (int i = 0; i <= lst_soc.Count - 1; i++)
                //{
                //    ViewBag.MATNR = lst_soc[i].Value;
                //    ViewBag.MAKTX = lst_soc[i].Text;
                //}
            }
            Session["SociedadesID"] = SociedadesID;
            string mensaje = Session["mensaje_ma"] as string;
            if (mensaje !="")
            {
                ViewBag.Message = mensaje;
                Session["mensaje_ma"] = "";
            }
            return View(lst.ToList());
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // ******************************************** Funciones ********************************************
        ////filtrando segun las sociedades a los que pertenece el usuario
        public JsonResult GetSociedades()
        {
            string user = Session["Usuario"] as string;
            var Lst1 = db.TBL_Sociedades.SqlQuery("select distinct s.* from TBL_Sociedades S INNER JOIN TBL_UsuariosSolicitantes L ON s.Id_Sociedades = l.Id_Sociedades where l.nombreSolicitante = {0}", user).ToList();

            var Lista1 = Lst1.Select(x => new SelectListItem
            {
                Value = x.Id_Sociedades.ToString(),
                Text = x.NombreSociedad
            }).ToList();
            return Json(Lista1, JsonRequestBehavior.AllowGet);
        }

        ////filtrando segun los centros a los que pertenece el usuario
        public JsonResult GetCentros(int sociedad_id)
        {
            string user = Session["Usuario"] as string;
            var Lst2 = db.TBL_Centros.SqlQuery("select distinct cn.* from TBL_Centros CN INNER JOIN TBL_UsuariosSolicitantes L ON cn.Id_Centros = l.ID_Centro INNER JOIN TBL_UsuariosSolicitantes M ON cn.Id_Sociedades= m.Id_Sociedades where l.nombreSolicitante = {0} and m.Id_Sociedades = {1}", user, sociedad_id).ToList();

            var Lista2 = Lst2.Select(x => new SelectListItem
            {
                Value = x.Id_Centros.ToString(),
                Text = x.NombreCentro
            }).ToList();
            return Json(Lista2, JsonRequestBehavior.AllowGet);
        }
        ////filtrando segun los almacenes a los que pertenece el usuario
        public JsonResult GetAlmacenes(int centro_id)
        {
            string user = Session["Usuario"] as string;
            var Lst3 = db.TBL_Almacenes.SqlQuery("select distinct a.* from TBL_Almacenes A INNER JOIN TBL_UsuariosSolicitantes L ON a.Id_Almacenes = l.ID_Almacen INNER JOIN TBL_UsuariosSolicitantes M ON a.Id_Centros= m.ID_Centro where l.nombreSolicitante = {0} and m.ID_Centro = {1}", user, centro_id).ToList();

            var Lista3 = Lst3.Select(x => new SelectListItem
            {
                Value = x.Id_Almacenes.ToString(),
                Text = x.Id_Almacenes.ToString(),
            }).ToList();
            return Json(Lista3, JsonRequestBehavior.AllowGet);
        }
        ////filtrando segun los cecos a los que pertenece el usuario
        public JsonResult GetCeco(int sociedad_id)
        {
            string user = Session["Usuario"] as string;
            var Lst4 = db.TBL_CECO.SqlQuery("select distinct cn.* from TBL_CECO CN INNER JOIN TBL_UsuariosSolicitantes L ON cn.ID_CECO = l.ID_CECO INNER JOIN TBL_UsuariosSolicitantes M ON cn.Id_Sociedades= m.Id_Sociedades where l.nombreSolicitante = {0} and m.Id_Sociedades = {1}", user, sociedad_id).ToList();

            var Lista4 = Lst4.Select(x => new SelectListItem
            {
                Value = x.ID_CECO.ToString(),
                Text = x.Nombre_CECO
            }).ToList();
            return Json(Lista4, JsonRequestBehavior.AllowGet);
        }

        ////Guardar los productos de sap en la tabla carrito
        [HttpGet]
        public ActionResult addToCart(string MATNR, string MAKTX, int Cantidad_Pedida, TBL_Materiales_Agregados tBL_Materiales_Agregados)
        {
            if (ModelState.IsValid)
            {
                db.TBL_Materiales_Agregados.Add(tBL_Materiales_Agregados);
                db.SaveChanges();
                var message = TempData["Guardado Exitosamente"];
                return RedirectToAction("filtrarLista");
            }

            return View(tBL_Materiales_Agregados);
        }


        // ******************************************** fin Funciones ********************************************




        //********************************************clases y funciones de sap*****************************************************
        //public class codigossap
        //{
        //    public string MATNR { get; set; }
        //    public string MAKTX { get; set; }
        //}

        public DataTable PostArticulos(string sociedad)
        {
            string resultado = "";
            string destinationconfigname = "";
            resultado = HttpContext.Application["resultado"] as string;
            destinationconfigname = HttpContext.Application["destinationconfigname"] as string;
           

            DataSet codigosap = new DataSet();
            codigosap = ET_Sociedad_Material(destinationconfigname, sociedad);

            DataTable firstTable = codigosap.Tables[0];

            return firstTable;

        }

        private RfcDestination RfcDestination;

        public DataSet ET_Sociedad_Material(string destinationname, string sociedad)
        {
            DataSet sociedad_material = new DataSet();
            try
            {
                if (RfcDestination == null)
                {
                    RfcDestination = RfcDestinationManager.GetDestination(destinationname);
                }

                //CODIGO QUE FUNCIONÓ
                RfcRepository rfcRepository = RfcDestination.Repository;
                IRfcFunction rfcfunction = rfcRepository.CreateFunction("ZRFC_MATERIAL_SOCIEDAD");
                rfcfunction.SetValue("I_SOCIEDAD", sociedad);
                rfcfunction.Invoke(RfcDestination);

                sociedad_material.Tables.Add(ConvertToDotNetTable(rfcfunction.GetTable("EP_MATERIALES")));

                string EP_ERROR = rfcfunction.GetString("EP_ERROR");
                string EP_MENSAJE = rfcfunction.GetString("EP_MENSAJE");
                //FIN CODIGO QUE FUNCIONÓ 
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR " + ex.Message);
            }
            return sociedad_material;
        }



        public DataTable ConvertToDotNetTable(IRfcTable RFCTable)
        {
            DataTable dtTable = new DataTable();

            //crear tabla
            for (int item = 0; item < RFCTable.ElementCount; item++)
            {
                RfcElementMetadata metadata = RFCTable.GetElementMetadata(item);
                dtTable.Columns.Add(metadata.Name);
            }
            foreach (IRfcStructure row in RFCTable)
            {
                DataRow dr = dtTable.NewRow();
                for (int item = 0; item < RFCTable.ElementCount; item++)
                {
                    RfcElementMetadata metadata = RFCTable.GetElementMetadata(item);
                    if (metadata.DataType == RfcDataType.BCD && metadata.Name == "ABC")
                    {
                        dr[item] = row.GetInt(metadata.Name);
                    }
                    else
                    {
                        dr[item] = row.GetString(metadata.Name);
                    }
                }
                dtTable.Rows.Add(dr);
            }
            return dtTable;
        }


    }
}
