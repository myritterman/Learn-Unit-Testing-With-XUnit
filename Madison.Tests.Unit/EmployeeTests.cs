using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using FluentAssertions;
using Madison.Business.Employee;
using Madison.Data.Enums;
using Madison.Data.Repositories;
using NSubstitute;
using Xunit;

namespace Employees.Tests.Unit;

public class EmployeeTests
{
    private readonly Employee _sut;
    private readonly IEmployeesRepository _employeesRepository = Substitute.For<IEmployeesRepository>();
    
    public EmployeeTests()
    {
        _sut = new Employee(_employeesRepository);
    }

    [Fact]
    public void CreateEmployee_Should_ThrowException_WhenStandardEmployeeCreates()
    {
        // arrange
        _employeesRepository.GetEmployeeById(Arg.Any<int>()).Returns(new Madison.Data.Models.Employee
        {
            Id = 0,
            FirstName = "John",
            LastName = "Doe",
            JobTitleId = (EmployeeTypes)1,
            StartDate = new DateTime(2021, 12, 25),
            IsWorkingFullTime = true
        });

        _employeesRepository.CreateEmployee(new Madison.Data.Models.Employee()).Returns(5);

        var newEmployee = new Madison.Data.Models.Employee
        {
            Id = 0,
            FirstName = "John",
            LastName = "Doe",
            JobTitleId = (EmployeeTypes)1,
            StartDate = new DateTime(2021, 12, 25),
            IsWorkingFullTime = true,
        };

        // act
       var results = () => _sut.CreateEmployee(newEmployee, 23);

        //assert
        results.Should().ThrowAsync<AuthenticationException>().WithMessage("You aren't authorized to do this action");
    }

    [Fact]
    public async Task CreateEmployee_ShouldCreateEmployee_WhenManagerEmployeeCreates()
    {
        // arrange
        _employeesRepository.GetEmployeeById(Arg.Any<int>()).Returns(new Madison.Data.Models.Employee
        {
            Id = 0,
            FirstName = "John",
            LastName = "Doe",
            JobTitleId = EmployeeTypes.Manager,
            StartDate = new DateTime(2021, 12, 25),
            IsWorkingFullTime = true
        });

        var newEmployeeId = 5;
        _employeesRepository.CreateEmployee(new Madison.Data.Models.Employee()).Returns(newEmployeeId);

        var newEmployee = new Madison.Data.Models.Employee
        {
            Id = newEmployeeId,
            FirstName = "John",
            LastName = "Doe",
            JobTitleId = (EmployeeTypes)1,
            StartDate = new DateTime(2021, 12, 25),
            IsWorkingFullTime = true,
        };

        // act
       var results = await _sut.CreateEmployee(newEmployee, 23);

        //assert
        results.Should().Be(newEmployee);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(-5, 2, -3)]
    [InlineData(100, 2, 102)]
    public void Add_(int a, int b, int expectedResult)
    {
        var result = Add(a, b);
        result.Should().Be(expectedResult);
    }


    public int Add(int a, int b)
    {
        return a + b;
    }
}
