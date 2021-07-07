import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { dataNewUser } from '../../interfaces/interfaces';

@Injectable({
  providedIn: 'root',
})
export class ShrUsersService {
  private MyAppUrl: string = 'https://localhost:44358/';
  private MyApiUrl: string = 'api/RH_Users';

  constructor(private _http: HttpClient) {}

  getRoles(): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(this.MyAppUrl + this.MyApiUrl + '/GetRoles', {
      headers: headers,
    });
  }

  addNewUser(dataUser: any): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.post(this.MyAppUrl + this.MyApiUrl, dataUser, {
      headers: headers,
    });
  }

  deleteUser(id: number): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.delete(this.MyAppUrl + this.MyApiUrl + '/' + id, {
      headers: headers,
    });
  }
}
