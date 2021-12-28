using Madison.Business.Employee;
using Madison.Data.Dtos;
using Madison.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IEmployeesRepository>(serviceProvider =>
    new EmployeesRepository(serviceProvider.GetService<IConfiguration>().GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("Employee-Benefits/{employeeId:int}", async ([FromRoute] int employeeId, IEmployeesRepository employeesRepository) =>
{
    var employee = new Employee(employeesRepository);
    var benefits = await employee.GetEmployeeBenefits(employeeId);
    
    return Results.Ok(benefits);
});

app.MapPost("/Create-Employee", async ([FromBody] CreateEmployeeDto createEmployeeDto, IEmployeesRepository employeesRepository) =>
{
    var employee = new Employee(employeesRepository);
    await employee.CreateEmployee(createEmployeeDto.Employee, createEmployeeDto.CreatorId);
});

app.Run();
