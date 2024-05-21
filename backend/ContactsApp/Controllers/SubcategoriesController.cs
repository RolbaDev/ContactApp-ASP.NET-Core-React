using ContactsApp.DTO;
using ContactsApp.Models;
using ContactsApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ContactsApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly ISubcategoryService _subcategoryService;

        public SubcategoriesController(ISubcategoryService subcategoryService)
        {
            _subcategoryService = subcategoryService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetSubcategoryResult>>> GetSubcategories()
        {
            var subcategories = await _subcategoryService.GetAllSubcategoriesAsync();
            return Ok(subcategories);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetSubcategoryResult>> GetSubcategory(int id)
        {
            var subcategory = await _subcategoryService.GetSubcategoryByIdAsync(id);
            if (subcategory == null)
            {
                return NotFound();
            }
            return Ok(subcategory);
        }

        [HttpPost]
        public async Task<ActionResult<GetSubcategoryResult>> PostSubcategory(CreateSubcategoryRequest subcategoryRequest)
        {
            var createdSubcategory = await _subcategoryService.CreateSubcategoryAsync(subcategoryRequest);
            return CreatedAtAction(nameof(GetSubcategory), new { id = createdSubcategory.Id }, createdSubcategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubcategory(int id, CreateSubcategoryRequest subcategoryRequest)
        {
            var updatedSubcategory = await _subcategoryService.UpdateSubcategoryAsync(id, subcategoryRequest);
            if (updatedSubcategory == null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcategory(int id)
        {
            try
            {
                var success = await _subcategoryService.DeleteSubcategoryAsync(id);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
