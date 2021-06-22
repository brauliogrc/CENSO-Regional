using CENSO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// TABLE HRU

namespace CensoAPI02.Models
{
    public class HRU
    {
        [Key]
        public int uId { get; set; } // Id del usuario

        [MaxLength(50)]
        [Required]
        public string uName { get; set; } // Nombre del usuario

        [MaxLength(80)]
        [Required]
        public string uEmail { get; set; } // Eamil del usuario

        [Required]
        public long uEmployeeNumber { get; set; }

        [Required]
        public DateTime uCreationDate { get; set; } // Fecha de cracion del usuario

        [Required]
        public int uCreationUser { get; set; } // Usuario creador del usuario

        public DateTime uModificationDate { get; set; } // Fecha de modificacion del usuario

        public int uModificationUser { get; set; } // Usuario modificador del usuario

        [Required]
        public bool uStatus { get; set; } // Estado del usuario -> true o false

        // Relationship M2M with table Theme
        public List<Theme> Themes { get; set; }

        // Relationship one-to-many wuith Location
        [Required]
        public int LocationId { get; set; }
        public Locations locations { get; set; }

        // Relationship one-to-many wuith Roles
        [Required]
        public int RoleId { get; set; }

        public Roles roles { get; set; }


    }
}
