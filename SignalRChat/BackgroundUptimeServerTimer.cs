using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using Humanizer;

namespace SignalRChat
{
    public class BackgroundUptimeServerTimer : IRegisteredObject
    {
        private readonly DateTime _internetBirthDate = Convert.ToDateTime("29/10/1969");
        private readonly IHubContext _uptimeHub;
        private Timer _timer;


        public BackgroundUptimeServerTimer()
        {
            _uptimeHub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

            StartTimer();
        }
        private void StartTimer()
        {
            var delayStartby = 2000;
            var repeatEvery = 1000;

            _timer = new Timer(BroadcastUptimeToClients, null, delayStartby, repeatEvery);
        }
        private void BroadcastUptimeToClients(object state)
        {
            TimeSpan uptime = DateTime.Now - _internetBirthDate;

            _uptimeHub.Clients.All.internetUpTime(uptime.Humanize(5));
        }

        public void Stop(bool immediate)
        {
            _timer.Dispose();

            HostingEnvironment.UnregisterObject(this);
        }
    }
}