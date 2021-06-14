using CENSO.Models;
using CensoAPI02.Intserfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly CDBContext _context;

        public LocationsController(CDBContext context)
        {
            _context = context;
        }

        // GET: api/<LocationsController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            // Obtencion de todas las localidades
            try
            {
                var locations = await _context.Locations.Select(l => new { l.lId, l.lName, l.lStatus }).ToListAsync();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<LocationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LocationsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddLocations value)
        {
            try
            {
                var newLocation = new Locations()
                {
                    lName = value.lName,
                    lStatus = value.lStatus,
                    lCreationDate = DateTime.Now,
                    //lCreationuser = 2
                };
                _context.Locations.Add(newLocation);
                await _context.SaveChangesAsync();
                return Ok(newLocation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<LocationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LocationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
