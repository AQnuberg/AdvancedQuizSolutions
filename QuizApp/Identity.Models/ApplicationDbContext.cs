namespace codingfreaks.samples.Identity.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// A basic implementation for an application database context compatible with ASP.NET Identity 2 using
    /// <see cref="long"/> as the key-column-type for all entities.
    /// </summary>
    /// <remarks>
    /// This type depends on some other types out of this assembly.
    /// </remarks>
    public class ApplicationDbContext : IdentityDbContext<MyUser, MyRole, long, MyLogin, MyUserRole, MyClaim>
    {
        #region constructors and destructors

        public ApplicationDbContext()
            : base("IdentityConnection")
        {
        }

        #endregion

        #region methods

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Map Entities to their tables.
            modelBuilder.Entity<MyUser>().ToTable("Account");
            modelBuilder.Entity<MyRole>().ToTable("Rol");
            modelBuilder.Entity<MyClaim>().ToTable("UserClaim");
            modelBuilder.Entity<MyLogin>().ToTable("UserLogin");
            modelBuilder.Entity<MyUserRole>().ToTable("UserRole");
            // Set AutoIncrement-Properties
            modelBuilder.Entity<MyUser>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("AccountID");
            modelBuilder.Entity<MyClaim>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<MyRole>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("RolID");
            // Override some column mappings that do not match our default
            modelBuilder.Entity<MyUser>().Property(r => r.Email).HasColumnName("Email");
            modelBuilder.Entity<MyUser>().Property(r => r.UserName).HasColumnName("Login");
            modelBuilder.Entity<MyUser>().Property(r => r.PasswordHash).HasColumnName("Wachtwoord");
            modelBuilder.Entity<MyRole>().Property(r => r.Name).HasColumnName("Rolnaam");
        }

        #endregion
    }
}