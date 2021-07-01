using CENSO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

//TABLE AnonRequest

namespace CensoAPI02.Models
{
    public class AnonRequest
    {
        [Key]
        [Required]
        public int arId { get; set; } // Id y folio de la request

        [Required]
        public int arEmployeeType { get; set; } // Tipo de empleado

        [MaxLength(500)]
        [Required]
        public string arIssue { get; set; } // Contenido

        [MaxLength(200)]
        public string arAttachement { get; set; } // Ruta de archivo adjunto

        [Required]
        public DateTime arCreationDate { get; set; }

        public DateTime arModificationDate { get; set; }

        public int arModificationUser { get; set; } // Podria ser el usuario que realiza la respuesta

        // Relationship one-2-one entities anonymousRequest and Qestion
        [Required]
        public int QuestionId { get; set; }

        public Question question { get; set; }

        // Relationship one-2-one entities anonymousRequest and AnswerAnonStatus
        public AnswerAnonStatus answerAnonStatus { get; set; }

        // Relationship one-2-may entities Area and AnonRequest
        [Required]
        public int AreaId { get; set; }

        public Area area { get; set; }

        //Relationship one-2-many entities AnonRequest and Theme
        [Required]
        public int ThemeId { get; set; }

        public Theme theme { get; set; }

        // Relationshio one-2-many entities Locations and AnonRequest
        [Required]
        public int LocationId { get; set; }

        public Locations locations { get; set; }
    }
}
