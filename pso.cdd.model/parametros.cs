//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pso.cdd.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class parametros
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public parametros()
        {
            this.perfil_usuario = new HashSet<perfil_usuario>();
        }
    
        public int id { get; set; }
        public string grupo { get; set; }
        public string detalle { get; set; }
        public string valor { get; set; }
        public string texto { get; set; }
        public Nullable<int> orden { get; set; }
        public System.DateTime fecha_mod { get; set; }
        public string usuario_mod { get; set; }
        public bool estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<perfil_usuario> perfil_usuario { get; set; }
    }
}
