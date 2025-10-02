using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class AdminLogin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Name must not contain '.'")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [MinLength(11, ErrorMessage = "Phone number must be at least 11 digits")]
        [RegularExpression(@"^[0-9]{11,}$", ErrorMessage = "Phone number must be numeric and at least 11 digits")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Password must not contain '.'")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Address must not contain '.'")]
        public required string Address { get; set; }

        public string Role { get; set; } = "pending";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}
