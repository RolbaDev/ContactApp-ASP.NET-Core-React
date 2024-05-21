using AutoMapper;
using ContactsApp.Data;
using ContactsApp.DTO;
using ContactsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ContactsApp.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all contacts
        public async Task<IEnumerable<GetContactResult>> GetAllContactsAsync()
        {
            var contacts = await _context.Contacts
                .Include(c => c.Category)
                .Include(c => c.Subcategory)
                .DefaultIfEmpty() // Left outer join dla subkategorii
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetContactResult>>(contacts);
        }

        // Get contact by ID
        public async Task<GetContactResult> GetContactByIdAsync(int id)
        {
            var contact = await _context.Contacts
                .Include(c => c.Category)
                .Include(c => c.Subcategory)
                .DefaultIfEmpty() // Left outer join dla subkategorii
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<GetContactResult>(contact);
        }

        // Create a new contact
        public async Task<GetContactResult> CreateContactAsync(CreateContactRequest contactRequest)
        {
            // Tworzenie kontaktu z mapowaniem z żądania
            var contact = _mapper.Map<Contact>(contactRequest);

            // Pobranie kategorii z bazy danych
            var category = await _context.Categories.FindAsync(contactRequest.CategoryId);
            if (category == null)
            {
                throw new ArgumentException("Invalid Category Id");
            }

            // Przypisanie kategorii do kontaktu
            contact.CategoryId = contactRequest.CategoryId;
            contact.Category = category;

            // Walidacja subkategorii na podstawie kategorii
            if (category.Name == "służbowy")
            {
                if (contactRequest.SubcategoryId == null)
                {
                    throw new ArgumentException("Subcategory is required for 'służbowy' category");
                }

                var subcategory = await _context.Subcategories.FindAsync(contactRequest.SubcategoryId);
                if (subcategory == null || (subcategory.Name != "szef" && subcategory.Name != "klient"))
                {
                    throw new ArgumentException("Invalid Subcategory for 'służbowy' category. Only 'szef' or 'klient' are allowed.");
                }

                contact.SubcategoryId = contactRequest.SubcategoryId;
                contact.Subcategory = subcategory;
            }
            else if (category.Name == "prywatny")
            {
                if (contactRequest.SubcategoryId != null)
                {
                    throw new ArgumentException("Subcategory is not allowed for 'prywatny' category");
                }

                contact.SubcategoryId = null;
            }
            else
            {
                contact.SubcategoryId = contactRequest.SubcategoryId == 0 ? null : contactRequest.SubcategoryId;
            }

            contact.Password = HashPassword(contactRequest.Password);

            _context.Contacts.Add(contact);

            await _context.SaveChangesAsync();

            return _mapper.Map<GetContactResult>(contact);
        }

        // Update an existing contact
        public async Task<GetContactResult> UpdateContactAsync(int id, CreateContactRequest contactRequest)
        {
            var existingContact = await _context.Contacts.FindAsync(id);
            if (existingContact == null)
                return null;

            _mapper.Map(contactRequest, existingContact);
            _context.Entry(existingContact).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<GetContactResult>(existingContact);
        }

        // Delete a contact
        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return false;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        // Hash a password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
