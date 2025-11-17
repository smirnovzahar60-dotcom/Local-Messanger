using Microsoft.AspNetCore.Mvc;
using LocalMessenger.Models;
using System.Collections.Generic;

public class UserController : Controller
{
    private static Dictionary<string, User> users = new();

    public static Dictionary<string, User> GetUsers()
    {
        return users;
    }
    
    [HttpPost]

    public IActionResult AddUser(string firstName, string secondName, string date, string id)
    {
        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(secondName) && !string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(id))
            users[id] = new User
            {
                FirstName = firstName,
                SecondName = secondName,
                Date = date
            };
        return RedirectToAction("Index", "Chat");
    }
}