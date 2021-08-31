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
        [HttpPost][Route("newQuestion")]
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
        [HttpDelete][Route("deleteQuestion/{questionId}")]
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

        // Acualización de la pregunta
        [HttpPatch][Route("questionUpdate")]
        public async Task<IActionResult> questionUpdate([FromBody] ItemUpdate item)
        {
            bool flagUpdate = false;
            try
            {
                var question = await _context.Questions.FindAsync(item.itemId);

                if ( question == null)
                {
                    return NotFound(new { message = $"No se ha encontrado la pregunta en la base de datos" });
                }

                if ( item.itemName != null && item.itemName.Length != 0 && question.qName != item.itemName )
                {
                    question.qName = item.itemName;
                    flagUpdate = true;
                }

                if ( item.itemStatus != null && question.qStatus != item.itemStatus )
                {
                    string newStatus = item.itemStatus.ToString();
                    question.qStatus = Boolean.Parse(newStatus);
                    flagUpdate = true;
                }

                if ( flagUpdate )
                {
                    question.qModificationUser = item.modificationUser;
                    question.qModificationDate = DateTime.Now;

                    _context.Questions.Update(question);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = $"La pregunta se ha actualizado con exito." });
                }

                return Ok(new { message = $"Ningun cambio realizado" });
            }
            catch ( Exception ex )
            {
                return BadRequest(new { message = $"Ha ocurrio un error al actualizar el usuario" });
            }
        }

        // Añadir relación entre una pregunta y un tema
        [HttpPost][Route("addRelatedTheme")]
        public async Task<IActionResult> addRelatedTheme([FromBody] AddThemeRelationship themeRelationship)
        {
            try
            {
                var search = (from qt in _context.QuestionsThemes
                              where qt.QuestionId == themeRelationship.itemId && qt.ThemeId == themeRelationship.themeId
                              select qt).FirstOrDefault();

                if ( search != null)
                {
                    return Ok(new { message = $"La pregunta ya se encuentra relacionada con este tema." });
                }

                var newRelationship = new QuestionsTheme()
                {
                    QuestionId = themeRelationship.itemId,
                    ThemeId = themeRelationship.themeId
                };

                _context.QuestionsThemes.Add(newRelationship);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Relación añadida correctamente." });
            }
            catch ( Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al agregar la relación de la pregunta con el tema. Error: {ex.Message}" });
            }
        }

        // Eliminación de relación entre una pregunta y un tema
        [HttpDelete][Route("deleteRelatedTheme/{themeId}/{questionId}")]
        public async Task<ActionResult> deleteRelatedTheme(int themeId, int questionId)
        {
            try
            {
                var relatedTheme = (from qt in _context.QuestionsThemes
                                    where qt.ThemeId == themeId && qt.QuestionId == questionId
                                    select qt).FirstOrDefault();

                if ( relatedTheme == null )
                {
                    return NotFound(new { message = $"No se ha encontrado la relación entre la pregunta y el tema" });
                }

                _context.QuestionsThemes.Remove(relatedTheme);
                await _context.SaveChangesAsync();

                return Ok( new { message = $"La relación se eliminó corectamente" });

            }
            catch ( Exception ex )
            {
                return BadRequest(new { message = $"Ha ocurrido un error al eliminar la relación con el tema. Error {ex.Message}" });
            }
        }
    }
}
