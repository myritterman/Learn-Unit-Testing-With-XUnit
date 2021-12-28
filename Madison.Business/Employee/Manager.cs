namespace Madison.Business.Employee;

public class Manager : IEmployee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsWorkingFullTime { get; set; }
    public bool IsAdmin  => true;
}
