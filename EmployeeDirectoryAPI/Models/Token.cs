using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectoryAPI.Models
{
    public class Token
    {
        [Key]
        public int id { get; set; }
        public string? token { get; set; }
    }
}
