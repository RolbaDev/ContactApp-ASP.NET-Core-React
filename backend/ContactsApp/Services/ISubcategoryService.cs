using ContactsApp.DTO;
using ContactsApp.Models;

namespace ContactsApp.Services
{
    public interface ISubcategoryService
    {
        Task<IEnumerable<GetSubcategoryResult>> GetAllSubcategoriesAsync();
        Task<GetSubcategoryResult> GetSubcategoryByIdAsync(int id);
        Task<GetSubcategoryResult> CreateSubcategoryAsync(CreateSubcategoryRequest subcategoryRequest);
        Task<GetSubcategoryResult> UpdateSubcategoryAsync(int id, CreateSubcategoryRequest subcategoryRequest);
        Task<bool> DeleteSubcategoryAsync(int id);
    }
}
