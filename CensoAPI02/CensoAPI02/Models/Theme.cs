using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CENSO.Models
{
    public class Theme
    {
        public int      ThemeId { get; set; }

        [MaxLength(50)]
        public string   Theme_Name { get; set; }

        public DateTime Theme_Creation_Date { get; set; }

        public int      Theme_Creation_User { get; set; }

        public DateTime Theme_Modification_date { get; set; }

        public int      Theme_Modification_User { get; set; }

        public bool     Theme_Status { get; set; }

        //Relationship many-2-many entities Locations and Theme
        //public int Theme_Location { get; set; } //Relacionada con la entidad Location
        public IList<Location_Theme> locations_theme { get; set; }

        //Relationship many-2-many entities HU_User and Theme
        //public int Theme_User { get; set; } //Relacionada con la entidad HR_User
        public IList<HRU_Theme> hru_theme { get; set; }

        //Relationship many-2-many entities Question and Theme
        //public int Theme_Question { get; set; } //Relacionada con la entidad Question
        public IList<Question_Theme> question_theme { get; set; }
    }
}
