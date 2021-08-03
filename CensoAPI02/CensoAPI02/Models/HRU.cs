using CENSO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// TABLE HRU

namespace CensoAPI02.Models
{
    public class HRU
    {
        //[Key]
        //public int uId { get; set; }

        [Key]
        public long uEmployeeNumber { get; set; }

        [MaxLength(50)]
        [Required]
        public string uName { get; set; }

        [MaxLength(80)]
        [Required]
        public string? uEmail { get; set; } // Nuleable

        [Required]
        public long uSupervisorNumber { get; set; }

        [Required]
        public DateTime uCreationDate { get; set; }

        [Required]
        public int uCreationUser { get; set; }

        public DateTime? uModificationDate { get; set; } // Nullable

        public int? uModificationUser { get; set; } // Nullable

        [Required]
        public bool uStatus { get; set; }


        // Relationship M2M with table Theme
        public List<Theme> Themes { get; set; }


        // Relationship one-to-many wuith Location
        [Required]
        public int LocationId { get; set; }
        public Locations locations { get; set; }


        // Relationship one-to-many wuith Roles
        [Required]
        public int RoleId { get; set; }
        public Roles roles { get; set; }


        // Relationship one-to-many entities HRU and AnswerSatus
        public List<AnswerStatus> answerStatus { get; set; }


        // Relationship one-to-many HRU Request
        public List<Request> requests { get; set; }


        // Relationship one-to-many HRU AnonRequest
        public List<AnonRequest> anonRequests { get; set; }
    }
}
