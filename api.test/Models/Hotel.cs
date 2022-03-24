using System;
using System.Collections.Generic;

#nullable disable

namespace api.test.Models
{
    public partial class Hotel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public byte Category { get; set; }
        public decimal? Price { get; set; }
    }
}
