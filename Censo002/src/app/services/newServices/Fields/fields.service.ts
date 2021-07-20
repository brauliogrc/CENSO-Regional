import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class FieldsService {
  private MyApiUrl: string = 'Fields/';

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
}
