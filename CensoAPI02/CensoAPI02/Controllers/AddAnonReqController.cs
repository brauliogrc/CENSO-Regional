using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models;
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
    [ApiController][AllowAnonymous]
    public class AddAnonReqController : ControllerBase
    {
        private readonly CDBContext _context;

        public AddAnonReqController( CDBContext context )
        {
            _context = context;
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
                    arCreationDate = DateTime.Now,
                    QuestionId = newAnomReq.QuestionId,
                    ThemeId = newAnomReq.ThemeId,
                    LocationId = newAnomReq.LocationId
                    
                };

                newAnonRequest.StatusId = 1;

                _context.AnonRequests.Add(newAnonRequest);
                await _context.SaveChangesAsync();
                return Ok(newAnonRequest);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
