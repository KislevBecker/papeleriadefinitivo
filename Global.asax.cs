using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SAP.Middleware.Connector;

namespace ProgramaDeRequisas
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //APERTURA CONECCION 
            string destinationconfigname = "QA";
            Application["destinationconfigname"] = destinationconfigname;
            IDestinationConfiguration destinationConfiguration = null;
            bool destinationisInialised = false;
            if (!destinationisInialised)
            {
                destinationConfiguration = new ECCDestinationConfig();
                destinationConfiguration.GetParameters(destinationconfigname);

                if (RfcDestinationManager.TryGetDestination(destinationconfigname) == null)
                {
                    RfcDestinationManager.RegisterDestinationConfiguration(destinationConfiguration);
                    destinationisInialised = true;
                    bool resultado = false;
                    resultado = testconnection(destinationconfigname);
                    Application["resultado"] = Convert.ToString(resultado);
                }
            }
            //FIN APERTURA CONECCION
        }

        private RfcDestination RfcDestination;

        public bool testconnection(string destinationname)
        {
            bool result = false;
            try
            {
                RfcDestination = RfcDestinationManager.GetDestination(destinationname);
                if (RfcDestination != null)
                {
                    RfcDestination.Ping();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception("Error de conexión " + ex.Message);
            }
            return result;

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

        public DataSet RetrieveSociety(string sociedad, string destinationName)
        {
            DataSet dsSociedad = new DataSet();
            try
            {
                if (RfcDestination == null)
                {
                    RfcDestination = RfcDestinationManager.GetDestination(destinationName);
                }
                RfcRepository rfcRepository = RfcDestination.Repository;
                IRfcFunction rfcFunction = rfcRepository.CreateFunction("ZRFC_SOCIEDAD_CENTRO_ALMACEN");
                rfcFunction.SetValue("BUKRS", sociedad);
        
                rfcFunction.Invoke(RfcDestination);
                IRfcStructure sociedadesData = rfcFunction.GetStructure("BUTXT");
                RfcDestination.Repository.GetTableMetadata("ZRFC_SOCIEDAD_CENTRO_ALMACEN").CreateTable();
                IRfcTable SociedadesSummary = sociedadesData.GetTable("ET_SOCIEDADES");
                dsSociedad.Tables.Add(ConvertToDotNetTable(SociedadesSummary));
                ShowFunction(rfcFunction);
            }
            catch (Exception ex)
            {
                throw new Exception("RetrieveSociety Error: " + ex.Message);
            }
            return dsSociedad;
        }

        private void ShowFunction(IRfcFunction rfcFunction)
        {
            throw new NotImplementedException();
        }


        public DataSet ET_Sociedad (/*string country,*/string destinationname)
        {
            DataSet socidedades = new DataSet();
            try
            {
                if (RfcDestination == null)
                {
                    RfcDestination = RfcDestinationManager.GetDestination(destinationname);
                }
                RfcRepository rfcRepository = RfcDestination.Repository;
                IRfcFunction rfcfunction = rfcRepository.CreateFunction("ZRFC_SOCIEDAD_CENTRO_ALMACEN");
                rfcfunction.Invoke(RfcDestination);
                socidedades.Tables.Add(ConvertToDotNetTable(rfcfunction.GetTable("ET_SOCIEDADES")));
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR " + ex.Message);
            }
            return socidedades;
        }

        public DataSet EP_MATERIALES (string destinationname,string codigo_material)
        {
            DataSet material = new DataSet();
            try
            {
                if (RfcDestination == null)
                {
                    RfcDestination = RfcDestinationManager.GetDestination(destinationname);
                }
                




                //CODIGO MERO BUENO 
                RfcRepository rfcRepository = RfcDestination.Repository;
                IRfcFunction rfcfunction = rfcRepository.CreateFunction("ZRFC_MATERIAL");
                rfcfunction.SetValue("I_MATERIAL", codigo_material);
                rfcfunction.Invoke(RfcDestination);
                material.Tables.Add(ConvertToDotNetTable(rfcfunction.GetTable("EP_MATERIALES")));
                string descripcion = rfcfunction.GetString("EP_DESCRIPCION");
                string EP_ERROR = rfcfunction.GetString("EP_ERROR");
                string EP_MENSAJE = rfcfunction.GetString("EP_MENSAJE");
                //FIN CODIGO MERO BUENO 
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR " + ex.Message);
            }
            return material;
        }
    }
}
