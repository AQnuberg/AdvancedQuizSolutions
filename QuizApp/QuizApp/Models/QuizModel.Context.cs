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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
        public virtual DbSet<UserClaim> UserClaim { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<UserRegistrationToken> UserRegistrationToken { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<VraagInQuiz> VraagInQuiz { get; set; }
    
        public virtual int procVraagActief(Nullable<int> rondeID, Nullable<int> vraagID)
        {
            var rondeIDParameter = rondeID.HasValue ?
                new ObjectParameter("rondeID", rondeID) :
                new ObjectParameter("rondeID", typeof(int));
    
            var vraagIDParameter = vraagID.HasValue ?
                new ObjectParameter("vraagID", vraagID) :
                new ObjectParameter("vraagID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("procVraagActief", rondeIDParameter, vraagIDParameter);
        }
    
        public virtual int SP_Team_PuntenTotaal(Nullable<int> teamID)
        {
            var teamIDParameter = teamID.HasValue ?
                new ObjectParameter("teamID", teamID) :
                new ObjectParameter("teamID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_Team_PuntenTotaal", teamIDParameter);
        }
    
        public virtual int SP_Team_Toevoegen(Nullable<int> evenementID, string teamnaam, Nullable<int> accountID)
        {
            var evenementIDParameter = evenementID.HasValue ?
                new ObjectParameter("evenementID", evenementID) :
                new ObjectParameter("evenementID", typeof(int));
    
            var teamnaamParameter = teamnaam != null ?
                new ObjectParameter("teamnaam", teamnaam) :
                new ObjectParameter("teamnaam", typeof(string));
    
            var accountIDParameter = accountID.HasValue ?
                new ObjectParameter("accountID", accountID) :
                new ObjectParameter("accountID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_Team_Toevoegen", evenementIDParameter, teamnaamParameter, accountIDParameter);
        }
    
        public virtual int SP_VraagInQuiz_isActief(Nullable<int> rondeID, Nullable<int> vraagID)
        {
            var rondeIDParameter = rondeID.HasValue ?
                new ObjectParameter("rondeID", rondeID) :
                new ObjectParameter("rondeID", typeof(int));
    
            var vraagIDParameter = vraagID.HasValue ?
                new ObjectParameter("vraagID", vraagID) :
                new ObjectParameter("vraagID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_VraagInQuiz_isActief", rondeIDParameter, vraagIDParameter);
        }
    
        public virtual int SP_VraaginQuiz_Toevoegen(Nullable<int> vraagID, Nullable<int> rondeID)
        {
            var vraagIDParameter = vraagID.HasValue ?
                new ObjectParameter("vraagID", vraagID) :
                new ObjectParameter("vraagID", typeof(int));
    
            var rondeIDParameter = rondeID.HasValue ?
                new ObjectParameter("rondeID", rondeID) :
                new ObjectParameter("rondeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_VraaginQuiz_Toevoegen", vraagIDParameter, rondeIDParameter);
        }
    }
}
