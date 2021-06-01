using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CENSO.Models
{
    public class Locations
    {
        public int      LocationsId { get; set; }

        [MaxLength(50)]
        public string   Location_Name { get; set; }

        public DateTime Location_Creation_Date { get; set; }

        public int      Location_Creation_User { get; set; }//Cambiar a int

        public DateTime Location_Modification_Date { get; set; }

        public int      Location_Modification_User { get; set; }//Cambiar a int

        public bool     Location_Status { get; set; }

        //Relationship many-2-many entities Locations and Theme
        //public IList<Location_Theme> locations_theme { get; set; }
        public List<Theme> Themes { get; set; }

        //Relationship many-2-many entities HR_User and location
        public List<HR_User> HR_Users { get; set; }
    }
}
