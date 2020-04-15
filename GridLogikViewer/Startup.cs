using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GridLogikViewer.Startup))]
namespace GridLogikViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
