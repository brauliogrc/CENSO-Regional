using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CENSO.Models
{
    public class Question
    {
        public int      QuestionId { get; set; }

        [MaxLength(70)]
        public string   Question_Name { get; set; }

        public DateTime Question_Creation_Date { get; set; }

        public int     Question_Creation_User { get; set; }//Cambiar a int

        public DateTime Question_Modification_Date { get; set; }

        public int     Question_Modification_User { get; set; }//Cambiar a int

        public bool     Question_Status { get; set; }

        //Relationship one-2-one entities Request and Qestion
        //public int RequestOfQuestion { get; set; }
        public Request request { get; set; }

        //Relationship many-2-many entities Question and Theme
        //public int Question_Theme { get; set; } //Relacionada con la entidad Theme
        //public IList<Question_Theme> question_theme { get; set; }
        public List<Theme> Themes { get; set; }
    }
}
