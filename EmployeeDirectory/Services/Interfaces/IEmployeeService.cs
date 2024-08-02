using EmployeeDirectory.Models;
using System.Net;

namespace EmployeeDirectory.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        public Task<Employee> GetEmployeeByIdAsync(int id);
        public Task<int> CreateEmployeeAsync(Employee employee);
        public Task<int> UpdateEmployeeAsync(Employee employee, int id);
        public Task<int> DeleteEmployeeAsync(int id);
    }
}
