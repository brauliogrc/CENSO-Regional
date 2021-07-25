using CENSO.Models;
using CensoAPI02.Intserfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CensoAPI02.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController][Authorize(Policy = "SURH")]
    public class RH_UsersController : ControllerBase
    {
        private readonly CDBContext _context;

        public RH_UsersController(CDBContext context)
        {
            _context = context;
        }
        // GET: api/<RH_UsersController>
        [HttpGet]
        public string Get()
        {
            return "Usuarios";
            /*try
            {
                /* La funcion JOIN pordemos llamar a nuestras colecciones de datos, estableciendo campos de relación entre las colecciones de hr_users y locations
                 * es la forma de hacer un INNER JOIN linq
                
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
            }*/
        }

        [HttpGet][Route("GetRoles")]
        public async Task<ActionResult> GetRoles()
        {
            try
            {
                var query = await _context.Roles.Select(rol => new
                {
                    rol.rolId,
                    rol.rolName
                }).ToListAsync();
                return Ok(query);
            }catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<RH_UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserInterface newUser)
        {
            try
            {
                var newUser2 = new HRU() // AGREGAR CREATIONuSER
                {
                    uName = newUser.uName,
                    uEmail = newUser.uEmail,
                    RoleId = newUser.RolId,
                    uCreationDate = DateTime.Now,
                    uCreationUser = 1,
                    uEmployeeNumber = newUser.EmployeeNumber,
                    //uModificationDate = DateTime.Now,
                    //LocationId = newUser.LocationId
                };

                _context.HRU.Add(newUser2);
                await _context.SaveChangesAsync();
                return Ok(newUser2);
            }catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<RH_UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var query = await _context.HRU.FindAsync(id);
                if(query == null)
                {
                    return NotFound();
                }

                query.uStatus = false;
                _context.HRU.Update(query);
                await _context.SaveChangesAsync();

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*private void insertData()
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
                uModificationUser = 2,
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
                uModificationUser = 2,
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
        }*/
    }
}
