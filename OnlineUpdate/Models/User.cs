using System.ComponentModel.DataAnnotations;

namespace LocalMessenger.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string ExternalId { get; set; }  // Внешний ID из формы
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string SecondName { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
    }
}