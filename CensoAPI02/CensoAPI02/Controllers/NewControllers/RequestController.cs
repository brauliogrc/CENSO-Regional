﻿using CENSO.Models;
using CensoAPI02.Intserfaces;
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
    [ApiController][Authorize]
    public class RequestController : ControllerBase
    {
        private readonly CDBContext _context;
        private EmailHandler handler = new EmailHandler();

        public  RequestController(CDBContext context)
        {
            _context = context;
        }

        // Registrar una nueva peticion (requiere autorize)
        [HttpPost][Route("newRequest")][AllowAnonymous]
        public async Task<IActionResult> addNewRequest([FromForm] AddRequestInterface newRequest)
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
                    //rAttachement = newRequest.rAttachement,
                    AreaId = newRequest.AreaId,
                    QuestionId = newRequest.QuestionId,
                    ThemeId = newRequest.ThemeId,
                    LocationId = newRequest.LocationId,
                    StatusId = 1,

                    // Datos nulos
                    rModificationUser = null,
                    rModificationDate = null
                };

                // Registro de la nueva peticion en la tabla Request
                _context.Requests.Add(addRequest);
                await _context.SaveChangesAsync();

                // Guardado del archivo adjunto
                var file = newRequest.rAttachement;
                var folderName = Path.Combine("Resources", "Evidences");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file != null)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);

                        // cambiando el nombre al archivo
                        //var assetsRoute = 'assets\\Evidences\\Tickets' + newRequest.rAttachement + '.' + extencion;
                        var extencion = Path.GetExtension(file.FileName).Substring(1);
                        var newName = addRequest.rId;
                        var newPath = pathToSave + '\\' + newName + '.' + extencion;

                        using (var fileStream = new FileStream(newPath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        Console.WriteLine($"Full Path: {fullPath}\ndbPath: {dbPath}");
                        addRequest.rAttachement = newPath;
                    }

                    _context.Requests.Update(addRequest);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    addRequest.rAttachement = null;
                }

                // Envio de correo electronico
                MailData mailData = new MailData(addRequest.rId, addRequest.ThemeId, addRequest.rIssue);
                mailData.themeName = handler.getThemeName(_context, mailData.themeId);
                mailData.emails = handler.getUserEmails(_context, mailData.themeId);
                handler.sendMails(mailData);

                return Ok(new { addRequest.rId, message = $"Peticion {addRequest.rId}, registrada con exito" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al registrar la peticion. Error {ex.Message}" });
            }
        }

        //Eliminacion ogica de la peticion (requiere policy staff rh)
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
