using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public required string Image { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
        public int CategoryId { get; set; }

        [Required]
        public required Category Categories { get; set; }
    }
}
