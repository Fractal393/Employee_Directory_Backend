using EmployeeDirectoryAPI.Models;
using static EmployeeDirectoryAPI.Services.UserService;

namespace EmployeeDirectoryAPI.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAsync();

        Task<bool> IsEmailTaken(string email);
        Task<User> Register(LoginDTO login);

        Task<User> Login(LoginDTO login);
    }
}
