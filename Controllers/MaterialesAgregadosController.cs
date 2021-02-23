using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgramaDeRequisas.Models;

namespace ProgramaDeRequisas.Controllers
{
    public class MaterialesAgregadosController : Controller
    {

        private SITEPLUSEntities2 db = new SITEPLUSEntities2();
        
        public ActionResult Edit(string MATNR, int? Cantidad, string MAKTX)
        {
            try
            {
                //var pMATNR = new SqlParameter
                //{
                //    ParameterName = "matnr",
                //    SqlDbType = System.Data.SqlDbType.VarChar,
                //    Value = MATNR,
                //};

                //var pMAKTX = new SqlParameter
                //{
                //    ParameterName = "maktx",
                //    SqlDbType = System.Data.SqlDbType.VarChar,
                //    Value = MAKTX,
                //};

                //var pCantidad = new SqlParameter
                //{
                //    ParameterName = "cantidad",
                //    SqlDbType = System.Data.SqlDbType.Int,
                //    Value = Cantidad,
                //};

                string user = Session["usuario"] as string;
                var pUsuario = new SqlParameter
                {
                    ParameterName = "usuario",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = user,
                };

                //var parameterOverallCount = new SqlParameter
                //{
                //    ParameterName = "salida",
                //    SqlDbType = System.Data.SqlDbType.VarChar,
                //    Size = 100,
                //    Direction = System.Data.ParameterDirection.Output,
                //    //Value="",
                //};

                var lst = (from t in db.TBL_Materiales_Agregados
                           where t.MATNR == MATNR
                           select t
                           ).ToList();
                if(lst.Count > 0)
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
            string codigoSociedad = Session["SociedadesID"] as string;
            Session["mensaje_ma"] = ViewBag.Message;
            return RedirectToAction("filtrarLista", "VSP_Sociedades_Centros_Almacenes", new { SociedadesID = codigoSociedad });
        }


    }
}