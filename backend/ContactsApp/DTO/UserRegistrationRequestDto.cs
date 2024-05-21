using System.ComponentModel.DataAnnotations;

namespace ContactsApp.DTO
{
    public class UserRegistrationRequestDto
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
