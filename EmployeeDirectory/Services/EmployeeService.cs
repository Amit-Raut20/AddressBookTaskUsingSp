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
        public int CreateEmployeeAsync(Employee employee)
        {
            //DataAccess dataAccess = new DataAccess();
            //DataAccessUsingSp dataAccessUsingSp = new DataAccessUsingSp();
            //_dataAccessUsingSp = new DataAccessUsingSp();
            return _dataAccessUsingSp.AddEmployeeAsync(employee).Result;
        }

        public int DeleteEmployeeAsync(int id)
        {
            //DataAccess dataAccess = new DataAccess();
            //DataAccessUsingSp dataAccessUsingSp = new DataAccessUsingSp();
            //_dataAccessUsingSp = new DataAccessUsingSp();
            return _dataAccessUsingSp.DeleteEmployeeAsync(id).Result;

        }

        public IEnumerable<Employee> GetAllEmployeesAsync()
        {
            //DataAccessUsingSp dataAccessUsingSp = new DataAccessUsingSp();
            //_dataAccessUsingSp = new DataAccessUsingSp();
            return _dataAccessUsingSp.GetAllEmployeesAsync().Result;
        }

        public Employee GetEmployeeByIdAsync(int id)
        {
            //DataAccess dataAccess = new DataAccess();
            //DataAccessUsingSp dataAccessUsingSp = new DataAccessUsingSp();
            //_dataAccessUsingSp = new DataAccessUsingSp();
            return _dataAccessUsingSp.GetEmployeeByIdAsync(id).Result;
        }

        public int UpdateEmployeeAsync(Employee employee, int id)
        {
            //DataAccess dataAccess = new DataAccess();
            //DataAccessUsingSp dataAccessUsingSp = new DataAccessUsingSp();
            //_dataAccessUsingSp = new DataAccessUsingSp();
            return _dataAccessUsingSp.UpdateEmployeeAsync(employee, id).Result;
        }
    }
}
