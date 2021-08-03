using System;
using System.Collections.Generic;

#nullable disable

namespace FileZilla.Models
{
    public partial class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Location { get; set; }
        public string LogoUrl { get; set; }
        public string Discription { get; set; }
    }
}
