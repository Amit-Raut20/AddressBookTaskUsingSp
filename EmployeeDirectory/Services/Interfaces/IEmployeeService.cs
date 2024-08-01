using EmployeeDirectory.Models;
using System.Net;

namespace EmployeeDirectory.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployeesAsync();
        Employee GetEmployeeByIdAsync(int id);
        int CreateEmployeeAsync(Employee employee);
        int UpdateEmployeeAsync(Employee employee, int id);
        int DeleteEmployeeAsync(int id);
    }
}
