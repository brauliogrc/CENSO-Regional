using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CENSO.Models
{
    public class Request
    {
        public int      RequestId { get; set; }

        // Sujeto a posible eliminación
        public int      Request_Theme { get; set; } //Relacionada con la entidad Theme ++Sujeto a posible eleiminacion++

        [MaxLength(50)]
        public string   Request_User_Name { get; set; }

        public int      Request_Employee_Type { get; set; } // Corregir tipo?? 

        [MaxLength(500)]
        public string   Request_Issue { get; set; } //Varchar de 500 (es donde la gente escribe su necesidad)

        [MaxLength(50)]
        public int      Request_Area { get; set; } //Varchar o int???

        public int      Request_Employee_Leader { get; set; } //Numerico (es el numero del empleado)

        [MaxLength(50)]
        public string   Request_Answer_Status { get; set; }

        [MaxLength(200)]
        public string   Request_Attachement { get; set; } //Ruta al elemento añadido (imagen)

        public DateTime Request_Creation_Date { get; set; }

        public int       Request_Creation_User { get; set; }//Cambiar a int

        public DateTime Request_Modification_Date { get; set; }

        public int      Request_Modification_User { get; set; }//Cambiar a int

        //Relationship one-2-one entities Request and Qestion
        public int QuestionId { get; set; }

        public Question question { get; set; } //Relacionada con la entidad Question
    }
}
