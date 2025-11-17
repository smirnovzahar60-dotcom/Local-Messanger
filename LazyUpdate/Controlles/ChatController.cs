using Microsoft.AspNetCore.Mvc;
using LocalMessenger.Models;
using System.Collections.Generic;

public class ChatController : Controller
{
    private static List<Message> messages = new();

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
    public IActionResult Send(string text, string id)
    {
        if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(id))
        {
            var users = UserController.GetUsers();
            if (users.ContainsKey(id))
                messages.Add(new Message
                {
                    Num = messages.Count + 1,
                    Person = users[id],
                    Text = text,
                    TimeStamp = DateTime.Now.ToShortTimeString()
                });
            else
                messages.Add(new Message
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
                });
        }
        return RedirectToAction("Index");
    }
}