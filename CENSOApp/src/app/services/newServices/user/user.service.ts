import { HttpClient, HttpHeaders } from '@angular/common/http';
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
  private headers = new HttpHeaders({
    'Authorization': `Bearer ${sessionStorage.getItem('token')}`,
  });

  constructor(private _http: HttpClient) {}

  // Registro de un nuevo usuario en la table HRU
  addNewUser(newUser: addUser): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newUser',
      newUser,
      { headers: this.headers }
    );
  }

  // Borrado logico de un usuario
  deleteUser(userId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteUser/' + userId,
      { headers: this.headers }
    );
  }

  // Busqueda de la informacion del usuario
  getUserInformation(employeeNumber: number): Observable<userInformation> {
    // let locationValidate = new LocationValidate();
    return this._http.get<userInformation>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'userInformation/' +
        // locationValidate.localityNameValidation() +
        Number( sessionStorage.getItem('location') ) +
        '/' +
        employeeNumber,
      { headers: this.headers }
    );
  }

  // Actualizacion de un usuario
  userUpdate(newUserData: userChanges): Observable<any> {
    return this._http.patch(
      `${environment.API_URL}` + this.MyApiUrl + 'userUpdate',
      newUserData,
      { headers: this.headers }
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
        themeId,
      { headers: this.headers }
    );
  }

  // Añadir una relacion entre un usuario y una localidad
  addRelatedTopic(relationship: userTheme): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'addRelatedTopic',
      relationship,
      { headers: this.headers }
    );
  }
}
