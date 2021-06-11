using CensoAPI02.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

//TABLE QUESTIONs

namespace CENSO.Models
{
    public class Question
    {
        [Key]
        public int      qId { get; set; } // Id de la question

        [MaxLength(70)]
        public string   qName { get; set; } // Nombre de la question

        public DateTime qCreationDate { get; set; } // Fecha de cración

        public int     qCreationUser { get; set; } // Usuario creador

        public DateTime qModificationDate { get; set; } // Fecha de modificación

        public int     qModificationUser { get; set; } // Usuario que realizó la modificación

        public bool     qStatus { get; set; }

        //Relationship one-2-one entities Request and Qestion
        //public int RequestOfQuestion { get; set; }
        public List<Request> request { get; set; }

        //Relationship one-2-one entities AnonRequest and Qestion
        public List<AnonRequest> anonRequest { get; set; }

        //Relationship many-2-many entities Question and Theme
        //public int Question_Theme { get; set; } //Relacionada con la entidad Theme
        //public IList<Question_Theme> question_theme { get; set; }
        public List<Theme> Themes { get; set; }
    }
}
