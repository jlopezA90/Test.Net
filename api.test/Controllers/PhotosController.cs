using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.test.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        // GET: api/<PhotosController>
        [HttpGet]
        public async Task<ActionResult<List<HotelPhoto>>> Get()
        {
            using testContext db = new();
            List<HotelPhoto> photos = await Task.Run(() => db.HotelPhotos.ToList());

            if (photos == null)
                return NotFound();

            return Ok(photos);
        }

        // GET api/<PhotosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<HotelPhoto>>> Get(int id)
        {
            using testContext db = new();
            List<HotelPhoto> photos = await Task.Run(() => db.HotelPhotos.Where(x => x.HotelId == id).ToList());

            if (photos == null)
                return NotFound();

            return Ok(photos);
        }

        // POST api/<PhotosController>
        [HttpPost]
        public async Task<ActionResult<HotelPhoto>> Post(HotelPhoto value)
        {
            using testContext db = new();
            await db.HotelPhotos.AddAsync(value);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = value.HotelId }, value);
        }

        // PUT api/<PhotosController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, HotelPhoto value)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid parameter" });

            using testContext db = new();
            HotelPhoto photo = await db.HotelPhotos.FindAsync(id);

            if (photo == null)
                return NotFound();

            photo.Url = value.Url;
            photo.Name = value.Name;

            await db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<PhotosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
