using CENSO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataTablesController : ControllerBase
    {
        private readonly CDBContext _context;
        private readonly IConfiguration _config;

        public DataTablesController(CDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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
        [Route("TableTikets/{id}")][AllowAnonymous]
        public async Task<ActionResult> GerTikets(int id)
        {
            try
            {
                // Consulta de tikets
                var tikets = from request in _context.Requests
                            join status in _context.RequestStatus on request.StatusId equals status.rsId
                            join theme in _context.Theme on request.ThemeId equals theme.tId
                            join question in _context.Questions on request.QuestionId equals question.qId
                            join area in _context.Areas on request.AreaId equals area.aId
                            join location in _context.Locations on request.LocationId equals location.lId
                            where location.lId == id
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

                // Consulta de tiket anonimos
                var anonTikets = from anonReq in _context.AnonRequests
                                 join status in _context.RequestStatus on anonReq.StatusId equals status.rsId
                                 join theme in _context.Theme on anonReq.ThemeId equals theme.tId
                                 join question in _context.Questions on anonReq.QuestionId equals question.qId
                                 join area in _context.Areas on anonReq.AreaId equals area.aId
                                 join location in _context.Locations on anonReq.LocationId equals location.lId
                                 where location.lId == id
                                 select new
                                 {
                                     // Datos del tiket
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
                                     //Datos del area
                                     area.aId,
                                     area.aName
                                 };

                return Ok( new { tikets, anonTikets } );
            }catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
