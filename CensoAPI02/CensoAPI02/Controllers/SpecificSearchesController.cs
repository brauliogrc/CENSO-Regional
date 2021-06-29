using CENSO.Models;
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
    public class SpecificSearches : ControllerBase
    {
        private readonly CDBContext _context;

        public SpecificSearches(CDBContext context)
        {
            _context = context;
        }

        // Busqueda de folios con base en el id
        // En el home.component
        [HttpGet][Route("folioSearch/{id}")]
        public async Task<ActionResult> GetFolio(int id)
        {
            try
            {
                var query = from theme in _context.Theme
                            join qth in _context.QuestionsThemes on theme.tId equals qth.ThemeId
                            join question in _context.Questions on qth.QuestionId equals question.qId
                            join request in _context.Requests on question.qId equals request.QuestionId
                            join area in _context.Areas on request.AreaId equals area.aId
                            //where theme.tStatus == true && question.qStatus == true && request.rId == id
                            where request.rId == id
                            select new
                            {
                                theme.tId,
                                theme.tName,
                                question.qId,
                                question.qName,
                                request.rId,
                                request.rIssue,
                                area.aId,
                                area.aName
                            };

                if (query == null || query.Count() == 0)
                {
                    return NotFound(new { message = "Ningun ticket encontrado en la base de atos" });
                }

                return Ok(query);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet][Route("folioAnonSearch/{id}")]
        public async Task<ActionResult> GetFolioAnon(int id)
        {
            try
            {
                var query = from anonReq in _context.AnonRequests
                            join question in _context.Questions on anonReq.QuestionId equals question.qId
                            join theme in _context.Theme on anonReq.ThemeId equals theme.tId
                            join area in _context.Areas on anonReq.AreaId equals area.aId
                            where anonReq.arId == id
                            select new
                            {
                                anonReq.arId,
                                anonReq.arEmployeeType,
                                anonReq.arIssue,
                                anonReq.arAttachement,
                                question.qId,
                                question.qName,
                                theme.tId,
                                theme.tName,
                                area.aId,
                                area.aName
                            };

                if(query == null || query.Count() == 0)
                {
                    return NotFound(new { message = "Ningun ticket encontrado en la base de atos" });
                }

                return Ok(query);
            }catch(Exception ex)
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
    }

    // ENUMERACION PARA EL TIPO DE EMPLEADO
}
