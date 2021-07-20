using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.UnionTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Controllers.NewControllers
{
    [Route("api/[controller]")]
    [ApiController][Authorize(Policy = "SURH")]
    public class ThemeController : ControllerBase
    {
        private readonly CDBContext _context;
        
        public ThemeController(CDBContext context)
        {
            _context = context;
        }

        // Registro de un nuevo tema
        [HttpPost][Route("newTheme")][AllowAnonymous]
        public async Task<IActionResult> addNewTheme([FromBody] AddThemeInterface newTheme)
        {
            try
            {
                // Asignacion de valores a los campos de la tabla Theme
                var addTheme = new Theme()
                {
                    tName = newTheme.tName,
                    tCreationDate = DateTime.Now,
                    tCreationUser = newTheme.tCreationUser,
                    tStatus = true
                };

                // Registro del nuevo tema en la tabla de la tabla Theme
                _context.Theme.Add(addTheme);
                await _context.SaveChangesAsync();

                // Asignacion de valores a los campos de la tabla LocationsThem para la realcion de los registros
                var addRelatioship = new LocationsTheme()
                {
                    ThemeId = addTheme.tId,
                    LocationId = newTheme.LocationId
                };

                // Registro de la realcion en la tabla LocationsThem
                _context.LocationsThemes.Add(addRelatioship);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"El tema {addTheme.tName}, se ha registrado correctamente" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al registar el tema. Error: {ex.Message}" });
            }
        }

        // Eliminación lógica de tema
        [HttpDelete][Route("deleteTheme/{themeId}")][AllowAnonymous]
        public async Task<IActionResult> deleteTheme(int themeId)
        {
            try
            {
                // Busqueda del tema por medio del id
                var query = await _context.Theme.FindAsync(themeId);

                if (query == null)
                {
                    return NotFound(new { message = $"El tema {themeId} no se encuentra en la base de datos" });
                }

                // Modificacion del status para la eliminacion lógica
                query.tStatus = false;
                _context.Theme.Update(query);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"El tema {themeId}, fue eliminado con exito" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al eliminar el tema. Error: {ex.Message}" });
            }
        }
    }
}
