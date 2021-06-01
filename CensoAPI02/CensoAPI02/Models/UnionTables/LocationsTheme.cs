using CENSO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.UnionTables
{
    public class LocationsTheme
    {
        public int LocationId   { get; set; }

        public int ThemeId      { get; set; }

        public Locations Locations { get; set; } // Propiedades de navegacion

        public Theme     Theme     { get; set; } // Propiedades de navegacion
    }
}
