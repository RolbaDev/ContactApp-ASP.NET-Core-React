using ContactsApp.DTO;
using ContactsApp.Models;

namespace ContactsApp.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryResult>> GetAllCategoriesAsync();
        Task<GetCategoryResult> GetCategoryByIdAsync(int id);
        Task<GetCategoryResult> CreateCategoryAsync(CreateCategoryRequest categoryRequest);
        Task<GetCategoryResult> UpdateCategoryAsync(int id, CreateCategoryRequest categoryRequest);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
