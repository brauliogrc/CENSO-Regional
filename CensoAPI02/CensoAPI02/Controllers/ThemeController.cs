using CENSO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CensoAPI02.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly CDBContext _context;

        public ThemeController( CDBContext context)
        {
            _context = context;
        }
        // GET: api/<ThemeController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                // Consulta LINQ para obtener el listao de temas junto con la location a la que pertenece y el usurio de RH que teine epermisos sobre dicho theme
                // var theme = await _context.Theme.ToListAsync();
                /*var theme2 = await _context.Theme.Include(hru => hru.HR_Users).Include(l => l.Locations).ToListAsync();*/
                // var listadP = await _context.Theme.Select(t => new {t.Theme_Name, t.Theme_Status, t.ThemeId, t.Locations }).Include(l => l.Locations).Select( l => new Locations() { l.LocationsId, l.})
                // var dataTheme = await _context.Locations.Join(_context.HR_Users, dir => dir.HR_Users, hru => hru.HR_UserId, (dir, hru) => new { dir, hru}

                /*var prueba1 = _context.LocationsThemes.Join(_context.Locations, lt => lt.LocationId, l => l.LocationsId, (lt, l) => new
                {
                    lt,
                    l
                }).Join(_context.Theme, lt => lt.lt.ThemeId, t => t.ThemeId, (lt, t) => new
                {
                    lt.l.LocationsId,
                    lt.l.Location_Name,
                    t.ThemeId,
                    t.Theme_Name
                }).ToListAsync();*/

                var thems = from location in _context.Locations
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

                // AUN NO FUNCIONA CON SINTAXIS DE METODO
                /*var prueba3 = _context.LocationsThemes.Join(_context.Locations, locth => locth.LocationId, loc => loc.LocationsId, (locth, loc) => new
                {
                    locth,
                    loc
                }).Join(_context.Theme, locth => locth.locth.ThemeId, th => th.ThemeId, (locth, th) => new
                {
                    locth.loc.LocationsId,
                    locth.loc.Location_Name,
                    th.ThemeId,
                    th.Theme_Name
                }).ToListAsync();*/

                return Ok(thems);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ThemeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ThemeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ThemeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ThemeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
