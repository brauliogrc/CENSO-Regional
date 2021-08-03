using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Intserfaces
{
    public class GenaeralInterfaz
    {
    }

    // Cada interfz contiene los datos que la API solicita para realizar alguna acción

    // Realizar el logueo de un usuario
    public class LoginInterface
    {
        [Required(ErrorMessage = "El numero nombre de usuario es obligatorio.")]
        public long userNumber { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string pass { get; set; }
    }

    // Registrar un nuevo usuario
    public class AddUserInterface // Modificada
    {
        public string uName { get; set; }

        public string uEmail { get; set; }

        public int RolId { get; set; }

        public long EmployeeNumber { get; set; }

        public int uCreationUser { get; set; }

        public bool uStatus { get; set; }

        //public int LocationId { get; set; }
    }

    // Registrar una nueva Request
    public class AddRequestInterface // Modificada
    {
        public int rUserId { get; set; }

        public string rUserName { get; set; }

        public int rEmployeeType { get; set; }

        public int rEmployeeLeader { get; set; }

        public int QuestionId { get; set; }

        public int AreaId { get; set; }

        public int ThemeId { get; set; }

        public int LocationId { get; set; }

        public string rIssue { get; set; }

        public IFormFile rAttachement { get; set; }
    }

    // Registar una nueva AonRequest
    public class AddAnonRequestInterface // Modificada
    {
        public int arEmployeeType { get; set; }

        public int QuestionId { get; set; }

        public int AreaId { get; set; }

        public int ThemeId { get; set; }

        public int LocationId { get; set; }

        public string arIssue { get; set; }

        public IFormFile arAttachement { get; set; }
    }

    // Registar una nueva Location
    public class AddLocationsInterface // Modificada
    {
        public string lName { get; set; }

        public int lCreationUser { get; set; }

        public bool lStatus { get; set; }
    }

    // Registar una nueva Question
    public class AddQuestionInterface // Modificada
    {
        public string qName { get; set; }

        public int qCreationUser { get; set; }

        public bool qStatus { get; set; }

        public int ThemeId { get; set; }
    }

    // Registar un nuevo Theme
    public class AddThemeInterface // Modificada
    {
        public string tName { get; set; }

        public int tCreationUser { get; set; }

        public bool tStatus { get; set; }

        public int LocationId { get; set; }
    }

    // Registar una nueva Area
    public class AddAreaInterface
    {
        public string aName { get; set; }

        public int LocationId { get; set; }
    }

    // Registrar respuesta de tiket
    public class AddAnswerInterface // Modificada
    {
        public int asUserId { get; set; }

        public string asAnswer { get; set; }

        public int RequestId { get; set; }

        public IFormFile asAttachement { get; set; }
    }

    // Datos necesarios para una busqueda (No implementada)
    public class SearchInterfcae
    {
        public int locationId { get; set; }

        public int itemId { get; set; }
    }

    // Busqueda inicial del usuario en la base de datos y obtencion de su informacion
    public class UserInformation
    {
        public int employeeNumber { get; set; }

        public string location { get; set; }

        public string name { get; set; }

        public string email { get; set; }
    }

    // Interfaz de informacion para el envio de Emails
    public class EmailInformation
    {
        public int themeId { get; set; }

        public string themeName { get; set; }

        public int locationId { get; set; }

        public string locationName { get; set; }

        public int ticketId { get; set; }

        public string Issue { get; set; }

        // setters
        public void setThemeId(int thmeId)
        {
            this.themeId = themeId;
        }

        public void setThemeName(string themeName)
        {
            this.themeName = themeName;
        }


    }
}
