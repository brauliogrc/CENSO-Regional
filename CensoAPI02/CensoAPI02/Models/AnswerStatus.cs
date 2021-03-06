using CENSO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Models
{
    public class AnswerStatus
    {
        [Key]
        public int asId { get; set; }

        [MaxLength(500)]
        [Required]
        public string asAnswer { get; set; }

        [MaxLength(200)]
        public string? asAttachement { get; set; }

        [Required]
        public DateTime asCreationDate { get; set; }

        // Relationship one-2-one entities Request and AnswerStatus
        public int? RequestId { get; set; }
        public Request request { get; set; }


        // Relationship one-2-one entities AnonRequest and AnswerStatus
        public int? AnonRequestId { get; set; }
        public AnonRequest anonRequest { get; set; }


        // Relationship one-to-many entities HRU and AnswerSatus
        [Required]
        public long UserId { get; set; }
        public HRU hru { get; set; }
    }
}
