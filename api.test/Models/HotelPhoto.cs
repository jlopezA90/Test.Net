using System;
using System.Collections.Generic;

#nullable disable

namespace api.test.Models
{
    public partial class HotelPhoto
    {
        public int PhotoId { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
