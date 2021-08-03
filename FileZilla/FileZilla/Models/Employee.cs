using System;
using System.Collections.Generic;

#nullable disable

namespace FileZilla.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsLocked { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmployeeNo { get; set; }
        public string MarriageStatus { get; set; }
        public bool? IsActived { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
