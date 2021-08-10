using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Models
{
    public class AnswerAnonStatus
    {
        [Key]
        public int anId { get; set; } // Id de la respuesta

        [MaxLength(500)]
        [Required]
        public string anAnswer { get; set; } // Contenido de la resppuesta

        [Required]
        public long anUserId { get; set; } // Id del usuario que respondió la pregunta

        [Required]
        public DateTime anCreationDate { get; set; } // Fecha de creacion de la respuesata -> podria estar ligada la arModificationDate de anonymousRequest

        // Relationship one-2-one entities anonymousRequest and AnswerAnonStatus
        //[Required]
        //public int anRequestId { get; set; }

        //public AnonRequest anonRequest { get; set; }
    }
}
