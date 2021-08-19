﻿using CENSO.Models;
using CensoAPI02.Intserfaces;
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
    [ApiController][Authorize(Policy = "Administrador")]
    public class LocationController : ControllerBase
    {
        private readonly CDBContext _context;

        public LocationController(CDBContext context)
        {
            _context = context;
        }

        // Registro de una nueva localidad
        [HttpPost][Route("newLocation")][AllowAnonymous]
        public async Task<IActionResult> addNewLocation([FromBody] AddLocationsInterface newLocation)
        {
            try
            {
                // Asignacion de valores a los campos de la tabla Locations
                var addLocation = new Locations()
                {
                    lName = newLocation.lName,
                    lCreationDate = DateTime.Now,
                    lCreationUser = newLocation.lCreationUser,
                    lStatus = newLocation.lStatus
                };

                // Registro de la nueva localidad en la tabla Locations
                _context.Locations.Add(addLocation);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"La localidad {addLocation.lName}, se ha registrado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al registrar la localidad. Error: {ex.Message}" });
            }
        }

        // Actualización de una localidad
        [HttpPatch][Route("locationUpdate")][AllowAnonymous]
        public async Task<IActionResult> locationUpdate()
        {
            return Ok();
        }

        // Eliminación lógica de localidad
        [HttpDelete][Route("deleteLocation/{locationId}")][AllowAnonymous]
        public async Task<IActionResult> deleteLocation(int locationId)
        {
            try
            {
                // Busqueda de la localidad por medio del Id
                var query = await _context.Locations.FindAsync(locationId);

                if (query == null)
                {
                    return NotFound(new { message = $"La localidad {locationId}, no se encuentra en la base de datos" });
                }

                // Modificacion del campo status para la eliminació lógica
                query.lStatus = false;
                _context.Locations.Update(query);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"La localidad {locationId}, ha sido eliminada correctamente" });
            }catch(Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurido un error al eliminar la localidad. Error: {ex.Message}" });
            }
        }
    }
}
