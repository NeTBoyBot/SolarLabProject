using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;

namespace Board.Chat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage",user,message);
        }
    }
}
