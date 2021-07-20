using CENSO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Controllers.TablesControllers
{
    [Route("api/[controller]")]
    [ApiController][Authorize(Policy = "StaffRH")] 
    public class TableController : ControllerBase
    {
        private readonly CDBContext _context;

        public TableController(CDBContext context)
        {
            _context = context;
        }

        // Listado de localidades (requiere policy administrator)
        [HttpGet][Route("locationList")][AllowAnonymous]
        public async Task<ActionResult> getLocationsList()
        {
            try
            {
                var query = await _context.Locations
                    .Where(l => l.lStatus == true)
                    .Select(l => new
                    {
                        // Datos de localidades
                        l.lId,
                        l.lName,
                        l.lStatus
                    })
                    .ToListAsync();

                if (query == null || query.Count == 0)
                {
                    return NotFound(new { message = $"Ninguna localidad encontrada en la base de datos" });
                }

                return Ok(query);

            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocuttido un error al obtener la lista de localidades. Error: {ex.Message}" });
            }
        }

        // Listado de usuarios (requiere policy SUHR)
        [HttpGet][Route("userList/{locationId}")][AllowAnonymous]
        public async Task<ActionResult> getUserList(int locationId)
        {
            try
            {
                var query = from user in _context.HRU
                            join location in _context.Locations on user.LocationId equals location.lId
                            join role in _context.Roles on user.uId equals role.rolId
                            where user.uStatus == true && location.lId == locationId
                            select new
                            {
                                // Datos del usuario
                                user.uId,
                                user.uEmployeeNumber,
                                user.uName,
                                user.uStatus,
                                // Datos del rol
                                role.rolId,
                                role.rolName,
                                // Datos de la localidad
                                location.lId,
                                location.lName
                            };

                if (query == null || query.Count() == 0)
                {
                    return NotFound(new { message = $"Ningun usuario se encuntra asociado a su localidad" });
                }

                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener la lista de usuarios. Error: {ex.Message}" });
            }
        }


        // Listado de temas (requiere policy SUHR)
        [HttpGet][Route("themeList/{locationId}")][AllowAnonymous]
        public async Task<ActionResult> getThemeList(int locationId)
        {
            try
            {
                var query = from theme in _context.Theme
                            join lt in _context.LocationsThemes on theme.tId equals lt.ThemeId
                            join location in _context.Locations on lt.LocationId equals location.lId
                            where theme.tStatus == true && location.lId == locationId
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
                    return NotFound(new { message = $"Ningun tema se encuentra asociado con tu localidad" });
                }

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener la lista de temas. Error: {ex.Message}" });
            }
        }

        // Listado de preguntas (requiere policy SUHR)
        [HttpGet][Route("questionList/{locationId}")][AllowAnonymous]
        public async Task<ActionResult> getQuestionList(int locationId)
        {
            try
            {
                var query = from question in _context.Questions
                            join qt in _context.QuestionsThemes on question.qId equals qt.QuestionId
                            join theme in _context.Theme on qt.ThemeId equals theme.tId
                            join lt in _context.LocationsThemes on theme.tId equals lt.ThemeId
                            join location in _context.Locations on lt.LocationId equals location.lId
                            where question.qStatus == true && location.lId == locationId
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
                    return NotFound(new { message = $"Ninguna pregunta se encuentra asociada con tu localidad" });
                }

                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener la lista de preguntas. Error: {ex.Message}" });
            }
        }

        // Listado de tikets
        [HttpGet][Route("ticketList/{locationId}")][AllowAnonymous]
        public async Task<ActionResult> getTiketList(int locationId)
        {
            try
            {
                var tickets = from request in _context.Requests
                              join theme in _context.Theme on request.ThemeId equals theme.tId
                              join question in _context.Questions on request.QuestionId equals question.qId
                              join location in _context.Locations on request.LocationId equals location.lId
                              join area in _context.Areas on request.AreaId equals area.aId
                              join status in _context.RequestStatus on request.StatusId equals status.rsId
                              where request.StatusId != 4 && location.lId == locationId
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

                

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener la lista de tickets. Error: {ex.Message}" });
            }
        }
    }
}
