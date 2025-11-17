using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using LocalMessenger.Models;
using SignalRApp;
using System.Collections.Generic;

public class UserController : Controller
{
    private static Dictionary<string, User> users = new();
    private readonly IHubContext<ChatHub> _hubContext;
    public UserController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public static Dictionary<string, User> GetUsers()
    {
        return users;
    }
    
    public IActionResult Index()
    {
        var ViewModel = new ViewModel
        {
            Users = users
        };
        return View(ViewModel);
    }

    [HttpPost]

    public async Task<IActionResult> Add(string firstName, string secondName, DateTime date, string id)
    {
        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(secondName) /*&& !string.IsNullOrEmpty(date)*/ && !string.IsNullOrEmpty(id))
        {
            var newUser = new User
            {
                FirstName = firstName,
                SecondName = secondName,
                Date = date,
                Age = DateTime.Now - date
            };
            users[id] = newUser;
            await _hubContext.Clients.All.SendAsync("ReceiveUser", newUser);
        }
        return RedirectToAction("Index");
    }
}