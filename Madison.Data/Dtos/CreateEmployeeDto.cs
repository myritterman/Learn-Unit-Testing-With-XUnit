using Madison.Data.Models;

namespace Madison.Data.Dtos;

public class CreateEmployeeDto
{
    public Employee Employee { get; set; }
    public int CreatorId { get; set; }
}
