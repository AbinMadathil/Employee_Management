using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using Microsoft.Net.Http.Headers;

namespace EmployeeManagementApi.Models
{
    public class Employee
    {
        public Employee()
        {
            Id = ++identity;
        }
        public static int identity { get; set; } = 10;
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;

        public ICollection<Leave> leaves { get; set; } = [];
    }
}