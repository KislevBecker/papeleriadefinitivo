using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramaDeRequisas.Models
{
    public class ClsUsuariosSolicitantes
    {
        public int ID { get; set; }
        public string nombreSolicitante { get; set; }
        public string pass { get; set; }
        public int ID_Estado { get; set; }
        public string Estado { get; set; }
        public int Id_Tipo { get; set; }
        public string TipoSolicitante { get; set; }
        public int Id_Sociedades { get; set; }
        public string NombreSociedad { get; set; }
        public int ID_Centro { get; set; }
        public string NombreCentro { get; set; }
        public int ID_Almacen { get; set; }
        public int ID_CECO { get; set; }
        public string Nombre_CECO { get; set; }
        public int ID_Rol { get; set; }
        public string Rol { get; set; }
    }
}