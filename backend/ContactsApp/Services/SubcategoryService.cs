    using AutoMapper;
    using ContactsApp.Data;
    using ContactsApp.DTO;
    using ContactsApp.Models;
    using Microsoft.EntityFrameworkCore;

    namespace ContactsApp.Services
    {
        public class SubcategoryService : ISubcategoryService
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;

            public SubcategoryService(AppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            // Get all subcategories
            public async Task<IEnumerable<GetSubcategoryResult>> GetAllSubcategoriesAsync()
            {
                var subcategories = await _context.Subcategories
                    .ToListAsync();
                return _mapper.Map<IEnumerable<GetSubcategoryResult>>(subcategories);
            }

             // Get subcategory by ID
            public async Task<GetSubcategoryResult> GetSubcategoryByIdAsync(int id)
            {
                var subcategory = await _context.Subcategories
                    .FirstOrDefaultAsync(s => s.Id == id);
                return _mapper.Map<GetSubcategoryResult>(subcategory);
            }
            
            // Create a new subcategory
            public async Task<GetSubcategoryResult> CreateSubcategoryAsync(CreateSubcategoryRequest subcategoryRequest)
            {
                var subcategory = _mapper.Map<Subcategory>(subcategoryRequest);
                _context.Subcategories.Add(subcategory);
                await _context.SaveChangesAsync();
                return _mapper.Map<GetSubcategoryResult>(subcategory);
            }
            
            // Update an existing subcategory
            public async Task<GetSubcategoryResult> UpdateSubcategoryAsync(int id, CreateSubcategoryRequest subcategoryRequest)
            {
                var subcategory = await _context.Subcategories.FindAsync(id);
                if (subcategory == null)
                {
                    return null;
                }

                _mapper.Map(subcategoryRequest, subcategory);
                _context.Entry(subcategory).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SubcategoryExists(id))
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }

                return _mapper.Map<GetSubcategoryResult>(subcategory);
            }
            
            // Delete a subcategory
            public async Task<bool> DeleteSubcategoryAsync(int id)
            {
                var subcategory = await _context.Subcategories.FindAsync(id);
                if (subcategory == null)
                {
                    return false;
                }

                var contactsWithSubcategory = await _context.Contacts.AnyAsync(c => c.SubcategoryId == id);
                if (contactsWithSubcategory)
                {
                    throw new InvalidOperationException("Nie można usunąć subkategorii, ponieważ istnieją kontakty powiązane z tą subkategorią.");
                }

                _context.Subcategories.Remove(subcategory);
                await _context.SaveChangesAsync();
                return true;
            }

            private async Task<bool> SubcategoryExists(int id)
            {
                return await _context.Subcategories.AnyAsync(e => e.Id == id);
            }
        }
    }
