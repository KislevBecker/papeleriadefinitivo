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
    
    public partial class TBL_Material
    {
        public int ID { get; set; }
        public string GrupoArticulos { get; set; }
        public int COD_Material { get; set; }
        public string Descripcion { get; set; }
        public Nullable<System.DateTime> Fecha_Cad { get; set; }
        public Nullable<int> Cantidad_Disponible { get; set; }
        public string Precio { get; set; }
    
        public virtual TBL_Material_X_Almacen TBL_Material_X_Almacen { get; set; }
    }
}