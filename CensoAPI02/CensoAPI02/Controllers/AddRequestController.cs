using CENSO.Models;
using CensoAPI02.Intserfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController][Authorize]
    public class AddRequestController : ControllerBase
    {
        private readonly CDBContext _context;

        public AddRequestController(CDBContext context)
        {
            _context = context;
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
                    rUserName = value.rUserName,
                    rEmployeeType = value.rEmployeeType,
                    QuestionId = value.QuestionId,
                    rIssue = value.rIssue,
                    rAttachement = value.rAttachement,
                    rCreationDate = DateTime.Now,
                    AreaId = value.AreaId,
                    ThemeId = value.ThemeId,
                    LocationId = value.LocationId,
                    StatusId = 1
                };

                _context.Requests.Add(newRequest);
                await _context.SaveChangesAsync();
                return Ok(newRequest);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
