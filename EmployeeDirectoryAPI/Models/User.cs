using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectoryAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{8,}$",
         ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string? Password { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }
    }
}
