using LocalMessenger.Models;

namespace LocalMessenger.Models
{

    public class ViewModel
    {
        public List<Message> Messages { get; set; } = new();
        public Dictionary<string, User> Users { get; set; } = new();
    }
}