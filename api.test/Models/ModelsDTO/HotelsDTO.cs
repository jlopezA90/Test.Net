using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.test.Models.ModelsDTO
{
    public class HotelsDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string HotelName { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public byte Category { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal? Price { get; set; }
    }
}
