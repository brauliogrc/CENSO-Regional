using CENSO.Models;
using CensoAPI02.Intserfaces;
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
                // +++++ELIMINAR COMENATARIOS++++++++++
                /* La funcion JOIN pordemos llamar a nuestras colecciones de datos, estableciendo campos de relación entre las colecciones de hr_users y locations
                 * es la forma de hacer un INNER JOIN linq
                */
                var users = await _context.HRU.Join(_context.Locations, hru => hru.LocationId, location => location.lId, (user, location) => new
                {
                    // Colocamos solo los datos que queremos que traiga nuestra consulta
                    user.uId, user.uName, user.uEmail, user.uRol, user.uStatus,
                    location.lName
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
        public async Task<IActionResult> Post([FromBody] AddUserInterface newUser)
        {
            try
            {
                var newUser2 = new HRU()
                {
                    uName = newUser.Name,
                    uEmail = newUser.Email,
                    uRol = newUser.Rol,
                    uStatus = newUser.Status,
                    uCreationDate = DateTime.UtcNow,
                    uModificationDate = DateTime.UtcNow,
                    LocationId = newUser.LocationId
                };

                _context.HRU.Add(newUser2);
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
                tName = "Tema 1 de prueba",
                tStatus = true,
                tCreationDate = DateTime.UtcNow,
                tCreationUser = 1,
                tModificationDate = DateTime.UtcNow,
                tModificationUser = 2,
            };

            var user1 = new HRU()
            {
                uName = "Braulio Grc",
                uEmail = "Braulio Email",
                uRol = "Admin",
                uStatus = true,
                uCreationUser = 1,
                uCreationDate = DateTime.UtcNow,
                uModificationDate = DateTime.UtcNow,
                lModificationUser = 2,
                Themes = new() { theme1 },
            };

            var location1 = new Locations()
            {
                lCreationDate = DateTime.UtcNow,
                lCreationuser = 1,
                lModificationDate = DateTime.UtcNow,
                lModificationUser = 2,
                lName = "Tijera",
                lStatus = true,
                HRU = new() { user1 },
                Themes = new() { theme1 }
            };

            var theme2 = new Theme
            {
                tCreationDate = DateTime.UtcNow,
                tCreationUser = 2,
                tModificationDate = DateTime.UtcNow,
                tModificationUser = 2,
                tName = "Psicopato",
                tStatus = true,
            };

            var user2 = new HRU()
            {
                uCreationUser = 2,
                uCreationDate = DateTime.UtcNow,
                uEmail = "Soledad Email",
                uModificationDate = DateTime.UtcNow,
                lModificationUser = 2,
                uRol = "hr",
                uName = "Soledad",
                uStatus = true,
                Themes = new() { theme2, theme1 }
            };

            var location2 = new Locations()
            {
                lCreationDate = DateTime.UtcNow,
                lCreationuser = 2,
                lModificationDate = DateTime.UtcNow,
                lModificationUser = 1,
                lName = "periferico",
                lStatus = true,
                HRU = new() { user2 },
                Themes = new() { theme2 }
            };

            var question1 = new Question()
            {
                qCreationDate = DateTime.UtcNow,
                qCreationUser = 1,
                qModificationDate = DateTime.UtcNow,
                qModificationUser = 2,
                qName = "El cereal si es sopa?",
                qStatus = true,
                Themes = new() { theme1 }
            };

            var question2 = new Question()
            {
                qCreationDate = DateTime.UtcNow,
                qCreationUser = 2,
                qModificationDate = DateTime.UtcNow,
                qModificationUser = 1,
                qName = "El agua está mojada?",
                qStatus = false,
                Themes = new() { theme2 }
            };

            _context.AddRange(theme1, user1, location1, theme2, user2, location2, question1, question2);
            _context.SaveChanges();
        }
    }
}
