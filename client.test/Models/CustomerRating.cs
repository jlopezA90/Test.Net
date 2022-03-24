using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace client.test.Models
{
    public class CustomerRating
    {
        public int ClientId { get; set; }
        public int HotelId { get; set; }
        public string UserName { get; set; }
        public byte Rating { get; set; }
    }
}
