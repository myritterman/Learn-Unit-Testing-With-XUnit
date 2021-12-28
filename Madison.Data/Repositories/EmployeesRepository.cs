using System.Data.SqlClient;
using System.Security.Authentication;
using Dapper;
using Madison.Data.Enums;
using Madison.Data.Models;

namespace Madison.Data.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly string _connectionString;

    public EmployeesRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Employee> GetEmployeeById(int id)
    {
        const string sql = "SELECT * FROM Employees WHERE Id = @id";

        await using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { id });
    }

    public async Task<int> CreateEmployee(Employee employee)
    {
        const string sql = @"INSERT INTO Employees (FirstName, LastName, JobTitleId, StartDate, IsWorkingFullTime) 
                             VALUES (@FirstName, @LastName, @JobTitleId, @StartDate, @IsWorkingFullTime)
                             SELECT SCOPE_IDENTITY()";

        await using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<int>(sql, new { employee.FirstName, employee.LastName, employee.JobTitleId, employee.StartDate, employee.IsWorkingFullTime });
    }

    public async Task<int> CreateEmployee_Untestable(Employee employee)
    {
        var creatorOfNewEmployee = await GetEmployeeById(employee.Id);

        if (!IsAdmin(creatorOfNewEmployee.JobTitleId))
        {
            throw new AuthenticationException("You aren't authorized to do this action");
        }

        const string sql = @"INSERT INTO Employees (FirstName, LastName, JobTitleId, StartDate, IsWorkingFullTime) 
                             VALUES (@FirstName, @LastName, @JobTitleId, @StartDate, @IsWorkingFullTime)
                             SELECT SCOPE_IDENTITY()";

        await using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<int>(sql, new { employee.FirstName, employee.LastName, employee.JobTitleId, employee.StartDate, employee.IsWorkingFullTime });
    }

    private static bool IsAdmin(EmployeeTypes employeeTypes) => employeeTypes is EmployeeTypes.Manager or EmployeeTypes.SeniorManager;
}
