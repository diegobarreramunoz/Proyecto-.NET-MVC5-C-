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
    
    public partial class perfil_usuario
    {
        public int id { get; set; }
        public int id_usuario { get; set; }
        public string perfil { get; set; }
    
        public virtual usuarios usuarios { get; set; }
        public virtual parametros parametros { get; set; }
    }
}
