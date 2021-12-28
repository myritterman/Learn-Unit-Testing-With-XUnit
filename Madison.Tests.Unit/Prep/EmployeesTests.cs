using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using FluentAssertions;
using Madison.Business.Employee;
using Madison.Data.Enums;
using Madison.Data.Repositories;
using NSubstitute;
using Xunit;

namespace Employees.Tests.Unit.Prep;

public class EmployeesTests
{
    private readonly Employee _sut;
    private readonly IEmployeesRepository _employeesRepository = Substitute.For<IEmployeesRepository>();

    public EmployeesTests()
    {
        _sut = new Employee(_employeesRepository);
    }

    // [Fact(Skip = "temporarily disabled")]
    [Fact]
    public async Task SetEmployeeBenefits_ShouldHaveAllBenefits_WhenWorkingForTenYears()
    {
        // arrange
        _employeesRepository.GetEmployeeById(Arg.Any<int>()).Returns(new Madison.Data.Models.Employee
        {
            FirstName = "John",
            LastName = "Doe",
            JobTitleId = EmployeeTypes.Manager,
            StartDate = new DateTime(2010, 01, 01),
            IsWorkingFullTime = true
        });

        var benefits = new Benefits
        {
            PaidLunch = true,
            DaysOff = 21,
            MedicalInsurance = true,
        };

        // act
        var result = await _sut.GetEmployeeBenefits(1);

        //assert
        result.Should().BeEquivalentTo(benefits);
    }

    [Fact]
    public async Task CreateEmployee_ShouldReturnSuccessfully_WhenManagerIsCreatingAUser()
    {
        // arrange
        var employeeBeingCreated = new Madison.Data.Models.Employee
        {
            FirstName = "John",
            LastName = "Doe",
            JobTitleId = EmployeeTypes.Standard,
            StartDate = new DateTime(2016, 09, 01),
            IsWorkingFullTime = true,
        };
        _employeesRepository.GetEmployeeById(Arg.Any<int>()).Returns(new Madison.Data.Models.Employee { JobTitleId = EmployeeTypes.Manager });
        _employeesRepository.CreateEmployee(Arg.Any<Madison.Data.Models.Employee>()).Returns(5);


        // act
        var result = await _sut.CreateEmployee(employeeBeingCreated, 1);

        //assert
        result.Should().Be(employeeBeingCreated);
    }

    [Fact]
    public async Task CreateEmployee_ShouldThrowException_WhenStandardEmployeeIsCreatingAUser()
    {
        // arrange
        var employeeBeingCreated = new Madison.Data.Models.Employee
        {
            FirstName = "John",
            LastName = "Doe",
            JobTitleId = EmployeeTypes.Standard,
            StartDate = new DateTime(2016, 09, 01),
            IsWorkingFullTime = true,
        };
        _employeesRepository.GetEmployeeById(Arg.Any<int>()).Returns(new Madison.Data.Models.Employee { JobTitleId = EmployeeTypes.Standard });
        _employeesRepository.CreateEmployee(Arg.Any<Madison.Data.Models.Employee>()).Returns(5);

        // act
        var result = async () => await _sut.CreateEmployee(employeeBeingCreated, 1);

        //assert
        await result.Should().ThrowAsync<AuthenticationException>();
    }
}
