import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  addUser,
  userInformation,
  userTheme,
} from 'src/assets/ts/interfaces/newInterfaces';
import { environment } from 'src/environments/environment';
import { LocationValidate } from '../../../../assets/ts/validations';
import { userChanges } from '../../../../assets/ts/interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private MyApiUrl: string = 'HRU/';

  constructor(private _http: HttpClient) {}

  // Registro de un nuevo usuario en la table HRU
  addNewUser(newUser: addUser): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newUser',
      newUser
    );
  }

  // Borrado logico de un usuario
  deleteUser(userId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteUser/' + userId
    );
  }

  // Busqueda de la informacion del usuario
  getUserInformation(employeeNumber: number): Observable<userInformation> {
    let locationValidate = new LocationValidate();
    return this._http.get<userInformation>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'userInformation/' +
        locationValidate.localityNameValidation() +
        '/' +
        employeeNumber
    );
  }

  // Actualizacion de un usuario
  userUpdate(newUserData: userChanges): Observable<any> {
    return this._http.patch(
      `${environment.API_URL}` + this.MyApiUrl + 'userUpdate',
      newUserData
    );
  }

  // Eliminación de relación de un usuario con un tema
  deleteRelatedTopic(employeenUmber: number, themeId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'deleteRelatedTopic/' +
        employeenUmber +
        '/' +
        themeId
    );
  }

  addRelatedTopic(relationship: userTheme): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'addRelatedTopic',
      relationship
    );
  }
}
