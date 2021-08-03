using System;
using System.Collections.Generic;

#nullable disable

namespace FileZilla.Models
{
    public partial class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
