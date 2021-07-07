using CENSO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataTablesController : ControllerBase
    {
        private readonly CDBContext _context;

        public DataTablesController(CDBContext context)
        {
            _context = context;
        }

        // Obtener datos para la tabla de Loctions
        [HttpGet]
        [Route("TableLocations")]
        public async Task<ActionResult> GetLocations()
        {
            try
            {
                var query = await _context.Locations.Select(l => new { l.lId, l.lName, l.lStatus }).Where(l => l.lStatus == true).ToListAsync();
                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Obtener datos para la tabla de HRU
        [HttpGet]
        [Route("TableUsers")]
        public async Task<ActionResult> GetHRU()
        {
            try
            {
                var query = await _context.HRU.Join(_context.Locations, hru => hru.LocationId, location => location.lId, (user, location) => new
                {
                    /*user.uId,
                    user.uName,
                    user.uEmail,
                    user.uStatus,
                    location.lName,
                    location.lId*/
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
                }).Where(hru => hru.uStatus == true).ToListAsync();
                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Obtener datos para la tabla de Theme
        [HttpGet]
        [Route("TableTheme")]
        public async Task<ActionResult> GetTheme()
        {
            try
            {
                var query = from location in _context.Locations
                            join lt in _context.LocationsThemes on location.lId equals lt.LocationId
                            join theme in _context.Theme on lt.ThemeId equals theme.tId
                            where theme.tStatus == true
                            select new
                            {
                                location.lId,
                                location.lName,
                                theme.tId,
                                theme.tName,
                                theme.tStatus
                            };
                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Obtener datos para la tabla de Questions
        [HttpGet]
        [Route("TableQuestions")]
        public async Task<ActionResult> GetQuestions()
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
                }).Where(q => q.qStatus == true).ToListAsync();
                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Obtener datos para la tabla de Areas
        [HttpGet]
        [Route("TableAreas")]
        public async Task<ActionResult> GetAreas()
        {
            try
            {
                var query = await _context.Areas.Join(_context.Locations, a => a.aId, l => l.lId, (a, l) => new
                {
                    a.aName,
                    a.aId,
                    l.lId,
                    l.lName
                }).ToListAsync();

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Obtener datos para la tabla de Tickets
        [HttpGet]
        [Route("TableTikets")]
        public async Task<ActionResult> GerTikets()
        {
            try
            {
                var query = from theme in _context.Theme
                            join qth in _context.QuestionsThemes on theme.tId equals qth.ThemeId
                            join question in _context.Questions on qth.QuestionId equals question.qId
                            join request in _context.Requests on question.qId equals request.QuestionId
                            join area in _context.Areas on request.AreaId equals area.aId
                            join user in _context.HRU on request.rUserId equals user.uId
                            //where theme.tStatus == true && question.qStatus == true
                            select new
                            {
                                theme.tId,
                                theme.tName,
                                question.qId,
                                question.qName,
                                request.rId,
                                request.rIssue,
                                area.aId,
                                area.aName,
                                user.uId,
                                user.uName
                            };

                if(query == null)
                {
                    return NotFound(new { message = "ningun ticket encontrado en la base de atos" });
                }

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
