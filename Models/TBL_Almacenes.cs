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
    
    public partial class TBL_Almacenes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Almacenes()
        {
            this.TBL_Material_X_Almacen = new HashSet<TBL_Material_X_Almacen>();
        }
    
        public int ID { get; set; }
        public int Id_Centros { get; set; }
        public int Id_Almacenes { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Material_X_Almacen> TBL_Material_X_Almacen { get; set; }
        public virtual TBL_Centros TBL_Centros { get; set; }
    }
}
