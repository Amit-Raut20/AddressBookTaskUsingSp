using EmployeeDirectory.Dal.Interfaces;
using EmployeeDirectory.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeDirectory.Dal
{
    public class DataAccessUsingSp : IDataAccessUsingSp
    {
        private static IConfiguration? Configuration { get; set; }

        public static string GetConnectionString()
        {
            //var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            //Configuration = builder.Build();

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            return Configuration.GetConnectionString("MyDbConnection");
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            decimal rowsAffected;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                await conn.OpenAsync();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.InsertEmployee";
                    command.Connection = conn;

                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Mobile", employee.Mobile);
                    command.Parameters.AddWithValue("@Landline", employee.Landline);
                    command.Parameters.AddWithValue("@Website", employee.Website);
                    command.Parameters.AddWithValue("@Address", employee.Address);

                    //rowsAffected = command.ExecuteNonQuery();
                    rowsAffected = (decimal)await command.ExecuteScalarAsync();
                }
            }

            return Convert.ToInt32(rowsAffected);
        }

        public async Task<int> DeleteEmployeeAsync(int id)
        {
            int rowsAffected;
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                await conn.OpenAsync();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.DeleteEmployeeById";
                    command.Connection= conn;
                    command.Parameters.AddWithValue("@Id", id);

                    rowsAffected = await command.ExecuteNonQueryAsync();
                    //await command.ExecuteScalarAsync();
                }
            }
            return rowsAffected;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees;
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                await conn.OpenAsync();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[GetAllEmployees]";
                    command.Connection = conn;
                    reader = await command.ExecuteReaderAsync();


                    employees = new List<Employee>();

                    while (reader.Read())
                    {
                        Console.WriteLine(reader);
                        Employee emp = new Employee
                        {
                            Id = reader["Id"].ToString(),
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
            Employee employee = null;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                await conn.OpenAsync();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.GetEmployeeById";
                    command.Connection = conn;
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
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

        public async Task<int> UpdateEmployeeAsync(Employee employee, int id)
        {
            int rowsAffected;
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                await conn.OpenAsync();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.UpdateEmployee";
                    command.Connection = conn;


                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Mobile", employee.Mobile);
                    command.Parameters.AddWithValue("@Landline", employee.Landline);
                    command.Parameters.AddWithValue("@Website", employee.Website);
                    command.Parameters.AddWithValue("@Address", employee.Address);
                    command.Parameters.AddWithValue("@Id", id);

                    rowsAffected = await command.ExecuteNonQueryAsync();
                    //rowsAffected = (int)await command.ExecuteScalarAsync();


                    if (rowsAffected == 0)
                        return 0;
                }
            }
            return rowsAffected;
        }
    }
}
