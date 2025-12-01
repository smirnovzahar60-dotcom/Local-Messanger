using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using LocalMessenger.Models;
using SignalRApp;
using LocalMessenger.Data;
using Microsoft.EntityFrameworkCore;

public class UserController : Controller
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly SettingsBD _context;
    
    public UserController(IHubContext<ChatHub> hubContext, SettingsBD context)
    {
        _hubContext = hubContext;
        _context = context;
    }
    
    public IActionResult Index()
    {
        var users = _context.Users.ToDictionary(u => u.ExternalId, u => u);
        
        var viewModel = new ViewModel
        {
            Users = users
        };
        
        return View(viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(string firstName, string secondName, DateTime date, string id)
    {
        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(secondName) && !string.IsNullOrEmpty(id))
        {
            // Проверяем, нет ли уже пользователя с таким ExternalId
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.ExternalId == id);
                
            if (existingUser == null)
            {
                var newUser = new User
                {
                    ExternalId = id,
                    FirstName = firstName,
                    SecondName = secondName,
                    Date = date
                };
                
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                
                await _hubContext.Clients.All.SendAsync("ReceiveUser", newUser);
            }
        }
        
        return RedirectToAction("Index");
    }
}