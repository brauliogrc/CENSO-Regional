using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models;
using CensoAPI02.Models.UnionTables;
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
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly CDBContext _context;

        public AreaController(CDBContext context)
        {
            _context = context;
        }

        // Registro de una nueva area (requiere policy SURh)
        [HttpPost][Route("newArea")][AllowAnonymous]
        public async Task<IActionResult> addNewArea([FromBody]AddAreaInterface newArea)
        {
            try
            {
                // Asignacion de vlores a los campos de la tabla Area
                var addArea = new Area()
                {
                    aName = newArea.aName,
                    aStatus = newArea.aStatus
                };

                // Registro de la nueva area en la tabala de Areas
                _context.Areas.Add(addArea);
                await _context.SaveChangesAsync();

                // Asinacion de relacion con las localidades
                var addRelationship = new AreasLocations()
                {
                    AreaId = addArea.aId,
                    LocationId = newArea.LocationId
                };

                // Registro de la relacion en la tabla AreasLocations
                _context.AreasLocations.Add(addRelationship);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"El area {addArea.aName}, se ha registrado correctamente" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al resgistrar el area. Error: {ex.Message}" });
            }
        }


        // Eliminación loógica del area
        [HttpDelete][Route("deleteArea/{areaId}")][AllowAnonymous]
        public async Task<IActionResult> deleteArea(int areaId)
        {
            try
            {
                var query = await _context.Areas.FindAsync(areaId);

                if (query == null)
                {
                    return NotFound(new { message = $"El area {areaId}, no se encuentra en la base de datos" });
                }

                // Modificación del status para la eliminación lógica
                query.aStatus = false;
                _context.Areas.Update(query);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"El area {areaId}, fue eliminada con exito" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al eliminar el area. Error: {ex.Message}" });
            }
        }

    }
}
