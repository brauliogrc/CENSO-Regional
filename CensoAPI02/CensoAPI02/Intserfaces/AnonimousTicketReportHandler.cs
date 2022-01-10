using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CensoAPI02.Intserfaces
{
    public class AnonimousTicketReportHandler
    {
        private readonly IConfiguration _config;
        private readonly Validations validations = new Validations();

        public AnonimousTicketReportHandler(IConfiguration config)
        {
            _config = config;
        }

        // Obtención del reporteo de tickets para un usuario administrador
        public List<ReportAnonTicket> adminAnonTicketReport( int locationId )
        {
            SqlConnection connectionString = new SqlConnection(_config.GetConnectionString("CensoProd"));
            List<ReportAnonTicket> anonTickets = new List<ReportAnonTicket>();
            using (SqlCommand command = new SqlCommand("sp_Report_AnonTicket", connectionString))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@locationId", locationId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    ReportAnonTicket reportAnonTicket = new ReportAnonTicket();

                    // Datos del ticket anonimo
                    reportAnonTicket.arId = Int32.Parse(row["arId"].ToString());
                    reportAnonTicket.arIssue = row["arIssue"].ToString();
                    reportAnonTicket.arAttachement = row["arAttachement"].ToString();
                    reportAnonTicket.arEmployeeType = Int32.Parse(row["arEmployeeType"].ToString());
                    reportAnonTicket.arCreationDate = DateTime.Parse(row["arCreationDate"].ToString());
                    // Datos de la localidad
                    reportAnonTicket.lId = Int32.Parse(row["lId"].ToString());
                    reportAnonTicket.lName = row["lName"].ToString();
                    // Datos del tema
                    reportAnonTicket.tId = Int32.Parse(row["tId"].ToString());
                    reportAnonTicket.tName = row["tName"].ToString();
                    // Datos de la pregunta
                    reportAnonTicket.qId = Int32.Parse(row["qId"].ToString());
                    reportAnonTicket.qName = row["qName"].ToString();
                    // Datos del area
                    reportAnonTicket.aId = Int32.Parse(row["aId"].ToString());
                    reportAnonTicket.aName = row["aName"].ToString();
                    // Datos del status
                    reportAnonTicket.rsId = Int32.Parse(row["rsId"].ToString());
                    reportAnonTicket.rsStatus = row["rsStatus"].ToString();
                    // Datos de la respuesta
                    reportAnonTicket.asId = validations.tryParseToInt32(row["asId"].ToString());
                    reportAnonTicket.asAnswer = row["asAnswer"].ToString();
                    reportAnonTicket.asAttachement = row["asAttachement"].ToString();
                    reportAnonTicket.asCreationDate = validations.tryParseToDateTime(row["asCreationDate"].ToString());
                    // Datos del usuario que responde
                    reportAnonTicket.uEmployeeNumber = validations.tryParseToInt64(row["uEmployeeNumber"].ToString());
                    reportAnonTicket.uName = row["uName"].ToString();
                    reportAnonTicket.uEmail = row["uEmail"].ToString();
                    reportAnonTicket.uSupervisorNumber = validations.tryParseToInt64(row["uSupervisorNumber"].ToString());

                    anonTickets.Add(reportAnonTicket);
                    reportAnonTicket = null;
                }
            }
            return anonTickets;
        }

        // Obtención del reporteo de tickets para un usuario no admiinistrador
        public List<ReportAnonTicket> notAdminAnonTicketReport( int locationId, long employeeNumber )
        {
            SqlConnection connectionString = new SqlConnection(_config.GetConnectionString("CensoProd"));
            List<ReportAnonTicket> anonTickets = new List<ReportAnonTicket>();
            using (SqlCommand command = new SqlCommand("sp_NotAdmin_Report_AnonTickets", connectionString))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@locationId", locationId);
                command.Parameters.AddWithValue("@employeeNumber", employeeNumber);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    ReportAnonTicket reportAnonTicket = new ReportAnonTicket();

                    // Datos del ticket anonimo
                    reportAnonTicket.arId = Int32.Parse(row["arId"].ToString());
                    reportAnonTicket.arIssue = row["arIssue"].ToString();
                    reportAnonTicket.arAttachement = row["arAttachement"].ToString();
                    reportAnonTicket.arEmployeeType = Int32.Parse(row["arEmployeeType"].ToString());
                    reportAnonTicket.arCreationDate = DateTime.Parse(row["arCreationDate"].ToString());
                    // Datos de la localidad
                    reportAnonTicket.lId = Int32.Parse(row["lId"].ToString());
                    reportAnonTicket.lName = row["lName"].ToString();
                    // Datos del tema
                    reportAnonTicket.tId = Int32.Parse(row["tId"].ToString());
                    reportAnonTicket.tName = row["tName"].ToString();
                    // Datos de la pregunta
                    reportAnonTicket.qId = Int32.Parse(row["qId"].ToString());
                    reportAnonTicket.qName = row["qName"].ToString();
                    // Datos del area
                    reportAnonTicket.aId = Int32.Parse(row["aId"].ToString());
                    reportAnonTicket.aName = row["aName"].ToString();
                    // Datos del status
                    reportAnonTicket.rsId = Int32.Parse(row["rsId"].ToString());
                    reportAnonTicket.rsStatus = row["rsStatus"].ToString();
                    // Datos de la respuesta
                    reportAnonTicket.asId = validations.tryParseToInt32(row["asId"].ToString());
                    reportAnonTicket.asAnswer = row["asAnswer"].ToString();
                    reportAnonTicket.asAttachement = row["asAttachement"].ToString();
                    reportAnonTicket.asCreationDate = validations.tryParseToDateTime(row["asCreationDate"].ToString());
                    // Datos del usuario que responde
                    reportAnonTicket.uEmployeeNumber = validations.tryParseToInt64(row["uEmployeeNumber"].ToString());
                    reportAnonTicket.uName = row["uName"].ToString();
                    reportAnonTicket.uEmail = row["uEmail"].ToString();
                    reportAnonTicket.uSupervisorNumber = validations.tryParseToInt64(row["uSupervisorNumber"].ToString());

                    anonTickets.Add(reportAnonTicket);
                    reportAnonTicket = null;
                }
            }
            return anonTickets;
        }
    }
}
