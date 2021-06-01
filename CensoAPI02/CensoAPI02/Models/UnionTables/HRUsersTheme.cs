using CENSO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Models.UnionTables
{
    public class HRUsersTheme
    {
        public int HRUId { get; set; }

        public int ThemeId { get; set; }

        public HR_User HR_User { get; set; } // Propiedades de navegacion

        public Theme Theme { get; set; } // Propiedades de navegacion
    }
}
