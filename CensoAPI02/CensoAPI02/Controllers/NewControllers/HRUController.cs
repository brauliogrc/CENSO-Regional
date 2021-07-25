using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Controllers.NewControllers
{
    [Route("api/[controller]")]
    [ApiController][Authorize(Policy = "SURH")]
    public class HRUController : ControllerBase
    {
        private readonly CDBContext _context;
        private readonly IConfiguration _config;

        public HRUController(CDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // Registro de un nuevo usuario
        [HttpPost][Route("newUser")][AllowAnonymous]
        public async Task<IActionResult> addNewUser([FromBody] AddUserInterface newUser)
        {
            try
            {
                // Asignacion de valores a los campos de la tabla HRU
                var addUser = new HRU()
                {
                    uEmployeeNumber = newUser.EmployeeNumber,
                    uName = newUser.uName,
                    uEmail = newUser.uEmail,
                    RoleId = newUser.RolId,
                    uStatus = newUser.uStatus,
                    uCreationUser = newUser.uCreationUser,
                    uCreationDate = DateTime.Now,
                };

                // Consulta del SupervisorNumber para asignarlo a los datos del usuario
                string query = "SELECT [SupervisorNumber], [Plant] FROM [p_HRPortal].[dbo].[VW_EmployeeData] WHERE EmployeeNumber = " + addUser.uEmployeeNumber;
                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("HRPortal")))
                {
                    SqlCommand command = new SqlCommand(query, conn);
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int flag = 0;
                        long supervisorNumber;
                        int locationId;
                        Validations validations = new Validations();
                        while (reader.Read() && flag == 0)
                        {
                            Console.WriteLine($"{reader.GetInt32(0)} - {reader.GetString(1)}");

                            supervisorNumber = Convert.ToInt64(reader.GetInt32(0));
                            locationId = validations.localityValidation(reader.GetString(1));
                            if (locationId == 0) return BadRequest(new { message = $"El usuario lo cuenta con una ocalidad" });


                            addUser.uSupervisorNumber = supervisorNumber;
                            addUser.LocationId = locationId;
                            flag++;
                        }
                    }

                    conn.Close();
                }

                // Registro del nuevo usuario en la tabla HRU
                _context.HRU.Add(addUser);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Usuario {addUser.uName} con numero de empleado {addUser.uEmployeeNumber}, registrado correctamente" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al registrar el usuario. Error: {ex.Message}" });
            }
        }

        // Eliminacioón lógica de usuario
        [HttpDelete] [Route("deleteUser/{userId}")][AllowAnonymous]
        public async Task<IActionResult> deleteUser(int userId)
        {
            try
            {
                // Busqieda del usuario por medio del Id
                var query = await _context.HRU.FindAsync(userId);

                if (query == null)
                {
                    return NotFound(new {message = $"El usuario {userId}, no se encuentra en la base de datos" });
                }

                // Modificacion del campo status para la eliminació lógica
                query.uStatus = false;
                _context.HRU.Update(query);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"El usuario {userId}, ha sido eliminado con exito" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurido un error al eliminar el usuatio. Error: {ex.Message}" });
            }
        }
    }
}
