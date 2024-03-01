using Microsoft.AspNetCore.SignalR;

namespace Semprg_BlazorIntroductory.Server.Services.Hubs;

public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await this.Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined", "system");
    }

    public async Task BroadCast(string messageContent)
    {
        //await this.Clients.All.ReceiveMessage(new ChatMessage(messageContent, Context.ConnectionId));
        await this.Clients.All.SendAsync("ReceiveMessage", messageContent, Context.ConnectionId);
    }
}