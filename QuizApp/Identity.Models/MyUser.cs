namespace codingfreaks.samples.Identity.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class MyUser : IdentityUser<long, MyLogin, MyUserRole, MyClaim>
    {
        #region properties

        public string PasswordAnswer { get; set; }

        public string PasswordQuestion { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        public string Woonplaats { get; set; }

        public string Straatnaam { get; set; }

        public int? Huisnummer { get; set; }

        public string Postcode { get; set; }

        #endregion

        #region methods

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(MyUserManager userManager)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        #endregion
    }
}
