import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import {
  searchData,
  User,
  ticketStatus,
} from '../../../../assets/ts/interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class FieldsService {
  private MyApiUrl: string = 'Fields/';
  private headers = new HttpHeaders({
    Authorization: `Bearer ${sessionStorage.getItem('token')}`,
  });

  constructor(private _http: HttpClient) {}

  // Obtencion de localidades
  getLocations(): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'getLocations'
    );
  }

  // Obtencion de los temas asociados a la localidad
  getThme(locationId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'getTheme/' + locationId
    );
  }

  // Obtencion de las preguntas asociadas al tema
  getQuestions(themeId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'getQuestions/' + themeId
    );
  }

  // Obtencion de las areas asociadas a la localidad
  getAreas(locationId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'getAreas/' + locationId
    );
  }

  // Obtencion de los roles
  getRoles(): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'getRoles',
      { headers: this.headers }
    );
  }

  // Obtención de los usuarios asociados al tema del ticket en una localidad
  getAvailableUsers(availableUser: searchData): Observable<User[]> {
    return this._http.get<User[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'userAssignment/' +
        availableUser.locationId +
        '/' +
        availableUser.itemId,
      { headers: this.headers }
    );
  }

  // Obtencion de los estado que puede tener un ticket
  getTicketStatus(): Observable<ticketStatus[]> {
    return this._http.get<ticketStatus[]>(
      `${environment.API_URL}` + this.MyApiUrl + 'getTicketStatus',
      { headers: this.headers }
    );
  }
}
