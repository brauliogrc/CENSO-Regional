using CensoAPI02.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CENSO.Models
{
    public class Request
    {
        [Key]
        public int rId { get; set; }

        [Required]
        public int rUserId { get; set; }

        [Required][MaxLength(120)]
        public string rUserName { get; set; }

        [MaxLength(500)]
        [Required]
        public string rIssue { get; set; }

        [MaxLength(200)]
        public string rAttachement { get; set; }

        [Required]
        public int rEmployeeType { get; set; }
 
        public int rEmployeeLeader { get; set; }

        [Required]
        public DateTime rCreationDate { get; set; }

        public DateTime rModificationDate { get; set; }


        // Relationship one-to-many entities HRU Request
        public int rModificationUser { get; set; }
        public HRU hru { get; set; }


        // Relationship one-2-one entities Request and Qestion
        [Required]
        public int QuestionId { get; set; }
        public Question question { get; set; }


        // Relationship one-2-one entities Request and AnswerStatus
        public AnswerStatus answerStatus { get; set; }


        // Relationship one-2-may entities Area and Request
        [Required]
        public int AreaId { get; set; }
        public Area area { get; set; }


        //Relationship one-2-many entities Request and Theme
        [Required]
        public int ThemeId { get; set; }
        public Theme theme { get; set; }


        // Relationshio one-2-many entities Locations and Request
        [Required ]
        public int LocationId { get; set; }
        public Locations locations { get; set; }


        // Relationship one-to-many entities RequestStatus and Request
        [Required]
        public int StatusId { get; set; }
        public RequestStatus requestStatus { get; set; }
    }
}
