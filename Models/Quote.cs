using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Quote
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Full name must not contain '.'")]
        public string Full_Name { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Company name must not contain '.'")]
        public string Company_Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Address must not contain '.'")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "City must not contain '.'")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "State must not contain '.'")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Postal code must not contain '.'")]
        public string Postal_Code { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Country must not contain '.'")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [MinLength(11, ErrorMessage = "Phone number must be at least 11 digits")]
        [RegularExpression(@"^[0-9]{11,}$", ErrorMessage = "Phone number must contain only digits and be at least 11 digits")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Comments are required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Comments must not contain '.'")]
        public string Comments { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
