using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CensoAPI02.Models;

// TABLE THEME

namespace CENSO.Models
{
    public class Theme
    {
        [Key]
        public int tId { get; set; }

        [MaxLength(50)]
        [Required]
        public string tName { get; set; }

        [Required]
        public DateTime tCreationDate { get; set; }

        [MaxLength(50)]
        [Required]
        public int tCreationUser { get; set; }

        public DateTime? tModificationDate { get; set; }

        [MaxLength(50)]
        public int? tModificationUser { get; set; }

        [Required]
        public bool tStatus { get; set; }

        //Relationship many-2-many entities Locations and Theme
        //public int Theme_Location { get; set; } //Relacionada con la entidad Location
        //public IList<Location_Theme> locations_theme { get; set; }
        public List<Locations> Locations { get; set; }

        //Relationship many-2-many entities HU_User and Theme
        //public int Theme_User { get; set; } //Relacionada con la entidad HR_User
        //public IList<HRU_Theme> hru_theme { get; set; }
        public List<HRU> HRU { get; set; }

        //Relationship many-2-many entities Question and Theme
        //public int Theme_Question { get; set; } //Relacionada con la entidad Question
        //public IList<Question_Theme> question_theme { get; set; }
        public List<Question> Questions { get; set; }

        //Relationship one-2-many entities Request and Theme
        public List<Request> Requests{ get; set; }

        //Relationship one-2-many entities AnonRequest and Theme
        public List<AnonRequest> AnonRequests { get; set; }
    }
}
