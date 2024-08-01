using EmployeeDirectory.Models;
using System.Net;

namespace EmployeeDirectory.Dal.Interfaces
{
    public interface IDataAccess
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee, int id);
        Task<int> DeleteEmployeeAsync(int id);
    }
}
