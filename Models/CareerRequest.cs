using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("Careers")]
    public class CareerRequest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Name must not contain '.'")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phone must contain digits only")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Address must not contain '.'")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Degree is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Degree must not contain '.'")]
        public string Degree { get; set; }

        [Required(ErrorMessage = "Institution is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Institution must not contain '.'")]
        public string Institution { get; set; }

        [Required(ErrorMessage = "Graduation year is required")]
        public string GraduationYear { get; set; }

        [Required(ErrorMessage = "Resume is required")]
        public string Resume { get; set; }

        [Required(ErrorMessage = "Cover letter is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Cover letter must not contain '.'")]
        public string CoverLetter { get; set; }

        [Required(ErrorMessage = "Job position is required")]
        [RegularExpression(@"^[^.]+$", ErrorMessage = "Job position must not contain '.'")]
        public string JobPosition { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public int? CandidateId { get; set; }
    }
}
