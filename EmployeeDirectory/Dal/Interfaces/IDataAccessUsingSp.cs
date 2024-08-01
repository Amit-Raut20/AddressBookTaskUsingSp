using EmployeeDirectory.Models;
using System.Net;

namespace EmployeeDirectory.Dal.Interfaces
{
    public interface IDataAccessUsingSp
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<int> AddEmployeeAsync(Employee employee);
        Task<int> UpdateEmployeeAsync(Employee employee, int id);
        Task<int> DeleteEmployeeAsync(int id);
    }
}
