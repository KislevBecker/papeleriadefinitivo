using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;

namespace ProgramaDeRequisas.Controllers
{
    public class SAPConnectorInterface
    {
        private RfcDestination rfcDestination;

        public bool TestConnection(string destinationName)
        {
            bool result = false;
            try
            {
                rfcDestination = RfcDestinationManager.GetDestination(destinationName);
                if (rfcDestination != null)
                {
                    rfcDestination.Ping();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception("Fallo de conexion: " + ex.Message);
            }
            return result;
        }
    }
}