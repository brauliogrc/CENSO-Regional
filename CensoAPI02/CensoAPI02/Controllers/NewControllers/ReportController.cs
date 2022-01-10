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
                
                /*List<ReportTicket> tickets = new List<ReportTicket>();
                SqlConnection connectionString = new SqlConnection(_config.GetConnectionString("CensoProd"));
                using ( SqlCommand command = new SqlCommand("sp_Report_Tickets", connectionString))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@locationId", locationId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                   adapter.Fill(table);

                    for ( int i = 0; i < table.Rows.Count; i++)
                    {
                        ReportTicket reportTicket = new ReportTicket();
                        // Datos del ticket
                        reportTicket.rId = Int32.Parse( table.Rows[i]["rId"].ToString() );
                        reportTicket.rUserId = Int32.Parse(table.Rows[i]["rUserId"].ToString() );
                        reportTicket.rUserName = table.Rows[i]["rUserName"].ToString();
                        reportTicket.rIssue = table.Rows[i]["rIssue"].ToString();
                        reportTicket.rAttachement = table.Rows[i]["rAttachement"].ToString();
                        reportTicket.rEmployeeLeader = Int32.Parse(table.Rows[i]["rEmployeeLeader"].ToString() );
                        reportTicket.rEmployeeType = Int32.Parse(table.Rows[i]["rEmployeeType"].ToString() );
                        reportTicket.rCreationDate = DateTime.Parse(table.Rows[i]["rCreationDate"].ToString() );
                        // Datos de la localidad
                        reportTicket.lId = Int32.Parse(table.Rows[i]["lId"].ToString() );
                        reportTicket.lName = table.Rows[i]["lName"].ToString();
                        // Datos del tema
                        reportTicket.tId = Int32.Parse(table.Rows[i]["tId"].ToString() );
                        reportTicket.tName = table.Rows[i]["tName"].ToString();
                        // Datos de la pregunta
                        reportTicket.qId = Int32.Parse(table.Rows[i]["qId"].ToString() );
                        reportTicket.qName = table.Rows[i]["qName"].ToString();
                        // Datos del area
                        reportTicket.aId = Int32.Parse(table.Rows[i]["aId"].ToString() );
                        reportTicket.aName = table.Rows[i]["aName"].ToString();
                        // Datos del estatus
                        reportTicket.rsId = Int32.Parse(table.Rows[i]["rsId"].ToString() );
                        reportTicket.rsStatus = table.Rows[i]["rsStatus"].ToString();
                        // Datos de la respuesta
                        reportTicket.asId = validations.tryParseToInt32(table.Rows[i]["asId"].ToString() );
                        reportTicket.asAnswer = table.Rows[i]["asAnswer"].ToString();
                        reportTicket.AsAttachement = table.Rows[i]["asAttachement"].ToString();
                        reportTicket.asCreationDate = validations.tryParseToDateTime(table.Rows[i]["asCreationDate"].ToString() );
                        // Datos del usuario que responde
                        reportTicket.uEmployeeNumber = validations.tryParseToInt64(table.Rows[i]["uEmployeeNumber"].ToString() );
                        reportTicket.uName = table.Rows[i]["uName"].ToString();
                        reportTicket.uEmail = table.Rows[i]["uEmail"].ToString();
                        reportTicket.uSupervisorNumber = validations.tryParseToInt64(table.Rows[i]["uSupervisorNumber"].ToString() );

                        tickets.Add(reportTicket);
                        reportTicket = null;
                    }
                };

                List<ReportAnonTicket> anonTickets = new List<ReportAnonTicket>();
                using( SqlCommand command = new SqlCommand("sp_Report_AnonTicket", connectionString))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@locationId", locationId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    foreach ( DataRow row in table.Rows )
                    {
                        ReportAnonTicket reportAnonTicket = new ReportAnonTicket();

                        // Datos del ticket anonimo
                        reportAnonTicket.arId = Int32.Parse( row["arId"].ToString() );
                        reportAnonTicket.arIssue = row["arIssue"].ToString();
                        reportAnonTicket.arAttachement = row["arAttachement"].ToString();
                        reportAnonTicket.arEmployeeType = Int32.Parse( row["arEmployeeType"].ToString() );
                        reportAnonTicket.arCreationDate = DateTime.Parse( row["arCreationDate"].ToString() );
                        // Datos de la localidad
                        reportAnonTicket.lId = Int32.Parse( row["lId"].ToString() );
                        reportAnonTicket.lName = row["lName"].ToString();
                        // Datos del tema
                        reportAnonTicket.tId = Int32.Parse( row["tId"].ToString() );
                        reportAnonTicket.tName = row["tName"].ToString();
                        // Datos de la pregunta
                        reportAnonTicket.qId = Int32.Parse(row["qId"].ToString() );
                        reportAnonTicket.qName = row["qName"].ToString();
                        // Datos del area
                        reportAnonTicket.aId = Int32.Parse( row["aId"].ToString() );
                        reportAnonTicket.aName = row["aName"].ToString();
                        // Datos del status
                        reportAnonTicket.rsId = Int32.Parse( row["rsId"].ToString() );
                        reportAnonTicket.rsStatus = row["rsStatus"].ToString();
                        // Datos de la respuesta
                        reportAnonTicket.asId = validations.tryParseToInt32( row["asId"].ToString() );
                        reportAnonTicket.asAnswer = row["asAnswer"].ToString();
                        reportAnonTicket.asAttachement = row["asAttachement"].ToString();
                        reportAnonTicket.asCreationDate = validations.tryParseToDateTime( row["asCreationDate"].ToString() );
                        // Datos del usuario que responde
                        reportAnonTicket.uEmployeeNumber = validations.tryParseToInt64( row["uEmployeeNumber"].ToString() );
                        reportAnonTicket.uName = row["uName"].ToString();
                        reportAnonTicket.uEmail = row["uEmail"].ToString();
                        reportAnonTicket.uSupervisorNumber = validations.tryParseToInt64( row["uSupervisorNumber"].ToString() );

                        anonTickets.Add(reportAnonTicket);
                        reportAnonTicket = null;
                    }
                }

                return Ok(new { tickets, anonTickets });*/
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
