﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAP.Middleware.Connector;  
  

namespace ProgramaDeRequisas { 
public class ECCDestinationConfig : IDestinationConfiguration
{
   
    public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        public bool ChangeEventsSupported()
        {
            return false;
        }


        public RfcConfigParameters GetParameters(string destinationName)
        {
        RfcConfigParameters parms = new RfcConfigParameters();

        //SAPConnectorInfo Parameters;
            if (destinationName.Equals("QA"))
        {
                parms.Add(RfcConfigParameters.Name, "QA");
                parms.Add(RfcConfigParameters.AppServerHost, "172.10.0.6");
                parms.Add(RfcConfigParameters.SystemNumber, "00");
                parms.Add(RfcConfigParameters.SystemID, "QAS");
                parms.Add(RfcConfigParameters.User, "INTERFACESAP");
                parms.Add(RfcConfigParameters.Password, "1nt3rf4c3sF4r!n73r");
                //parms.Add(RfcConfigParameters.User, "REQUISICION");
                //parms.Add(RfcConfigParameters.Password, "GF4rint3r#2020");
                parms.Add(RfcConfigParameters.Client, "300");
                parms.Add(RfcConfigParameters.Language, "ES");
                parms.Add(RfcConfigParameters.PoolSize, "5");
                parms.Add(RfcConfigParameters.PeakConnectionsLimit, "10");
                parms.Add(RfcConfigParameters.ConnectionIdleTimeout, "600"); //IdleTimeout
            }

        return parms;
    }



        public void GetCompanies()
        {

            ECCDestinationConfig cfg = new ECCDestinationConfig();

            RfcDestinationManager.RegisterDestinationConfiguration(cfg);

            RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");

            RfcRepository repo = dest.Repository;

            IRfcFunction testfn = repo.CreateFunction("BAPI_COMPANYCODE_GETLIST");

            testfn.Invoke(dest);

            var companyCodeList = testfn.GetTable("COMPANYCODE_LIST");

            // companyCodeList now contains a table with companies and codes

        }

    }
}