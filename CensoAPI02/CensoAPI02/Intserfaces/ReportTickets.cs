using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Intserfaces
{
    public class ReportTicket
    {
		// Datos del ticket
        public int rId { get; set; }

        public int rUserId { get; set; }

        public string rUserName { get; set; }

        public string rIssue { get; set; }

        public string? rAttachement { get; set; }

        public int rEmployeeLeader { get; set; }

        public int rEmployeeType { get; set; }

        public DateTime rCreationDate { get; set; }

		// Datos de la localidad
        public int lId { get; set; }

        public string lName { get; set; }

		// Datos del tema
        public int tId { get; set; }

        public string tName { get; set; }

		// Datos de la pregunta
        public int qId { get; set; }

        public string qName { get; set; }

        // Datos del area
        public int aId { get; set; }

        public string aName { get; set; }

        // Datos del status
        public int rsId { get; set; }

        public string rsStatus { get; set; }

        // Datos de la respuesta
        public int? asId { get; set; }

        public string? asAnswer { get; set; }

        public string? AsAttachement { get; set; }

        public DateTime? asCreationDate { get; set; }

		// Datos del usuario que responde
        public long? uEmployeeNumber { get; set; }

        public string? uName { get; set; }

        public string? uEmail { get; set; }

        public long? uSupervisorNumber { get; set; }
    }

    public class ReportAnonTicket
    {
        // Datos del ticket anonimo
        public int arId { get; set; }

        public string arIssue { get; set; }

        public string arAttachement { get; set; }

        public int arEmployeeType { get; set; }

        public DateTime arCreationDate { get; set; }

        // Datos de la localidad
        public int lId { get; set; }

        public string lName { get; set; }

        // Dato del tema
        public int tId { get; set; }

        public string tName { get; set; }

        // Datos de la pregunta
        public int qId { get; set; }

        public string qName { get; set; }

        // Datos del area
        public int aId { get; set; }

        public string aName { get; set; }

        // Datos del status
        public int rsId { get; set; }

        public string rsStatus { get; set; }

        // Datos de la repuesta
        public int? asId { get; set; }

        public string? asAnswer { get; set; }

        public string? asAttachement { get; set; }

        public DateTime? asCreationDate { get; set; }

        // Datos del usuario que responde
        public long? uEmployeeNumber { get; set; }

        public string? uName { get; set; }

        public string? uEmail { get; set; }

        public long? uSupervisorNumber { get; set; }
    }
}
