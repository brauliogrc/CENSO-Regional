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
        public string anAnswer { get; set; } // Contenido de la resppuesta

        public int anUserId { get; set; } // Id del usuario que respondió la pregunta

        public DateTime anCreationDate { get; set; } // Fecha de creacion de la respuesata -> podria estar ligada la arModificationDate de anonymousRequest

        //public int anArea { get; set; } // Area del usuario que responde la request -> opcional

        // Relationship one-2-one entities anonymousRequest and AnswerAnonStatus
        public int anRequestId { get; set; }

        public AnonRequest anonRequest { get; set; }
    }
}
