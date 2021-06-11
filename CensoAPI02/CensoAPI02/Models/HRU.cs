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
        public string uName { get; set; } // Nombre del usuario

        [MaxLength(80)]
        public string uEmail { get; set; } // Eamil del usuario
        
        [MaxLength(6)]
        public string uRol { get; set; } // Rol -> pendientes a definir

        public DateTime uCreationDate { get; set; } // Fecha de cracion del usuario

        public int uCreationUser { get; set; } // Usuario creador del usuario

        public DateTime uModificationDate { get; set; } // Fecha de modificacion del usuario

        public int lModificationUser { get; set; } // Uusuario modificador del usuario

        public bool uStatus { get; set; } // Estado del usuario -> true o false

        // Relationship M2M with table Theme
        public List<Theme> Themes { get; set; }

        // Relationship one-to-many wuith Location
        public int LocationId { get; set; }
        public Locations locations { get; set; }
    }
}
