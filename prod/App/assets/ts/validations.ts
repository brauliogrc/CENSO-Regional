export class LocationValidate {
  constructor() {}
  
  // MÉTODO EN DESUSO
  localityValidation(locationName: string | null): number | null {
    if (locationName == 'AIS SLP')                       return 1;
    if (locationName == 'Benecke Kaliko')                return 2;
    if (locationName == 'Cauutla')                       return 3;
    if (locationName == 'Cuautla GDL')                   return 4;
    if (locationName == 'Federal Distr')                 return 5;
    if (locationName == 'Finance Center')                return 6;
    if (locationName == 'HBS Greenfield')                return 7;
    if (locationName == 'Juarez')                        return 8;
    if (locationName == 'Juarez 1')                      return 9;
    if (locationName == 'Juarez 2')                      return 10;
    if (locationName == 'Las Colinas')                   return 11;
    if (locationName == 'Montemorelos')                  return 12;
    if (locationName == 'Monterrey')                     return 13;
    if (locationName == 'Morelia')                       return 14;
    if (locationName == 'Morganton')                     return 15;
    if (locationName == 'MX-AG-Aguascalientes-Auto')     return 16;
    if (locationName == 'MX-GT- Silao-FC Vitesco-')      return 17;
    if (locationName == 'MX-GT-Silao-Las Colinas II')    return 18;
    if (locationName == 'MX-GT-Silao-Las Colinas MX')    return 19;
    if (locationName == 'MX-MO-Cuautla-Vitesco')         return 20;
    if (locationName == 'MX-SL-San Luis Potosi-HBS MX')  return 21;
    if (locationName == 'Nogales')                       return 22;
    if (locationName == 'Old Juarez 1')                  return 23;
    if (locationName == 'Planta Fipasi')                 return 24;
    if (locationName == 'Puebla')                        return 25;
    if (locationName == 'Queretaro')                     return 26;
    if (locationName == 'Rubi')                          return 27;
    if (locationName == 'San Luis Potosi')               return 28;
    if (locationName == 'Santa Anita')                   return 29;
    if (locationName == 'Texcoco')                       return 30;
    if (locationName == 'Tijera')                        return 31;
    if (locationName == 'Tire SLP')                      return 32;
    if (locationName == 'Tlalnepantla')                  return 33;
    if (locationName == 'Tlaquepaque')                   return 34;
    if (locationName == 'Vib. Control')                  return 35;
    if (locationName == 'Zapopan')                       return 36;
    if (locationName == 'CT Fluid Mexicana')             return 37;

    return null;
  }

  // MÉTODO EN DESUSO
  localityNameValidation(): string | null {
    let locationId: number = Number(sessionStorage.getItem('location'));

    if (locationId ==  1)     return 'AIS SLP';
    if (locationId ==  2)     return 'Benecke Kaliko';
    if (locationId ==  3)     return 'Cuautla';
    if (locationId ==  4)     return 'Cuautla GDL';
    if (locationId ==  5)     return 'Federal Distr';
    if (locationId ==  6)     return 'Finance Center';
    if (locationId ==  7)     return 'HBS Greenfield';
    if (locationId ==  8)     return 'Juarez';
    if (locationId ==  9)     return 'Juarez 1';
    if (locationId ==  10)    return 'Juarez 2';
    if (locationId ==  11)    return 'Las Colinas';
    if (locationId ==  12)    return 'Montemorelos';
    if (locationId ==  13)    return 'Monterrey';
    if (locationId ==  14)    return 'Morelia';
    if (locationId ==  15)    return 'Morganton';
    if (locationId ==  16)    return 'MX-AG-Aguascalientes-Auto';
    if (locationId ==  17)    return 'MX-GT- Silao-FC Vitesco-';
    if (locationId ==  18)    return 'MX-GT-Silao-Las Colinas II';
    if (locationId ==  19)    return 'MX-GT-Silao-Las Colinas MX';
    if (locationId ==  20)    return 'MX-MO-Cuautla-Vitesco';
    if (locationId ==  21)    return 'MX-SL-San Luis Potosi-HBS MX';
    if (locationId ==  22)    return 'Nogales';
    if (locationId ==  23)    return 'Old Juarez 1';
    if (locationId ==  24)    return 'Planta Fipasi';
    if (locationId ==  25)    return 'Puebla';
    if (locationId ==  26)    return 'Queretaro';
    if (locationId ==  27)    return 'Rubi';
    if (locationId ==  28)    return 'San Luis Potosi';
    if (locationId ==  29)    return 'Santa Anita';
    if (locationId ==  30)    return 'Texcoco';
    if (locationId ==  31)    return 'Tijera';
    if (locationId ==  32)    return 'Tire SLP';
    if (locationId ==  33)    return 'Tlalnepantla';
    if (locationId ==  34)    return 'Tlaquepaque';
    if (locationId ==  35)    return 'Vib. Control';
    if (locationId ==  36)    return 'Zapopan';
    if (locationId ==  37)    return 'CT Fluid Mexicana';

    return null;
  }

  employeeTypeValidation(employeeType: number): string | null {
    if (employeeType == 1) return 'Administrativo';
    if (employeeType == 2) return 'Sindicalizado';

    return null;
  }
}
