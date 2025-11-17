using LocalMessenger.Models;

namespace LocalMessenger.Models
{

    public class Message
    {
        public int Num { get; set; }
        public User Person { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}