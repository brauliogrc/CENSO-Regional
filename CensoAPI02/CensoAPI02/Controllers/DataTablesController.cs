﻿using CENSO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                var query = await _context.Locations.Select(l => new { l.lId, l.lName, l.lStatus }).ToListAsync();
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
                    user.uId,
                    user.uName,
                    user.uEmail,
                    user.uRol,
                    user.uStatus,
                    location.lName,
                    location.lId
                }).ToListAsync();
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
                            join hrth in _context.HRUsersThemes on theme.tId equals hrth.ThemeId
                            join user in _context.HRU on hrth.HRUId equals user.uId
                            select new
                            {
                                location.lId,
                                location.lName,
                                theme.tId,
                                theme.tName,
                                theme.tStatus,
                                user.uName,
                                user.uId
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
                }).ToListAsync();
                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}