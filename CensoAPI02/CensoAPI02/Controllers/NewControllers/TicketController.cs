using CENSO.Models;
using CensoAPI02.Intserfaces;
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
    [ApiController][Authorize(Policy = "StaffRH")]
    public class TicketController : ControllerBase
    {

        private readonly CDBContext _context;
        private static TicketAnswered ticketAnswered = new TicketAnswered();
        private AnswerData answer;

        public TicketController(CDBContext context)
        {
            _context = context;
        }

        // Busqueda de los datos del ticket a responder (requiere policy staff rh)
        [HttpGet][Route("ticketData/{ticketId}")][AllowAnonymous]
        public async Task<ActionResult> getTicketData(int ticketId)
        {

            int daysPassed;
            try
            {


                var ticketData = from request in _context.Requests
                                 join area in _context.Areas on request.AreaId equals area.aId
                                 join theme in _context.Theme on request.ThemeId equals theme.tId
                                 join question in _context.Questions on request.QuestionId equals question.qId
                                 join status in _context.RequestStatus on request.StatusId equals status.rsId
                                 where request.rId == ticketId
                                 select new
                                 {
                                     // Datos del ticket
                                     request.rId,
                                     request.rCreationDate,
                                     request.rEmployeeLeader,
                                     request.rEmployeeType,
                                     request.rUserName,
                                     request.rUserId,
                                     request.rIssue,
                                     request.rAttachement,
                                     // Datos del area
                                     area.aId,
                                     area.aName,
                                     // Datos del tema
                                     theme.tId,
                                     theme.tName,
                                     // Datos de la pregunta
                                     question.qId,
                                     question.qName,
                                     // Datos del satatus
                                     status.rsId,
                                     status.rsStatus
                                 };

                if (ticketData == null || ticketData.Count() == 0)
                {
                    var anonTicketData = from anonRequest in _context.AnonRequests
                                         join area in _context.Areas on anonRequest.AreaId equals area.aId
                                         join theme in _context.Theme on anonRequest.ThemeId equals theme.tId
                                         join question in _context.Questions on anonRequest.QuestionId equals question.qId
                                         join status in _context.RequestStatus on anonRequest.StatusId equals status.rsId
                                         where anonRequest.arId == ticketId
                                         select new
                                         {
                                             //Datos del ticket
                                             anonRequest.arId,
                                             anonRequest.arCreationDate,
                                             anonRequest.arEmployeeType,
                                             anonRequest.arIssue,
                                             anonRequest.arAttachement,
                                             // Datos del area
                                             area.aId,
                                             area.aName,
                                             // Datos del tema
                                             theme.tId,
                                             theme.tName,
                                             // Datos de la pregunta
                                             question.qId,
                                             question.qName,
                                             // Datos del satatus
                                             status.rsId,
                                             status.rsStatus
                                         };

                    if (anonTicketData == null || anonTicketData.Count() == 0)
                    {
                        return NotFound(new { message = $"Ticket o encontrado en la base de datos" });
                    }

                    daysPassed = Convert.ToInt32((DateTime.Now - anonTicketData.First().arCreationDate).TotalDays);
                    if (daysPassed < 1) daysPassed = 0;

                    // Verifcacionde la existencia de la respuesta del ticket
                    this.answer = ticketAnswered.answered(_context, ticketId);

                    if (this.answer.flag == 0)
                    {
                        return Ok(new { anonTicketData, daysPassed });
                    }

                    return Ok(new { anonTicketData, daysPassed, this.answer });

                }

                daysPassed = Convert.ToInt32((DateTime.Now - ticketData.First().rCreationDate).TotalDays);
                if (daysPassed < 1) daysPassed = 0;

                return Ok(new { ticketData, daysPassed });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error en la obtencion de los datos del ticket. Error: {ex.Message}" });
            }
        }


        // Borrado logico de un ticket (requiere policy staff rh)
        [HttpDelete][Route("deleteTicket/{ticketId}")][AllowAnonymous]
        public async Task<IActionResult> deleteTicket(int ticketId)
        {
            try
            {
                var ticket = await _context.Requests.FindAsync(ticketId);

                if (ticket == null)
                {
                    var anonTicket = await _context.AnonRequests.FindAsync(ticketId);

                    anonTicket.StatusId = 4;
                    _context.AnonRequests.Update(anonTicket);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = $"Ticket eliminado correctamente" });
                }

                ticket.StatusId = 4;
                _context.Requests.Update(ticket);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Ticket eliminado correctamente" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al eliminar el ticket. Error: {ex.Message}" });
            }
        }


        // Consulta del estatus de un ticket anonimo (allowanonymous)
        [HttpGet][Route("anonTicketStatus/{ticketId}")][AllowAnonymous]
        public async Task<ActionResult> getAnonTicketStatus(int ticketId)
        {
            try
            {
                var anonTicket = from anonRequest in _context.AnonRequests
                                 join status in _context.RequestStatus on anonRequest.StatusId equals status.rsId
                                 where anonRequest.arId == ticketId && anonRequest.StatusId != 4
                                 select new
                                 {
                                     // Datos del ticket
                                     anonRequest.arId,
                                     anonRequest.arIssue,
                                     anonRequest.arAttachement,
                                     // Datos del status
                                     status.rsId,
                                     status.rsStatus,
                                 };

                var answer = from answerStatus in _context.Answer
                             //join user in _context.HRU on answerStatus.UserId equals user.uId
                             join user in _context.HRU on answerStatus.UserId equals user.uEmployeeNumber
                             join anonRequest in _context.AnonRequests on answerStatus.AnonRequestId equals anonRequest.arId
                             where answerStatus.AnonRequestId == ticketId && anonRequest.StatusId != 4
                             select new
                             {
                                 // Datos de la respuesta
                                 answerStatus.asId,
                                 answerStatus.asCreationDate,
                                 answerStatus.asAnswer,
                                 answerStatus.asAttachement,
                                 // Datos del usuario
                                 //user.uId,
                                 user.uName,
                                 user.uEmployeeNumber
                             };

                if (anonTicket == null || anonTicket.Count() == 0)
                {
                    return NotFound(new { message = $"Ticket no encontrado en la base de datos" });
                }

                if (answer == null || answer.Count() == 0)
                {
                    return Ok(new { anonTicket, message = $"El ticker no ha sido respondido aún" });
                }

                return Ok(new { anonTicket, answer });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al consultar el estado del ticket. Error: {ex.Message}" });
            }
        }


        // Consulta del status de un ticket (requiere policy staff rh)
        [HttpGet][Route("ticketStatus/{employeeNumber}/{ticketId}")][AllowAnonymous]
        public async Task<ActionResult> getTicketStatus(int employeeNumber, int ticketId)
        {
            try
            {
                var ticket = from request in _context.Requests
                             join status in _context.RequestStatus on request.StatusId equals status.rsId
                             where request.rId == ticketId && request.StatusId != 4 && request.rUserId == employeeNumber
                             select new
                             {
                                 // Datos del ticket
                                 request.rId,
                                 request.rIssue,
                                 request.rAttachement,
                                 request.rUserName,
                                 request.rUserId,
                                 // Datos del status
                                 status.rsId,
                                 status.rsStatus
                             };

                var answer = from answerStatus in _context.Answer
                             //join user in _context.HRU on answerStatus.UserId equals user.uId
                             join user in _context.HRU on answerStatus.UserId equals user.uEmployeeNumber
                             join request in _context.Requests on answerStatus.RequestId equals request.rId
                             where answerStatus.RequestId == ticketId && request.StatusId != 4
                             select new
                             {
                                 // Datos de la respuesta
                                 answerStatus.asId,
                                 answerStatus.request,
                                 answerStatus.asCreationDate,
                                 // Datos del usuario
                                 //user.uId,
                                 user.uName,
                                 user.uEmployeeNumber
                             };

                if (ticket == null || ticket.Count() == 0)
                {
                    return NotFound(new { message = $"Ticket no encontrado en la base de datos" });
                }

                if (answer == null || answer.Count() == 0)
                {
                    return Ok(new { ticket, message = $"El ticker no ha sido respondido aún" });
                }

                return Ok(new { ticket, answer });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al consultar es estado del ticket. Error: {ex.Message}" });
            }
        }
    }
}
