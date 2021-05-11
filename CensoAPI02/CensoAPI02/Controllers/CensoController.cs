using CENSO.Models;
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
    public class CensoController : ControllerBase
    {
        private readonly CDBContext _context;

        //Creamos el controlador y le inyectamos las dependencias
        public CensoController(CDBContext context)
        {
            _context = context;
        }

        // GET: api/<CensoController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            //Cambiar IEnumerable <> por un Task<ActionResult>
            //return new string[] { "value1", "value2" };
            try
            {
                var ListUsers = await _context.HR_Users.ToListAsync();
                return Ok( ListUsers );
            }catch ( Exception error)
            {
                return BadRequest( error.Message );
            }
        }

        // GET api/<CensoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CensoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CensoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CensoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
