using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CENSO.Models
{
    public class HR_User
    {
        public int      HR_UserId { get; set; }

        [MaxLength(50)]
        public string   User_Name { get; set; }

        [MaxLength(50)]
        public string   User_Email { get; set; }

        [MaxLength(15)]
        public string   User_Rol { get; set; }
        
        public DateTime User_Creeation_Date { get; set; }

        [MaxLength(50)]
        public string   User_Creation_User { get; set; } //Cambiar a int

        public DateTime User_Modification_Date{ get; set; }

        [MaxLength(50)]
        public string   User_Modification_User { get; set; }//cambiar a int

        public bool     User_Status { get; set; }

        //Relationship many-2-many entities HR_User and Theme
        //public string User_Theme { get; set; } //Relacionada con la entidad Theme
        public IList<HRU_Theme> hru_theme { get; set; }
    }
}
