using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly CDBContext _context;

        public AreasController(CDBContext context)
        {
            _context = context;
        }
    
        // GET: api/<AreasController>
            [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AreasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AreasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddArea value)
        {
            try
            {
                var newArea = new Area()
                {
                    aName = value.aName,
                    locationId = value.LocationId
                };
                _context.Areas.Add(newArea);
                await _context.SaveChangesAsync();
                return Ok(newArea);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AreasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AreasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
