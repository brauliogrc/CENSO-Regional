using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using Newtonsoft.Json.Linq;

namespace CensoAPI02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LoginController : ControllerBase
    {
        private readonly CDBContext _context;
        private readonly IConfiguration _config;
        private bool authenticated;
        private string token;

        public LoginController(CDBContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpPost][AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] LoginInterface dataLogin)
        {
            try
            {
                if (!verifyHRPortal(dataLogin))
                {
                    return NotFound(new { message = "Usuario o contraseña incorrectos" });
                }

                // Consulta de usuario en la base de datos Censo
                var user = await _context.HRU
                    .Where(hru => hru.uEmployeeNumber == dataLogin.userNumber)
                    .Select(hru => new
                    {
                        hru.uName,
                        hru.uId,
                        hru.uEmail,
                        hru.LocationId,
                        hru.RoleId,
                        hru.uEmployeeNumber
                    }).FirstOrDefaultAsync();

                if (user == null)
                {
                    string query = "SELECT [EmployeeNumber], [Plant], [FullName], [SupervisorNumber] FROM [p_HRPortal].[dbo].[VW_EmployeeData] where EmployeeNumber=" + dataLogin.userNumber;
                    using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("HRPortal")))
                    {
                        UserAuthData userAuthData = new UserAuthData();
                        SqlCommand command = new SqlCommand(query, conn);
                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int i = 0;
                            while (reader.Read() && i == 0)
                            {
                                Console.WriteLine($"{reader.GetInt32(0)} - {reader.GetString(1)} - {reader.GetString(2)} - {reader.GetInt32(3)}");
                                userAuthData.setUserEmployeeNumer(reader.GetInt32(0));
                                userAuthData.setUserLocation(reader.GetString(1));
                                userAuthData.setUsername(reader.GetString(2));
                                userAuthData.setSupervisorNumber(reader.GetInt32(3));

                                i++;
                            }
                        }
                        conn.Close();
                        token = generateUserToken(userAuthData);
                        userAuthData.setToken(token);
                        return Ok(new { token });
                    }
                }

                AdministratorAuthData administrator = new AdministratorAuthData();
                administrator.setAdminId(user.uId);
                administrator.setAdminEmployeeNumber(user.uEmployeeNumber);
                administrator.setAdminRole(user.RoleId);
                administrator.setAdminLocation(user.LocationId);
                administrator.setAdminName(user.uName);
                administrator.setAdminEmail(user.uEmail);

                token = generateAdministratorToken(administrator);
                administrator.setToken(token);

                return Ok(new { token });
            }
            catch( Exception ex )
            {
                return BadRequest( ex.Message );
            }
        }

        // Obtencion de datos del claim
        [HttpGet]
        [Authorize(Policy = "Administrador")]
        public IActionResult Get()
        {
            var username = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            var claims = User.Claims.ToList();

            if (username == null)
            {
                return Ok(new { message = "Nombre no encontrado" });
            }

            return Ok(new { message = "Valor en el claim: " + username.Value });
        }

        // Comprobacion de existencia del usuario en HRPotal
        [HttpPost][AllowAnonymous][Route("Procedure")]
        public bool verifyHRPortal([FromBody] LoginInterface data)
        {
            SqlConnection connectionString = new SqlConnection(_config.GetConnectionString("HRPortal"));

            using (SqlCommand storedProcedureHRPortal = new SqlCommand("SP_AuthenticateUserSAP", connectionString))
            {

                storedProcedureHRPortal.CommandType = CommandType.StoredProcedure;
                storedProcedureHRPortal.Parameters.AddWithValue("@EMPLOYEE_PASSWORD", data.pass);
                storedProcedureHRPortal.Parameters.AddWithValue("@EMPLOYEE_NUMBER", data.userNumber);

                connectionString.Open();

                var valores = storedProcedureHRPortal.ExecuteNonQuery();

                DataTable resultAuth = new DataTable();
                SqlDataAdapter adapterAuth = new SqlDataAdapter(storedProcedureHRPortal);
                adapterAuth.Fill(resultAuth);
                int auth = Int32.Parse(resultAuth.Rows[0][0].ToString());

                connectionString.Close();

                authenticated = Convert.ToBoolean(auth);
                return authenticated;
            };
        }
        
        // Genera el token del administrador
        [HttpGet][Route("generateAdminToken")][AllowAnonymous]
        public string generateAdministratorToken(AdministratorAuthData adminData)
        {
            var secretKey = _config.GetValue<string>("SecretKey"); // Leemos la llave secreta
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new ClaimsIdentity(); // Creamos los claims para poder iniciar sesion
            claims.AddClaim(new Claim( ClaimTypes.NameIdentifier, adminData.getAdminId().ToString() )); // Creación de un claim con el nombre del usuario
            claims.AddClaim(new Claim( "Username", adminData.getAdminName().ToString() ));
            claims.AddClaim(new Claim( "Location", adminData.getAdminLocation().ToString() ));
            claims.AddClaim(new Claim( "Role", adminData.getAdminRole().ToString() ));
            claims.AddClaim(new Claim("EmployeeNumber", adminData.getAdminEmployeeNumber().ToString()));
            claims.AddClaim(new Claim("Email", adminData.getAdminEmail().ToString()));

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

            return bearer_token;
        }

        // Genera token del usuario
        [HttpGet][Route("generateUserToken")][AllowAnonymous]
        public string generateUserToken(UserAuthData userData)
        {
            var secretKey = _config.GetValue<string>("SecretKey"); // Leemos la llave secreta
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new ClaimsIdentity(); // Creamos los claims para poder iniciar sesion
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userData.getUserEmployeeNumber().ToString()));
            claims.AddClaim(new Claim("Location", userData.getUserLocation().ToString()));
            claims.AddClaim(new Claim("Username", userData.getUsername().ToString()));
            claims.AddClaim(new Claim("SupervisorNumber", userData.getSupervisorNumber().ToString()));

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

            return bearer_token;
        }
    }
}
