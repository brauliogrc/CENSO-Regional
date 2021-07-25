using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models;
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
    [ApiController][Authorize(Policy = "StaffRH")]
    public class AnonRequestController : ControllerBase
    {
        private readonly CDBContext _context;

        public AnonRequestController(CDBContext context)
        {
            _context = context;
        }

        // Registrar una nueva peticion anonima
        [HttpPost][Route("newAnonRequest")][AllowAnonymous]
        public async Task<IActionResult> addNewAnonRequest([FromBody] AddAnonRequestInterface newAnonRequest)
        {
            try
            {
                // Asignacion de valores a los campos de la tabla AnonRequest
                var addAnonRequest = new AnonRequest()
                {
                    arEmployeeType = newAnonRequest.arEmployeeType,
                    arIssue = newAnonRequest.arIssue,
                    arAttachement = newAnonRequest.arAttachemen,
                    arCreationDate = DateTime.Now,
                    QuestionId = newAnonRequest.QuestionId,
                    AreaId = newAnonRequest.AreaId,
                    ThemeId = newAnonRequest.ThemeId,
                    LocationId = newAnonRequest.LocationId,
                    StatusId = 1,

                    // Datos nulos
                    arModificationUser = null,
                    arModificationDate = null
                };

                // Registro de la nueva peticion anonima en la tabla AnonRequest
                _context.AnonRequests.Add(addAnonRequest);
                await _context.SaveChangesAsync();

                return Ok(new { addAnonRequest, message = $"Peticion {addAnonRequest.arId} registrada con exito" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al registrar la peticion. Error: {ex.InnerException}" });
            }
        }

        // Eliminación lógica de la peticion anonima
        [HttpDelete][Route("deleteAnonRequest/{anonRequestId}")][AllowAnonymous]
        public async Task<IActionResult> deleteAnonRequest(int anonRequestId)
        {
            try
            {
                // Busqueda de la pticion por medio del Id
                var query = await _context.AnonRequests.FindAsync(anonRequestId);

                if (query == null)
                {
                    return NotFound(new { message = $"La peticion {anonRequestId} no se encuentra en la base de datos" });
                }

                // Modificacion del campo status para la eliminació lógica
                query.StatusId = 4;
                _context.AnonRequests.Update(query);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"La peticion {query.arId}, fue eliminada con exito" });

            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al eliminar la peticion. Error: {ex.Message}" });
            }
        }
    }
}
