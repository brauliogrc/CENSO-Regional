﻿using System;
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

        public string rIssue { get; set; }

        public string rAttachement { get; set; }
    }

    // Registart una nueva AonRequest
    public class AddAnonRequestInterface
    {
        public int arEmployeeType { get; set; }

        public int QuestionId { get; set; }

        public int AreaId { get; set; }

        public string arIssue { get; set; }

        public string arAttachemen { get; set; }
    }

    public class AddLocations
    {
        public string lName { get; set; }

        public bool lStatus { get; set; }
    }
}