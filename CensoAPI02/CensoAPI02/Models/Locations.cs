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

        public string   Location_Creation_User { get; set; }

        public DateTime Location_Modification_Date { get; set; }

        public string   Location_Modification_User { get; set; }

        public bool     Location_Status { get; set; }

        //Relationship many-2-many entities Locations and Theme
        public IList<Location_Theme> locations_theme { get; set; }
    }
}
