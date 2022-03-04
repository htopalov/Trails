using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Trails.Api.Models;

namespace Trails.Api.Hub
{
    public class BroadcastHub : Hub<IBroadcastHub>
    {
        public async Task BroadcastData(BeaconDataBroadcastModel data)
        {
            await Clients.All.ReceiveData(JsonConvert.SerializeObject(data));
        }
    }
}
