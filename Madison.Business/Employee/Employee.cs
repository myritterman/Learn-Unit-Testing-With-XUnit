using System.Security.Authentication;
using Madison.Data.Repositories;
using static Madison.Business.Helpers;

namespace Madison.Business.Employee;

public class Employee
{
    private readonly IEmployeesRepository _employeesRepository;

    public Employee(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public async Task<Benefits> GetEmployeeBenefits(int employeeId)
    {
        var employee = await GetEmployee(employeeId);
        var benefits = new Benefits
        {
            PaidLunch = employee.IsWorkingFullTime,
            MedicalInsurance = employee.IsWorkingFullTime && employee.StartDate < DateTime.Today.AddDays(-30),
            DaysOff = Math.Min(DifferenceInYearsBetweenTwoDates(DateTime.Today, employee.StartDate) * 7, 21),
        };

        return benefits;
    }

    public async Task<Data.Models.Employee> CreateEmployee(Data.Models.Employee employee, int createdBy)
    {
        var creatorOfNewEmployee = await GetEmployee(createdBy);

        if (!creatorOfNewEmployee.IsAdmin)
        {
            throw new AuthenticationException("You aren't authorized to do this action");
        }

        employee.Id = await _employeesRepository.CreateEmployee(employee);

        return employee;
    }

    private async Task<IEmployee> GetEmployee(int employeeId)
    {
        var creatorOfNewEmployee = await _employeesRepository.GetEmployeeById(employeeId);
        return creatorOfNewEmployee.ConvertDbEmployee();
    }
}
