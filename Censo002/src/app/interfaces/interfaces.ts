
/************** Interfaces para las tablas */
export interface dataLocations{
    lId     :   number;
    lName   :   string;
    lStatus :   boolean;
}

export interface dataUsers{
    uId     :   number;
    lId     :   number;
    rolId   :   number;
    uName   :   string;
    uStatus :   string;
    uEmail  :   string;
    lName   :   string;
    rolName    :   string;
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

/************** Interfaes para registro de Request */
export interface availableQues{
    qId     :   number;
    qName   :   string;
}

export interface availableLocations{
    lId     :   number;
    lName   :   string;
}

export interface availableRoles{
    rolId   :   number;
    rolName :   string;
}

export interface availableTheme {
    tId     :   number;
    tName   :   string;
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

/************** Interfaes para datos de nuevos registros */
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

export interface dataNewUser{
    uName   :   string;
    uEmail  :   string;
    RolId   :   number;
    uStatus :   number;
    LocationId  :   number;
}

export interface dataNewLocation{
    lName   :   string;
    lStatus :   boolean;
}

export interface dataNewTheme{
    tName   :   string;
    tStatus :   boolean;
    LocationId  :   number;
}

export interface dataNewQuestion {
    qName   :   string;
    qStatus :   boolean;
    ThemeId :   number;
}