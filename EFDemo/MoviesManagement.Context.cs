﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFDemo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MoviesManagementEntities : DbContext
    {
        public MoviesManagementEntities()
            : base("name=MoviesManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ACTOR> ACTORs { get; set; }
        public virtual DbSet<DIRECTOR> DIRECTORS { get; set; }
        public virtual DbSet<GENRE> GENREs { get; set; }
        public virtual DbSet<MOVIE> MOVIEs { get; set; }
        public virtual DbSet<CAST> CASTS { get; set; }
    }
}
