using Microsoft.Owin;
using Owin;
using System.Web.Hosting;

[assembly: OwinStartup(typeof(SignalRChat.Startup))]
namespace SignalRChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HostingEnvironment.RegisterObject(new BackgroundUptimeServerTimer());
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}