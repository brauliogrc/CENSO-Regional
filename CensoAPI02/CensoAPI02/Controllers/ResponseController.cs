using CENSO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly CDBContext _context;

        public ResponseController(CDBContext context)
        {
            _context = context;
        }

        // Registro de una respuesta de tiket
        [HttpPost] [Route("ResponseTikets")]
        public async Task<IActionResult> newResponse([FromBody] bool valor)
        {
            return Ok();
        }

        // Registro de una respuesta de tiket anonimo
        [HttpPost][Route("ResponseAnonTikets")]
        public async Task<IActionResult> newAnonResponse([FromBody] bool valor)
        {
            return Ok();
        }
    }
}
