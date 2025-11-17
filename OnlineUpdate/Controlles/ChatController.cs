using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using LocalMessenger.Models;
using SignalRApp;
using System.Collections.Generic;

public class ChatController : Controller
{
    private static List<Message> messages = new();
    private readonly IHubContext<ChatHub> _hubContext;
    public ChatController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }
    public IActionResult Index()
    {
        var ViewModel = new ViewModel
        {
            Messages = messages
        };
        return View(ViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Send(string text, string id)
    {
        if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(id))
        {
            var users = UserController.GetUsers();
            
            if (users.ContainsKey(id))
            {
                var newMessage = new Message
                {
                    Num = messages.Count + 1,
                    Person = users[id],
                    Text = text,
                    TimeStamp = DateTime.Now
                };
                messages.Add(newMessage);
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", newMessage);
            }
        }
        return RedirectToAction("Index");
    }
}