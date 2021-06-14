using CENSO.Models;
using CensoAPI02.Intserfaces;
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
    public class AddRequestController : ControllerBase
    {
        private readonly CDBContext _context;

        public AddRequestController(CDBContext context)
        {
            _context = context;
        }
        // GET: api/<AddRequestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AddRequestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AddRequestController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddRequestInterface value)
        {
            try
            {
                var newRequest = new Request()
                {
                    rUserId = value.rUserId,
                    rEmployeeType = value.rEmployeeType,
                    QuestionId = value.QuestionId,
                    rIssue = value.rIssue,
                    rAttachement = value.rAttachement,
                    rCreationDate = DateTime.UtcNow,
                    AreaId = value.AreaId
                };
                _context.Requests.Add(newRequest);
                await _context.SaveChangesAsync();
                return Ok(newRequest);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AddRequestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AddRequestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
