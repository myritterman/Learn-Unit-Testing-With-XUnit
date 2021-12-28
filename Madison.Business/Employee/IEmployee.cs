namespace Madison.Business.Employee;

public interface IEmployee
{
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    DateTime StartDate { get; set; }
    bool IsWorkingFullTime { get; set; }
    bool IsAdmin { get; }
}
