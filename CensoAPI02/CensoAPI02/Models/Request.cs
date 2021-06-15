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
        public int rId { get; set; } // Id y folio de la request

        [Required]
        public int rUserId { get; set; } // Igual a creation User (Se obtendrá desde el login)

        [MaxLength(500)]
        [Required]
        public string rIssue { get; set; } // Contenido

        [MaxLength(200)]
        public string rAttachement { get; set; } // Ruta de archivo adjunto

        [Required]
        public int rEmployeeType { get; set; } // Tipo de empleado -> implementar enumercion para este camo

        //Posible eliminacion
        public int rEmployeeLeader { get; set; } // Lider del empleado

        [Required]
        public DateTime rCreationDate { get; set; } // Fecha de creación de la petición (Ingresada automáticamente)

        public DateTime rModificationDate { get; set; } // Es el Id del usurio que realizó la Request

        public int rModificationUser { get; set; } // Podria ser el usuario que realiza la respuesta

        // Relationship one-2-one entities Request and Qestion
        [Required]
        public int QuestionId { get; set; }

        public Question question { get; set; } // Relacionada con la entidad Question (propiedad de navegación)

        // Relationship one-2-one entities Request and AnswerStatus
        public AnswerStatus answerStatus { get; set; }

        // Relationship one-2-may entities Area and Request
        [Required]
        public int AreaId { get; set; }

        public Area area { get; set; }
    }
}
