using CENSO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// 
/// CONTROLADOR QUE SE ENCARGA DE LAS BUSQUEDAS ESPECÍFICAS DE LOCALIDADES, TEMAS, PREGUNTAS Y PETICIONES
/// TOMANDO COMO CRITERIO DE BUSQUEDA EL ID DE CADA ELEMENTO. SE LLAMA A ESTE CONTROLADOR POR MEDIO DEL CAMPO DE BUSQUEDA 
/// EN LOS COMPONENT DE CADA UNO DE ELLOS.
/// 
/// </summary>

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SpecificSearches : ControllerBase
    {
        private readonly CDBContext _context;

        public SpecificSearches(CDBContext context)
        {
            _context = context;
        }

        // Busqueda de folios con base en el id
        // En el home.component
        [HttpGet] [Route("tiketsSearch/{locationId}/{requestId}")] [AllowAnonymous] // [Authorize(Policy = "StaffRH")]
        public async Task<ActionResult> GetTikets(int locationId, int requestId)
        {
            try
            {
                var tiket = from request in _context.Requests
                            join status in _context.RequestStatus on request.StatusId equals status.rsId
                            join theme in _context.Theme on request.ThemeId equals theme.tId
                            join question in _context.Questions on request.QuestionId equals question.qId
                            join area in _context.Areas on request.AreaId equals area.aId
                            join location in _context.Locations on request.LocationId equals location.lId
                            where request.rId == requestId && location.lId == locationId
                            select new
                            {
                                // Datos del tiket
                                request.rId,
                                request.rIssue,
                                request.rUserId,
                                request.rUserName,
                                // Datos del status
                                status.rsId,
                                status.rsStatus,
                                // Datos del theme
                                theme.tId,
                                theme.tName,
                                // Datos de la question
                                question.qId,
                                question.qName,
                                // Datos del area
                                area.aId,
                                area.aName
                            };

                if (tiket == null || tiket.Count() == 0)
                {
                    var anonTiket = from anonReq in _context.AnonRequests
                                    join status in _context.RequestStatus on anonReq.StatusId equals status.rsId
                                    join theme in _context.Theme on anonReq.ThemeId equals theme.tId
                                    join question in _context.Questions on anonReq.QuestionId equals question.qId
                                    join area in _context.Areas on anonReq.AreaId equals area.aId
                                    join location in _context.Locations on anonReq.LocationId equals location.lId
                                    where anonReq.arId == requestId && location.lId == locationId
                                    select new
                                    {
                                        // Datos del tiket anonimo
                                        anonReq.arId,
                                        anonReq.arIssue,
                                        // Datos del status
                                        status.rsId,
                                        status.rsStatus,
                                        // Datos del theme
                                        theme.tId,
                                        theme.tName,
                                        // Datos de la question
                                        question.qId,
                                        question.qName,
                                        // Datos del area
                                        area.aId,
                                        area.aName
                                    };

                    if (anonTiket == null || anonTiket.Count() == 0)
                    {
                        return NotFound(new { message = "Ningun ticket encontrado en la base de atos" });
                    }

                    return Ok(anonTiket);
                }

                return Ok(tiket);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet][Route("LocationSearch/{id}")]
        public async Task<ActionResult> GetLocation(int id)
        {
            try
            {
                var query = await _context.Locations.Select(l => new { l.lId, l.lName, l.lStatus }).Where(l => l.lId == id && l.lStatus == true).FirstOrDefaultAsync();
                if (query == null)
                {
                    return NotFound(new { message = "La localidad no existe en la base de datos" });
                }
                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet][Route("UserSearch/{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            try
            {
                var query = await _context.HRU.Join(_context.Locations, hru => hru.LocationId, location => location.lId, (user, location) => new
                {
                    user,
                    location
                }).Join(_context.Roles, hru => hru.user.RoleId, rol => rol.rolId, (user, rol) => new
                {
                    user.user.uId,
                    user.user.uName,
                    user.user.uEmail,
                    user.user.uStatus,
                    user.location.lName,
                    user.location.lId,
                    rol.rolId,
                    rol.rolName
                }).Where(hru => hru.uStatus == true && hru.uId == id).FirstOrDefaultAsync();

                if (query == null)
                {
                    return NotFound(new { message = "El usuario no se encuentra en la base de datos" });
                }
                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet][Route("ThemeSearch/{id}")]
        public async Task<ActionResult> GetTheme(int id)
        {
            try
            {
                var query = await _context.Theme.Join(_context.LocationsThemes, th => th.tId, lt => lt.ThemeId, (th, lt) => new
                {
                    th,
                    lt
                }).Join(_context.Locations, lt => lt.lt.LocationId, l => l.lId, (lt, l) => new
                {
                    l.lId,
                    l.lName,
                    lt.th.tId,
                    lt.th.tName,
                    lt.th.tStatus
                }).Where(th => th.tStatus == true && th.tId == id).FirstOrDefaultAsync();

                if (query == null)
                {
                    return NotFound(new { message = "Tema no encontrado en la base de datos." });
                }

                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet][Route("QuestionSearch/{id}")]
        public async Task<ActionResult> GetQuestion(int id)
        {
            try
            {
                var query = await _context.Questions.Join(_context.QuestionsThemes, q => q.qId, qth => qth.QuestionId, (q, qth) => new
                {
                    q,
                    qth
                }).Join(_context.Theme, qth => qth.qth.ThemeId, th => th.tId, (qth, th) => new
                {
                    qth.q.qId,
                    qth.q.qName,
                    qth.q.qStatus,
                    th.tId,
                    th.tName
                }).Where(q => q.qStatus == true && q.qId == id).FirstOrDefaultAsync();

                if (query == null)
                {
                    return NotFound(new { message = "Pregunta no encontrada en la base de datos" });
                }

                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ENUMERACION PARA EL EMPLOYEETYPE
        enum EmployeeType
        {
            node = 0,

        }
    }
}
