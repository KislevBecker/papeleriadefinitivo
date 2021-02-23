using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramaDeRequisas.Models
{
    public class ClsMaterialesAgregadosIndex
    {
        public string MATNR { get; set; }
        public string MAKTX { get; set; }
        public int Cantidad_Solicitada { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Solicitud { get; set; }
        public string usuario_solicitante { get; set; }
    }
}