using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.test.Models;
using api.test.Models.ModelsDTO;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        // GET: api/<HotelController>
        [HttpGet]
        public async Task<ActionResult<List<Hotel>>> Get()
        {
            using testContext db = new();
            List<Hotel> hotels = await Task.Run(() => db.Hotels.ToList());
            return Ok(hotels);
            
        }

        // GET api/<HotelController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> Get(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid parameter" });

            using testContext db = new();
            Hotel hotel = await Task.Run(() => db.Hotels.Where(x => x.HotelId == id).FirstOrDefault());

            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        [HttpGet("/Category/{category}")]
        public async Task<ActionResult<Hotel>> GetByCategory(int category)
        {
            if (category == 0)
                return BadRequest(new { message = "Invalid parameter" });

            using testContext db = new();
            List<Hotel> hotel = await Task.Run(() => db.Hotels.Where(x => x.Category == category).ToList());
            return Ok(hotel);
        }

        [HttpGet("/Rating/{rating}")]
        public async Task<ActionResult<Hotel>> GetByRating(int rating)
        {
            if (rating == 0)
                return BadRequest(new { message = "Invalid parameter" });

            using testContext db = new();
            List<Hotel> hotels = await Task.Run(() => (from h in db.Hotels
                                  join r in db.CustomerRatings on h.HotelId equals r.HotelId
                                  where r.Rating == rating
                                  select new Hotel
                                  {
                                      HotelId = h.HotelId,
                                      HotelName = h.HotelName,
                                      Category = h.Category,
                                      Price = h.Price
                                  }).ToList());

            if (hotels == null)
                return NotFound();

            return Ok(hotels);
        }

        [HttpGet("/order/{type}")]
        public async Task<ActionResult<List<Hotel>>> GetSortByPrice(string type)
        {
            if (type.Trim() == string.Empty)
                return BadRequest(new { message = "Parameter type is required" });

            type = type.ToUpper();

            using testContext db = new();
            List<Hotel> hotels = await Task.Run(() => db.Hotels.ToList());

            if (hotels == null)
                return NotFound();

            if (type == "ASC")
                hotels = hotels.OrderBy(x => x.Price).ToList();
            else if (type == "DESC")
                hotels = hotels.OrderByDescending(x => x.Price).ToList();

            return Ok(hotels);
        }

        // POST api/<HotelController>
        [HttpPost]
        public async Task<ActionResult> Post(HotelsDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Hotel hotel = new()
                {
                    HotelName = value.HotelName,
                    Category = value.Category,
                    Price = value.Price
                };
                using testContext db = new();
                await Task.Run(() => db.Hotels.Add(hotel));
                await db.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = hotel.HotelId }, hotel);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT api/<HotelController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, HotelsDTO value)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid parameter" });

            try
            {
                using testContext db = new();
                Hotel hotel = await db.Hotels.FindAsync(id);

                if (hotel == null)
                    return NotFound();

                hotel.HotelName = value.HotelName;
                hotel.Category = value.Category;
                hotel.Price = value.Price;

                await db.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // DELETE api/<HotelController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid Parameter" });

            Hotel hotel = new() { HotelId = id };

            try
            {
                using testContext db = new();
                db.Hotels.Remove(hotel);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
