using EmployeeDirectoryAPI.Models;
using EmployeeDirectoryAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace EmployeeDirectoryAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private EmpContext _context;

        public EmployeeService(EmpContext context)
        {
            _context = context; 
        }


        /// <summary>
        /// Get All the Employees Present
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> GetAsync()
        {
            List<Employee> employees;
            try
            {
                employees = await _context.Employees.ToListAsync(); //The Type is already Set using DBset so it's not required.
                //employees = await _context.Set<Employee>().ToListAsync(); //Dynamic Binding
            }
            catch (Exception)
            {
                throw;
            }
            return employees;
        }
        /// <summary>
        /// Get the Employee by their respective ID
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<Employee?> GetByIdAsync(int employeeId)
        {
            Employee? employee;

            try
            {
                employee = await _context.FindAsync<Employee>(employeeId);
            }
            catch (Exception)
            {
                throw; //Makes no sense You need to send null because it's being checked in the controller
            }

            return employee;
        }

        public async Task<List<Employee>> GetFilter(string searchText, string searchBy)
        {
            List<Employee> employee;

            try
            {
                switch (searchBy.ToLower())
                {
                    case "department":
                        employee = await _context.Employees.Where(e => e.Department != null && e.Department.Contains(searchText)).ToListAsync();
                        break;
                    case "office":
                        employee = await _context.Employees.Where(e => e.Office != null && e.Office.Contains(searchText)).ToListAsync();
                        break;
                    case "firstname":
                        employee = await _context.Employees.Where(e => e.FirstName != null && e.FirstName.Contains(searchText)).ToListAsync();
                        break;
                    default:
                        employee = new List<Employee>();
                        break;
                }
            }
            catch(Exception)
            {
                throw;
            }

            return employee;
        }


        public async Task<List<Employee>> GetLetters(string letter)
        {
            List<Employee> employee;
            try
            {
                employee = await _context.Employees.Where(e => e.FirstName != null && e.FirstName.StartsWith(letter)).ToListAsync();
            }
            catch(Exception)
            {
                throw;
            }
            return employee;
        }

        public async Task<List<Employee>> SidebarFilters(string department, string office, string jobTitle)
        {
            List<Employee> employee;

            try
            {
                if (department != null)
                {
                    employee = await _context.Employees.Where(e => e.Department != null && e.Department.StartsWith(department)).ToListAsync();
                }
                else if(office != null)
                {
                    employee = await _context.Employees.Where(e => e.Office == office).ToListAsync();
                }
                else
                {
                    employee = await _context.Employees.Where(e => e.JobTitle == jobTitle).ToListAsync();
                }
            }

            catch (Exception)
            {
                throw;
            }
            return employee;
        }

        /// <summary>
        /// Add a new Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<ResponseModel> AddAsync(Employee employee)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                //Employee? existingEmployee = await GetByIdAsync(employee.EmployeeId);

                //if (existingEmployee != null)
                //{
                //    return new ResponseModel()
                //    {
                //        IsSuccess = false,
                //        Message = "Please Enter Employee Details"
                //    };
                //}
                
                var result = await _context.AddAsync<Employee>(employee);
                if (await _context.SaveChangesAsync() < 0)
                {
                    return new ResponseModel()
                    {
                        IsSuccess = false,
                        Message = "Employee Insertion Failed"
                    };
                } 
                responseModel.IsSuccess = true;
                responseModel.Message = "Employee Inserted Succesfully";
                //Returns value which needs to be checked and only then responsemodel should be updated

             }

            catch (Exception ex)
            {
                responseModel.IsSuccess= false;
                responseModel.Message = $"Error:{ex.Message}";
            }

            return responseModel;
        }

        /// <summary>
        /// Update the details of an existing employee.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<ResponseModel> UpdateAsync(Employee employee)
        {
            ResponseModel responseModel = new ResponseModel();
            //UPDATE FIX
            try
            {
                Employee? existingEmployee = await GetByIdAsync(employee.EmployeeId);
                if (existingEmployee == null)
                    return new ResponseModel() {
                        IsSuccess = false, Message = $"Damn! Employee doesnt exists with Id:{employee.EmployeeId}" 
                    };


                // mapper
                existingEmployee.FirstName = employee.FirstName ?? existingEmployee.FirstName;
                existingEmployee.LastName = employee.LastName ?? existingEmployee.LastName;
                existingEmployee.PreferredName = employee.PreferredName ?? existingEmployee.PreferredName;
                existingEmployee.Email = employee.Email ?? existingEmployee.Email;
                existingEmployee.JobTitle = employee.JobTitle ?? existingEmployee.JobTitle;
                existingEmployee.Office = employee.Office ?? existingEmployee.Office;
                existingEmployee.Department = employee.Department ?? existingEmployee.Department;
                existingEmployee.PhoneNumber = employee.PhoneNumber ?? existingEmployee.PhoneNumber;
                existingEmployee.SkypeID = employee.SkypeID ?? existingEmployee.SkypeID;
                existingEmployee.ImagePath = employee.ImagePath ?? existingEmployee.ImagePath;


                //_context.Update<Employee>(existingEmployee);
                if (await _context.SaveChangesAsync() > 0)
                {
                responseModel.Message = "Employee Updated Successfully";
                responseModel.IsSuccess = true;

                }
            }

            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = $"Error:{ex.Message}";
            }

            return responseModel;
        }

        /// <summary>
        /// Delete an employee by their EmployeeId
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<ResponseModel> DeleteAsync(int employeeId)
        {
            ResponseModel responseModel = new();

            try
            {
                Employee? existingEmployee = await GetByIdAsync(employeeId);

                if (existingEmployee == null)
                {
                    return new ResponseModel()
                    {
                        IsSuccess = false,
                        Message = "Employee Not Found"
                    };
                }

                _context.Remove<Employee>(existingEmployee);
                if (await _context.SaveChangesAsync() > 0)
                {
                    responseModel.IsSuccess = true;
                    responseModel.Message = "Employee Deleted Successfully";
                }
            }

            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = $"Error :{ex.Message}";
            }
            return responseModel;
        }
    }
}





