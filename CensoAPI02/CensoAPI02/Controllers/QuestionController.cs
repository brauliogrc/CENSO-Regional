using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models.UnionTables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {

        private readonly CDBContext _context;

        public QuestionController(CDBContext context)
        {
            _context = context;
        }

        // POST api/<QuestionController>
        // Agregar una nueva pregunta a la base de datos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddQuestion value)
        {
            try
            {
                var newQuestion = new Question() // AGREGAR CREATIONUSER
                {
                    qName = value.qName,
                    qStatus = value.qStatus,
                    qCreationDate = DateTime.Now,
                    qCreationUser = 1 // Se obtiene automaticamente tras el logueo
                };

                _context.Questions.Add(newQuestion);
                await _context.SaveChangesAsync();

                var getQuestionId = await _context.Questions
                    .Where(q => q.qName == value.qName && q.qCreationDate == newQuestion.qCreationDate)
                    .Select(q => q.qId)
                    .FirstOrDefaultAsync();

                var newRelationship = new QuestionsTheme()
                {
                    QuestionId = getQuestionId,
                    ThemeId = value.ThemeId
                };

                _context.QuestionsThemes.Add(newRelationship);
                await _context.SaveChangesAsync();

                return Ok(newQuestion);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<QuestionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<QuestionController>/5
        // Eliminación lógica de pregunta
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var query = await _context.Questions.FindAsync(id);

                if(query == null)
                {
                    return NotFound();
                }
                query.qStatus = false;
                _context.Questions.Update(query);
                await _context.SaveChangesAsync();

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
