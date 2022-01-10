using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CENSO.Models;

namespace CensoAPI02.Intserfaces
{
    public class Validations
    {
        private readonly CDBContext _context;

        // Contructor sin argumentos
        public Validations() {}

        // Sobrecarga de contructor con un argumento
        public Validations ( CDBContext context )
        {
            _context = context;
        }
        
        // Obtención del id de la localidad
        public int localityValidation( string locationName )
        {
            var location = from l in _context.Locations
                            where l.lName == locationName
                            select l;

            return location.First().lId;
        }

        // Try parce to Int32
        public int? tryParseToInt32(string value)
        {
            int result;
            if (Int32.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        // Try parce to Int64
        public long? tryParseToInt64(string value)
        {
            long result;
            if (Int64.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        // Try parce to DateTime
        public DateTime? tryParseToDateTime(string value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }
    }
}
