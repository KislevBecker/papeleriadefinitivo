using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramaDeRequisas.Models
{
    public class ClsRequisas
    {
        public int id { get; set; }
        public int Id_Sociedades { get; set; }
        public string NombreSociedad { get; set; }
        public int Id_Centros { get; set; }
        public string NombreCentro { get; set; }
        public int Id_Almacenes { get; set; }
        public int MATNR { get; set; } // código del material traido de sap
        public string MAKTX { get; set; } //Nombre del material traido desde sap

    }
}