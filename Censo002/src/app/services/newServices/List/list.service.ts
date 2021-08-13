import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { areaList } from '../../../../assets/ts/interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class ListService {
  private MyApiUrl: string = 'Table/';

  constructor(private _http: HttpClient) {}

  // Obtencion del listado de localidades
  getLocationList(): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'locationList'
    );
  }

  // Obtencion del listado de usuarios en una localidad
  getUserList(locationId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'userList/' + locationId
    );
  }

  // Obtencion del listado de temas en una localidad
  getThemeList(locationId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'themeList/' + locationId
    );
  }

  // Obtencion del listado de preguntas en una localidad
  getQuestionList(locationId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'questionList/' + locationId
    );
  }

  // Obtencion del listado de tickets en una localidad
  getTicketList(locationId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'ticketList/' + locationId
    );
  }

  // Obtencion del listado de areas en una localidad
  getAreaList(locationId: number): Observable<areaList[]> {
    return this._http.get<areaList[]>(
      `${environment.API_URL}` + this.MyApiUrl + 'areaList/' + locationId
    );
  }
}
