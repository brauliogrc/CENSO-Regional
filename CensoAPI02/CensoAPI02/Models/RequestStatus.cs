using CENSO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Models
{
    public class RequestStatus
    {
        [Key]
        public int rsId { get; set; }

        [Required][MaxLength(20)]
        public string rsStatus { get; set; }

        // Relationship one-to-many entities RequestStatus and Request
        public List<Request> requests { get; set; }

        // Relationship one-to-many entities RequestStatus and AnonRequest
        public List<AnonRequest> anonRequests { get; set; }
    }
}
