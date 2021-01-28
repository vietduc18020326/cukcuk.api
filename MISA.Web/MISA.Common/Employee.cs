using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Common
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public int Gender { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IndentifyNumber { get; set; }
        public string ReleaseLocation { get; set; }
        public DateTime ReleaseDay { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public int StatusJob { get; set; }
        public Guid EmployeePositionId { get; set; }
        public Guid EmployeeDepartmentId { get; set; }
        public string EmployeeTaxCode { get; set; }
    }
}
