
/************** Interfaces para las tablas */
export interface dataLocations{
    lId     :   number;
    lName   :   string;
    lStatus :   boolean;
}

export interface dataUsers{
    uId     :   number;
    lId     :   number;
    uName   :   string;
    uStatus :   string;
    uRol    :   string;
    uEmail   :   string;
    lName   :   string;
}

export interface dataTheme{
   lId  :   number;
   uId  :   number
   tId  :   number;
   lName    :   string;
   tName    :   string;
   tStatus  :   string;
   uName    :   string;
}

export interface dataQuestion{
    tId :   number;
    qId :   number;
    tName   :   string;
    qName   :   string;
    qStatus :   string;
}

export interface dataArea{
    aId :   number;
    lId :   number;
    aName   :   string;
    lName   :   string;
}
/************** Interfaces para las tablas */

/************** Interfaes para registro de Request */
export interface availableQues{
    qId     :   number;
    qName   :   string;
}

/************** Interfaes para datos devueltos por el Login */
export interface saveDataLogin{
    uId     :   number;
    uName   :   string;
    uEmail  :   string;
    locationId  :   number;
}

/************** Interfae para datos enviados por el Login */
export interface dataLogin{
    username    :   string;
    email   :   string;
}

/************** Interfaes para datos de registro de Request */
export interface newRequest{ // Agregar el theme
    AreaId  :   number;
    rIssue  :   string;
    QuestionId  :   number;
    rAttachement    :   string;
    rEmployeeType   :   number;
}

export interface newAnonRequest{ // Agrega el theme
    AreaId  :   number;
    arIssue :   string;
    QuestionId  :   number;
    arEmployeeType  :   number;
    arAttachemen    :   string;
}