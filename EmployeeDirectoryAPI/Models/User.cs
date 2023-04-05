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
        public string? Password { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }
    }
}
