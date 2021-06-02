using CENSO.Models;
using CensoAPI02.Intserfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Post([FromBody] string login)
        {
            try
            {
                var user = _context.HR_Users.Where(log => log.User_Name == login).FirstOrDefault();
                return Ok(user);
            }catch( Exception ex)
            {
                return BadRequest( ex.Message);
            }
        }
    }
}
