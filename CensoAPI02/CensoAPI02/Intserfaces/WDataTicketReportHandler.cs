using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CensoAPI02.Intserfaces
{
    public class WDataTicketReportHandler
    {
        private readonly IConfiguration _config;
        private readonly Validations validations = new Validations();

        public WDataTicketReportHandler(IConfiguration config)
        {
            _config = config;
        }

        // Obtención del reporteo de tickets para un usuario administrador
        public List<ReportTicket> adminTicketReport( int locationId )
        {
            SqlConnection connectionString = new SqlConnection(_config.GetConnectionString("CensoProd"));
            List<ReportTicket> tickets = new List<ReportTicket>();
            using (SqlCommand command = new SqlCommand("sp_Report_Tickets", connectionString))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@locationId", locationId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    ReportTicket reportTicket = new ReportTicket();
                    // Datos del ticket
                    reportTicket.rId = Int32.Parse(table.Rows[i]["rId"].ToString());
                    reportTicket.rUserId = Int32.Parse(table.Rows[i]["rUserId"].ToString());
                    reportTicket.rUserName = table.Rows[i]["rUserName"].ToString();
                    reportTicket.rIssue = table.Rows[i]["rIssue"].ToString();
                    reportTicket.rAttachement = table.Rows[i]["rAttachement"].ToString();
                    reportTicket.rEmployeeLeader = Int32.Parse(table.Rows[i]["rEmployeeLeader"].ToString());
                    reportTicket.rEmployeeType = Int32.Parse(table.Rows[i]["rEmployeeType"].ToString());
                    reportTicket.rCreationDate = DateTime.Parse(table.Rows[i]["rCreationDate"].ToString());
                    // Datos de la localidad
                    reportTicket.lId = Int32.Parse(table.Rows[i]["lId"].ToString());
                    reportTicket.lName = table.Rows[i]["lName"].ToString();
                    // Datos del tema
                    reportTicket.tId = Int32.Parse(table.Rows[i]["tId"].ToString());
                    reportTicket.tName = table.Rows[i]["tName"].ToString();
                    // Datos de la pregunta
                    reportTicket.qId = Int32.Parse(table.Rows[i]["qId"].ToString());
                    reportTicket.qName = table.Rows[i]["qName"].ToString();
                    // Datos del area
                    reportTicket.aId = Int32.Parse(table.Rows[i]["aId"].ToString());
                    reportTicket.aName = table.Rows[i]["aName"].ToString();
                    // Datos del estatus
                    reportTicket.rsId = Int32.Parse(table.Rows[i]["rsId"].ToString());
                    reportTicket.rsStatus = table.Rows[i]["rsStatus"].ToString();
                    // Datos de la respuesta
                    reportTicket.asId = validations.tryParseToInt32(table.Rows[i]["asId"].ToString());
                    reportTicket.asAnswer = table.Rows[i]["asAnswer"].ToString();
                    reportTicket.AsAttachement = table.Rows[i]["asAttachement"].ToString();
                    reportTicket.asCreationDate = validations.tryParseToDateTime(table.Rows[i]["asCreationDate"].ToString());
                    // Datos del usuario que responde
                    reportTicket.uEmployeeNumber = validations.tryParseToInt64(table.Rows[i]["uEmployeeNumber"].ToString());
                    reportTicket.uName = table.Rows[i]["uName"].ToString();
                    reportTicket.uEmail = table.Rows[i]["uEmail"].ToString();
                    reportTicket.uSupervisorNumber = validations.tryParseToInt64(table.Rows[i]["uSupervisorNumber"].ToString());

                    tickets.Add(reportTicket);
                    reportTicket = null;
                }
            };
            return tickets;
        }

        // Obtención del reporteo de tickets para un usuario no administrador
        public List<ReportTicket> notAdminTicketReport( int locationId, long employeeNumber )
        {
            SqlConnection connectionString = new SqlConnection(_config.GetConnectionString("CensoProd"));
            List<ReportTicket> tickets = new List<ReportTicket>();
            using (SqlCommand command = new SqlCommand("sp_NotAdmin_Report_Tickets", connectionString))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@locationId", locationId);
                command.Parameters.AddWithValue("@employeeNumber", employeeNumber);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    ReportTicket reportTicket = new ReportTicket();
                    // Datos del ticket
                    reportTicket.rId = Int32.Parse(table.Rows[i]["rId"].ToString());
                    reportTicket.rUserId = Int32.Parse(table.Rows[i]["rUserId"].ToString());
                    reportTicket.rUserName = table.Rows[i]["rUserName"].ToString();
                    reportTicket.rIssue = table.Rows[i]["rIssue"].ToString();
                    reportTicket.rAttachement = table.Rows[i]["rAttachement"].ToString();
                    reportTicket.rEmployeeLeader = Int32.Parse(table.Rows[i]["rEmployeeLeader"].ToString());
                    reportTicket.rEmployeeType = Int32.Parse(table.Rows[i]["rEmployeeType"].ToString());
                    reportTicket.rCreationDate = DateTime.Parse(table.Rows[i]["rCreationDate"].ToString());
                    // Datos de la localidad
                    reportTicket.lId = Int32.Parse(table.Rows[i]["lId"].ToString());
                    reportTicket.lName = table.Rows[i]["lName"].ToString();
                    // Datos del tema
                    reportTicket.tId = Int32.Parse(table.Rows[i]["tId"].ToString());
                    reportTicket.tName = table.Rows[i]["tName"].ToString();
                    // Datos de la pregunta
                    reportTicket.qId = Int32.Parse(table.Rows[i]["qId"].ToString());
                    reportTicket.qName = table.Rows[i]["qName"].ToString();
                    // Datos del area
                    reportTicket.aId = Int32.Parse(table.Rows[i]["aId"].ToString());
                    reportTicket.aName = table.Rows[i]["aName"].ToString();
                    // Datos del estatus
                    reportTicket.rsId = Int32.Parse(table.Rows[i]["rsId"].ToString());
                    reportTicket.rsStatus = table.Rows[i]["rsStatus"].ToString();
                    // Datos de la respuesta
                    reportTicket.asId = validations.tryParseToInt32(table.Rows[i]["asId"].ToString());
                    reportTicket.asAnswer = table.Rows[i]["asAnswer"].ToString();
                    reportTicket.AsAttachement = table.Rows[i]["asAttachement"].ToString();
                    reportTicket.asCreationDate = validations.tryParseToDateTime(table.Rows[i]["asCreationDate"].ToString());
                    // Datos del usuario que responde
                    reportTicket.uEmployeeNumber = validations.tryParseToInt64(table.Rows[i]["uEmployeeNumber"].ToString());
                    reportTicket.uName = table.Rows[i]["uName"].ToString();
                    reportTicket.uEmail = table.Rows[i]["uEmail"].ToString();
                    reportTicket.uSupervisorNumber = validations.tryParseToInt64(table.Rows[i]["uSupervisorNumber"].ToString());

                    tickets.Add(reportTicket);
                    reportTicket = null;
                }
            };
            return tickets;
        }
    }
}
