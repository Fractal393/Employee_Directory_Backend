using Microsoft.EntityFrameworkCore;
//using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectoryAPI.Models
{
    public enum PreferredNameType
    {
        FirstName,
        LastName
    }
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        //ADD  VALIDATION
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }
        public PreferredNameType? PreferredName { get; set; }

        //public string? PreferredName { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string? Email { get; set; }

        [StringLength(25)]
        public string? JobTitle { get; set; }

        [StringLength(25)]
        public string? Office { get; set; }

        [StringLength(25)]
        public string? Department { get; set; }

        [RegularExpression("^[0-9]*$")]
        public string? PhoneNumber { get; set; }
        public string? SkypeID { get; set; }

        public string? ImagePath { get; set; }

    }

    public class EmpContext: DbContext
    {
        public EmpContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
    }
}


