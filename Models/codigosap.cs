using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProgramaDeRequisas.Models
{
    public class codigossap
    {
        [Key]
        public string MATNR { get; set; }
        public string MAKTX { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "* Solo se permiten números.")]
        [Range(1, 20000, ErrorMessage = "El Campo {0} debe ser un numero entre 1 y {2}")]
        public int Cantidad_Pedida { get; set; }
    }
}