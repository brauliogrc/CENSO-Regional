using CENSO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Models.UnionTables
{
    public class AreasLocations
    {
        public int AreaId { get; set; }

        public int LocationId { get; set; }

        public Area Area { get; set; } // Propiedad de navegacion

        public Locations Locations { get; set; } // Propiedad de navegacion
    }
}
