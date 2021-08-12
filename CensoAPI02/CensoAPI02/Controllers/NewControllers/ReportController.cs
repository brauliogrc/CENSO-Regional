using CENSO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Controllers.NewControllers
{
    [Route("api/[controller]")]
    [ApiController][Authorize(Policy = "SURH")]
    public class ReportController : ControllerBase
    {
        private readonly CDBContext _context;

        public ReportController( CDBContext context)
        {
            _context = context;
        }

        [HttpGet][Route("ticketsReport/{locationId}")][AllowAnonymous]
        public async Task<ActionResult> getTicketReport(int locationId)
        {
            try
            {
                var tickets = from request in _context.Requests
                              join user in _context.HRU on request.rUserId equals user.uEmployeeNumber
                              join theme in _context.Theme on request.ThemeId equals theme.tId
                              join question in _context.Questions on request.QuestionId equals question.qId
                              join location in _context.Locations on request.LocationId equals location.lId
                              join area in _context.Areas on request.AreaId equals area.aId
                              join status in _context.RequestStatus on request.StatusId equals status.rsId
                              join answer in _context.Answer on request.rId equals answer.RequestId
                              where location.lId == locationId //&& request.StatusId != 4
                              select new
                              {
                                  
                              };

                var anonTickets = from anonReq in _context.AnonRequests
                                  join theme in _context.Theme on anonReq.ThemeId equals theme.tId
                                  join question in _context.Questions on anonReq.QuestionId equals question.qId
                                  join location in _context.Locations on anonReq.LocationId equals location.lId
                                  join area in _context.Areas on anonReq.AreaId equals area.aId
                                  join status in _context.RequestStatus on anonReq.StatusId equals status.rsId
                                  join answer in _context.Answer on anonReq.arId equals answer.AnonRequestId
                                  where location.lId == locationId //&& anonReq.StatusId != 4
                                  select new
                                  {
                                      
                                  };

                if ((tickets == null || tickets.Count() == 0) && (anonTickets == null || anonTickets.Count() == 0))
                {
                    return NotFound(new { message = $"Ningun ticket se encuentra asociado con tu localidad" });
                }

                return Ok(new { tickets, anonTickets });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener la lista de tickets. Error: {ex.Message}" });
            }
        }
    }
}
