using Madison.Business.Employee;
using Madison.Data.Enums;

namespace Madison.Business;

public static class Extensions
{
    public static IEmployee ConvertDbEmployee(this Data.Models.Employee employee)
    {
        return employee.JobTitleId switch
        {
            EmployeeTypes.Standard => ConvertDbEmployee(employee, new StandardEmployee()),
            EmployeeTypes.ManagerAssistant => ConvertDbEmployee(employee, new ManagerAssistant()),
            EmployeeTypes.Manager => ConvertDbEmployee(employee, new Manager()),
            EmployeeTypes.SeniorManager => ConvertDbEmployee(employee, new SeniorManager()),
            _ => throw new Exception("Job title not recognized")
        };
    }

    private static IEmployee ConvertDbEmployee(Data.Models.Employee dbEmployee, IEmployee employee)
    {
        employee.Id = dbEmployee.Id;
        employee.FirstName = dbEmployee.FirstName;
        employee.LastName = dbEmployee.LastName;
        employee.StartDate = dbEmployee.StartDate;
        employee.IsWorkingFullTime = dbEmployee.IsWorkingFullTime;
        return employee;
    }
}
