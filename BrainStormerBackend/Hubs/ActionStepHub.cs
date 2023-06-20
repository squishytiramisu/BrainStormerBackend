using Microsoft.AspNetCore.SignalR;

namespace BrainStormerBackend.Hubs
{
    public class ActionStepHub: Hub
    {

        public async Task SendActionStep(string call,int id)
        {
            await Clients.All.SendAsync("GetActionStep", call, id);
        }
    }
}
