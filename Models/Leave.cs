using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Leave
    {

        public Leave()
        {
            Leave_Id = identity_leave++;
        }
        public static int identity_leave = 100;
        public int Leave_Id { get; set; }
        public int Emp_id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string status { get; set; } = "Pending";
    }
}