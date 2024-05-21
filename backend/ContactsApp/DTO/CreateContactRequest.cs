using ContactsApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ContactsApp.DTO
{
    public class CreateContactRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int CategoryId { get; set; }

        public int? SubcategoryId { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
