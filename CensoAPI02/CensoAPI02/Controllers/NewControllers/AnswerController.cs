using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models;
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
    public class AnswerController : ControllerBase
    {
        private readonly CDBContext _context;

        public AnswerController(CDBContext context)
        {
            _context  = context;
        }

        // Registro de una nueva respuesta de tiket y actualizacion del tiket respondido
        [HttpPost]
        [Route("newAnswer")]
        [AllowAnonymous]
        public async Task<IActionResult> addNewAnswer([FromBody] AddAnswerInterface newAnswer)
        {
            try
            {
                var addAnswer = new AnswerStatus()
                {
                    UserId = newAnswer.asUserId,
                    asAnswer = newAnswer.asAnswer,                    
                    asCreationDate = DateTime.Now,

                };                

                // Modificacion del tiket
                var ticketModification = await _context.Requests.FindAsync(newAnswer.RequestId);

                if (ticketModification == null)
                {
                    var anonTicketModification = await _context.AnonRequests.FindAsync(newAnswer.RequestId);

                    addAnswer.AnonRequestId = newAnswer.RequestId;
                    addAnswer.RequestId = null;
                    anonTicketModification.arModificationDate = DateTime.Now;
                    anonTicketModification.arModificationUser = newAnswer.asUserId;
                    anonTicketModification.StatusId = 4;

                    // Regsitr de la moficacion el ticket anonimo
                    _context.AnonRequests.Update(anonTicketModification);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = $"Respuesta {addAnswer.asId} del tiket {newAnswer.RequestId}, registrada correctamente" });
                }

                addAnswer.RequestId = newAnswer.RequestId;
                addAnswer.anonRequest = null;
                ticketModification.rModificationUser = newAnswer.asUserId;
                ticketModification.rModificationDate = DateTime.Now;
                ticketModification.StatusId = 2;

                // Regsitro de las modificaciones del tiket
                _context.Requests.Update(ticketModification);
                await _context.SaveChangesAsync();

                // Registro de la nueva respuesta en la tabla AnswerStatus
                _context.Answer.Add(addAnswer);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Respuesta {addAnswer.asId} del tiket {newAnswer.RequestId}, registrada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al registrar la respuesta. Error: {ex.InnerException.Message}" });
            }
        }

        // Busqueda del ticket a responder
        [HttpGet][Route("findTicket/{locationId}/{itemId}")][AllowAnonymous]
        public async Task<ActionResult> findTiket(int locationId, int itemId)
        {
            try
            {
                var ticket = from request in _context.Requests
                             join theme in _context.Theme on request.ThemeId equals theme.tId
                             join question in _context.Questions on request.QuestionId equals question.qId
                             join location in _context.Locations on request.LocationId equals location.lId
                             join area in _context.Areas on request.AreaId equals area.aId
                             join status in _context.RequestStatus on request.StatusId equals status.rsId
                             where request.StatusId != 4 && request.rId == itemId && location.lId == locationId
                             select new
                             {
                                 // Datos del ticket
                                 request.rId,                   // Folio
                                 request.rUserName,             // Nombre del usuario
                                 request.rIssue,                // Contenido del ticket
                                 request.rAttachement,          // Evidencia del ticket
                                 request.rCreationDate,         // Fecha de creacion (realizar calculo)
                                 request.rUserId,               // Numero de empleado que realizo el ticket
                                 request.rEmployeeLeader,       // Numero de supervisor (Obtener el nombre)
                                 request.rEmployeeType,         // Tipo de empleado (Convertir en angular)
                                 // Datos del tema
                                 theme.tId,
                                 theme.tName,
                                 // Datos de la pregunta 
                                 question.qId,
                                 question.qName,
                                 // Datos del area
                                 area.aId,
                                 area.aName,
                                 // Datos del status
                                 status.rsId,
                                 status.rsStatus
                             };

                if (ticket == null || ticket.Count() == 0)
                {
                    var anonTicket = from anonReq in _context.AnonRequests
                                     join theme in _context.Theme on anonReq.ThemeId equals theme.tId
                                     join question in _context.Questions on anonReq.QuestionId equals question.qId
                                     join location in _context.Locations on anonReq.LocationId equals location.lId
                                     join area in _context.Areas on anonReq.AreaId equals area.aId
                                     join status in _context.RequestStatus on anonReq.StatusId equals status.rsId
                                     where anonReq.StatusId != 4 && anonReq.arId == itemId && location.lId == locationId
                                     select new
                                     {
                                         // Datos del ticket
                                         anonReq.arId,              // Folio
                                         anonReq.arIssue,           // Contenido del ticket
                                         anonReq.arAttachement,     // Evidencia del ticket
                                         anonReq.arCreationDate,    // Fecha de creación
                                         anonReq.arEmployeeType,    // Tipo de empleado (convertir e angular)
                                         // Datos del tema
                                         theme.tId,
                                         theme.tName,
                                         // Datos de la pregunta
                                         question.qId,
                                         question.qName,
                                         // Datos del area
                                         area.aId,
                                         area.aName,
                                         // Datos del status
                                         status.rsId,
                                         status.rsStatus
                                     };

                    if (anonTicket == null || anonTicket.Count() == 0)
                    {
                        return NotFound(new { message = $"El tiket no se encuentra en la localidad" });
                    }

                    return Ok(anonTicket);
                }
                
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener la información del ticket. Error: {ex.InnerException.Message}" });
            }
        }

    }
}

