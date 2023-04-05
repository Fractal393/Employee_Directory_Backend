using EmployeeDirectoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryAPI
{
    public class EmpContext : DbContext
    {
        public EmpContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Token> Tokens { get; set; }

    }
}
