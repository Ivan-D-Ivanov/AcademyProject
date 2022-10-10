using System.Data.SqlClient;
using AcademyProjectModels.Users;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AcademyProjectDL.Repositories.MsSQL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ILogger<EmployeeRepository> _logger;
        private readonly IConfiguration _configuration;

        public EmployeeRepository(ILogger<EmployeeRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    
                    return (await connection
                        .QueryAsync<Employee>
                        ($"INSERT INTO Employee (EmployeeID,NationalIDNumber,EmployeeName,LoginID,JobTitle,BirthDate,MaritalStatus,Gender,HireDate,VacationHours,SickLeaveHours,rowguid,ModifiedDate) VALUES(@EmployeeID,@NationalIDNumber,@EmployeeName,@LoginID,@JobTitle,@BirthDate,@MaritalStatus,@Gender,@HireDate,@VacationHours,@SickLeaveHours,@rowguid,@ModifiedDate)", employee))
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(AddEmployee)} : {e}");
            }

            return null;
        }

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    return (await connection.QueryAsync<Employee>($"DELETE FROM Employee WHERE EmployeeID = @Id", new { Id = employeeId })).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(DeleteEmployee)} : {e}");
            }

            return null;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = $"SELECT * FROM Employee WITH(NOLOCK)";
                    await connection.OpenAsync();

                    var employees = await connection.QueryAsync<Employee>(query);
                    return employees;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllEmployees)} : {e}");
            }

            return Enumerable.Empty<Employee>();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var employee = await connection.QueryFirstOrDefaultAsync<Employee>($"SELECT * FROM Employee WITH(NOLOCK) WHERE Employee.EmployeeID = @EmployeeID", new { EmployeeID = id });
                    return employee;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetEmployeeById)} : {e}");
            }

            return null;
        }

        public async Task<Employee> GetEmployeeByName(string name)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var employee = await connection.QueryFirstOrDefaultAsync<Employee>($"SELECT * FROM Employee WITH(NOLOCK) WHERE Employee.EmployeeName LIKE @EmployeeName", new { EmployeeName = name });
                    return employee;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(GetEmployeeByName)} : {e}");
            }

            return null;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            try
            {
                await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    return (await connection
                        .QueryAsync<Employee>
                        ($"UPDATE UserInfo SET NationalIDNumber=@NationalIDNumber,EmployeeName=@EmployeeName,LoginID=@LoginID,JobTitle=@JobTitle,BirthDate=@BirthDate,MaritalStatus=@MaritalStatus,Gender=@Gender,HireDate=@HireDate,VacationHours=@VacationHours,SickLeaveHours=@SickLeaveHours,rowguid=@rowguid,ModifiedDate=@ModifiedDate WHERE EmployeeID = @EmployeeID", employee))
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(UpdateEmployee)} : {e}");
            }

            return null;
        }
    }
}
