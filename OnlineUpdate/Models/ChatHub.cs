using Microsoft.AspNetCore.SignalR;
using LocalMessenger.Models;
 
namespace SignalRApp
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task AddUser(User user)
        {
            await Clients.All.SendAsync("ReceiveUser", user);
        }
    }
}