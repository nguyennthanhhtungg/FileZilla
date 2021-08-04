using System;
using System.Collections.Generic;

#nullable disable

namespace FileZilla.Models
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public string ProductCode { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public double Price { get; set; }
        public float? Discount { get; set; }
        public double? Weight { get; set; }
        public int Number { get; set; }
        public string AttachedGift { get; set; }
        public string Origin { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public double Tax { get; set; }
        public string ProductNameNoSign { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
