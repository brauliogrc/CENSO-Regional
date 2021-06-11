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
        public int asId { get; set; } // Id de la respuesta

        public int asUserId { get; set; } // Id del usurio que responde

        [MaxLength(500)]
        public string asAnswer { get; set; } // Contenido de la repuesta

        public DateTime asCrestionDate { get; set; } // Fecha de creacion de la respuesata -> podria estar ligada la arModificationDate de anonymousRequest

        // Relationship one-2-one entities Request and AnswerStatus
        public int RequestId { get; set; }

        public Request request { get; set; }
    }
}
