using CENSO.Models;
using CensoAPI02.Intserfaces;
using Microsoft.AspNetCore.Authorization;
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
    [ApiController][Authorize]
    public class LocationsController : ControllerBase
    {
        private readonly CDBContext _context;

        public LocationsController(CDBContext context)
        {
            _context = context;
        }

        // POST api/<LocationsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddLocationsInterface value)
        {
            try
            {
                var newLocation = new Locations() // AGREGAR CREATIONUSER
                {
                    lName = value.lName,
                    lCreationDate = DateTime.Now,
                    //lCreationuser = 2
                };
                _context.Locations.Add(newLocation);
                await _context.SaveChangesAsync();

                Console.WriteLine(newLocation.lId);
                return Ok(newLocation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<LocationsController>/5
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var query = await _context.Locations.FindAsync(id);
                if(query == null)
                {
                    return NotFound();
                }

                query.lStatus = false;
                _context.Locations.Update(query);
                await _context.SaveChangesAsync();

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
