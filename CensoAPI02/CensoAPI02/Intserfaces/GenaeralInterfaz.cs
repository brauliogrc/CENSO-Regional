using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Intserfaces
{
    public class GenaeralInterfaz
    {
    }

    // Cada interfz contiene los datos que la API solicita para realizar alguna acción
    // Registrar un nuevo usuario
    public class AddUserInterface
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Rol { get; set; }

        public bool Status { get; set; }

        public int LocationId { get; set; }
    }

    // Realizar el logueo de un usuario
    public class LoginInterface
    {

        public string username { get; set; }

        public string email { get; set; }
    }

    // Registrar una nueva Request
    public class AddRequestInterface
    {
        public int rUserId { get; set; }

        public int rEmployeeType { get; set; }

        public int QuestionId { get; set; }

        public int AreaId { get; set; }

        public int ThemeId { get; set; }

        public string rIssue { get; set; }

        public string rAttachement { get; set; }
    }

    // Registar una nueva AonRequest
    public class AddAnonRequestInterface
    {
        public int arEmployeeType { get; set; }

        public int QuestionId { get; set; }

        public int AreaId { get; set; }

        public int ThemeId { get; set; }

        public string arIssue { get; set; }

        public string arAttachemen { get; set; }
    }

    // Registar una nueva Location
    public class AddLocations
    {
        public string lName { get; set; }

        public bool lStatus { get; set; }
    }

    // Registar una nueva Question
    public class AddQuestion
    {
        public string qName { get; set; }

        public bool qStatus { get; set; }

        public int ThemeId { get; set; }
    }

    // Registar un nuevo Theme
    public class AddTheme
    {
        public string tName { get; set; }

        public bool tStatus { get; set; }

        public int QuestionId { get; set; }
    }

    // Registar una nueva Area
    public class AddArea
    {
        public string aName { get; set; }

        public int LocationId { get; set; }
    }
}
