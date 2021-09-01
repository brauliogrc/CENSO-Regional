using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CensoAPI02.Controllers.NewControllers
{
    [Route("api/[controller]")]
    [ApiController][Authorize(Policy = "StaffRH")]
    public class AnswerController : ControllerBase
    {
        private readonly CDBContext _context;
        private static TicketAnswered answered = new TicketAnswered();
        private AnswerData respuesta;
        private ImageManager imageManager = new ImageManager();
        private string path;


        public AnswerController(CDBContext context)
        {
            _context  = context;
        }

        // Registro de una nueva respuesta de tiket y actualizacion del tiket respondido
        [HttpPost]
        [Route("newAnswer")]
        public async Task<IActionResult> addNewAnswer([FromForm] AddAnswerInterface newAnswer)
        {
            int RequestId = Int32.Parse(newAnswer.RequestId);
            try
            {
                /*var addAnswer = new AnswerStatus()
                {
                    UserId = newAnswer.asUserId,
                    asCreationDate = DateTime.Now,
                };*/

                // Verifica si el ticket ya ha sido respondido, en caso de que si, se actualiza el contenido de la respuesta
                this.respuesta = answered.answered(_context, Int32.Parse(newAnswer.RequestId));
                if (this.respuesta.flag != 0)
                {
                    var actualizaRespuesta = await _context.Answer.FindAsync(this.respuesta.asId);
                    if (newAnswer.asAnswer != null)
                    {
                        actualizaRespuesta.asAnswer = newAnswer.asAnswer;
                    }
                    /*// Actualizacion del archivo adjunto
                    if (newAnswer.asAttachement != null)
                    {

                    }*/

                    actualizaRespuesta.asCreationDate = DateTime.Now;
                    actualizaRespuesta.UserId = newAnswer.asUserId;

                    var ticket = await _context.Requests.FindAsync(Int32.Parse(newAnswer.RequestId));
                    if (ticket == null)
                    {
                        var anonTicket = await _context.AnonRequests.FindAsync(Int32.Parse(newAnswer.RequestId));
                        if (anonTicket == null)
                        {
                            return NotFound(new { message = $"Ticket no encontrado en la base de datos" });
                        }

                        anonTicket.arModificationDate = DateTime.Now;
                        anonTicket.StatusId = newAnswer.requestStatus;
                        anonTicket.arModificationUser = newAnswer.asUserId;

                        _context.AnonRequests.Update(anonTicket);
                        await _context.SaveChangesAsync();

                        _context.Answer.Update(actualizaRespuesta);
                        await _context.SaveChangesAsync();

                        return Ok(new { message = $"Se ha actualizado la respuesta del ticket." });
                    }

                    ticket.rModificationDate = DateTime.Now;
                    ticket.StatusId = newAnswer.requestStatus;
                    ticket.rModificationUser = newAnswer.asUserId;

                    _context.Requests.Update(ticket);
                    await _context.SaveChangesAsync();

                    _context.Answer.Update(actualizaRespuesta);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = $"Se ha actualizado la respuesta del ticket." });
                }


                // Registro de la nueva respuesta
                var addAnswer = new AnswerStatus()
                {
                    UserId = newAnswer.asUserId,
                    asAnswer = newAnswer.asAnswer,
                    asCreationDate = DateTime.Now,
                };

                // Modificacion del tiket
                var ticketModification = await _context.Requests.FindAsync(RequestId);

                if (ticketModification == null)
                {
                    var anonTicketModification = await _context.AnonRequests.FindAsync(RequestId);

                    addAnswer.AnonRequestId = RequestId;
                    addAnswer.RequestId = null;
                    anonTicketModification.arModificationDate = DateTime.Now;
                    anonTicketModification.arModificationUser = newAnswer.asUserId;
                    anonTicketModification.StatusId = newAnswer.requestStatus;

                    // Regsitr de la moficacion el ticket anonimo
                    _context.AnonRequests.Update(anonTicketModification);
                    await _context.SaveChangesAsync();

                    // Registro de la nueva respuesta en la tabla AnswerStatus
                    _context.Answer.Add(addAnswer);
                    await _context.SaveChangesAsync();

                    // Guardado del archivio adjunto a la respuesta
                    this.path = imageManager.saveTicketImage(newAnswer.asAttachement, addAnswer.asId);

                    if (this.path != null)
                    {
                        addAnswer.asAttachement = this.path;
                        _context.Answer.Update(addAnswer);
                        await _context.SaveChangesAsync();
                    }

                    return Ok(new { message = $"Respuesta {addAnswer.asId} del tiket {RequestId}, registrada correctamente" });
                }

                addAnswer.RequestId = RequestId;
                addAnswer.anonRequest = null;
                ticketModification.rModificationUser = newAnswer.asUserId;
                ticketModification.rModificationDate = DateTime.Now;
                ticketModification.StatusId = newAnswer.requestStatus;

                // Regsitro de las modificaciones del tiket
                _context.Requests.Update(ticketModification);
                await _context.SaveChangesAsync();

                // Registro de la nueva respuesta en la tabla AnswerStatus
                _context.Answer.Add(addAnswer);
                await _context.SaveChangesAsync();

                // Guardado del archivio adjunto a la respuesta
                this.path = imageManager.saveTicketImage(newAnswer.asAttachement, addAnswer.asId);

                if (this.path != null)
                {
                    addAnswer.asAttachement = this.path;
                    _context.Answer.Update(addAnswer);
                    await _context.SaveChangesAsync();
                }

                /*// Guardado del archivo adjuto
                var file = newAnswer.asAttachement;
                var folderName = Path.Combine("Resources", "Answer");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file != null)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);

                        // cambiando el nombre al archivo
                        var extencion = Path.GetExtension(file.FileName).Substring(1);
                        var newName = addAnswer.asId;
                        var newPath = pathToSave + '\\' + newName + '.' + extencion;

                        using (var fileStream = new FileStream(newPath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        Console.WriteLine($"Full Path: {fullPath}\ndbPath: {dbPath}");
                        addAnswer.asAttachement = newPath;
                    }

                    _context.Answer.Update(addAnswer);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    addAnswer.asAttachement = null;
                }*/

                return Ok(new { message = $"Respuesta {addAnswer.asId} del tiket {RequestId}, registrada correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al registrar la respuesta. Error: {ex.Message}" });
            }
        }
    }
}

