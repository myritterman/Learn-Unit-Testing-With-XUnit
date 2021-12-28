using Madison.Data.Enums;

namespace Madison.Data.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public EmployeeTypes JobTitleId { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsWorkingFullTime { get; set; }
}
