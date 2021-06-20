using System.ComponentModel.DataAnnotations;

namespace HooliCash.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string TransactionType { get; set; }

        public string IconUrl { get; set; }
    }
}
