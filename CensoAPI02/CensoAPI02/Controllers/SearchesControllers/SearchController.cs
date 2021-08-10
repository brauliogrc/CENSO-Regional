using CENSO.Models;
using CensoAPI02.Intserfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Controllers.SearchesControllers
{
    [Route("api/[controller]")]
    [ApiController][Authorize(Policy = "StaffRH")]
    public class SearchController : ControllerBase
    {
        private readonly CDBContext _context;

        public SearchController(CDBContext context)
        {
            _context = context;
        }

        // Busqueda de una localidad especifica (requiere policy Administrator)
        [HttpGet][Route("locationSearch/{locationId}")][AllowAnonymous]
        public async Task<ActionResult> locationSearch(int locationId)
        {
            try
            {
                var query = await _context.Locations
                    .Where(l => l.lStatus == true && l.lId == locationId)
                    .Select(l => new
                    {
                        // Datos de la localidad
                        l.lId,
                        l.lName,
                        l.lStatus
                    })
                    .ToListAsync();

                if (query == null || query.Count == 0)
                {
                    return NotFound(new { message = $"La localidad no se encuentra en la base de datos" });
                }

                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocuttido un error al obtener la localidad. Error: {ex.Message}" });
            }
        }

        //Busqueda de un usuario especifico en la localidad (requiere policy SUHR)
        [HttpGet][Route("userSearch/{locationId}/{itemId}")][AllowAnonymous]
        public async Task<ActionResult> userSearch(int locationId, long itemId)
        {
            try
            {
                var query = from user in _context.HRU
                            join location in _context.Locations on user.LocationId equals location.lId
                            join role in _context.Roles on user.RoleId equals role.rolId
                            where user.uStatus == true && user.uEmployeeNumber == itemId && location.lId == locationId 
                            select new
                            {
                                // Datos del usuario
                                //user.uId,
                                user.uEmployeeNumber,
                                user.uName,
                                user.uStatus,
                                user.uEmail,
                                // Datos del rol
                                role.rolId,
                                role.rolName,
                                // Datos de la localidad
                                location.lId,
                                location.lName
                            };

                if (query == null || query.Count() == 0)
                {
                    return NotFound(new { message = $"El usuario no se encuentra en la localidad" });
                }

                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocuttido un error al obtener el usuario. Error: {ex.Message}" });
            }
        }

        // Busqueda de un tema especifico en la localidad (requiere policy SUHR)
        [HttpGet][Route("themeSearch/{locationId}/{itemId}")][AllowAnonymous]
        public async Task<ActionResult> themeSearch(int locationId, int itemId)
        {
            try
            {
                var query = from theme in _context.Theme
                            join lt in _context.LocationsThemes on theme.tId equals lt.ThemeId
                            join location in _context.Locations on lt.LocationId equals location.lId
                            where theme.tStatus == true && theme.tId == itemId && location.lId == locationId
                            select new
                            {
                                // Datos del tema
                                theme.tId,
                                theme.tName,
                                theme.tStatus,
                                // Datos de la localidad
                                location.lId,
                                location.lName
                            };

                if (query == null || query.Count() == 0)
                {
                    return NotFound(new { message = $"El tema no se encuentra en la localidad" });
                }

                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocuttido un error al obtener el tema. Error: {ex.Message}" });
            }
        }

        // Busqueda de una pregunta especifica en la localidad (requiere policy SUHR)
        [HttpGet][Route("questionSearch/{locationId}/{itemId}")][AllowAnonymous]
        public async Task<ActionResult> questionSearch(int locationId, int itemId)
        {
            try
            {
                var query = from question in _context.Questions
                            join qt in _context.QuestionsThemes on question.qId equals qt.QuestionId
                            join theme in _context.Theme on qt.ThemeId equals theme.tId
                            join lt in _context.LocationsThemes on theme.tId equals lt.ThemeId
                            join location in _context.Locations on lt.LocationId equals location.lId
                            where question.qStatus == true && question.qId == itemId && location.lId == locationId
                            select new
                            {
                                // Datos de la pregunta
                                question.qId,
                                question.qName,
                                question.qStatus,
                                // Datos del tema
                                theme.tId,
                                theme.tName
                            };

                if (query == null || query.Count() == 0)
                {
                    return NotFound(new { message = $"La pregunta no se encuentra en la localidad" });
                }

                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocuttido un error al obtener la Pregunta. Error: {ex.Message}" });
            }
        }

        // Busqueda de un tiket especifico en la localidad
        [HttpGet][Route("ticketSearch/{locationId}/{itemId}")][AllowAnonymous]
        public async Task<ActionResult> ticketSearch(int locationId, int itemId)
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
                                 request.rId,
                                 request.rUserName,
                                 request.rIssue,
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
                                         anonReq.arId,
                                         anonReq.arIssue,
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
                        return NotFound(new { message = $"El ticket no se encuentra en la localidad" });
                    }

                    return Ok(anonTicket);
                }

                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocuttido un error al obtener el ticket. Error: {ex.Message}" });
            }
        }

        // Busqueda de un area especifica en la localidad (requiere policy SUHR)
        [HttpGet][Route("areaSearch/{locationId}/{itemId}")][AllowAnonymous]
        public async Task<ActionResult> areaSearch(int locationId, int itemId)
        {
            try
            {
                var query = from area in _context.Areas
                            join al in _context.AreasLocations on area.aId equals al.AreaId
                            join location in _context.Locations on al.LocationId equals location.lId
                            // where area.aStatus == true && area.aId == itemId && location.lId == locationId
                            where area.aId == itemId && location.lId == locationId
                            select new
                            {
                                // Datos del area
                                area.aId,
                                area.aName,
                                area.aStatus,
                                // Datos de la localiad
                                location.lId,
                                location.lName
                            };

                if (query == null || query.Count() == 0)
                {
                    return NotFound(new { message = $"El area no se encuentra en la localidad" });
                }

                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocuttido un error al obtener el area. Error: {ex.Message}" });
            }
        }
    }
}
