using CENSO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Models
{
    public class Area
    {
        [Key]
        public int aId { get; set; }

        [MaxLength(50)]
        [Required]
        public string aName { get; set; }

        [Required]
        public bool aStatus { get; set; }

        // Relacion many-2-many con Locations
        //public int locationId { get; set; }

        public List<Locations> locations { get; set; }


        //Relationship one-2-many entities Area and Request
        public List<Request> request { get; set; }


        //Relationship one-2-many entities Area and AnonRequest
        public List<AnonRequest> anonRequest { get; set; }
    }
}
