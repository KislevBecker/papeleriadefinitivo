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
    
    public partial class TBL_Requisa
    {
        public int ID { get; set; }
        public int ID_Requisa { get; set; }
        public int ID_Carrito { get; set; }
        public string Estado_requisa { get; set; }
        public Nullable<System.DateTime> Fecha_Creacion { get; set; }
        public Nullable<System.DateTime> Fecha_Actualizacion { get; set; }
        public string Solicitante { get; set; }
        public string usuario_aprueba { get; set; }
        public Nullable<int> id_sociedad { get; set; }
        public Nullable<int> id_centro { get; set; }
        public Nullable<int> id_almacen { get; set; }
        public Nullable<int> id_ceco { get; set; }
        public string Descripcion { get; set; }
    
        public virtual TBL_Carrito TBL_Carrito { get; set; }
    }
}
