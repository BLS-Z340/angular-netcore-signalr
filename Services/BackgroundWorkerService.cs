using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalIrAngular.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalIrAngular.Services
{
	public class BackgroundWorkerService : IHostedService, IDisposable
    {
        private Timer _timer;
        private IHubContext<ExampleHub> HubContext { get; }

        public BackgroundWorkerService(IHubContext<ExampleHub> hubContext)
        {
            HubContext = hubContext;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            await this.HubContext.Clients.All.SendAsync("BroadcastMessage", "Arg1", DateTime.Now.ToString());
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
