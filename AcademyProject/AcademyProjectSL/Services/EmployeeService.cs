using AcademyProjectDL.Repositories.MsSQL;
using AcademyProjectModels.Users;
using AcademyProjectSL.Interfaces;

namespace AcademyProjectSL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees() => await _employeeRepository.GetAllEmployees();

        public async Task<Employee> AddEmployee(Employee employee)
        {
            if (employee == null) return null;

            var authorExist = await _employeeRepository.GetEmployeeById(employee.EmployeeID);
            if (authorExist != null) return null;

            return await _employeeRepository.AddEmployee(employee);
        }

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            if (employeeId <= 0) return null;
            return await _employeeRepository.DeleteEmployee(employeeId);
        }


        public async Task<Employee> GetEmployeeById(int id)
        {
            if (id <= 0) return null;
            return await _employeeRepository.GetEmployeeById(id);
        }

        public async Task<Employee> GetEmployeeByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var result = await _employeeRepository.GetEmployeeByName(name);
            if (result == null) return null;
            return result;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            if (employee == null) return null;

            var authorExist = await _employeeRepository.GetEmployeeByName(employee.EmployeeName);
            if (authorExist == null) return null;

            return await _employeeRepository.UpdateEmployee(employee);
        }
    }
}
