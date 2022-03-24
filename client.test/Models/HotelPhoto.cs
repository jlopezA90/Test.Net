using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace client.test.Models
{
    public class HotelPhoto
    {
        public int PhotoId { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
