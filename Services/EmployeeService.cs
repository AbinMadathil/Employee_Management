using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeManagement.DTO;
using EmployeeManagement.Models;
using EmployeeManagementApi.Models;
using Microsoft.Extensions.Options;

namespace EmployeeManagementApi.Services
{
    public class EmployeeService
    {
        private readonly List<Employee> _employees = new List<Employee>();
        private readonly string _dataFilePath = "employeeData.json";

        public EmployeeService()
        {
            LoadDataFromFile();
        }

        public Employee AddEmployee(EmployeeDTO empDTO)
        {
            Employee emp = new Employee
            {
                Id = Employee.identity,
                Name = empDTO.Name,
                Role = empDTO.Role

            };
            _employees.Add(emp);
            SaveDataToFile();
            return emp;
        }

        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee? GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        private void LoadDataFromFile()
        {
            if (File.Exists(_dataFilePath))
            {
                var jsonData = File.ReadAllText(_dataFilePath);
                _employees.AddRange(JsonSerializer.Deserialize<List<Employee>>(jsonData));
            }
        }

        public List<Leave> GetLeaves(int empId)
        {
            var empDetails = _employees.FirstOrDefault(option => option.Id == empId);
            if (empDetails != null)
            {
                var LeaveArray = empDetails.leaves.ToList();
                return LeaveArray;
            }
            return [];
        }

        public void ApplyLeave(int empId, LeaveDTO leaveDto)
        {
            var empDetails = _employees.FirstOrDefault(option => option.Id == empId);
            if (empDetails != null)
            {

                empDetails.leaves.Add(new Leave
                {
                    Emp_id = empId,
                    StartDate = leaveDto.StartDate,
                    EndDate = leaveDto.EndDate,
                    status = leaveDto.status
                });
            }
            return;

        }

        public List<Leave> PendingLeaves(int empId)
        {
            var empDetails = _employees.FirstOrDefault(option => option.Id == empId);
            if (empDetails != null)
            {
                var PendingLeaves = empDetails.leaves.Where(leave => leave.status == "Pending").ToList();
                return PendingLeaves;
            }
            return [];
        }

        private void SaveDataToFile()
        {
            var jsonData = JsonSerializer.Serialize(_employees);
            File.WriteAllText(_dataFilePath, jsonData);
        }
    }
}