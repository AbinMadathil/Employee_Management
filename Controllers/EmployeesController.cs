using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.DTO;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeManagement.Controllers;
[ApiController]
[Route("api/[controller]")]
// [Authorize(Roles = "Manager")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeesController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    public IActionResult AddEmployee(EmployeeDTO employee)
    {
        try
        {
            var emp = _employeeService.AddEmployee(employee);
            return Ok(new { EmployeeId = emp.Id });
        }
        catch (Exception e)
        {
            // Log the exception details (consider using a logging library like Serilog, NLog, or built-in ASP.NET Core logging)
            Console.Error.WriteLine($"Error adding employee: {e.Message}");

            // Return a more informative response
            return StatusCode(500, new { ErrorMessage = "Internal server error. Please try again later." });
        }
    }

    [HttpGet]
    public IActionResult GetAllEmployees()
    {
        var employees = _employeeService.GetAllEmployees();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployeeById(int id)
    {
        var employee = _employeeService.GetEmployeeById(id);
        if (employee == null)
            return NotFound();

        return Ok(employee);
    }
}
