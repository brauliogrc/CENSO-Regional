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
        // Variables globales
        private readonly CDBContext _context;
        private readonly IConfiguration _config;
        private bool authenticated;
        private string token;

        // Constructores
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
                        hru.uEmployeeNumber,
                        hru.uSupervisorNumber,
                        hru.uStatus
                    }).FirstOrDefaultAsync();

                if (user != null)
                {
                    if (user.uStatus == false)
                    {
                        return Unauthorized(new { message = $"Acceso no autorizado" });
                    }
                }
                
                if (user == null)
                {
                    // Busqueda del usuario en la base de datos de HRPortal
                    string query = "SELECT [EmployeeNumber], [Plant], [FullName], [SupervisorNumber], [EMail] FROM [p_HRPortal].[dbo].[VW_EmployeeData] where EmployeeNumber=" + dataLogin.userNumber;
                    using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("HRPortal")))
                    {
                        UserAuthData userAuthData = new UserAuthData();
                        SqlCommand command = new SqlCommand(query, conn);
                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int flag = 0;
                            while (reader.Read() && flag == 0)
                            {
                                Console.WriteLine($"{reader.GetInt32(0)} - {reader.GetString(1)} - {reader.GetString(2)} - {reader.GetInt32(3)} - {reader.GetString(4)}");
                                userAuthData.setUserEmployeeNumer(reader.GetInt32(0));
                                userAuthData.setUserLocation(reader.GetString(1));
                                userAuthData.setUsername(reader.GetString(2));
                                userAuthData.setSupervisorNumber(reader.GetInt32(3));
                                userAuthData.setUserEmail(reader.GetString(4));

                                flag++;
                            }
                        }
                        conn.Close();
                        token = generateUserToken(userAuthData);
                        userAuthData = null;
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
                administrator = null;
                return Ok(new { token });
            }
            catch( Exception ex )
            {
                return BadRequest( ex.Message );
            }
        }

        // Obtencion de datos del claim
        /*[HttpGet]
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
        }*/

        // Verifica la existencia del usuario en la base de datos HRPortal
        [HttpGet][Authorize]
        private bool verifyHRPortal(LoginInterface data)
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

        // Generador de token de Administrador
        [HttpGet][Authorize]
        private string generateAdministratorToken(AdministratorAuthData adminData)
        {
            var secretKey = _config.GetValue<string>("SecretKey"); // Leemos la llave secreta
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new ClaimsIdentity(); // Creamos los claims para poder iniciar sesion
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, adminData.getAdminEmployeeNumber().ToString())); // Creación de un claim con el nombre del usuario
            claims.AddClaim(new Claim("userId", adminData.getAdminId().ToString()));
            claims.AddClaim(new Claim("Username", adminData.getAdminName().ToString()));
            claims.AddClaim(new Claim("SupervisorNumber", adminData.getSupervisorNumber().ToString()));
            claims.AddClaim(new Claim("Location", adminData.getAdminLocation().ToString()));
            claims.AddClaim(new Claim("Role", adminData.getAdminRole().ToString()));
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

        // Generador de token de usuario
        [HttpGet]
        private string generateUserToken(UserAuthData userData)
        {
            var secretKey = _config.GetValue<string>("SecretKey"); // Leemos la llave secreta
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new ClaimsIdentity(); // Creamos los claims para poder iniciar sesion
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userData.getUserEmployeeNumber().ToString()));
            claims.AddClaim(new Claim("Location", userData.getUserLocation().ToString()));
            claims.AddClaim(new Claim("Username", userData.getUsername().ToString()));
            claims.AddClaim(new Claim("SupervisorNumber", userData.getSupervisorNumber().ToString()));
            claims.AddClaim(new Claim("Email", userData.getUserEmail().ToString()));

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
