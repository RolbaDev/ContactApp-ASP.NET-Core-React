namespace ContactsApp.DTO
{
    public class GetContactResult
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public GetCategoryResult? Category { get; set; }
        public GetSubcategoryResult Subcategory { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
