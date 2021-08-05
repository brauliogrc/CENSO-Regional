// interface de logueo
export interface dataLogin {
  usernumber: number;
  pass: string;
}

export interface Token {
  token: string;
}

export interface Location {
  lId: number;
  lName: string;
}

export interface Theme {
  tId: number;
  tName: string;
}

export interface Question {
  qId: number;
  qName: string;
}

export interface Area {
  aId: number;
  aName: string;
}

export interface Rol {
  rolId: number;
  rolName: string;
}

// Interfaces de nuevas peticiones
export interface addRequest {
  rUserId: number;
  rUserName: string | null;
  rEmployeeType: number;
  rEmployeeLeader: number;
  QuestionId: number;
  AreaId: number;
  ThemeId: number;
  LocationId: number;
  rIssue: string;
  rAttachement: string;
}

export interface addAnonRequest {
  arEmployeeType: number;
  QuestionId: number;
  AreaId: number;
  ThemeId: number;
  LocationId: number;
  arIssue: string;
  arAttachemen: string;
}

// Interfaces de listado
export interface locationList {
  // Datos de la localidad
  lId: number;
  lName: string;
  lStatus: boolean;
}

export interface userList {
  // Datos del usuario
  uId: number;
  uEmployeeNumber: number;
  uName: string;
  uStatus: boolean;
  uEmail: string;
  // Datos del rol
  rolId: number;
  rolName: string;
  // Datos de la localidad
  lId: number;
  lName: string;
}

export interface themeList {
  // Datos del tema
  tId: number;
  tName: string;
  tStatus: boolean;
  // Datos de la localidad
  lId: number;
  lName: string;
}

export interface questionList {
  // Datos de la pregunta
  qId: number;
  qName: string;
  qStatus: boolean;
  // Datos del tema
  tId: number;
  tName: string;
}

export interface areaList {
  // Datos del area
  aId: number;
  aName: string;
  aStatus: boolean;
  // Datos de la localiad
  lId: number;
  lName: string;
}

export interface ticketList {
  // Datos del ticket
  rId: number;
  rUserName: string;
  rIssue: string;
  // Datos del tema
  tId: number;
  tName: string;
  // Datos de la pregunta
  qId: number;
  qName: string;
  // Datos del area
  aId: number;
  aName: string;
  // Datos del status
  rsId: number;
  rsStatus: string;
}

export interface anonTicketList {
  // Datos del ticket
  arId: number;
  arIssue: string;
  // Datos del tema
  tId: number;
  tName: string;
  // Datos de la pregunta
  qId: number;
  qName: string;
  // Datos del area
  aId: number;
  aName: string;
  // Datos del status
  rsId: number;
  rsStatus: string;
}

// Interface de busquda
export interface searchData {
  locationId: number;
  itemId: number;
}

// Interface nueva localidad
export interface addLocation {
  lName: string;
  lCreationUser: number;
  lStatus: boolean;
}

export interface addUser {
  uName: string;
  uEmail: string | null;
  RolId: number;
  EmployeeNumber: number;
  uStatus: boolean;
  uCreationUser: number;
}

export interface addTheme {
  tName: string;
  tStatus: boolean;
  tCreationUser: number;
  LocationId: number;
}

export interface addQuestion {
  qName: string;
  qCreationUser: number;
  qStatus: boolean;
  ThemeId: number;
}

export interface addArea {
  aName: number;
  aStatus: boolean;
  LocationId: number;
}

export interface addAnswer {
  asUserId: number;
  asAnswer: string;
  RequestId: number;
}

// Interface de la informacion del usuario
export interface userInformation {
  employeeNumber: number;
  location: string;
  name: string;
  email: string | null;
}
