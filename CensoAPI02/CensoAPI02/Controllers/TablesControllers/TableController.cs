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
        [HttpGet][Route("locationList")]
        [Authorize(Policy = "Administrador")]
        public async Task<ActionResult> getLocationsList()
        {
            try
            {
                var query = await _context.Locations
                    //.Where(l => l.lStatus == true)
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
        [HttpGet][Route("userList/{locationId}")]
        [Authorize(Policy = "SURH")]
        public async Task<ActionResult> getUserList(int locationId)
        {
            try
            {
                var query = from user in _context.HRU
                            join location in _context.Locations on user.LocationId equals location.lId
                            join role in _context.Roles on user.RoleId equals role.rolId
                            where location.lId == locationId //&& user.uStatus == true
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
        [HttpGet][Route("themeList/{locationId}")]
        [Authorize(Policy = "SURH")]
        public async Task<ActionResult> getThemeList(int locationId)
        {
            try
            {
                var query = from theme in _context.Theme
                            join lt in _context.LocationsThemes on theme.tId equals lt.ThemeId
                            join location in _context.Locations on lt.LocationId equals location.lId
                            where location.lId == locationId //&& theme.tStatus == true
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
        [HttpGet][Route("questionList/{locationId}")]
        [Authorize(Policy = "SURH")]
        public async Task<ActionResult> getQuestionList(int locationId)
        {
            try
            {
                var query = from question in _context.Questions
                            join qt in _context.QuestionsThemes on question.qId equals qt.QuestionId
                            join theme in _context.Theme on qt.ThemeId equals theme.tId
                            join lt in _context.LocationsThemes on theme.tId equals lt.ThemeId
                            join location in _context.Locations on lt.LocationId equals location.lId
                            where location.lId == locationId //&& question.qStatus == true
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
        [HttpGet][Route("ticketList/{locationId}/{employeeNumber}")]
        [Authorize(Policy = "StaffRH")]
        public async Task<ActionResult> getTiketList(int locationId, long employeeNumber)
        {
            try
            {
                // Validación de rol de usuario
                int rol;
                var isAdmin = await _context.HRU.FindAsync(employeeNumber);
                rol = isAdmin.RoleId;

                // Si es admin entonces
                if ( rol == 1)
                {
                    var tickets = from request in _context.Requests
                                  join theme in _context.Theme on request.ThemeId equals theme.tId
                                  join ut in _context.HRUsersThemes on theme.tId equals ut.ThemeId
                                  join user in _context.HRU on ut.UserId equals user.uEmployeeNumber
                                  join question in _context.Questions on request.QuestionId equals question.qId
                                  join location in _context.Locations on request.LocationId equals location.lId
                                  join area in _context.Areas on request.AreaId equals area.aId
                                  join status in _context.RequestStatus on request.StatusId equals status.rsId
                                  where location.lId == locationId
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

                    var anonTickets = from anonReq in _context.AnonRequests
                                      join theme in _context.Theme on anonReq.ThemeId equals theme.tId
                                      join ut in _context.HRUsersThemes on theme.tId equals ut.ThemeId
                                      join user in _context.HRU on ut.UserId equals user.uEmployeeNumber
                                      join question in _context.Questions on anonReq.QuestionId equals question.qId
                                      join location in _context.Locations on anonReq.LocationId equals location.lId
                                      join area in _context.Areas on anonReq.AreaId equals area.aId
                                      join status in _context.RequestStatus on anonReq.StatusId equals status.rsId
                                      where location.lId == locationId
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

                    if ((tickets == null || tickets.Count() == 0) && (anonTickets == null || anonTickets.Count() == 0))
                    {
                        return NotFound(new { message = $"Ningun ticket se encuentra asociado con tu localidad" });
                    }

                    return Ok(new { tickets, anonTickets });
                }
                // Si es caulquier otro tipo de rol entonces
                else
                {
                    var tickets = from request in _context.Requests
                                  join theme in _context.Theme on request.ThemeId equals theme.tId
                                  join ut in _context.HRUsersThemes on theme.tId equals ut.ThemeId
                                  join user in _context.HRU on ut.UserId equals user.uEmployeeNumber
                                  join question in _context.Questions on request.QuestionId equals question.qId
                                  join location in _context.Locations on request.LocationId equals location.lId
                                  join area in _context.Areas on request.AreaId equals area.aId
                                  join status in _context.RequestStatus on request.StatusId equals status.rsId
                                  where location.lId == locationId &&
                                        (from ut in _context.HRUsersThemes where ut.UserId == employeeNumber select ut.ThemeId).Contains(theme.tId) &&
                                        user.uEmployeeNumber == employeeNumber
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

                    var anonTickets = from anonReq in _context.AnonRequests
                                      join theme in _context.Theme on anonReq.ThemeId equals theme.tId
                                      join ut in _context.HRUsersThemes on theme.tId equals ut.ThemeId
                                      join user in _context.HRU on ut.UserId equals user.uEmployeeNumber
                                      join question in _context.Questions on anonReq.QuestionId equals question.qId
                                      join location in _context.Locations on anonReq.LocationId equals location.lId
                                      join area in _context.Areas on anonReq.AreaId equals area.aId
                                      join status in _context.RequestStatus on anonReq.StatusId equals status.rsId
                                      where location.lId == locationId &&
                                            (from ut in _context.HRUsersThemes where ut.UserId == employeeNumber select ut.ThemeId).Contains(theme.tId) &&
                                            user.uEmployeeNumber == employeeNumber
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
                
                    if ((tickets == null || tickets.Count() == 0) && (anonTickets == null || anonTickets.Count() == 0))
                    {
                        return NotFound(new { message = $"Ningun ticket se encuentra asociado con tu localidad" });
                    }

                    return Ok(new { tickets, anonTickets });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener la lista de tickets. Error: {ex.Message}" });
            }
        }

        // Listado de areas(requiere policy SUHR)
        [HttpGet][Route("areaList/{locationId}")]
        [Authorize(Policy = "SURH")]
        public async Task<ActionResult> getAreaList(int locationId)
        {
            try
            {
                var query = from area in _context.Areas
                            join al in _context.AreasLocations on area.aId equals al.AreaId
                            join location in _context.Locations on al.LocationId equals location.lId
                            where location.lId == locationId
                            //where area.aStatus == true && location.lId == locationId
                            select new
                            {
                                // Datos el area
                                area.aId,
                                area.aName,
                                area.aStatus,
                                // Datos de la localidad
                                location.lId,
                                location.lName
                            };

                if (query == null || query.Count() == 0)
                {
                    return NotFound(new { message = $"Ninguna area se encuentra asociada a tu localidad" });
                }

                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener la lista de areas. Error: {ex.Message}" });
            }
        }
    }
}
