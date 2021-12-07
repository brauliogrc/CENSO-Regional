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
    public class ReportController : ControllerBase
    {
        private readonly CDBContext _context;
        private readonly IConfiguration _config;
        private readonly Validations validations = new Validations();

        public ReportController( CDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet][Route("ticketsReport/{locationId}")]
        public async Task<ActionResult> getTicketReport(int locationId)
        {
            try
            {

                /*var tickets = from request in _context.Requests
                              //join user in _context.HRU on request.rUserId equals user.uEmployeeNumber
                              join theme in _context.Theme on request.ThemeId equals theme.tId
                              join question in _context.Questions on request.QuestionId equals question.qId
                              join location in _context.Locations on request.LocationId equals location.lId
                              join area in _context.Areas on request.AreaId equals area.aId
                              join status in _context.RequestStatus on request.StatusId equals status.rsId
                              //join answer in _context.Answer on request.rId equals answer.RequestId
                              where location.lId == locationId //&& request.StatusId != 4
                              select new
                              {
                                  // Datos del ticket
                                  request.rId,
                                  request.rUserId,
                                  request.rUserName,
                                  request.rEmployeeLeader,
                                  request.rEmployeeType,
                                  request.rCreationDate,
                                  // Datos de la localidad
                                  location.lId,
                                  location.lName,
                                  // Datos del usuario
                                  //user.uEmployeeNumber,
                                  //user.uName
                                  // Datos del tema
                                  theme.tId,
                                  theme.tName,
                                  // Datos de la pregunta
                                  question.qId,
                                  question.qName,
                                  // Datos del area
                                  area.aId,
                                  area.aName,
                                  // Datos de la respuesta
                                  //answer.asId,
                                  //answer.asAnswer,
                                  //answer.asCreationDate,
                                  // Datos del estatus
                                  status.rsId,
                                  status.rsStatus
                              };

                var anonTickets = from anonReq in _context.AnonRequests
                                  //join user in _context.HRU on anonReq.arModificationUser equals user.uEmployeeNumber
                                  join theme in _context.Theme on anonReq.ThemeId equals theme.tId
                                  join question in _context.Questions on anonReq.QuestionId equals question.qId
                                  join location in _context.Locations on anonReq.LocationId equals location.lId
                                  join area in _context.Areas on anonReq.AreaId equals area.aId
                                  join status in _context.RequestStatus on anonReq.StatusId equals status.rsId
                                  //join answer in _context.Answer on anonReq.arId equals answer.AnonRequestId
                                  where location.lId == locationId //&& anonReq.StatusId != 4
                                  select new
                                  {
                                      // Datos del ticket
                                      anonReq.arId,
                                      anonReq.arEmployeeType,
                                      anonReq.arCreationDate,
                                      // Datos de la localidad
                                      location.lId,
                                      location.lName,
                                      // Datos del usuario
                                      //user.uEmployeeNumber,
                                      //user.uName,
                                      // Datos del tema
                                      theme.tId,
                                      theme.tName,
                                      // Datos de la pregunta
                                      question.qId,
                                      question.qName,
                                      // Datos del area
                                      area.aId,
                                      area.aName,
                                      // Datos de la respuesta
                                      //answer.asId,
                                      //answer.asAnswer,
                                      //answer.asCreationDate,
                                      // Datos del estatus
                                      status.rsId,
                                      status.rsStatus
                                  };

                if ((tickets == null || tickets.Count() == 0) && (anonTickets == null || anonTickets.Count() == 0))
                {
                    return NotFound(new { message = $"Ningun ticket se encuentra asociado con tu localidad" });
                }

                return Ok(new { tickets, anonTickets });*/
                
                List<ReportTicket> tickets = new List<ReportTicket>();
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

                return Ok(new { tickets, anonTickets });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un error al obtener la lista de tickets. Error: {ex.Message}" });
            }
        }

        
    }
}
