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
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class avisos
    {


        [Display(Name = "ID")]
        public int id_aviso { get; set; }

        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MMM-dd}", ApplyFormatInEditMode = true)]
        public DateTime  fecha_ini { get; set; }

        [Display(Name = "Fecha Fin")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MMM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> fecha_fin { get; set; }


        [Display(Name = "Mensaje"), Required]
        public string msg { get; set; }

        [Display(Name = "Tipo Aviso")]
        public string tipo_aviso { get; set; }

        [Display(Name = "Fecha Modificaci�n")]
        public Nullable<System.DateTime> fecha_mod { get; set; }

        [Display(Name = "Usuario Modificaci�n")]
        public string usuario_mod { get; set; }

       
        public Nullable<bool> estado { get; set; }
    }
}