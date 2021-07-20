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
