using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly CDBContext _context;

        public LoginController(CDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginInterface login)
        {
            try
            {
                var user = await _context.HRU.Select(hru => new { hru.uName, hru.uId, hru.uEmail, hru.LocationId }).Where(hru => hru.uName == login.username && hru.uEmail == login.email).FirstOrDefaultAsync();
                /*var user = await _context.HRU
                    .Where(hru => hru.uStatus == true && hru.uName == login.username && hru.uEmail == login.email)
                    .Select(hru => new
                {
                    hru.uName,
                    hru.uEmployeeNumber,
                    hru.uId,
                    hru.uEmail,
                    hru.LocationId
                }).FirstOrDefaultAsync();*/

                if(user == null)
                {
                    return NotFound(new { message = "Usuario no encontrado en la base de datos" });
                }

                return Ok(user);
            }catch( Exception ex )
            {
                return BadRequest( ex.Message);
            }
        }
    }
}
