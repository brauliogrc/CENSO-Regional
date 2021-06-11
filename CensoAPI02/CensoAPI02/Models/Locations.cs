using CensoAPI02.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// TABLE LOCATION

namespace CENSO.Models
{
    public class Locations
    {
        [Key]
        public int      lId { get; set; }

        [MaxLength(50)]
        public string   lName { get; set; }

        public DateTime lCreationDate { get; set; }

        public int      lCreationuser { get; set; }//Cambiar a int

        public DateTime lModificationDate { get; set; }

        public int      lModificationUser { get; set; }//Cambiar a int

        public bool     lStatus { get; set; }

        //Relationship many-2-many entities Locations and Theme
        //public IList<Location_Theme> locations_theme { get; set; }
        public List<Theme> Themes { get; set; }

        //Relationship many-2-many entities HR_User and location
        public List<HRU> HRU { get; set; }
    }
}
