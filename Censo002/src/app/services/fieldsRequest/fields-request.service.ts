import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FieldsRequestService {

  private MyAppUrl : string = 'https://localhost:44358/';
  private MyApiUrl : string = 'api/EmpyCenso/';

  constructor(
    private _http:HttpClient
  ) { }

  // Llamada a la api para consultar las localidades disponibles
  getLocations() : Observable<any> {
    return this._http.get( this.MyAppUrl + this.MyApiUrl + 'FieldLocations' );
    //https://localhost:44358/api/EmpyCenso/FieldLocations
  }

  // Llamada a la api para consultar los temas de una localidad
  getTheme(locationId : any) : Observable<any> {
    return this._http.get( this.MyAppUrl + this.MyApiUrl + 'FieldTheme/' + locationId );
  }

  // Llamada a la api para consultar las preguntas disponibles
  getQuestions(themeid : any) : Observable<any> {
    return this._http.get( this.MyAppUrl + this.MyApiUrl + 'FieldQuestions/' + themeid);
  }

  // Llamada a la api para consultar las areas disoponibles
  getAreas(locationId : any) : Observable<any> {
    return this._http.get( this.MyAppUrl + this.MyApiUrl + 'FieldAreas/' + locationId );
  }
}
