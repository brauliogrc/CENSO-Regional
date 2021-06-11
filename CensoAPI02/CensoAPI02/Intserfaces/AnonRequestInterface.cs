using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Models.prueba
{
    public class AnonRequestInterface
    {
        public int arEmployeeType { get; set; }

        public int QuestionId { get; set; }

        public int arShip { get; set; }

        public string arIssue { get; set; }

        public string arAttachemen { get; set; }

    }
}
