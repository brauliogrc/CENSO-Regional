using CENSO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// 
/// CONTROLADOR PARA OBTENER LAS OPCIONES QUE MOSTRARÁN LOS SEECT EN EL REGISTRO DE UNA PETICIÓN
/// O UNA PETICIÓN ANONIMA
/// 
/// </summary>

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsSearchesController : ControllerBase
    {
        private readonly CDBContext _context;

        public FieldsSearchesController(CDBContext context)
        {
            _context = context;
        }

        // Obtencion de las localidades para el folioanonimoindex.component
        [HttpGet][Route("FieldLocations")]
        public async Task<ActionResult> GetLocations()
        {
            try
            {
                var query = await _context.Locations.Where(l => l.lStatus == true).Select(l => new { l.lId, l.lName }).ToListAsync();

                if(query == null || query.Count == 0)
                {
                    return NotFound(new { message = "No hay localidades disponibles" });
                }

                return Ok(query);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        // Obtención de temas segun la localidad seleccionada
        [HttpGet][Route("FieldTheme/{id}")]
        public async Task<ActionResult> GetTheme(int id)
        {
            try
            {
                var query = await _context.Theme.Join(_context.LocationsThemes, th => th.tId, lt => lt.ThemeId, (th, lt) => new
                {
                    lt,
                    th
                }).Join(_context.Locations, lt => lt.lt.LocationId, l => l.lId, (lt, l) => new
                {
                    lt.th.tId,
                    lt.th.tName,
                    lt.th.tStatus,
                    l.lId
                }).Where(condition => condition.lId == id && condition.tStatus == true).ToListAsync();

                if(query == null || query.Count == 0)
                {
                    return NotFound(new {message="Ningun tema encontrado en esta localidad" });
                }

                return Ok(query);
            }catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // Obtención de preguntas segun el tema seleccionado
        [HttpGet][Route("FieldQuestions/{id}")]
        public async Task<ActionResult> GetQuestions(int id)
        {
            try
            {
                var query = await _context.Questions.Join(_context.QuestionsThemes, q => q.qId, qt => qt.QuestionId, (q, qt) => new
                {
                    q,
                    qt
                }).Join(_context.Theme, qt => qt.qt.ThemeId, th => th.tId, (qt, th) => new
                {
                    qt.q.qId,
                    qt.q.qName,
                    qt.q.qStatus,
                    th.tId
                }).Where(condition => condition.tId == id && condition.qStatus == true).ToListAsync();

                if(query == null || query.Count == 0)
                {
                    return NotFound(new { message = "Nunguna pregunta encontrada en este tema" });
                }

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Obtencion de las areas disponibles segun la localidad seleccioanda
        [HttpGet][Route("FieldAreas/{id}")]
        public async Task<ActionResult> GetAreas(int id)
        {
            try
            {
                var query = await _context.Areas.Join(_context.Locations, a => a.locationId, l => l.lId, (a, l) => new
                {
                    a.aId,
                    a.aName,
                    l.lId
                }).Where(condition => condition.lId == id).ToListAsync();

                if(query == null || query.Count == 0)
                {
                    return NotFound(new { message = "Ningun area encontrada en esta localidad" });
                }

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
