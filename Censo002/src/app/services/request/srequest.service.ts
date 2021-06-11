import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataAnonRequest } from 'src/app/interfaces/interfaces';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SrequestService {
  private MyAppUrl : string = 'https://localhost:44358/';
  private MyApiUrl  : string = 'api/';

  constructor( private _http:HttpClient ) { }

  // Llamamos al controlador de la API que se encarga de agregar la nueva anonRequest a la base de datos, los datos a registrar se enceuntran en la variable requestData
  saveAnonRequest( requestData : any ): Observable<any>{ // asignar requestData a una interface
    return this._http.post( this.MyAppUrl + this.MyApiUrl + 'AddAnonReq', requestData);
  }

  // Llamamos al controlador de la API que se encarga de agregar la nueva Request a la base de datos, los datos a registrar se enceuntran en la variable requestData
  saveRequest(requestData : any): Observable<any>{ // asignar requestData a una interface
    return this._http.post(this.MyAppUrl + this.MyApiUrl + 'AddRequest', requestData);
  }
  
}
