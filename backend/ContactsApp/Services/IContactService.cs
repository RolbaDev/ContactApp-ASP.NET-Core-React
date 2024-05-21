using ContactsApp.DTO;

namespace ContactsApp.Services
{
    public interface IContactService
    {
        Task<IEnumerable<GetContactResult>> GetAllContactsAsync();
        Task<GetContactResult> GetContactByIdAsync(int id);
        Task<GetContactResult> CreateContactAsync(CreateContactRequest contactRequest);
        Task<GetContactResult> UpdateContactAsync(int id, CreateContactRequest contactRequest);
        Task<bool> DeleteContactAsync(int id);
    }
}
