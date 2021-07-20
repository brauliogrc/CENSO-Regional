/************** Interfaces para las tablas */
export interface dataLocations {
  lId: number;
  lName: string;
  lStatus: boolean;
}

export interface dataUsers {
  uId: number;
  lId: number;
  rolId: number;
  uName: string;
  uStatus: string;
  uEmail: string;
  lName: string;
  rolName: string;
}

export interface dataTheme {
  lId: number;
  tId: number;
  lName: string;
  tName: string;
  tStatus: string;
}

export interface dataQuestion {
  tId: number;
  qId: number;
  tName: string;
  qName: string;
  qStatus: string;
}

export interface dataArea {
  aId: number;
  lId: number;
  aName: string;
  lName: string;
}

export interface dataTickets {
  thId: number;
  tName: string;
  qId: number;
  qName: string;
  rId: number;
  rIssue: string;
  aId: number;
  aName: string;
  uId: number;
  rUserName: string;
  rsId: number;
  rsStatus: string;
}

export interface dataAnonTikets {
  thId: number;
  tName: string;
  qId: number;
  qName: string;
  arId: number;
  arIssue: string;
  aId: number;
  aName: string;
  rsId: number;
  rsStatus: string;
}

aId: 1;
aName: 'Area 002';
arId: 100000;
arIssue: 'Tiket anonimo 001';
qId: 2;
qName: 'Pregunta 003';
rsId: 1;
rsStatus: 'En proceso';
tId: 1;
tName: 'Tema 002';
/************** Interfaes para registro de Request */
export interface availableQues {
  qId: number;
  qName: string;
}

export interface availableLocations {
  lId: number;
  lName: string;
}

export interface availableRoles {
  rolId: number;
  rolName: string;
}

export interface availableTheme {
  tId: number;
  tName: string;
}

export interface availableAreas {
  aId: number;
  aName: string;
}

/************** Interfaes para datos devueltos por el Login */
export interface saveDataLogin {
  uId: number;
  roleId: number;
  uName: string;
  uEmail: string;
  locationId: number;
}

/************** Interfae para datos enviados por el Login */
export interface dataLogin {
  usernumber: number;
  pass: string;
}

export interface resultLogin {}

/************** Interfaes para datos de nuevos registros */
export interface newRequest {
  rUserId: number;
  ThemeId: number;
  LocationId: number;
  AreaId: number;
  rIssue: string;
  QuestionId: number;
  rAttachement: string;
  rEmployeeType: number;
  rUserName: string;
}

export interface newAnonRequest {
  AreaId: number;
  arIssue: string;
  QuestionId: number;
  ThemeId: number;
  LocationId: number;
  arEmployeeType: number;
  arAttachemen: string;
}

export interface dataNewUser {
  uName: string;
  uEmail: string;
  RolId: number;
  uStatus: number;
  LocationId: number;
  EmployeeNumber: number;
}

export interface dataNewLocation {
  lName: string;
  lStatus: boolean;
}

export interface dataNewTheme {
  tName: string;
  tStatus: boolean;
  LocationId: number;
}

export interface dataNewQuestion {
  qName: string;
  qStatus: boolean;
  ThemeId: number;
}
