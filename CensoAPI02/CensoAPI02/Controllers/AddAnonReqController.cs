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
    public class AddAnonReqController : ControllerBase
    {
        private readonly CDBContext _context;

        public AddAnonReqController( CDBContext context )
        {
            _context = context;
        }
        // GET: api/<AddAnonReqController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AddAnonReqController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AddAnonReqController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddAnonRequestInterface newAnomReq)
        {
            try
            {
                var newAnonRequest = new AnonRequest()
                {
                    AreaId = newAnomReq.AreaId,
                    arEmployeeType = newAnomReq.arEmployeeType,
                    arIssue = newAnomReq.arIssue,
                    arAttachement = newAnomReq.arAttachemen,
                    arCreationDate = DateTime.UtcNow,
                    // arModificationDate = DateTime.UtcNow,
                    QuestionId = newAnomReq.QuestionId,
                };
                _context.AnonRequests.Add(newAnonRequest);
                await _context.SaveChangesAsync();
                return Ok(newAnonRequest);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AddAnonReqController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AddAnonReqController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
