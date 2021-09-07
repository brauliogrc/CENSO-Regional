using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Intserfaces
{
    public class Validations
    {
        // Tranformacion de la localidad a int
        public int localityValidation(string locationName) {
            if (locationName == "AIS SLP")                      return 1;
            if (locationName == "Benecke Kaliko")               return 2;
            if (locationName == "Cuautla")                      return 3;
            if (locationName == "Cuautla GDL")                  return 4;
            if (locationName == "Federal Distr")                return 5;
            if (locationName == "Finance Center")               return 6;
            if (locationName == "HBS Greenfield")               return 7;
            if (locationName == "Juarez")                       return 8;
            if (locationName == "Juarez 1")                     return 9;
            if (locationName == "Juarez 2")                     return 10;
            if (locationName == "Las Colinas")                  return 11;
            if (locationName == "Montemorelos")                 return 12;
            if (locationName == "Monterrey")                    return 13;
            if (locationName == "Morelia")                      return 14;
            if (locationName == "Morganton")                    return 15;
            if (locationName == "MX-AG-Aguascalientes-Auto")    return 16;
            if (locationName == "MX-GT- Silao-FC Vitesco")      return 17;
            if (locationName == "MX-GT-Silao-Las Colinas II")   return 18;
            if (locationName == "MX-GT-Silao-Las Colinas MX")   return 19;
            if (locationName == "MX-MO-Cuautla-Vitesco")        return 20;
            if (locationName == "MX-SL-San Luis Potosi-HBS MX") return 21;
            if (locationName == "Nogales")                      return 22;
            if (locationName == "Old Juarez 1")                 return 23;
            if (locationName == "Planta Fipasi")                return 24;
            if (locationName == "Puebla")                       return 25;
            if (locationName == "Queretaro")                    return 26;
            if (locationName == "Rubi")                         return 27;
            if (locationName == "San Luis Potosi")              return 28;
            if (locationName == "Santa Anita")                  return 29;
            if (locationName == "Texcoco")                      return 30;
            if (locationName == "Tijera")                       return 31;
            if (locationName == "Tire SLP")                     return 32;
            if (locationName == "Tlalnepantla")                 return 33;
            if (locationName == "Tlaquepaque")                  return 34;
            if (locationName == "Vib. Control")                 return 35;
            if (locationName == "Zapopan")                      return 36;

            return 0;
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
