using CENSO.Models;
using CensoAPI02.Intserfaces;
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
    [ApiController][Authorize]
    public class RequestController : ControllerBase
    {
        private readonly CDBContext _context;

        public  RequestController(CDBContext context)
        {
            _context = context;
        }

        // Registrar una nueva peticion
        [HttpPost][Route("newRequest")][AllowAnonymous]
        public async Task<IActionResult> addNewRequest([FromBody] AddRequestInterface newRequest)
        {
            try
            {
                // Asignacion de valores a los campos de la tabla Request
                var addRequest = new Request()
                {
                    rUserId = newRequest.rUserId,
                    rEmployeeLeader = newRequest.rEmployeeLeader,
                    rUserName = newRequest.rUserName,
                    rEmployeeType = newRequest.rEmployeeType,
                    rCreationDate = DateTime.Now,
                    rIssue = newRequest.rIssue,
                    rAttachement = newRequest.rAttachement,
                    AreaId = newRequest.AreaId,
                    QuestionId = newRequest.QuestionId,
                    ThemeId = newRequest.ThemeId,
                    LocationId = newRequest.LocationId,
                    StatusId = 1
                };

                // Registro de la nueva peticion en la tabla Request
                _context.Requests.Add(addRequest);
                await _context.SaveChangesAsync();

                return Ok(new { addRequest, message = $"Peticion {addRequest.rId}, registrada con exito" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al registrar la peticion. Error {ex.Message}" });
            }
        }

        //Eliminacion ogica de la peticion
        [HttpDelete][Route("deleteRequest/{requestId}")][AllowAnonymous]
        public async Task<IActionResult> deleteRequest(int requestId)
        {
            try
            {
                // Busqueda de la peticion por medio del id
                var query = await _context.Requests.FindAsync(requestId);

                if (query == null)
                {
                    return NotFound(new { message = $"La prticion {requestId}, no se encuentra en la base de datos" });
                }

                // Modificacion del campo status para la elimiacion logica
                query.StatusId = 4;
                _context.Requests.Update(query);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"La peticion {requestId}, se ha eliminado con exito" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error el eliminar la peticion. Error: {ex.Message}" });
            }
        }
    }
}
