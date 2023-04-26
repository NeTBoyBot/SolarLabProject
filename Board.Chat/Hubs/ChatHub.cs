using Microsoft.AspNetCore.SignalR;

namespace Board.Chat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string username,string message)
        {
            await Clients.All.SendAsync("Send",username, message);
        }
    }
}
