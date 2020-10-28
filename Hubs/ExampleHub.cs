using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalIrAngular.Hubs
{
	public class ExampleHub : Hub
	{
		public Task SendMessage(string user, string message)
		{
			return Clients.All.SendAsync("ReceiveMessage", user, message);
		}
	}
}
