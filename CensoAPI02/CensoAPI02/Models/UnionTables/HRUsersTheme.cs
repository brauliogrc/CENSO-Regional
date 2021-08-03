using CENSO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Models.UnionTables
{
    public class HRUsersTheme
    {
        public long UserId { get; set; }

        public int ThemeId { get; set; }

        public HRU HRU { get; set; } // Propiedades de navegacion

        public Theme Theme { get; set; } // Propiedades de navegacion
    }
}
