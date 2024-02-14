using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeManagementApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly EmployeeService _employeeservice;

    public LoginController(EmployeeService employeeservice){
        this._employeeservice=employeeservice;
    }

    [HttpPost("SignIn")]
    public IActionResult SignIn(int Emp_Id, string Emp_Role){
       var employee= _employeeservice.GetEmployeeById(Emp_Id);
       if(employee.Role!=Emp_Role){
            return BadRequest("The user is not present");
       }
       var claims=new List<Claim>{
        new Claim(ClaimTypes.NameIdentifier,Emp_Id.ToString()),
        new Claim(ClaimTypes.Role,Emp_Role)
       };
       var identity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
       var principal=new ClaimsPrincipal(identity);
       HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
        return Ok("Log in Successfull");

    }

}
