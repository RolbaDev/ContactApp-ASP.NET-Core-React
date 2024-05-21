using ContactsApp.DTO;
using ContactsApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetContactResult>>> GetContacts()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            return Ok(contacts);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetContactResult>> GetContact(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return contact;
        }

        [HttpPost]
        public async Task<ActionResult<GetContactResult>> CreateContact(CreateContactRequest contactRequest)
        {
            try
            {
                var createdContact = await _contactService.CreateContactAsync(contactRequest);
                return CreatedAtAction(nameof(GetContact), new { id = createdContact.Id }, createdContact);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, CreateContactRequest contactRequest)
        {
            var updatedContact = await _contactService.UpdateContactAsync(id, contactRequest);
            if (updatedContact == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var result = await _contactService.DeleteContactAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}