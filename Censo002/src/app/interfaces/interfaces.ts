export interface dataLocations{
    locationsId     :   number;
    location_Name   :   string;
    location_Status :   boolean;
}

export interface dataUsers{
    hR_UserId   :   number;
    user_Name   :   string;
    user_Status :   string;
    user_Rol    :   string;
    user_Email  :   string;
    location_Name   :   string;
}

export interface dataTheme{
    themeId : number;
    theme_Name : string;
    theme_Status : string;
    hR_UserId : number;
    user_Name : string;
    location_Name : string;
    locationId : number;
}

export interface dataQuestion{
    questionId : number;
    question_Name : string;
    question_Status : string;
    themeId : number;
    theme_Name : string;
}