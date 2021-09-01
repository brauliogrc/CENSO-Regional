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
    [ApiController][Authorize(Policy = "SURH")]
    public class AreaController : ControllerBase
    {
        private readonly CDBContext _context;

        public AreaController(CDBContext context)
        {
            _context = context;
        }

        // Registro de una nueva area (requiere policy SURh)
        [HttpPost][Route("newArea")]
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
        [HttpDelete][Route("deleteArea/{areaId}")]
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

        // Actualización de un area
        [HttpPost][Route("areaUpdate")]
        public async Task<IActionResult> areaUpdate([FromBody] ItemUpdate item)
        {
            bool flagUpdate = false;
            try
            {
                var area = await _context.Areas.FindAsync(item.itemId);

                if ( area == null )
                {
                    return NotFound(new { message = $"No se ha encontrado el area en la base de datos" });
                }

                if ( item.itemName != null && item.itemName.Length != 0 && area.aName != item.itemName )
                {
                    area.aName = item.itemName;
                    flagUpdate = true;
                }

                if ( item.itemStatus != null && area.aStatus != item.itemStatus )
                {
                    string newStatus = item.itemStatus.ToString();
                    area.aStatus = Boolean.Parse(newStatus);
                    flagUpdate = true;
                }

                if ( item.locationId != null && item.locationId != 0 )
                {
                    var search = (from al in _context.AreasLocations
                                  where al.AreaId == area.aId
                                  select al).FirstOrDefault();

                    if ( search != null )
                    {
                        string newLocation = item.locationId.ToString();
                        search.LocationId = Int32.Parse(newLocation);

                        _context.AreasLocations.Update(search);
                        await _context.SaveChangesAsync();

                        flagUpdate = true;
                    }
                }

                if ( flagUpdate )
                {
                    _context.Areas.Update(area);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = $"El area se ha actualizado con exito." });
                }

                return Ok(new { message = $"Ningun cambio realizado." });
            }
            catch ( Exception ex )
            {
                return BadRequest(new { message = $"Ha ocurrido un error al actualizar el area. Error: {ex.Message}" });
            }
        }
    }
}
