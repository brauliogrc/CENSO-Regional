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
    hR_UserId   :   number;
    user_Name   :   string;
    theme_Name  :   string;
    locationId  :   number;
    theme_Status    :   string;
    location_Name   :   string;
}

export interface dataQuestion{
    themeId     :   number;
    questionId  :   number;
    theme_Name  :   string;
    question_Name   :   string;
    question_Status :   string;
}

export interface UserDataLog{
    username    :   string;
    password    :   string;
}

export interface UserResponse{
    userId  :   number;
    rol     :   string;
}