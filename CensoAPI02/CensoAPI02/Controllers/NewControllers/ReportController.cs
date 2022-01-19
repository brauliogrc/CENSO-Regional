using CENSO.Models;
using CensoAPI02.Intserfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Controllers.NewControllers
{
    [Route("api/[controller]")]
    [ApiController][Authorize(Policy = "SURH")]
    //[AllowAnonymous]
    public class ReportController : ControllerBase
    {
        private readonly CDBContext _context;
        private readonly IConfiguration _config;
        private readonly Validations validations = new Validations();
        private readonly AnonimousTicketReportHandler anonTicketReport;
        private readonly WDataTicketReportHandler wDataTicketReport;

        public ReportController( CDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            anonTicketReport = new AnonimousTicketReportHandler(_config);
            wDataTicketReport = new WDataTicketReportHandler(_config);
        }

        [HttpGet][Route("ticketsReport/{locationId}/{employeeNumber}")]
        public async Task<ActionResult> getTicketReport(int locationId, long employeeNumber)
        {
            List<ReportTicket> tickets = new List<ReportTicket>();
            List<ReportAnonTicket> anonTickets = new List<ReportAnonTicket>();
            try
            {
                var user = from usr in _context.HRU
                           where usr.uEmployeeNumber == employeeNumber
                           select usr;

                if ( user.First().RoleId == 1 )
                {
                    tickets = wDataTicketReport.adminTicketReport( locationId );
                    anonTickets = anonTicketReport.adminAnonTicketReport( locationId );
                }
                else
                {
                    tickets = wDataTicketReport.notAdminTicketReport( locationId, employeeNumber );
                    anonTickets = anonTicketReport.notAdminAnonTicketReport( locationId, employeeNumber );
                }

                return Ok( new { tickets, anonTickets } );
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener la lista de tickets. Error: {ex.Message}" });
            }
        }

        // Generación del reporte llamando al stored procedure de manejador de rol del usuario
        [HttpGet][Route("improvedReporting/{employeeNumber}")][AllowAnonymous]
        public async Task<ActionResult> improvedReporting( long employeeNumber )
        {
            List<int> IDs;
            try
            {
                SqlConnection connectionString = new SqlConnection(_config.GetConnectionString("CensoProd"));
                
                using ( SqlCommand command = new SqlCommand("sp_For_Report_UserRole_Handler", connectionString))
                {
                    connectionString.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@employeeNumber", employeeNumber);

                    SqlDataReader dr = command.ExecuteReader();

                    while ( dr.Read() )
                    {
                        Console.WriteLine( "Datos: " + dr["rId"] );
                        IDs = new List<int> { Convert.ToInt32( dr["rId"] ) };
                    }
                }
                connectionString.Close();
                return Ok();
            }
            catch ( Exception ex )
            {
                return BadRequest( new { message = $"Ha ocurrido un error al obtener la lista de tickets. ERROR: { ex.Message }" } );
            }
        }
    }
}
