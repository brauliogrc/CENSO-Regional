using Microsoft.Data.SqlClient;
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

namespace CensoAPI02.Intserfaces
{
    public class TokenGenerator
    {
        // Variables globales
        private readonly IConfiguration _config;

        // Constructores
        public TokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public TokenGenerator()
        {

        }

        // Verifica la existencia del usuario en la base de datos HRPortal
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

                bool authenticated = Convert.ToBoolean(auth);
                return authenticated;
            };
        }

        // Generador de token de Administrador
        public string generateAdministratorToken(AdministratorAuthData adminData)
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
        public string generateUserToken(UserAuthData userData)
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
