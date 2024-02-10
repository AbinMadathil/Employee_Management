using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.DTO;
using EmployeeManagement.Models;
using EmployeeManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize(Roles = "Manager,Employee")]
public class LeavesController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public LeavesController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    public IActionResult ApplyForLeave(int employeeId, LeaveDTO leaveDto)
    {
        _employeeService.ApplyLeave(employeeId, leaveDto);
        return Ok(1);
    }

    [HttpGet]
    public IActionResult GetEmployeeLeaves(int EmployeeId)
    {
        var leaves = _employeeService.GetLeaves(EmployeeId);
        return Ok(leaves);
    }

    [Authorize(Roles = "Manager")]
    [HttpPost("approve/{employeeId}")]
    public IActionResult ApproveLeave(int employeeId)
    {

        return Ok();
    }
}
