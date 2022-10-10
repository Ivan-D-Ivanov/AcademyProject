using AcademyProjectModels.Users;

namespace AcademyProjectSL.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeById(int id);
        Task<Employee?> GetEmployeeByName(string name);
        Task<Employee?> AddEmployee(Employee employee);
        Task<Employee?> DeleteEmployee(int employeeId);
        Task<Employee?> UpdateEmployee(Employee employee);
    }
}
