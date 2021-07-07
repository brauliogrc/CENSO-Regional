using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AreasController : ControllerBase
    {
        private readonly CDBContext _context;

        public AreasController(CDBContext context)
        {
            _context = context;
        }

        // POST api/<AreasController>
        // Agregar una nueva area a la base de datos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddArea value)
        {
            try
            {
                var newArea = new Area()
                {
                    aName = value.aName,
                    locationId = value.LocationId
                };
                _context.Areas.Add(newArea);
                await _context.SaveChangesAsync();
                return Ok(newArea);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
