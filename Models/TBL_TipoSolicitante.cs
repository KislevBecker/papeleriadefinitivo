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
    
    public partial class TBL_TipoSolicitante
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_TipoSolicitante()
        {
            this.TBL_UsuariosSolicitantes = new HashSet<TBL_UsuariosSolicitantes>();
        }
    
        public int Id_Tipo { get; set; }
        public string TipoSolicitante { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_UsuariosSolicitantes> TBL_UsuariosSolicitantes { get; set; }
    }
}