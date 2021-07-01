import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { saveDataLogin } from '../../interfaces/interfaces';

@Injectable({
  providedIn: 'root'
})
export class SloginService {
  private MyAppUrl  : string = 'https://localhost:44358/';
  private MyApiUrl  : string = 'api/Login';

  // Guardado temporal de datos de los datos de usuario
  private user : saveDataLogin = {
    uId: 0,
    uEmail: "",
    uName: "",
    locationId: 0
  }


  constructor( private _http:HttpClient ) { }

  // Llamamos al controlador del login de la API, le enviamos los datos obtenidos en el formulario de login.component
  Login(ahutData : any): Observable<any>{ // asignar ahutData a una interface
    console.log('desde service');
    
    console.log(ahutData);
    
    return this._http.post( this.MyAppUrl + this.MyApiUrl, ahutData );
  }

  // Almacenamos los datos recuperados del usuario en una varibale para accederla más tarde
  setData(locationId : any, userName : any, userId : any, userEmail : any){
    this.user.locationId = locationId;
    this.user.uName      = userName;
    this.user.uId        = userId;
    this.user.uEmail     = userEmail;
    // console.log(this.user);
  }

  getUser(){
    return this.user;
  }
}
