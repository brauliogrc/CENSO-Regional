using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models.UnionTables;
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
    [ApiController][Authorize(Policy = "SURH")]
    public class QuestionController : ControllerBase
    {
        private readonly CDBContext _context;

        public QuestionController(CDBContext context)
        {
            _context = context;
        }

        // Registro de una nueva pregunta
        [HttpPost][Route("newQuestion")][AllowAnonymous]
        public async Task<IActionResult> addNewQuestion([FromBody] AddQuestionInterface newQuestion)
        {
            try
            {
                // Asignacion de valores a los campos de la tabla Questions
                var addQuestion = new Question()
                {
                    qName = newQuestion.qName,
                    qCreationDate = DateTime.Now,
                    qCreationUser = newQuestion.qCreationUser,
                    qStatus = newQuestion.qStatus
                };

                // Registro de la nueva pregunta a la tabla Questions
                _context.Questions.Add(addQuestion);
                await _context.SaveChangesAsync();

                // Asignacion de los valores a los campos de la tabla QuestionsTheme para la relación de los registros
                var addRelationship = new QuestionsTheme()
                {
                    QuestionId = addQuestion.qId,
                    ThemeId = newQuestion.ThemeId
                };

                // Registro de la relacion en la tabla QuestionsTheme
                _context.QuestionsThemes.Add(addRelationship);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"La pregunta {addQuestion.qName}, se ha registrado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al registar la pregunta. Error: {ex.Message}" });
            }
        }

        // Eliminacion logica de pregunta
        [HttpDelete][Route("deleteQuestion/{questionId}")][AllowAnonymous]
        public async Task<IActionResult> deleteQuestion(int questionId)
        {
            try
            {
                // Busqueda de la pregunta por medio del id
                var query = await _context.Questions.FindAsync(questionId);

                if (query == null)
                {
                    return NotFound(new { message = $"La pregunta {questionId}, no se encuentra en la bse de datos" });
                }

                // Modificacion del status para la eliminacion logica
                query.qStatus = false;
                _context.Questions.Update(query);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"La pregunta {questionId}, fue eliminada con exito" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al eliminar la pregunta. Error: {ex.Message}" });
            }
        }
    }
}
