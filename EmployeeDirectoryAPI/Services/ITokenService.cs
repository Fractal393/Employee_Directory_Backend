using EmployeeDirectoryAPI.Models;
using EmployeeDirectoryAPI.ViewModels;

namespace EmployeeDirectoryAPI.Services
{
    public interface ITokenService
    {
        Task<List<Token>> GetAsync();

        Task<ResponseModel> AddAsync(Token token);

    }
}
