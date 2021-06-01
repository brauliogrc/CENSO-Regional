using CENSO.Models;
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
        // GET: api/<QuestionController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {

            try
            {
                var questionList = await _context.Questions.Include(t => t.Themes).ToListAsync();

                var prueba1 = await _context.Questions.Join(_context.QuestionsThemes, q => q.QuestionId, qth => qth.QuestionId, (q, qth) => new
                {
                    q,
                    qth
                }).Join(_context.Theme, qth => qth.qth.ThemeId, th => th.ThemeId, (qth, th) => new
                {
                    qth.q.QuestionId,
                    qth.q.Question_Name,
                    qth.q.Question_Status,
                    th.ThemeId,
                    th.Theme_Name
                }).ToListAsync();

                /*var prueba2 = from question in _context.Questions
                              join qt in _context.QuestionsThemes on question.QuestionId equals qt.QuestionId
                              join theme in _context.Theme on qt.ThemeId equals theme.ThemeId
                              select new
                              {
                                  question.Question_Name,
                                  question.QuestionId,
                                  theme.ThemeId,
                                  theme.Theme_Name

                              };*/

                return Ok(prueba1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<QuestionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<QuestionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<QuestionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<QuestionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
