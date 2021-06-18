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
}
