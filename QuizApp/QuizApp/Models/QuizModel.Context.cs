﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuizApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AQSDatabaseEntities : DbContext
    {
        public AQSDatabaseEntities()
            : base("name=AQSDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Evenement> Evenement { get; set; }
        public virtual DbSet<Locatie> Locatie { get; set; }
        public virtual DbSet<MeerkeuzeAntwoord> MeerkeuzeAntwoord { get; set; }
        public virtual DbSet<QuizRonde> QuizRonde { get; set; }
        public virtual DbSet<QuizVraag> QuizVraag { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamAntwoord> TeamAntwoord { get; set; }
        public virtual DbSet<Thema> Thema { get; set; }
        public virtual DbSet<VraagInQuiz> VraagInQuiz { get; set; }
    }
}
