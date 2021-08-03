using System;
using System.Collections.Generic;

#nullable disable

namespace FileZilla.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsLocked { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string CustomerNo { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
