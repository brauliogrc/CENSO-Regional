import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { addUser } from 'src/app/interfaces/newInterfaces';
import { environment } from 'src/environments/environment';

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
}
