using ContactsApp.Models;

namespace ContactsApp.DTO
{
    public class ContactQueryResult
    {
        public Contact Contact { get; set; }
        public Category Category { get; set; }
        public Subcategory Subcategory { get; set; }
    }
}
