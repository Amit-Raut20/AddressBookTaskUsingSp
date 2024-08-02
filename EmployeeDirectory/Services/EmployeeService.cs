using EmployeeDirectory.Models;
using EmployeeDirectory.Services.Interfaces;
using EmployeeDirectory.Dal;
using EmployeeDirectory.Dal.Interfaces;

namespace EmployeeDirectory.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IDataAccessUsingSp _dataAccessUsingSp;

        public EmployeeService(IDataAccessUsingSp dataAccessUsingSp)
        {
            _dataAccessUsingSp = dataAccessUsingSp;
        }
        public async Task<int> CreateEmployeeAsync(Employee employee)
        {
            //DataAccess dataAccess = new DataAccess();
            //DataAccessUsingSp dataAccessUsingSp = new DataAccessUsingSp();
            //_dataAccessUsingSp = new DataAccessUsingSp();
            return await _dataAccessUsingSp.AddEmployeeAsync(employee);
        }

        public async Task<int> DeleteEmployeeAsync(int id)
        {
            //DataAccess dataAccess = new DataAccess();
            //DataAccessUsingSp dataAccessUsingSp = new DataAccessUsingSp();
            //_dataAccessUsingSp = new DataAccessUsingSp();
            return await _dataAccessUsingSp.DeleteEmployeeAsync(id);

        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            //DataAccessUsingSp dataAccessUsingSp = new DataAccessUsingSp();
            //_dataAccessUsingSp = new DataAccessUsingSp();
            return await _dataAccessUsingSp.GetAllEmployeesAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            //DataAccess dataAccess = new DataAccess();
            //DataAccessUsingSp dataAccessUsingSp = new DataAccessUsingSp();
            //_dataAccessUsingSp = new DataAccessUsingSp();
            return await _dataAccessUsingSp.GetEmployeeByIdAsync(id);
        }

        public async Task<int> UpdateEmployeeAsync(Employee employee, int id)
        {
            //DataAccess dataAccess = new DataAccess();
            //DataAccessUsingSp dataAccessUsingSp = new DataAccessUsingSp();
            //_dataAccessUsingSp = new DataAccessUsingSp();
            return await _dataAccessUsingSp.UpdateEmployeeAsync(employee, id);
        }
    }
}
