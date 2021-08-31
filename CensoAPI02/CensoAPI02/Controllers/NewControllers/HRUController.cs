using CENSO.Models;
using CensoAPI02.Intserfaces;
using CensoAPI02.Models;
using CensoAPI02.Models.UnionTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        [HttpPost][Route("newUser")]
        public async Task<IActionResult> addNewUser([FromBody] AddUserInterface newUser)
        {
            try
            {
                // Verifica si el usuario ya se encuentra registrado
                var userExistence = from user in _context.HRU
                                    where user.uEmployeeNumber == newUser.EmployeeNumber
                                    select user.uEmployeeNumber;


                if (!(userExistence == null || userExistence.Count() == 0))
                {
                    return Conflict(new { message = $"El usuario ya se enceuntra registrado" });
                }

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
                    uModificationDate = null,
                    uModificationUser = null
                };

                // En caso de correo nulo
                if (addUser.uEmail == "" || addUser.uEmail.Length == 0)
                {
                    addUser.uEmail = null;
                }

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
                return BadRequest(new { message = $"Ha ocurrido un error al registrar el usuario. Error: {ex.InnerException}" });
            }
        }


        // Busqueda de informacion de un usuario, para su registro (requiere policy surh)
        [HttpGet]
        [Route("userInformation/{location}/{employeeNumber}")]
        public async Task<ActionResult> getUserInformation(string location, int employeeNumber)
        {
            try
            {
                var userExistence = from user in _context.HRU
                                    where user.uEmployeeNumber == employeeNumber
                                    select user.uEmployeeNumber;


                if ( !(userExistence == null || userExistence.Count() == 0) )
                {
                    return Conflict(new { message = $"El usuario ya se enceuntra registrado" });
                }

                string query = "SELECT [EmployeeNumber], [Plant], [FullName], [EMail] FROM [p_HRPortal].[dbo].[VW_EmployeeData] WHERE Plant = '" + location + "' and EmployeeNumber = " + employeeNumber;
                using( SqlConnection connection = new SqlConnection( _config.GetConnectionString( "HRPortal" ) ) )
                {
                    UserInformation userInformation = new UserInformation();

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int flag = 0;
                        while(reader.Read() && flag == 0)
                        {
                            //Console.WriteLine($"{reader.GetInt32(0)} - {reader.GetString(0)} - {reader.GetString(2)} - {reader.GetString(3)}");
                            Console.WriteLine($"{reader.GetInt32(0)} - {reader.GetString(1)} - {reader.GetString(2)} - {reader.GetString(3)}");

                            userInformation.employeeNumber = reader.GetInt32(0);
                            userInformation.location = reader.GetString(1);
                            userInformation.name = reader.GetString(2);
                            userInformation.email = reader.GetString(3);

                            flag = 1;
                        }
                    }else
                    {
                        return NotFound(new { message = $"Usurio no ecnontrado en su localidad" });
                    }

                    connection.Close();
                    return Ok(userInformation);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener la informacion del usuario. Error: {ex.Message}" });
            }
        }

        // Actualización de un usuario (requiere policy surh)
        [HttpPatch][Route("userUpdate")]
        public async Task<IActionResult> userUpdate([FromBody] UserUpdate userUpdate)
        {
            bool flagUpdate = false;
            try
            {
                var user = await _context.HRU.FindAsync(userUpdate.employeeNumber);

                if ( userUpdate.uName != null && userUpdate.uName.Length != 0 && (userUpdate.uName != user.uName) )
                {
                    user.uName = userUpdate.uName;
                    flagUpdate = true;
                }

                if ( userUpdate.uEmail != null && userUpdate.uEmail.Length != 0 && (userUpdate.uEmail != user.uEmail) )
                {
                    user.uEmail = userUpdate.uEmail;
                    flagUpdate = true;
                }

                if ( userUpdate.LocationId != null && (user.LocationId != userUpdate.LocationId) )
                {
                    string newLocation = userUpdate.LocationId.ToString();
                    user.LocationId = Int32.Parse(newLocation);
                    flagUpdate = true;
                }

                if ( userUpdate.roleId != null && (user.RoleId != userUpdate.roleId) )
                {
                    string newRol = userUpdate.roleId.ToString();
                    user.RoleId = Int32.Parse(newRol);
                    flagUpdate = true;
                }

                if ( user.uStatus != userUpdate.uStatus && userUpdate.uStatus != null)
                {
                    string newStatus = userUpdate.uStatus.ToString();
                    user.uStatus = Boolean.Parse(newStatus);
                    flagUpdate = true;
                }

                if ( flagUpdate )
                {
                    user.uModificationUser = userUpdate.modificationUser;
                    user.uModificationDate = DateTime.Now;

                    _context.HRU.Update(user);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = $"El usuario se actualizó correctamente." });
                }

                return Ok(new { message = $"Ningún cambio realizado" } );

            }
            catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrio un error al actualizar el usuario. Error: {ex.Message}" });
            }
        }

        // Eliminacioón lógica de usuario
        [HttpDelete] [Route("deleteUser/{employeeNumber}")]
        public async Task<IActionResult> deleteUser(long employeeNumber)
        {
            try
            {
                // Busqieda del usuario por medio del Id
                var query = await _context.HRU.FindAsync(employeeNumber);

                if (query == null)
                {
                    return NotFound(new {message = $"El usuario {employeeNumber}, no se encuentra en la base de datos" });
                }

                // Modificacion del campo status para la eliminació lógica
                query.uStatus = false;
                _context.HRU.Update(query);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"El usuario {employeeNumber}, ha sido eliminado con exito" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurido un error al eliminar el usuatio. Error: {ex.Message}" });
            }
        }

        // Eliminar relacion de un usuario con un tema (requiere policy su)
        [HttpDelete][Route("deleteRelatedTopic/{employeeNumber}/{themeId}")]
        public async Task<ActionResult> deleteRelatedTeme(long employeeNumber, int themeId)
        {
            try
            {
                var relatedTeme = (from ht in _context.HRUsersThemes
                                   where ht.UserId == employeeNumber && ht.ThemeId == themeId
                                   select ht).FirstOrDefault();

                if ( relatedTeme == null)
                {
                    return NotFound(new { message = $"No se ha encontrado la relación entre el usuatio y el tema especificado" });
                }

                _context.HRUsersThemes.Remove(relatedTeme);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"La relación se eliminó correctamente" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al eliminar la relación con el tema. Error: {ex.Message}" });
            }
        }

        // Añadir relación entre un usuario y un tema ( requiere policy surh)
        [HttpPost][Route("addRelatedTopic")]
        public async Task<IActionResult> addRelatedTopic([FromBody] AddUserTopicRelationship addTopic)
        {
            try
            {
                var search = (from ht in _context.HRUsersThemes
                              where ht.UserId == addTopic.employeeNumber && ht.ThemeId == addTopic.themeId
                              select ht).FirstOrDefault();

                if ( search != null )
                {
                    return Ok(new { message = $"El usuario ya se encuentra relacionado con este tema." } );
                }

                var newRelationship = new HRUsersTheme()
                {
                    UserId = addTopic.employeeNumber,
                    ThemeId = addTopic.themeId,
                };

                _context.HRUsersThemes.Add(newRelationship);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Relación añadida correctamente." });
            }
            catch( Exception ex )
            {
                return BadRequest(new { message = $"Ha ocurrido un error al añadir el tema al usuario. Error:{ex.Message}" });
            }
        }
    }
}
