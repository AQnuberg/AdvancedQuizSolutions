using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuizApp.Startup))]
namespace QuizApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
