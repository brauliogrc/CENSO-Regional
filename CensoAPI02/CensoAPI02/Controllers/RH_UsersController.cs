using CENSO.Models;
using CensoAPI02.Intserfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RH_UsersController : ControllerBase
    {
        private readonly CDBContext _context;

        public RH_UsersController(CDBContext context)
        {
            _context = context;
        }
        // GET: api/<RH_UsersController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                //insertData();
                // +++++ELIMINAR COMENATARIOS++++++++++++++
                //var users = await _context.HR_Users.Include(t => t.LocationsId).ToListAsync();
                //var users = await _context.HR_Users.Select(HRU => new { HRU.HR_UserId, HRU.User_Name, HRU.User_Email, HRU.User_Rol, HRU.User_Status, HRU.LocationsId }).ToListAsync();
                //var localidades = await _context.Locations.Where(x => x.LocationsId == 1).Include(x => x.HR_Users).ToListAsync();
                // var users = await _context.Locations.Include(hru => hru.HR_Users).ToListAsync();

                /* La funcion JOIN pordemos llamar a nuestras colecciones de datos, estableciendo campos de relación entre las colecciones de hr_users y locations
                 * es la forma de hacer un INNER JOIN linq
                */
                var users = await _context.HR_Users.Join(_context.Locations, hru => hru.LocationsId, location => location.LocationsId, (user, location) => new
                {
                    // Colocamos solo los datos que queremos que traiga nuestra consulta
                    user.HR_UserId, user.User_Name, user.User_Email, user.User_Rol, user.User_Status,
                    location.Location_Name
                }).ToListAsync();

                return Ok(users);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<RH_UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RH_UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserInterface newUser)
        {
            try
            {
                var newUser2 = new HR_User()
                {
                    User_Name = newUser.Name,
                    User_Email = newUser.Email,
                    User_Rol = newUser.Rol,
                    User_Status = newUser.Status,
                    User_Creeation_Date = DateTime.UtcNow,
                    User_Modification_Date = DateTime.UtcNow,
                    LocationsId = newUser.LocationId
                };

                _context.HR_Users.Add(newUser2);
                // Console.WriteLine( newUser. );
                await _context.SaveChangesAsync();
                return Ok(newUser2);
            }catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<RH_UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RH_UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private void insertData()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var theme1 = new Theme()
            {
                Theme_Name = "Tema 1 de prueba",
                Theme_Status = true,
                Theme_Creation_Date = DateTime.UtcNow,
                Theme_Creation_User = 1,
                Theme_Modification_date = DateTime.UtcNow,
                Theme_Modification_User = 2,
            };

            var user1 = new HR_User()
            {
                User_Name = "Braulio Grc",
                User_Email = "Braulio Email",
                User_Rol = "Admin",
                User_Status = true,
                User_Creation_User = 1,
                User_Creeation_Date = DateTime.UtcNow,
                User_Modification_Date = DateTime.UtcNow,
                User_Modification_User = 2,
                Themes = new() { theme1 },
            };

            var location1 = new Locations()
            {
                Location_Creation_Date = DateTime.UtcNow,
                Location_Creation_User = 1,
                Location_Modification_Date = DateTime.UtcNow,
                Location_Modification_User = 2,
                Location_Name = "Tijera",
                Location_Status = true,
                HR_Users = new() { user1 },
                Themes = new() { theme1 }
            };

            var theme2 = new Theme
            {
                Theme_Creation_Date = DateTime.UtcNow,
                Theme_Creation_User = 2,
                Theme_Modification_date = DateTime.UtcNow,
                Theme_Modification_User = 2,
                Theme_Name = "Psicopato",
                Theme_Status = true,
            };

            var user2 = new HR_User()
            {
                User_Creation_User = 2,
                User_Creeation_Date = DateTime.UtcNow,
                User_Email = "Soledad Email",
                User_Modification_Date = DateTime.UtcNow,
                User_Modification_User = 2,
                User_Rol = "hr",
                User_Name = "Soledad",
                User_Status = true,
                Themes = new() { theme2, theme1 }
            };

            var location2 = new Locations()
            {
                Location_Creation_Date = DateTime.UtcNow,
                Location_Creation_User = 2,
                Location_Modification_Date = DateTime.UtcNow,
                Location_Modification_User = 1,
                Location_Name = "periferico",
                Location_Status = true,
                HR_Users = new() { user2 },
                Themes = new() { theme2 }
            };

            var question1 = new Question()
            {
                Question_Creation_Date = DateTime.UtcNow,
                Question_Creation_User = 1,
                Question_Modification_Date = DateTime.UtcNow,
                Question_Modification_User = 2,
                Question_Name = "El cereal si es sopa?",
                Question_Status = true,
                Themes = new() { theme1 }
            };

            var question2 = new Question()
            {
                Question_Creation_Date = DateTime.UtcNow,
                Question_Creation_User = 2,
                Question_Modification_Date = DateTime.UtcNow,
                Question_Modification_User = 1,
                Question_Name = "El agua está mojada?",
                Question_Status = false,
                Themes = new() { theme2 }
            };

            _context.AddRange(theme1, user1, location1, theme2, user2, location2, question1, question2);
            _context.SaveChanges();
        }
    }
}
