using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ContactsApp.DTO;

namespace ContactsApp.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public static implicit operator Category(GetCategoryResult v)
        {
            throw new NotImplementedException();
        }
    }
}
