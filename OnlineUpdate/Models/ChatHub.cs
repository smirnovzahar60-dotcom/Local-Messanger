using Microsoft.AspNetCore.SignalR;
using LocalMessenger.Models;
using LocalMessenger.Data;
 
namespace SignalRApp
{
    public class ChatHub : Hub
    {
        private readonly SettingsBD _context;
        
        public ChatHub(SettingsBD context)
        {
            _context = context;
        }
        
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