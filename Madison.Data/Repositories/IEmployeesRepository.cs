using Madison.Data.Models;

namespace Madison.Data.Repositories;

public interface IEmployeesRepository
{
    Task<Employee> GetEmployeeById(int id);
    Task<int> CreateEmployee(Employee employee);
}
