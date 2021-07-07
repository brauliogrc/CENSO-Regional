using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LoginController : ControllerBase
    {
        private readonly CDBContext _context;
        private readonly IConfiguration _config;

        public LoginController(CDBContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpPost][AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] LoginInterface login)
        {
            try
            {
                var user = await _context.HRU.Select(hru => new { hru.uName, hru.uId, hru.uEmail, hru.LocationId, hru.RoleId }).Where(hru => hru.uName == login.username && hru.uEmail == login.email).FirstOrDefaultAsync();
                /*var user = await _context.HRU
                    .Where(hru => hru.uStatus == true && hru.uName == login.username && hru.uEmail == login.email)
                    .Select(hru => new
                {
                    hru.uName,
                    hru.uEmployeeNumber,
                    hru.uId,
                    hru.uEmail,
                    hru.LocationId
                }).FirstOrDefaultAsync();*/

                if(user == null)
                {
                    return NotFound(new { message = "Usuario no encontrado en la base de datos" });
                }

                var secretKey = _config.GetValue<string>("SecretKey"); // Leemos la llave secreta
                var key = Encoding.ASCII.GetBytes(secretKey); 

                var claims = new ClaimsIdentity(); // Creamos los claims para poder iniciar sesion
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.uId.ToString())); // Creación de un claim con el nombre del usuario
                claims.AddClaim(new Claim("Username", user.uName));
                claims.AddClaim(new Claim("Location", user.LocationId.ToString()));
                claims.AddClaim(new Claim("Role", user.RoleId.ToString()));

                

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims, // A grregamos los claims al descriptor de token
                    Expires = DateTime.UtcNow.AddMinutes(59), // Definimos el tiempo de expiración
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    // Agregamos los datos de inicio de sesion encriptados en Sha256
                };

                var tokenHnadler = new JwtSecurityTokenHandler();
                var createdToken = tokenHnadler.CreateToken(tokenDescriptor);
                string bearer_token = tokenHnadler.WriteToken(createdToken);

                UserResponse userResponse = new UserResponse(user.uName, user.uEmail, user.uId, user.LocationId, user.RoleId, bearer_token);
                return Ok(userResponse);
            }catch( Exception ex )
            {
                return BadRequest( ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = "Administrador")]
        public IActionResult Get()
        {
            var username = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            //var username = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes)
            var claims = User.Claims.ToList();

            if (username == null)
            {
                return Ok(new { message = "Nombre no encontrado" });
            }

            return Ok(new { message = "Valor en el claim: " + username.Value });
        }
    }
}
