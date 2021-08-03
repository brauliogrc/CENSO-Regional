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
                // Asignacion de valores a los campos de la tabla AnonRequest
                var addAnonRequest = new AnonRequest()
                {
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

                // Agregando los datos a la interface del correo
                

                EmailInformation emailInformation = new EmailInformation();
                emailInformation.themeId = newAnonRequest.ThemeId;
                emailInformation.locationId = newAnonRequest.LocationId;
                emailInformation.Issue = newAnonRequest.arIssue;

                // Registro de la nueva peticion anonima en la tabla AnonRequest
                _context.AnonRequests.Add(addAnonRequest);
                await _context.SaveChangesAsync();

                // Guardado del archivo adjunto
                var file = newAnonRequest.arAttachement;
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
                }

                List<string> emails = new List<string>();

                var email = from user in _context.HRU
                            join ut in _context.HRUsersThemes on user.uEmployeeNumber equals ut.UserId
                            join theme in _context.Theme on ut.ThemeId equals theme.tId
                            where user.uStatus == true && theme.tId == newAnonRequest.ThemeId
                            select new { user.uEmail };

                foreach (var item in email)
                {
                    emails.Add(item.uEmail);
                }

                EmailHandler emailHandler = new EmailHandler();
                emailHandler.sendMails(emails, emailInformation);
                emailHandler = null;


                return Ok(new { addAnonRequest, message = $"Peticion {addAnonRequest.arId} registrada con exito" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al registrar la peticion. Error: {ex.Message}" });
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
