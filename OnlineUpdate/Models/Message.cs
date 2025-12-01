using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalMessenger.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        [Required]
        public User Person { get; set; }
        
        [Required]
        public string Text { get; set; }
        
        [Required]
        public DateTime TimeStamp { get; set; }
    }
}