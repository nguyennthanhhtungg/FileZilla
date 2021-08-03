using System;
using System.Collections.Generic;

#nullable disable

namespace FileZilla.Models
{
    public partial class ExtraFee
    {
        public int ExtraFeeId { get; set; }
        public int FromHour { get; set; }
        public int FromMinute { get; set; }
        public int ToHour { get; set; }
        public int ToMinute { get; set; }
        public double ExtraDeliveryFee { get; set; }
    }
}
