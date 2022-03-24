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
    public class RatingsController : ControllerBase
    {
        // GET: api/<RatingsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RatingsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CustomerRating>>> Get(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid parameter" });

            using testContext db = new();
            List<CustomerRating> ratings = await Task.Run(() => db.CustomerRatings.Where(x => x.HotelId == id).ToList());

            if (ratings == null)
                return NotFound();

            return Ok(ratings);
        }

        // POST api/<RatingsController>
        [HttpPost]
        public async Task<ActionResult<CustomerRating>> Post(CustomerRating value)
        {
            using testContext db = new();
            db.CustomerRatings.Add(value);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = value.HotelId }, value);
        }

        // PUT api/<RatingsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RatingsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
