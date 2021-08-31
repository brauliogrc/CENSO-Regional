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

        [HttpGet][Route("ticketsReport/{locationId}")]
        public async Task<ActionResult> getTicketReport(int locationId)
        {
            try
            {
                /*var lftJoin = from request in _context.Requests
                              join theme in _context.Theme
                                on request.ThemeId equals theme.tId into themeGroup
                              from themeG in themeGroup.DefaultIfEmpty()
                              join question in _context.Questions
                               on request.QuestionId equals question.qId into questionGroup
                              from questionG in questionGroup.DefaultIfEmpty()
                              join location in _context.Locations
                               on request.LocationId equals location.lId into locationGroup
                              from locationG in locationGroup.DefaultIfEmpty()
                              join area in _context.Areas
                               on request.AreaId equals area.aId into areaGroup
                              from areaG in areaGroup.DefaultIfEmpty()
                              join status in _context.RequestStatus
                               on request.StatusId equals status.rsId into statusGroup
                              from statusG in statusGroup.DefaultIfEmpty()
                              join answer in _context.Answer
                               on request.rId equals answer.RequestId into answerGroup
                              from answerG in answerGroup.DefaultIfEmpty()
                              join user in _context.HRU
                               on answerG.UserId equals user.uEmployeeNumber into userGroup
                              from userG in userGroup.DefaultIfEmpty()
                              where request.rId == answerG.RequestId  answerG.RequestId = null
                              select new
                              {

                              };*/

                                

                var tickets = from request in _context.Requests
                              //join user in _context.HRU on request.rUserId equals user.uEmployeeNumber
                              join theme in _context.Theme on request.ThemeId equals theme.tId
                              join question in _context.Questions on request.QuestionId equals question.qId
                              join location in _context.Locations on request.LocationId equals location.lId
                              join area in _context.Areas on request.AreaId equals area.aId
                              join status in _context.RequestStatus on request.StatusId equals status.rsId
                              //join answer in _context.Answer on request.rId equals answer.RequestId
                              where location.lId == locationId //&& request.StatusId != 4
                              select new
                              {
                                  // Datos del ticket
                                  request.rId,
                                  request.rUserId,
                                  request.rUserName,
                                  request.rEmployeeLeader,
                                  request.rEmployeeType,
                                  request.rCreationDate,
                                  // Datos de la localidad
                                  location.lId,
                                  location.lName,
                                  // Datos del usuario
                                  /*user.uEmployeeNumber,
                                  user.uName,*/
                                  // Datos del tema
                                  theme.tId,
                                  theme.tName,
                                  // Datos de la pregunta
                                  question.qId,
                                  question.qName,
                                  // Datos del area
                                  area.aId,
                                  area.aName,
                                  // Datos de la respuesta
                                  /*answer.asId,
                                  answer.asAnswer,
                                  answer.asCreationDate,*/
                                  // Datos del estatus
                                  status.rsId,
                                  status.rsStatus
                              };

                var anonTickets = from anonReq in _context.AnonRequests
                                  //join user in _context.HRU on anonReq.arModificationUser equals user.uEmployeeNumber
                                  join theme in _context.Theme on anonReq.ThemeId equals theme.tId
                                  join question in _context.Questions on anonReq.QuestionId equals question.qId
                                  join location in _context.Locations on anonReq.LocationId equals location.lId
                                  join area in _context.Areas on anonReq.AreaId equals area.aId
                                  join status in _context.RequestStatus on anonReq.StatusId equals status.rsId
                                  //join answer in _context.Answer on anonReq.arId equals answer.AnonRequestId
                                  where location.lId == locationId //&& anonReq.StatusId != 4
                                  select new
                                  {
                                      // Datos del ticket
                                      anonReq.arId,
                                      anonReq.arEmployeeType,
                                      anonReq.arCreationDate,
                                      // Datos de la localidad
                                      location.lId,
                                      location.lName,
                                      // Datos del usuario
                                      /*user.uEmployeeNumber,
                                      user.uName,*/
                                      // Datos del tema
                                      theme.tId,
                                      theme.tName,
                                      // Datos de la pregunta
                                      question.qId,
                                      question.qName,
                                      // Datos del area
                                      area.aId,
                                      area.aName,
                                      // Datos de la respuesta
                                      /*answer.asId,
                                      answer.asAnswer,
                                      answer.asCreationDate,*/
                                      // Datos del estatus
                                      status.rsId,
                                      status.rsStatus
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
