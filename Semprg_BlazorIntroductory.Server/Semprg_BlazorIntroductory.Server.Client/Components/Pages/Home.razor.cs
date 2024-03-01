using Microsoft.AspNetCore.SignalR.Client;

namespace Semprg_BlazorIntroductory.Client.Components.Pages;

public partial class Home : IAsyncDisposable
{
    private const int k_MaxMessages = 8;
    private readonly List<(string Content, string Sender)> _messages = new(k_MaxMessages);

    private HubConnection? hubConnection;
    private string? messageToSend; //@bind
    private async Task Handle_JoinClick()
    {
        _messages.Clear();

        hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7058/chatHub")
            .Build();

        hubConnection.On<string, string>("ReceiveMessage", Handle_ReceiveMessage);

        await hubConnection.StartAsync();
    }

    private async Task Handle_SendClick()
    {
        if (hubConnection is null)
            return;

        await hubConnection.SendAsync("BroadCast", messageToSend);
        
        messageToSend = string.Empty;
        StateHasChanged();
    }

    private void Handle_ReceiveMessage(string messageContent, string sender)
    {
        _messages.Add((messageContent, sender));

        if (_messages.Count > k_MaxMessages)
        {
            _messages.RemoveAt(0);
        }

        StateHasChanged();
    }

    public async ValueTask DisposeAsync()
    {
        if (hubConnection is not null)
        {
            await hubConnection.StopAsync();
            await hubConnection.DisposeAsync();
        }

        GC.SuppressFinalize(this); //Opt
        return;
    }
}