using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramaDeRequisas.Models
{
    public class Requisas
    {
        public int id { get; set; }
        public int Id_Sociedades { get; set; }
        public int Id_Centros { get; set; }
        public int Id_Almacenes { get; set; }
        public int Id_Material { get; set; }
    }
}