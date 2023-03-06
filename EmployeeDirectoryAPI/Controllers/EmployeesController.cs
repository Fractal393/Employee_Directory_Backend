using EmployeeDirectoryAPI.Models;
using EmployeeDirectoryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDirectoryAPI.Controllers
{
    public class EmployeesController : ControllerBase
    {
        IEmployeeService _employeeService;
        //private readonly EmpContext _context;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employees/5
        [HttpGet("Employees/{employeeId}")]
        public async Task<ActionResult<Employee>> GetById(int employeeId)
        {
            var result = await _employeeService.GetByIdAsync(employeeId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="letter"></param>
        /// <param name="searchBy"></param>
        /// <param name="department"></param>
        /// <param name="office"></param>
        /// <param name="jobTitle"></param>
        /// <returns></returns>
        /// 
        [HttpGet("Employees")]
        public async Task<ActionResult<Employee>> GetFilter([FromQuery] string searchText, [FromQuery] string letter, [FromQuery] string searchBy, [FromQuery] string? department, [FromQuery]  string? office, [FromQuery] string? jobTitle)
        {
            List<Employee>? result;

            if (searchText == null && letter == null && department == null && office == null && jobTitle == null)
            {
                result = await _employeeService.GetAsync();
            }
            else if (searchText != null && letter == null && department == null && office == null && jobTitle == null)
            {

                result = await _employeeService.DropdownFilter(searchText, searchBy);
            }
            else if (searchText == null && letter != null && department == null && office == null && jobTitle == null)
            {
                result = await _employeeService.AlphabetFilter(letter);
            }
            else
            {

#pragma warning disable CS8604 // Possible null reference argument.
                result = await _employeeService.SidebarFilters(department, office, jobTitle);
#pragma warning restore CS8604 // Possible null reference argument.

            }

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }



        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Employees/{employeeId}")]
        public async Task<IActionResult> Update(int employeeId, [FromBody] Employee employee)
        {
            var result = await _employeeService.UpdateAsync(employee);
            if (!result.IsSuccess)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Employees")]
        public async Task<IActionResult> Add([FromBody] Employee employee)
        {
            var result = await _employeeService.AddAsync(employee);
            if (!result.IsSuccess)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { employeeId = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("Employees/{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var result = await _employeeService.DeleteAsync(employeeId);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
