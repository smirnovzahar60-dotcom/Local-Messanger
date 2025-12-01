using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using LocalMessenger.Models;
using SignalRApp;
using LocalMessenger.Data;
using Microsoft.EntityFrameworkCore;

public class ChatController : Controller
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly SettingsBD _context;
    
    public ChatController(IHubContext<ChatHub> hubContext, SettingsBD context)
    {
        _hubContext = hubContext;
        _context = context;
    }
    
    public IActionResult Index()
    {
        // Загружаем сообщения с пользователями
        var messages = _context.Messages
            .Include(m => m.Person)
            .ToList();
        
        var viewModel = new ViewModel
        {
            Messages = messages
        };
        
        return View(viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Send(string text, string id)
    {
        if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(id))
        {
            // Ищем пользователя по ExternalId
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.ExternalId == id);
            
            if (user != null)
            {
                var newMessage = new Message
                {
                    Person = user,
                    UserId = user.Id,
                    Text = text,
                    TimeStamp = DateTime.Now
                };
                
                _context.Messages.Add(newMessage);
                await _context.SaveChangesAsync();
                
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", newMessage);
            }
        }
        
        return RedirectToAction("Index");
    }
}