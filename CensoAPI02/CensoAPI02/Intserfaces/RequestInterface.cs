using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Intserfaces
{
    public class RequestInterface
    {
        public int rUserId { get; set; }

        public int rEmployeeType { get; set; }

        public int QuestionId { get; set; }

        public int rShip { get; set; }

        public string rIssue { get; set; }

        public string rAttachement { get; set; }
    }
}
