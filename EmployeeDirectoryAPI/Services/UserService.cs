using EmployeeDirectoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryAPI.Services
{
    public class UserService : IUserService
    {
        private readonly EmpContext _context;

        public UserService(EmpContext context)
        {
            _context = context;
        }

        public class LoginDTO
        {
            public string? email { get; set; }
            public string? password { get; set; }
        }

        public async Task<List<User>> GetAsync()
        {
            List<User> users;
            try
            {
                users = await _context.Users.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return users;
        }



        public async Task<bool> IsEmailTaken(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }
        public async Task<User> Register(LoginDTO login)
        {
            var user = new User { Email = login.email, Password = BCrypt.Net.BCrypt.HashPassword(login.password) };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;

        }

        public async Task<User> Login(LoginDTO login)
        {
            var user = await _context.Users.SingleOrDefaultAsync(e => e.Email == login.email);

            if (user == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(login.password, user.Password))
            {
                return null;
            }

            return user;
        }
    }
}
