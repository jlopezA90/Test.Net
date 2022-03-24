using System;
using System.Collections.Generic;

#nullable disable

namespace api.test.Models
{
    public partial class CustomerRating
    {
        public int ClientId { get; set; }
        public int HotelId { get; set; }
        public string UserName { get; set; }
        public byte? Rating { get; set; }
    }
}
