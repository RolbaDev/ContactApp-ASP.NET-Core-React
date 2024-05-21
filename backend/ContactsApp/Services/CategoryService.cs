using AutoMapper;
using ContactsApp.Data;
using ContactsApp.DTO;
using ContactsApp.Models;

using Microsoft.EntityFrameworkCore;


namespace ContactsApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all categories
        public async Task<IEnumerable<GetCategoryResult>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<GetCategoryResult>>(categories);
        }

        // Get category by ID
        public async Task<GetCategoryResult> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return null;
            }
            return _mapper.Map<GetCategoryResult>(category);
        }

        // Create a new category
        public async Task<GetCategoryResult> CreateCategoryAsync(CreateCategoryRequest categoryRequest)
        {
            var category = _mapper.Map<Category>(categoryRequest);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetCategoryResult>(category);
        }

        // Update an existing category
        public async Task<GetCategoryResult> UpdateCategoryAsync(int id, CreateCategoryRequest categoryRequest)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return null;
            }

            _mapper.Map(categoryRequest, category);
            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<GetCategoryResult>(category);
        }

        // Delete a category
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            var contactsWithCategory = await _context.Contacts.AnyAsync(c => c.CategoryId == id);
            if (contactsWithCategory)
            {
                throw new InvalidOperationException("Nie można usunąć kategorii, ponieważ istnieją kontakty powiązane z tą kategorią.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }


        private async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(e => e.Id == id);
        }
    }
}
