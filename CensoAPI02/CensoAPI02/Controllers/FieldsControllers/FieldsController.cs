using CENSO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Controllers.FieldsControllers
{
    // CONTROLADOR PARA EL FOLIO ANONIMO Y EL PANEL DE USUARIO
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly CDBContext _context;

        public FieldsController(CDBContext context)
        {
            _context = context;
        }

        // Obtencnion de localidades
        [HttpGet][Route("getLocations")]
        public async Task<ActionResult> getLocations()
        {
            try
            {
                var query = await _context.Locations
                    .Where(l => l.lStatus == true)
                    .Select(l => new { l.lId, l.lName })
                    .ToListAsync();

                if (query == null || query.Count == 0)
                {
                    return NotFound(new { message = $"Ninguna localidad encontrada en la base de datos" });
                }

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest (new { message = $"Ha ocurrido un error al obtener las localidades. Error: {ex.Message}" });
            }
        }

        // Obtencion de temas segun la locaidad
        [HttpGet][Route("getTheme/{locationId}")]
        public async Task<ActionResult> getTheme(int locationId)
        {
            try
            {               
                var query = from theme in _context.Theme
                            join lt in _context.LocationsThemes on theme.tId equals lt.ThemeId
                            join location in _context.Locations on lt.LocationId equals location.lId
                            where theme.tStatus == true && location.lId == locationId
                            select new
                            {
                                // Datos del tema
                                theme.tId,
                                theme.tName
                            };

                if (query == null || query.Count() == 0)
                {
                    return NotFound(new { message = $"Ningun thema se encuentra relacionado con esta localidad" });
                }

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener los temas. Error: {ex.Message}" });
            }
        }

        // Obtencion de preguntas segun el tema
        [HttpGet][Route("getQuestions/{themeId}")]
        public async Task<ActionResult> getQuestions(int themeId)
        {
            try
            {
                var query = from question in _context.Questions
                            join qt in _context.QuestionsThemes on question.qId equals qt.QuestionId
                            join theme in _context.Theme on qt.ThemeId equals theme.tId
                            where question.qStatus == true && theme.tId == themeId
                            select new
                            {
                                // Datos de la pregunta
                                question.qId,
                                question.qName
                            };

                if (query == null || query.Count() == 0)
                {
                    return NotFound(new { message = $"Nunguna pregunta se encuentra relacionada con este tema" });
                }

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener las preguntas. Error: {ex.Message}" });
            }
        }

        // Obtencion de las areas segun la localidad
        [HttpGet][Route("getAreas/{locationId}")]
        public async Task<ActionResult> getAreas(int locationId)
        {
            try
            {
                var query = from area in _context.Areas
                            join location in _context.Locations on area.locationId equals location.lId
                            where location.lId == locationId
                            select new
                            {
                                // Datos del area
                                area.aId,
                                area.aName
                            };

                if (query == null || query.Count() == 0)
                {
                    return NotFound(new { message = $"ninguna area se encuentra relacionada con est localidad" });
                }

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener las areas. Error: {ex.Message}" });
            }
        }
    }
}
