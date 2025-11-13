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
        var users = UserController.GetUsers();
        var ViewModel = new ViewModel
        {
            Messages = messages,
            Users = users
        };
        return View(ViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Send(string text, string id)
    {
        if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(id))
        {
            Message newMessage;
            var users = UserController.GetUsers();
            
            if (users.ContainsKey(id))
            {
                newMessage = new Message
                {
                    Num = messages.Count + 1,
                    Person = users[id],
                    Text = text,
                    TimeStamp = DateTime.Now.ToShortTimeString()
                };
            }
            else
            {
                newMessage = new Message
                {
                    Num = messages.Count + 1,
                    Person = new User
                    {
                        FirstName = "Аноним",
                        SecondName = "Undefined",
                        Date = "Undefined"
                    },
                    Text = text,
                    TimeStamp = DateTime.Now.ToShortTimeString()
                };
            }
            
            messages.Add(newMessage);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", newMessage);
        }
        return RedirectToAction("Index");
    }
}