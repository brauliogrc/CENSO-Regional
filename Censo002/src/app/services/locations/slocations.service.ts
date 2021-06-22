import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SlocationsService {
  private MyAppUrl : string = 'https://localhost:44358/';
  private MyApiUrl : string = 'api/Locations';
  
  constructor( private _http:HttpClient) { }

  addNewLocation(dataLocation : any) : Observable<any> {
    console.log('desde el service');
    console.log(dataLocation);
    return this._http.post(this.MyAppUrl + this.MyApiUrl, dataLocation);
  }

  getSpecificLocation(idLocation : any) : Observable<any> {
    return this._http.get( this.MyAppUrl + this.MyApiUrl + '/' + idLocation);
  } 

  deleteLocation(id : number) : Observable<any> {
    console.log('Se elimnara la localidad con el id ' + id);
    return this._http.delete( this.MyAppUrl + this.MyApiUrl + '/' + id);
  }
}
