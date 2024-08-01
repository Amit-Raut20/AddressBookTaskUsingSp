using EmployeeDirectory.Dal.Interfaces;
using EmployeeDirectory.Models;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Data.SqlClient;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System.Reflection;

namespace EmployeeDirectory.Dal
{
    public class DataAccess : IDataAccess
    {
        //private string? connectionString;

        //public DataAccess(IConfiguration configuration)
        //{
        //    connectionString = configuration.GetConnectionString("MyDbConnection");
        //}
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            int rowsAffected;
            Employee insertedEmployee;

            using (SqlConnection conn = new SqlConnection("Data Source=L-IT-0017\\MSSQLSERVER03;Initial Catalog=empDetails;Integrated Security=True;Encrypt=False"))
            {
                await conn.OpenAsync();

                //string query = "INSERT INTO AddressBook (Name, Email, Mobile, Landline, Website, Address) " +
                //               "VALUES (@Name, @Email, @Mobile, @Landline, @Website, @Address)";

                //string query = "INSERT INTO AddressBook (Name, Email, Mobile, Landline, Website, Address) "+
                //       "VALUES (@Name, @Email, @Mobile, @Landline, @Website, @Address)";

                string query = "INSERT INTO AddressBook (Name, Email, Mobile, Landline, Website, Address) " +
                       "OUTPUT INSERTED.Id " +
                       "VALUES (@Name, @Email, @Mobile, @Landline, @Website, @Address)";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Mobile", employee.Mobile);
                    command.Parameters.AddWithValue("@Landline", employee.Landline);
                    command.Parameters.AddWithValue("@Website", employee.Website);
                    command.Parameters.AddWithValue("@Address", employee.Address);


                    //rowsAffected = command.ExecuteNonQuery();
                    //Console.WriteLine((int)await command.ExecuteScalarAsync());
                    rowsAffected = (int)await command.ExecuteScalarAsync();
                    Console.WriteLine(rowsAffected);
                }

                insertedEmployee = GetEmployeeByIdAsync(rowsAffected).Result;
            }

            return insertedEmployee;
        }

        public async Task<int> DeleteEmployeeAsync(int id)
        {
            int rowsAffected;
            using (SqlConnection conn = new SqlConnection("Data Source=L-IT-0017\\MSSQLSERVER03;Initial Catalog=empDetails;Integrated Security=True;Encrypt=False"))
            {
                await conn.OpenAsync();

                string query = "DELETE FROM AddressBook OUTPUT DELETED.Id, DELETED.Name, DELETED.Email, DELETED.Mobile, DELETED.Landline, DELETED.Website, DELETED.Address WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    //int rowsAffected = await command.ExecuteNonQueryAsync();
                    await command.ExecuteScalarAsync();
                }
            }
            return id;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees;
            SqlDataReader reader;
            using (SqlConnection conn=new SqlConnection("Data Source=L-IT-0017\\MSSQLSERVER03;Initial Catalog=empDetails;Integrated Security=True;Encrypt=False"))
            {
                await conn.OpenAsync();

                string query = "SELECT * FROM AddressBook";

                using(SqlCommand command = new SqlCommand(query,conn))
                {
                    reader = await command.ExecuteReaderAsync();

                    employees = new List<Employee>();

                    while(reader.Read())
                    {
                        Console.WriteLine(reader);
                        Employee emp = new Employee
                        {
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Mobile = reader["Mobile"].ToString(),
                            Landline = reader["Landline"].ToString(),
                            Website = reader["Website"].ToString(),
                            Address = reader["Address"].ToString()
                        };
                        employees.Add(emp);
                    }
                }
            }

            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            Employee employee=null;

            using (SqlConnection conn = new SqlConnection("Data Source=L-IT-0017\\MSSQLSERVER03;Initial Catalog=empDetails;Integrated Security=True;Encrypt=False"))
            {
                await conn.OpenAsync();

                string selectQuery = "SELECT * FROM AddressBook WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(selectQuery, conn))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee = new Employee
                            {
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Mobile = reader["Mobile"].ToString(),
                                Landline = reader["Landline"].ToString(),
                                Website = reader["Website"].ToString(),
                                Address = reader["Address"].ToString()
                            };
                        }                        
                    }

                }
            }
            return employee;
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee, int id)
        {
            int rowsAffected;
            using (SqlConnection conn = new SqlConnection("Data Source=L-IT-0017\\MSSQLSERVER03;Initial Catalog=empDetails;Integrated Security=True;Encrypt=False"))
            {
                await conn.OpenAsync();

                string query = "UPDATE AddressBook " +
                               "SET Name = @Name, " +
                               "    Email = @Email, " +
                               "    Mobile = @Mobile, " +
                               "    Landline = @Landline, " +
                               "    Website = @Website, " +
                               "    Address = @Address " +
                               "OUTPUT INSERTED.Id "+
                               "WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Mobile", employee.Mobile);
                    command.Parameters.AddWithValue("@Landline", employee.Landline);
                    command.Parameters.AddWithValue("@Website", employee.Website);
                    command.Parameters.AddWithValue("@Address", employee.Address);
                    command.Parameters.AddWithValue("@Id", id);

                    //rowsAffected = await command.ExecuteNonQueryAsync();
                    rowsAffected = (int)await command.ExecuteScalarAsync();

                    Console.WriteLine(rowsAffected);


                    if (rowsAffected == 0)
                        return null;
                }
            }
            return await GetEmployeeByIdAsync(rowsAffected);
        }
    }
}
