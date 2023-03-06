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

        // GET: api/Employees
        //[HttpGet("Employees")]
        //public async Task<ActionResult<Employee>> Get()
        //{
        //    var result = await _employeeService.GetAsync();
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(result);
        //}

        //public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        //{
        //    return await _context.Employees.ToListAsync();
        //}

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

        [HttpGet("Employees")]
        public async Task<ActionResult<Employee>> GetFilter([FromQuery] string searchText, [FromQuery] string letter, string searchBy)
        {
            List<Employee>? result;

            if (searchText == null && letter == null)
            {
                result = await _employeeService.GetAsync();
            }
            else if (searchText!= null)
            {

                result = await _employeeService.GetFilter(searchText, searchBy);
            } 
            else
            {
                result = await _employeeService.GetLetters(letter);
            }

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("Employee")]

        public async Task<ActionResult<Employee>> SidebarFilters(string department, string office, string jobTitle)
        {          
           var result = await _employeeService.SidebarFilters(department, office, jobTitle);

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

        //private bool EmployeeExists(int id)
        //{
        //    return _context.Employees.Any(e => e.EmployeeId == id);
        //}
    }
}
