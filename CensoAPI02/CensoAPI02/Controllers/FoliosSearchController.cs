using CENSO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoliosSearchController : ControllerBase
    {
        private readonly CDBContext _context;

        public FoliosSearchController(CDBContext context)
        {
            _context = context;
        }

        // Busqueda de folios con base en el id
        [HttpGet][Route("folioSearch/{id}")]
        public async Task<ActionResult> GetFolio(int id)
        {
            try
            {
                var query = await _context.AnonRequests.Join(_context.Theme, ar => ar.ThemeId, th => th.tId, (ar, th) => new
                {
                    ar.arId,
                    ar.arIssue,
                    th.tId,
                    th.tName,
                    th.tStatus,
                }).Where(condition => condition.arId == id && condition.tStatus == true).FirstOrDefaultAsync();

                if(query == null)
                {
                    return NotFound(new { message = "Folio no encontrado en la base de datos" });
                }

                return Ok(query);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
