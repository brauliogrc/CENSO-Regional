using CENSO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CensoAPI02.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models.UnionTables;
using CensoAPI02.UnionTables;

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
        // Obtencion de temas para ser mostrados en la tabla del temas.component
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                // Consulta LINQ para obtener el listao de temas junto con la location a la que pertenece y el usurio de RH que teine epermisos sobre dicho theme
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

        // POST api/<ThemeController>
        // Agregar un nuevo tema a la base de datos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddTheme value)
        {
            try
            {
                var newTheme = new Theme() // AGREGAR CREATIONUSER
                {
                    tName = value.tName,
                    tStatus = value.tStatus,
                    tCreationDate = DateTime.Now,
                    tCreationUser = 1
                };
                _context.Theme.Add(newTheme);
                await _context.SaveChangesAsync();

                var getThemeId = await _context.Theme
                    .Where(t => t.tName == value.tName && t.tCreationDate == newTheme.tCreationDate)
                    .Select(t => t.tId)
                    .FirstOrDefaultAsync();

                var newRelationship = new LocationsTheme()
                {
                    ThemeId = getThemeId,
                    LocationId = value.LocationId
                };
                _context.LocationsThemes.Add(newRelationship);
                await _context.SaveChangesAsync();

                return Ok(newTheme);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ThemeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ThemeController>/5
        // Eliminación lógica de un tema
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var query = await _context.Theme.FindAsync(id);
                if(query == null)
                {
                    return BadRequest();
                }

                query.tStatus = false;

                _context.Theme.Update(query);
                await _context.SaveChangesAsync();

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
