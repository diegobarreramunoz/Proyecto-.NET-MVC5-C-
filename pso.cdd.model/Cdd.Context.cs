﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TSEntities : DbContext
    {
        public TSEntities()
            : base("name=TSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<avisos> avisos { get; set; }
        public virtual DbSet<cargos> cargos { get; set; }
        public virtual DbSet<categorias> categorias { get; set; }
        public virtual DbSet<empresas> empresas { get; set; }
        public virtual DbSet<periodos> periodos { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tipos_servicios> tipos_servicios { get; set; }
        public virtual DbSet<unidades> unidades { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }
        public virtual DbSet<cab_ts> cab_ts { get; set; }
        public virtual DbSet<valor_hh> valor_hh { get; set; }
        public virtual DbSet<perfil_usuario> perfil_usuario { get; set; }
        public virtual DbSet<emp_car_tsrv> emp_car_tsrv { get; set; }
        public virtual DbSet<parametros> parametros { get; set; }
    }
}
