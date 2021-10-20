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
    public class AnonRequestController : ControllerBase
    {
        private readonly CDBContext _context;
        private EmailHandler handler = new EmailHandler();
        private ImageManager imageManager = new ImageManager();

        public AnonRequestController(CDBContext context)
        {
            _context = context;
        }

        // Registrar una nueva peticion anonima
        [HttpPost][Route("newAnonRequest")][AllowAnonymous]
        public async Task<IActionResult> addNewAnonRequest([FromForm] AddAnonRequestInterface newAnonRequest)
        {
            try
            {
                int countRequest = (from reuqest in _context.Requests select reuqest.rId).Count();
                int countAnonRequest = (from anonRequest in _context.AnonRequests select anonRequest.arId).Count();
                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString();
                string day = DateTime.Now.Day.ToString();
                int date = Int32.Parse(year + month + day);
                int arId = date + countRequest + countAnonRequest;
                // Asignacion de valores a los campos de la tabla AnonRequest
                var addAnonRequest = new AnonRequest()
                {
                    arId = arId,
                    arEmployeeType = newAnonRequest.arEmployeeType,
                    arIssue = newAnonRequest.arIssue,
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

                // Guardado del archivio adjunto del ticket
                string path = imageManager.saveTicketImage(newAnonRequest.arAttachement, addAnonRequest.arId);

                if (path != null)
                {
                    addAnonRequest.arAttachement = path;
                    _context.AnonRequests.Update(addAnonRequest);
                    await _context.SaveChangesAsync();
                }

                /*// Guardado del archivo adjunto
                var file = newAnonRequest.arAttachement;
                var folderName = Path.Combine("Resources", "Request");
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
                        var newName = addAnonRequest.arId;
                        var newPath = pathToSave + '\\' + newName + '.' + extencion;

                        using (var fileStream = new FileStream(newPath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        Console.WriteLine($"Full Path: {fullPath}\ndbPath: {dbPath}");
                        addAnonRequest.arAttachement = newPath;
                    }

                    _context.AnonRequests.Update(addAnonRequest);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    addAnonRequest.arAttachement = null;
                }*/


                // Envio de correo electronico
                MailData mailData = new MailData(addAnonRequest.arId, addAnonRequest.ThemeId, addAnonRequest.arIssue);
                mailData.themeName = handler.getThemeName(_context, mailData.themeId);
                mailData.emails = handler.getUserEmails(_context, mailData.themeId);

                if (mailData.themeName == null || mailData.emails == null)
                {
                    return Ok(new { addAnonRequest, message = $"La peticion ha sido registrada, pero ha ocurrido un error al enviar correo de notificacion a RH. Favor de ponerse en contacto con soporte" });
                }

                handler.sendMails(mailData);

                return Ok(new { addAnonRequest, message = $"Peticion {addAnonRequest.arId} registrada con exito" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al registrar la peticion. Error: {ex.Message}" });
            }
        }

        // Eliminación lógica de la peticion anonima
        [HttpDelete][Route("deleteAnonRequest/{anonRequestId}")][Authorize(Policy = "StaffRH")]
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
