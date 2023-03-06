using EmployeeDirectoryAPI.Models;
using EmployeeDirectoryAPI.ViewModels;

namespace EmployeeDirectoryAPI.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAsync();

        Task<Employee?> GetByIdAsync(int employeeId);

        Task<ResponseModel> AddAsync(Employee employee);

        Task<ResponseModel> UpdateAsync(Employee employee);

        Task<ResponseModel> DeleteAsync(int employeeid);

        Task<List<Employee>> GetFilter(string value, string searchBy);

        Task<List<Employee>> GetLetters(string letter);
        Task<List<Employee>> SidebarFilters(string department, string office, string jobTitle);

    }
}
