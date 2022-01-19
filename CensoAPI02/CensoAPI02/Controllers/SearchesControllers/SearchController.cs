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
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly CDBContext _context;

        public SearchController(CDBContext context)
        {
            _context = context;
        }

        // Busqueda de una localidad especifica (requiere policy Administrator)
        [HttpGet][Route("locationSearch/{locationName}")][Authorize(Policy = "Administrador")]
        public async Task<ActionResult> locationSearch(string locationName)
        {
            try
            {
                /*var query = await _context.Locations
                    .Where(l => l.lStatus == true && l.lId == locationId)
                    .Select(l => new
                    {
                        // Datos de la localidad
                        l.lId,
                        l.lName,
                        l.lStatus
                    })
                    .ToListAsync();*/

                var query = from loc in _context.Locations
                            where loc.lName.ToUpper().Contains(locationName.ToUpper())
                            select new
                            {
                                loc.lId,
                                loc.lName,
                                loc.lStatus
                            };

                if (query == null)
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
        [HttpGet][Route("userSearch/{locationId}/{itemId}")][Authorize(Policy = "SURH")]
        public async Task<ActionResult> userSearch(int locationId, long itemId)
        {
            try
            {
                var query = from user in _context.HRU
                            join location in _context.Locations on user.LocationId equals location.lId
                            join role in _context.Roles on user.RoleId equals role.rolId
                            where user.uEmployeeNumber == itemId && location.lId == locationId //&& user.uStatus == true
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
        [HttpGet][Route("themeSearch/{locationId}/{itemId}")][Authorize(Policy = "SURH")]
        public async Task<ActionResult> themeSearch(int locationId, string itemId)
        {
            try
            {
                var query = from theme in _context.Theme
                            join lt in _context.LocationsThemes on theme.tId equals lt.ThemeId
                            join location in _context.Locations on lt.LocationId equals location.lId
                            where theme.tName.ToUpper().Contains(itemId.ToUpper()) && location.lId == locationId //&& theme.tStatus == true
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
        [HttpGet][Route("questionSearch/{locationId}/{itemId}")][Authorize(Policy = "SURH")]
        public async Task<ActionResult> questionSearch(int locationId, string itemId)
        {
            try
            {
                var query = from question in _context.Questions
                            join qt in _context.QuestionsThemes on question.qId equals qt.QuestionId
                            join theme in _context.Theme on qt.ThemeId equals theme.tId
                            join lt in _context.LocationsThemes on theme.tId equals lt.ThemeId
                            join location in _context.Locations on lt.LocationId equals location.lId
                            where ( question.qName.ToUpper().Contains(itemId.ToUpper()) || theme.tName.ToUpper().Contains(itemId.ToUpper()) ) && 
                                    location.lId == locationId //&& question.qStatus == true
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
        [HttpGet][Route("ticketSearch/{locationId}/{itemId}")][Authorize(Policy = "StaffRH")]
        public async Task<ActionResult> ticketSearch(int locationId, string itemId)
        {
            try
            {
                var ticket = from request in _context.Requests
                             join theme in _context.Theme on request.ThemeId equals theme.tId
                             join question in _context.Questions on request.QuestionId equals question.qId
                             join location in _context.Locations on request.LocationId equals location.lId
                             join area in _context.Areas on request.AreaId equals area.aId
                             join status in _context.RequestStatus on request.StatusId equals status.rsId
                             //where request.rId == itemId && location.lId == locationId //&& request.StatusId != 4
                             where location.lId == locationId &&
                                ( request.rId.ToString().Contains(itemId) ||
                                request.rIssue.Contains(itemId) ||
                                request.rUserName.Contains(itemId) ||
                                theme.tName.Contains(itemId) ||
                                question.qName.Contains(itemId) ||
                                area.aName.Contains(itemId) ||
                                status.rsStatus.Contains(itemId) )
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
                
                    var anonTicket = from anonReq in _context.AnonRequests
                                     join theme in _context.Theme on anonReq.ThemeId equals theme.tId
                                     join question in _context.Questions on anonReq.QuestionId equals question.qId
                                     join location in _context.Locations on anonReq.LocationId equals location.lId
                                     join area in _context.Areas on anonReq.AreaId equals area.aId
                                     join status in _context.RequestStatus on anonReq.StatusId equals status.rsId
                                     //where anonReq.arId == itemId && location.lId == locationId //&& anonReq.StatusId != 4
                                     where location.lId == locationId &&
                                        ( anonReq.arId.ToString().Contains(itemId) ||
                                        anonReq.arIssue.Contains(itemId) ||
                                        theme.tName.Contains(itemId) ||
                                        question.qName.Contains(itemId) ||
                                        area.aName.Contains(itemId) ||
                                        status.rsStatus.Contains(itemId) )
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

                if ((anonTicket == null || anonTicket.Count() == 0) && (ticket == null || ticket.Count() == 0))
                {
                    return NotFound(new { message = $"El ticket no se encuentra en la localidad" });
                }

                return Ok(new {ticket, anonTicket });
               
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocuttido un error al obtener el ticket. Error: {ex.Message}" });
            }
        }

        // Busqueda de un area especifica en la localidad (requiere policy SUHR)
        [HttpGet][Route("areaSearch/{locationId}/{itemId}")][Authorize(Policy = "SURH")]
        public async Task<ActionResult> areaSearch(int locationId, string itemId)
        {
            try
            {
                var query = from area in _context.Areas
                            join al in _context.AreasLocations on area.aId equals al.AreaId
                            join location in _context.Locations on al.LocationId equals location.lId
                            // where area.aStatus == true && area.aId == itemId && location.lId == locationId
                            where area.aName.ToUpper().Contains(itemId.ToUpper()) && location.lId == locationId
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

        // Busqueda de los tickets del usuario logueado
        [HttpGet][Route("userTickets/{employeenumber}")][Authorize]
        public async Task<ActionResult> getUserTickets( long employeenumber)
        {
            try
            {
                var userTickets = from request in _context.Requests
                                  join location in _context.Locations on request.LocationId equals location.lId
                                  join theme in _context.Theme on request.ThemeId equals theme.tId
                                  join question in _context.Questions on request.QuestionId equals question.qId
                                  join status in _context.RequestStatus on request.StatusId equals status.rsId
                                  where request.StatusId != 4 && request.rUserId == employeenumber
                                  select new
                                  {
                                      // Datos del ticket
                                      request.rId,
                                      request.rIssue,
                                      //request.rUserName,
                                      // Datos de la localidad
                                      location.lId,
                                      location.lName,
                                      // Datos del tema
                                      theme.tId,
                                      theme.tName,
                                      // Datos de la pregunta
                                      question.qId,
                                      question.qName,
                                      // Datos del estatus
                                      status.rsId,
                                      status.rsStatus
                                  };

                if ( userTickets == null || userTickets.Count() == 0 )
                {
                    return NotFound(new { message = $"No se ha encontrado nungún ticket relacionado a su usuario" } );
                }

                return Ok( userTickets );
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = $" Haocurrido un error en la obtención de tus tickets" });
            }
        }

        // OBTENCION DE DATOS PARA LA ACTIAIZACIÓN DE CAMPOS

        // Obtención de la informacion a actualizar de un usuario (policity surh)
        [HttpGet]
        [Route("existingUser/{employeeNumber}")]
        [Authorize(Policy = "SURH")]
        public async Task<ActionResult> getUpdateUserInformation(long employeeNumber)
        {
            try
            {
                var userInformation = from user in _context.HRU
                                      join location in _context.Locations on user.LocationId equals location.lId
                                      join role in _context.Roles on user.RoleId equals role.rolId
                                      where user.uEmployeeNumber == employeeNumber
                                      select new
                                      {
                                          // Daatos del usuario
                                          user.uEmployeeNumber,
                                          user.uName,
                                          user.uEmail,
                                          user.uStatus,
                                          // Datos de la localidad
                                          location.lId,
                                          location.lName,
                                          // Datos del rol
                                          role.rolId,
                                          role.rolName,
                                      };

                if (userInformation == null || userInformation.Count() == 0)
                {
                    return NotFound(new { message = $"El usuario no se encuentra en la base de datos" });
                }

                return Ok(userInformation);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al recuperar la infomación del usuario. Error: {ex.Message}" });
            }
        }

        //Obtención de los temas relacionados al ursuario a actuaizar
        [HttpGet][Route("relatedUserTopics/{employeeNumber}")]
        [Authorize(Policy = "SURH")]
        public async Task<ActionResult> getRelatedTopics(long employeeNumber)
        {
            try
            {
                var temas = from ht in _context.HRUsersThemes
                            join theme in _context.Theme on ht.ThemeId equals theme.tId
                            where ht.UserId == employeeNumber && theme.tStatus == true
                            select new
                            {
                                // Datos del tema
                                theme.tId,
                                theme.tName
                            };

                if (temas == null || temas.Count() == 0)
                {
                    return NotFound(new { message = $"No se encuentra ningun tema relacionado con este usuario" });
                }

                return Ok(temas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al recuperar la infomación del usuario. Error: {ex.Message}" });
            }
        }

        // Obtenció de los temas relacionados con la pregunta
        [HttpGet]
        [Route("relatedQuestionTopics/{questionId}")]
        [Authorize(Policy = "SURH")]
        public async Task<ActionResult> relatedQuestionTopics(int questionId)
        {
            try
            {
                var temas = from qt in _context.QuestionsThemes
                            join theme in _context.Theme on qt.ThemeId equals theme.tId
                            where qt.QuestionId == questionId
                            select new
                            {
                                // Datos del tema
                                theme.tId,
                                theme.tName
                            };

                if ( temas == null || temas.Count() == 0)
                {
                    return NotFound(new { message = $"No se encuentra ningun tema relacionado con esta pregunta." });
                }

                return Ok(temas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener los temas relacionados. Error: {ex.Message}" });
            }
        }

        // Obtencion de la localidades relacionadas al tema a actualizar
        [HttpGet][Route("relatedThemeLocations/{themeId}")]
        [Authorize(Policy = "SURH")]
        public async Task<ActionResult> relatedLocations(int themeId)
        {
            try
            {
                var location = from lt in _context.LocationsThemes
                               join locations in _context.Locations on lt.LocationId equals locations.lId
                               where lt.ThemeId == themeId
                               select new
                               {
                                   // Datos de la loccalidad
                                   locations.lId,
                                   locations.lName
                               };

                if (location == null || location.Count() == 0)
                {
                    return NotFound(new { message = $"No se encuentra ninguna localidad relacionada a este tema." });
                }

                return Ok(location);
            } 
            catch ( Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un rror al obtener las localidades relacionadas al tema. Error: {ex.Message}" });
            }
        }

        // Obtencion de la información a actualizar de una localidad
        [HttpGet][Route("existingLocation/{locationId}")]
        [Authorize(Policy = "Administrador")]
        public async Task<ActionResult> existingLocation(int locationId)
        {
            try
            {
                var locationInformation = from location in _context.Locations
                                          where location.lId == locationId
                                          select new
                                          {
                                              // Datos de la localidad
                                              location.lId,
                                              location.lName,
                                              location.lStatus
                                          };

                if ( locationInformation == null )
                {
                    return NotFound(new { message = $"La localidad no fue encontrada en la base de datos" });
                }

                return Ok(locationInformation);
            }
            catch ( Exception ex )
            {
                return BadRequest(new { message = $"Ha ocurrido un error al recuperar la información de la localidad. Error: {ex.Message}" });
            }
        }

        // Obtención de la información a actualizar de un tema
        [HttpGet][Route("existingTheme/{themeId}")]
        [Authorize(Policy = "SURH")]
        public async Task<ActionResult> existingTheme(int themeId )
        {
            try
            {
                var themeInformation = from theme in _context.Theme
                                       where theme.tId == themeId
                                       select new
                                       {
                                           // Datos del tema
                                           theme.tId,
                                           theme.tName,
                                           theme.tStatus
                                       };

                if ( themeInformation == null )
                {
                    return NotFound(new { message = $"El tema no se encuentra en la base datos" });
                }

                return Ok(themeInformation);
            }
            catch( Exception ex )
            {
                return BadRequest(new { message = $"Ha ocurrido un error al recuperar la información del tema. Error: {ex.Message}" });
            }
        }

        // Obtención de la información a actualizar de una pregunta
        [HttpGet][Route("existingQuestion/{questionId}")][Authorize(Policy = "SURH")]
        public async Task<ActionResult> existingQuestion(int questionId)
        {
            try
            {
                var questionInformaction = from question in _context.Questions
                                           where question.qId == questionId
                                           select new
                                           {
                                               // Datos de la pregunta
                                               question.qId,
                                               question.qName,
                                               question.qStatus
                                           };

                if ( questionInformaction == null)
                {
                    return NotFound(new { message = $"La pregunta no se encuentra en la base de datos." });
                }

                return Ok(questionInformaction);
            }
            catch( Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener la infromación de la pregunta. Error: {ex.Message}" });
            }
        }

        // Obtención de la información a actualizar del area
        [HttpGet][Route("existingArea/{areaId}")][Authorize(Policy = "SURH")]
        public async Task<ActionResult> existingArea(int areaId)
        {
            try
            {
                var areaInformation = from area in _context.Areas
                                      join al in _context.AreasLocations on area.aId equals al.AreaId
                                      join location in _context.Locations on al.LocationId equals location.lId
                                      where area.aId == areaId
                                      select new
                                      {
                                          // Datos del area
                                          area.aId,
                                          area.aName,
                                          area.aStatus,
                                          // Datos de la localidad
                                          location.lId,
                                          location.lName
                                      };

                if ( areaInformation == null )
                {
                    return NotFound(new { message = $"El area no se encuentra en la base de datos." });
                }

                return Ok(areaInformation);
            }
            catch ( Exception ex )
            {
                return BadRequest( new { message = $"Ha ocurrido un error al obtener la información del area. Error:{ex.Message}" });
            }
        }

    }
}
