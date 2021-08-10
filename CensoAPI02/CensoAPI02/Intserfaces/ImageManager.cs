using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CensoAPI02.Intserfaces
{
    public class ImageManager
    {
        public string saveTicketImage(IFormFile image, int requestId)
        {
            // Guardado del archivo adjunto
            var file = image;
            var folderName = Path.Combine("Resources", "Request");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            string newPath;

            if (file != null)
            {
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    // cambiando el nombre al archivo
                    var extencion = Path.GetExtension(file.FileName).Substring(1);
                    var newName = requestId;
                    newPath = pathToSave + '\\' + newName + '.' + extencion;

                    using (var fileStream = new FileStream(newPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    Console.WriteLine($"Full Path: {fullPath}\ndbPath: {dbPath}");
                    return newPath;
                }

                return null;
                //_context.AnonRequests.Update(addAnonRequest);
                //await _context.SaveChangesAsync();
            }
            else
            {
                return null;
            }
        }

        public string saveAnswerImage(IFormFile image, int answerId)
        {
            // Guardado del archivo adjunto
            var file = image;
            var folderName = Path.Combine("Resources", "Answer");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            string newPath;

            if (file != null)
            {
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    // cambiando el nombre al archivo
                    var extencion = Path.GetExtension(file.FileName).Substring(1);
                    var newName = answerId;
                    newPath = pathToSave + '\\' + newName + '.' + extencion;

                    using (var fileStream = new FileStream(newPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    Console.WriteLine($"Full Path: {fullPath}\ndbPath: {dbPath}");
                    return newPath;
                }

                return null;
                //_context.AnonRequests.Update(addAnonRequest);
                //await _context.SaveChangesAsync();
            }
            else
            {
                return null;
            }
        }
    }
}
