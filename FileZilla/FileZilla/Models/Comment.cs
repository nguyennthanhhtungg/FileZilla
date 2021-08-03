using System;
using System.Collections.Generic;

#nullable disable

namespace FileZilla.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int Rate { get; set; }
        public string Review { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
