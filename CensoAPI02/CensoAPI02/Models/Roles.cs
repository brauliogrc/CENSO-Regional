using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Models
{
    public class Roles
    {
        [Key]
        [Required]
        public int rolId { get; set; }

        [Required][MaxLength(15)]
        public string rolName { get; set; }

        // Relationship one-to-many wuith HRU
        public List<HRU> hru{ get; set; }
    }
}
