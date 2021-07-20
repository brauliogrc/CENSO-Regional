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
    public class AnswerController : ControllerBase
    {
        private readonly CDBContext _context;

        public AnswerController(CDBContext context)
        {
            _context  = context;
        }

        // Registro de una nueva respuesta de tiket y actualizacion del tiket respondido
        [HttpPost]
        [Route("newAnswer")]
        [AllowAnonymous]
        public async Task<IActionResult> addNewAnswer([FromBody] AddAnswerInterface newAnswer)
        {
            try
            {
                var addAnswer = new AnswerStatus()
                {
                    UserId = newAnswer.asUserId,
                    asAnswer = newAnswer.asAnswer,
                    RequestId = newAnswer.RequestId,
                    asCrestionDate = DateTime.Now
                };

                // Registro de la nueva respuesta en la tabla AnswerStatus
                _context.Answer.Add(addAnswer);
                await _context.SaveChangesAsync();

                // Modificacion del tiket
                var ticketModification = await _context.Requests.FindAsync(newAnswer.RequestId);
                ticketModification.rModificationUser = newAnswer.asUserId;
                ticketModification.rModificationDate = DateTime.Now;
                ticketModification.StatusId = 2;

                // Regsitro de las modificaciones del tiket
                _context.Requests.Update(ticketModification);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Respuesta {addAnswer.asId} del tiket {newAnswer.RequestId}, registrada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al registrar la respuesta. Error: {ex.InnerException.Message}" });
            }
        }
    }
}

