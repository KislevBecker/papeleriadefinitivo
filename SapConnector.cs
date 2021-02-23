﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using SAP.Middleware.Connector;

//namespace ProgramaDeRequisas
//{
//    public class SapConnector
//    {
//    }
//}


namespace ProgramaDeRequisas
{
    public class SAPDestinationConfig : IDestinationConfiguration
    {
        public bool ChangeEventsSupported()
        {

            return false;

        }
        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;
        public RfcConfigParameters GetParameters(string destinationName)
        {
            RfcConfigParameters parms = new RfcConfigParameters();
            parms.Add(RfcConfigParameters.Name, "QA");

            parms.Add(RfcConfigParameters.AppServerHost, ConfigurationManager.AppSettings["SAP_APPSERVERHOST"]);

            parms.Add(RfcConfigParameters.SystemNumber, ConfigurationManager.AppSettings["SAP_SYSTEMNUM"]);

            parms.Add(RfcConfigParameters.SystemID, ConfigurationManager.AppSettings["SAP_CLIENT"]);

            parms.Add(RfcConfigParameters.User, ConfigurationManager.AppSettings["SAP_USERNAME"]);

            parms.Add(RfcConfigParameters.Password, ConfigurationManager.AppSettings["SAP_PASSWORD"]);

            parms.Add(RfcConfigParameters.Client, ConfigurationManager.AppSettings["SAP_CLIENT"]);

            parms.Add(RfcConfigParameters.Language, ConfigurationManager.AppSettings["SAP_LANGUAGE"]);

            parms.Add(RfcConfigParameters.Poolsize, ConfigurationManager.AppSettings["SAP_POOLSIZE"]);

            return parms;
        }
    }
}
