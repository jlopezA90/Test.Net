using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace client.test.Models
{
    public class Hotels
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public byte Category { get; set; }
        public decimal? Price { get; set; }
    }
}
