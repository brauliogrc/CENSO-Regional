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
        [Required]
        public string   lName { get; set; }

        [Required]
        public DateTime lCreationDate { get; set; }

        [Required]
        public int      lCreationUser { get; set; }

        public DateTime lModificationDate { get; set; }

        public int      lModificationUser { get; set; }

        [Required]
        public bool     lStatus { get; set; }

        //Relationship many-2-many entities Locations and Theme
        public List<Theme> Themes { get; set; }

        //Relationship many-2-many entities HR_User and Location
        public List<HRU> HRU { get; set; }

        //Relationship one-2-many entities Area and Location
        public List<Area> areas { get; set; }

        // Relationshio one-2-many entities Locations and Request
        public List<Request> Request { get; set; }

        // Relationshio one-2-many entities Locations and AnonRequest
        public List<AnonRequest> anonRequest { get; set; }

    }
}
