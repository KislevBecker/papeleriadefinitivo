//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProgramaDeRequisas.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_Material_X_Almacen
    {
        public int ID { get; set; }
        public Nullable<int> Id_Centros { get; set; }
        public Nullable<int> Id_Almacenes { get; set; }
        public Nullable<int> COD_Material { get; set; }
        public string Lote { get; set; }
        public string Lote_Prov { get; set; }
    
        public virtual TBL_Almacenes TBL_Almacenes { get; set; }
        public virtual TBL_Centros TBL_Centros { get; set; }
        public virtual TBL_Material TBL_Material { get; set; }
    }
}