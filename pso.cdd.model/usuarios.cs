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
    using System.ComponentModel.DataAnnotations;

    public partial class usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuarios()
        {
            this.cab_ts = new HashSet<cab_ts>();
            this.perfil_usuario = new HashSet<perfil_usuario>();
        }
    
        public int id_usu { get; set; }
        [Display(Name = "Nombre Usuario")]
        public string Nom_usu { get; set; }
        [Display(Name = "Usuario")]
        public string Nom_cor_usu { get; set; }
        public string pass_usu { get; set; }
        public Nullable<int> jefatura { get; set; }
        public int id_cargo { get; set; }
        public int id_unidad { get; set; }
        [Display(Name = "Fecha Modificaci�n")]
        public Nullable<System.DateTime> fecha_mod { get; set; }
        [Display(Name = "Usuario Modificador")]
        public string usuario_mod { get; set; }
        public Nullable<bool> estado { get; set; }
        public string jerarquia { get; set; }
    
        public virtual cargos cargos { get; set; }
        public virtual unidades unidades { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cab_ts> cab_ts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<perfil_usuario> perfil_usuario { get; set; }
    }
}