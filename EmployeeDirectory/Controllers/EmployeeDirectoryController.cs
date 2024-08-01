using EmployeeDirectory.Models;
using EmployeeDirectory.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace EmployeeDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDirectoryController : ControllerBase
    {
        IEmployeeService _employeeService;
        public EmployeeDirectoryController(IEmployeeService employeeService) 
        {
            _employeeService = employeeService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody]Employee employee)
        {
            return Ok(_employeeService.CreateEmployeeAsync(employee));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //Task<Employee> fetchedEmp = qRCodeService.GetEmployee(id);

            return Ok(_employeeService.GetEmployeeByIdAsync(id));
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromBody]Employee employee, int id)
        {
            return Ok(_employeeService.UpdateEmployeeAsync(employee, id));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_employeeService.DeleteEmployeeAsync(id));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_employeeService.GetAllEmployeesAsync());
        }
    }
}
